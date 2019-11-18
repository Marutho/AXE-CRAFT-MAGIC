using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuButtons : MonoBehaviour
{
    [SerializeField] MController mController;
    [SerializeField] Animator animator;
    [SerializeField] int newIndex;
    public GameObject menu1, menu2;

    void Update()
    {
        if (mController.index == newIndex)
        {
            animator.SetBool("selected", true);
            if (Input.GetAxis("Submit") == 1)
            {
                animator.SetBool("pressed", true);
                menu1.SetActive(false);
                menu2.SetActive(true);
            }
            else
            {
                if (animator.GetBool("pressed"))
                {
                    animator.SetBool("pressed", false);
                }
            }
        }
        else
        {
            animator.SetBool("selected", false);
        }
    }
}