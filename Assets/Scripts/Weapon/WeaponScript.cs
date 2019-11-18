using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public bool activated;
    public float rotationSpeed;

    //Sounds
    public AudioClip collisionVSwall;
    public AudioClip normalCollision;
    public AudioClip collisionVSGround;
    
    private Rigidbody rb;
    private float timer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        timer = 0.1f;
    }

    void Update()
    {
        timer -= Time.deltaTime;

       if (activated)
        {
            transform.Rotate(Vector3.left * rotationSpeed * Time.deltaTime);
        } 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            //print(collision.gameObject.name);
            rb.Sleep();
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            rb.isKinematic = true;
            activated = false;

            if (timer <= 0)
            {
                timer = 0.1f;
                SoundManager.instance.collisionVSwallSound(GetComponent<AudioSource>());
            }
        }
        else if(collision.gameObject.layer == 11)
        {
            //print(collision.gameObject.name);
            rb.Sleep();
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            rb.isKinematic = true;
            activated = false;

            if (timer <= 0)
            {
                timer = 0.1f;
                SoundManager.instance.collisionVSGroundSound(GetComponent<AudioSource>());
            }
        }
        else if(collision.gameObject.layer==13)
        {
            if (timer <= 0)
            {
                timer = 0.1f;
                SoundManager.instance.collisionVSChickenSound(GetComponent<AudioSource>());
            }
        }
        else if (collision.gameObject.layer == 15)
        {
            activated = false;
            rb.AddForce(Vector3.down * 20, ForceMode.VelocityChange);
            collision.gameObject.SetActive(false);
        }
        else if(collision.gameObject.layer == 19)
        {
            SoundManager.instance.collisionVSwoodBridge(GetComponent<AudioSource>());
        }
        else
        {
            activated = false;
            rb.AddForce(Vector3.down * 20, ForceMode.VelocityChange);

            if (timer <= 0)
            {
                timer = 0.1f;
                SoundManager.instance.normalCollisionSound(GetComponent<AudioSource>());
            }
        }
        
        

    }

   private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Breakable"))
        {
            if (timer <= 0)
            {
                timer = 0.1f;
                SoundManager.instance.collisionVSBoxSound(GetComponent<AudioSource>());
            }
            if (other.GetComponent<BreakBoxScript>() != null)
            {               
                other.GetComponent<BreakBoxScript>().Break();
            }
        }
    }
}
