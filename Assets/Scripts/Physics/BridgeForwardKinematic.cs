using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeForwardKinematic : MonoBehaviour
{

    public Transform joint1;
    public Transform joint2;

    private bool isJoint1Finished;
    private bool isJoint2Finished;
    private float anglesRotated;

    // Start is called before the first frame update
    void Start()
    {
        isJoint1Finished = false;
        isJoint2Finished = true;
        anglesRotated = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(gameObject.tag == "Activated")
        {
            if(!isJoint1Finished)
            {                
                joint1.Rotate(new Vector3(0f, 0f, Time.deltaTime * 10f));
                anglesRotated += Time.deltaTime * 10f;

                if (anglesRotated >= 90)
                {
                    isJoint1Finished = true;
                    isJoint2Finished = false;
                    anglesRotated = 0f;
                }                    
            }

            if (!isJoint2Finished)
            {
                joint2.Rotate(new Vector3(0f, 0f, Time.deltaTime * 10f));
                anglesRotated += Time.deltaTime * 10f;

                if (anglesRotated >= 90)
                {
                    isJoint2Finished = true;
                    anglesRotated = 0f;
                }
            }


        }
    }
}
