using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    public void NewGame(string lvl)
    {
        SceneManager.LoadScene(lvl);
    }

    public void LoadGame(string lvl)
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
