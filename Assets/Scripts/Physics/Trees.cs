using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    public float timer;
    public GameObject newTree;

    private bool grow;
    private float newtimer;
    private GameObject growingTree;

    private void Start()
    {
        growingTree = Instantiate(newTree, transform.position, transform.rotation);
        growingTree.SetActive(false);
        newtimer = timer;
        grow = false;
    }

    void FixedUpdate()
    {
        if (grow)
        {
            growingTree.transform.localScale += Vector3.one * 0.0002f;
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                grow = false;
                growingTree.SetActive(false);
                gameObject.transform.localScale = growingTree.transform.localScale;
                timer = newtimer;
            }
        }
    }

    void OnDisable()
    {
        if (!grow)
        {
            if (gameObject.tag == "i_Wood")
            {
                ObjectPoolManager.Instance.GetObjectFromPool("i_Wood", transform.position + new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity);
            }

            if (gameObject.tag == "i_Stone")
            {
                ObjectPoolManager.Instance.GetObjectFromPool("i_Stone", transform.position + new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity);
            }

            transform.localScale = new Vector3(0, 0, 0);
            growingTree.transform.localScale = new Vector3(0.05f,0.05f,0.05f);
            
            Invoke("Reset", 0.5f);
        }
        else
        {
            Invoke("FalseAlarm", 0f);
        }
    }

    void Reset()
    {
        growingTree.SetActive(true);
        gameObject.SetActive(true);
        grow = true;
    }  

    void FalseAlarm()
    {
        growingTree.SetActive(true);
        gameObject.SetActive(true);
    }
}
