using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour {

	public GameObject chickenPrefab;
	public int numChicken = 8;
	public GameObject[] allChicken;
	public Vector3 farmLimits = new Vector3(7,2,7);
	public Vector3 wishedPos;

	public float minSpeed = 0.3f;
	public float maxSpeed = 0.4f;
	public float neighbourDistance = 5.0f;
	public float rotationSpeed = 2.7f;

	// Use this for initialization
	void Start () {
		allChicken = new GameObject[numChicken];
		for(int i = 0; i < numChicken; i++)
		{
			Vector3 pos = this.transform.position + new Vector3(Random.Range(-farmLimits.x, farmLimits.x), Random.Range(-farmLimits.y, farmLimits.y), Random.Range(-farmLimits.z, farmLimits.z));
            allChicken[i] = (GameObject) Instantiate(chickenPrefab, pos, Quaternion.identity);
            allChicken[i].GetComponent<Flock>().polloManager = this;
		}
		wishedPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(Random.Range(0,100)<10)
			wishedPos = this.transform.position + new Vector3(Random.Range(-farmLimits.x, farmLimits.x), Random.Range(-farmLimits.y, farmLimits.y), Random.Range(-farmLimits.z, farmLimits.z));
	}
}
