using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftItem : MonoBehaviour
{

    public Transform craftMachine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<Movement>().canCraftItem )
        {
            if (gameObject.tag == "i_Plastic" && ItemManager.Instance.GetItemNumber("i_Wood") >= 2)
            {
                ItemManager.Instance.UpdateItem("i_Wood", -2);
                ObjectPoolManager.Instance.GetObjectFromPool("i_Plastic", craftMachine.position + new Vector3(0f, 2f, 0f), Quaternion.identity);
            }

            if (gameObject.tag == "i_Iron" && ItemManager.Instance.GetItemNumber("i_Stone") >= 2)
            {
                ItemManager.Instance.UpdateItem("i_Stone", -2);
                ObjectPoolManager.Instance.GetObjectFromPool("i_Iron", craftMachine.position + new Vector3(0f, 2f, 0f), Quaternion.identity);
            }

            other.GetComponent<Movement>().canCraftItem = false;
        }
    }
}
