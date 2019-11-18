using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBehaviour : MonoBehaviour
{

    private void Update()
    {
        if (gameObject.active)
            transform.Rotate(new Vector3(0f, 50f, 0f) * Time.deltaTime);       
            
    }

    private void OnTriggerStay(Collider other)
    {
        
        if(other.tag == "Player")
        {           
            if (!other.GetComponent<Movement>().availableItems.Contains(gameObject.tag))
            {
                other.GetComponent<Movement>().availableItems.Add(gameObject.tag);
            }

            ItemManager.Instance.UpdateItem(gameObject.tag, 1);

            ObjectPoolManager.Instance.ReturnObjectToPool(gameObject, gameObject.tag);
        }
    }
}
