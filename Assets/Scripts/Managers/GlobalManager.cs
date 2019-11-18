using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : Singleton<GlobalManager>
{

    protected GlobalManager() { }

    GameObject player;
    // Start is called before the first frame update

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetPlayer()
    {
        player.transform.position = new Vector3(0, 1, 0);
        player.GetComponent<Movement>().currentItem = 0;
    }
}
