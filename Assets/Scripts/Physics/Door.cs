using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int minMAss;
    public GameObject door;

    private Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Ball")
        {
            rb = collision.gameObject.GetComponent<Rigidbody>();

            if(rb.mass>=minMAss)
            {
                door.tag="Activated";
            }
        }
    }
}
