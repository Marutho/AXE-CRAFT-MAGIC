using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MController : MonoBehaviour
{
    public int index;
    [SerializeField] bool down;
    [SerializeField] int numButtons;

    void Update()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            if (!down)
            {
                if (Input.GetAxis("Vertical") < 0)
                {
                    if (index < numButtons)
                    {
                        index++;
                    }
                    else
                    {
                        index = 0;
                    }
                }
                else
                {
                    if (Input.GetAxis("Vertical") > 0)
                    {
                        if (index > 0)
                        {
                            index--;
                        }
                        else
                        {
                            index = numButtons;
                        }
                    }
                }
                down = true;
            }
        }
        else
        {
            down = false;
        }
    }
}
