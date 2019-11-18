using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stickyBall : MonoBehaviour {

    public float facingAngle = 0;
    public Vector3 miVector;
    float x = 0;
    float z = 0;
    Vector2 unitV2;

    public GameObject cameraReference;
    float distanceToCamera = 5;

    float size = 1;

    public GameObject category1;
    bool category1Unlocked = false;
    public GameObject category2;
    bool category2Unlocked = false;
    public GameObject category3;
    bool category3Unlocked = false;

    bool ballActive;

    public Camera ballCamera;

    //body 
    public GameObject realBody;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(ballActive)
        {
            ballCamera.enabled = true;
            realBody.SetActive(false);

            //BALL CODE WHEN ACTIVE
            //User controls
            x = Input.GetAxis("Horizontal") * Time.deltaTime;
            z = Input.GetAxis("Vertical") * Time.deltaTime;

            //Facing Angle
            facingAngle += x;
            unitV2 = new Vector2(Mathf.Cos(facingAngle * Mathf.Deg2Rad), Mathf.Sin(facingAngle * Mathf.Deg2Rad));
        }
        else if (!ballActive)
        {
            realBody.SetActive(true);
            ballCamera.enabled = false;

        }

        if (Input.GetMouseButtonDown(0))
        {
            ballActive = false;
        }

    }

    private void FixedUpdate()
    {
        if(ballActive)
        {
            //Apply force behind the ball
            transform.Translate(new Vector3(0, 0, z*3));
            transform.Rotate(0, x*10, 0);

            //Set Camera position behind the ball based on rotation
            //cameraReference.transform.position = new Vector3(-unitV2.x * distanceToCamera, distanceToCamera, -unitV2.y * distanceToCamera) + this.transform.position;

            unlockPickupCateories();
        }
        
    }

    private void LateUpdate()
    {
        if(ballActive)
        {

            //Set Camera position behind the ball based on rotation
            
            //cameraReference.transform.position = transform.position + miVector;
                //new Vector3(-unitV2.x * distanceToCamera, distanceToCamera, -unitV2.y * distanceToCamera) + this.transform.position;
        }
    }

    void unlockPickupCateories()
    {
        if(category1Unlocked == false)
        {
            if(size >=1)
            {
                category1Unlocked = true;
                for(int i = 0; i<category1.transform.childCount; i++)
                {
                    category1.transform.GetChild(i).GetComponent<Collider>().isTrigger = true;
                }
            }
        }
        else if (category2Unlocked == false)
        {
            if (size >= 1.5f)
            {
                category2Unlocked = true;
                for (int i = 0; i < category2.transform.childCount; i++)
                {
                    category2.transform.GetChild(i).GetComponent<Collider>().isTrigger = true;
                }
            }
        }
        else if (category3Unlocked == false)
        {
            if (size >= 2)
            {
                category3Unlocked = true;
                for (int i = 0; i < category3.transform.childCount; i++)
                {
                    category3.transform.GetChild(i).GetComponent<Collider>().isTrigger = true;
                }
            }
        }
    }
    //pick up sticky objects
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Sticky"))
        {
            //grow the sticky ball
            transform.localScale += new Vector3(0.01f,0.01f,0.01f);
            size += 0.1f;
            distanceToCamera += 0.08f;
            other.enabled = false;

            //becomes child so it stays with the sticky ball
            other.transform.SetParent(this.transform);

            SoundManager.instance.pickUp(GetComponent<AudioSource>());
        }

        if (other.gameObject.layer == 16)
        {
            ballActive = true;
            Destroy(other.gameObject);
        }
    }
}
