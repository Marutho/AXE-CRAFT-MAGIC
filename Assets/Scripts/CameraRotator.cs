using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public float speed;
    public Camera cameraObject;
    private bool objectActive;

    //body 
    public GameObject realBody;
    public GameObject staff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(objectActive)
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
            cameraObject.enabled = true;
            realBody.SetActive(false);                
            staff.SetActive(false);
            
        }
        else if (!objectActive)
        {
            realBody.SetActive(true);
            cameraObject.enabled = false;
         
        }

        if (Input.GetMouseButtonDown(0))
        {
            objectActive = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 16)
        {
            objectActive = true;
            Destroy(other.gameObject);
        }
    }
}
