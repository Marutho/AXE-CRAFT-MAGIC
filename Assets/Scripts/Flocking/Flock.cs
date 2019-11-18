using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {

	public FlockManager polloManager;
	public float speed;
	bool turning = false;

	// Use this for initialization
	void Start () {
		speed = Random.Range(polloManager.minSpeed, polloManager.maxSpeed);
	}
	
	// Update is called once per frame
	void Update () {

		//multiplicamos por dos porque los limites que damos son a modo de radio
		Bounds b = new Bounds(polloManager.transform.position, polloManager.farmLimits * 2);
		
		//si el pollo va a chocar contra un limite o un objeto gira
		RaycastHit hit = new RaycastHit();
		Vector3 direction = Vector3.zero;

		if(!b.Contains(transform.position) )
		{
			turning = true;
			direction = polloManager.transform.position - transform.position;
		} 
		else if (Physics.Raycast(transform.position, this.transform.forward * 50, out hit))
		{
			turning = true;
			direction = Vector3.Reflect(this.transform.forward,hit.normal);
		}
		else
			turning = false;

		if(turning)
		{
			//vuelve al centro de los limites			
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), polloManager.rotationSpeed * Time.deltaTime);
		}
		else
		{  
			if(Random.Range(0,100) < 10)
				speed = Random.Range(polloManager.minSpeed, polloManager.maxSpeed);
			if(Random.Range(0,100) < 15)
				ApplyRules();
		}
		
		transform.Translate(0, 0, Time.deltaTime * speed);
	}

	void ApplyRules()
	{
		GameObject[] gos;
		gos = polloManager.allChicken;
		
		Vector3 vcentre = Vector3.zero;
		Vector3 vavoid = Vector3.zero;
		float gSpeed = 0.01f;
		float nDistance;
		int groupSize = 0;

		foreach (GameObject go in gos) 
		{
			if(go != this.gameObject)
			{
				nDistance = Vector3.Distance(go.transform.position,this.transform.position);
				if(nDistance <= polloManager.neighbourDistance)
				{
					vcentre += go.transform.position;	
					groupSize++;	
					
					if(nDistance < 1.0f)		
					{
						vavoid = vavoid + (this.transform.position - go.transform.position);
					}
					
					Flock anotherFlock = go.GetComponent<Flock>();
					gSpeed = gSpeed + anotherFlock.speed;
				}
			}
		} 
		
		if(groupSize > 0)
		{
			vcentre = vcentre/groupSize + (polloManager.wishedPos - this.transform.position);
			speed = gSpeed/groupSize;
			
			Vector3 direction = (vcentre + vavoid) - transform.position;
			if(direction != Vector3.zero)
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), polloManager.rotationSpeed * Time.deltaTime);
		
		}
	}
}
