using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public MController mController;
    public Animator animator;
    public int newIndex;
    public GameObject menu1, menu2;

    void Update()
    {
        if(mController.index == newIndex)
        {
            animator.SetBool("selected", true);
            if(Input.GetAxis("Submit")==1)
            {
                animator.SetBool("pressed", true);
                switch (newIndex)
                {
                    case 0:
                        NewGame();
                        break;
                    case 1:
                        menu1.SetActive(false);
                        menu2.SetActive(true);
                        break;
                    case 2:
                        Quit();
                        break;
                }
            }
            else
            {
                if(animator.GetBool("pressed"))
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

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
