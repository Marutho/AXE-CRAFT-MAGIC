using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBoxScript : MonoBehaviour
{

    public GameObject breakedBox;
    public GameObject[] items;


    public void Break()
    {
        GameObject breaked = Instantiate(breakedBox, transform.position, transform.rotation);
        GameObject[] itemsDrop = new GameObject[items.Length];
        Rigidbody[] rbs = breaked.GetComponentsInChildren<Rigidbody>();
        
        foreach (Rigidbody rb in rbs)
        {
            rb.AddExplosionForce(150, transform.position, 30);
        }
        for (int i = 0; i < items.Length; i++)
        {
            itemsDrop[i]= Instantiate(items[i], transform.position, Quaternion.identity);
            float xForce = Random.Range(-3.0f, 10.0f);
            float yForce = Random.Range(3.0f, 10.0f);
            float zForce = Random.Range(-3.0f, 10.0f);

            Vector3 force = new Vector3(xForce, yForce, zForce);
            itemsDrop[i].GetComponent<Rigidbody>().velocity=force;
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        
    }
}

