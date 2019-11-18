using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsertText : MonoBehaviour
{
    public GameObject mobject;
    public float timer;
    public int size;
    public bool permanent;

    private Text mText,pText;
    private bool activated;
    private float retimer;
    
    void Start()
    {
        retimer = timer;
        mText = gameObject.GetComponent<Text>();
        pText = mobject.GetComponentInChildren<Text>();
        activated = false;
        pText.fontSize = size;
    }
    
    void FixedUpdate()
    {
        if(activated)
        {
            timer -= Time.deltaTime;
            mobject.SetActive(true);

            if (timer<=0)
            {
                if(!permanent)
                {
                    gameObject.SetActive(false);
                }
                timer = retimer;
                mobject.SetActive(false);
                activated = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag=="Player")
        {
            pText.text = mText.text;
            mobject.SetActive(true);
            activated = true;
        }
    }
}
