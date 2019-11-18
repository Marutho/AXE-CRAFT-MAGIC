using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCraft : MonoBehaviour
{
    [System.Serializable]
    public class Requisites
    {
        public string[] materials;
        public int[] number;
        
    }

    public GameObject triggeredObject;
    public Requisites requirement;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool CheckRequirements(GameObject player)
    {
        for (int i = 0; i < requirement.materials.Length; i++)
        {
            if (player.GetComponent<Movement>().availableItems.Contains(requirement.materials[i]) && ItemManager.Instance.GetItemNumber(requirement.materials[i]) >= requirement.number[i])
            {

            }

            else
            {
                return false;
            }
        }

        return true;
    }

    void UpdatePlayerItems(GameObject player)
    {
        for (int i = 0; i < requirement.materials.Length; i++)
        {
            ItemManager.Instance.UpdateItem(requirement.materials[i], -1 * requirement.number[i]);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<Movement>().canCraftItem && CheckRequirements(other.gameObject))
        {
            UpdatePlayerItems(other.gameObject);
            triggeredObject.tag = "Activated";
        }
    }
}
