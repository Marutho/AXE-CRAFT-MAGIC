using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public float force,speed;
    public GameObject particle;
    public int tam;
    
    private float timer,range;
    private List<GameObject> free;

    private void Start()
    {
        free = new List<GameObject>();

        for(int i = 0;i<tam;i++)
        {
            GameObject p = Instantiate(particle);
            p.SetActive(false);
            free.Add(p);
        }

        timer = 0.001f;
        range = 0.5f;
    }

    void FixedUpdate()
    {
        RaycastHit[] hits = Physics.BoxCastAll(transform.position, Vector3.right * transform.localScale.x + Vector3.up * transform.localScale.y + Vector3.forward * transform.localScale.z,
                            transform.forward, new Quaternion(0, 0, 0, 0),0);

        foreach (RaycastHit objectHit in hits)
        {
            
            if (objectHit.rigidbody != null)
            {
                objectHit.rigidbody.AddForce(transform.forward * force, ForceMode.Force);
            }
        }

        timer -= Time.deltaTime;
     
        if (timer <= 0)
        {
            Spawn();
            timer = 0.001f;
        }
    }

    private void Spawn()
    {
        for (int i = 0; i < free.Count; i++)
        {
            if (!free[i].activeInHierarchy)
            {
                Vector3 position = Random.Range(-range * transform.localScale.y, range * transform.localScale.y) * transform.up + Random.Range(-range * transform.localScale.x, range * transform.localScale.x) * transform.right + transform.forward;

                free[i].transform.position = transform.position - position;
                free[i].transform.rotation = transform.rotation * Quaternion.Euler(90,0,0);
                Rigidbody pRB = free[i].GetComponent<Rigidbody>();
                pRB.velocity = free[i].transform.up * speed;
                free[i].SetActive(true);
                break;
            }
        }
    }
}
