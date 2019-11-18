using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemResourcesUI : MonoBehaviour
{

    public Image[] resourceImage;
    public Text numberText;
    public Text[] resourceText;
    public float timePassed;


    // Start is called before the first frame update
    void Start()
    {
        DeactivateUIElements();

        timePassed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(ItemManager.Instance.updatedItem)
        {
            DeactivateUIElements();
            ActivateItemUI();
            timePassed += Time.deltaTime;

            if(timePassed >= 1.0f)
            {
                timePassed = 0.0f;
                DeactivateUIElements();
                ItemManager.Instance.updatedItem = false;
            }
        }

        ShowAllItems();
    }

    void DeactivateUIElements()
    {
        for (int i = 0; i < resourceImage.Length; i++)
        {
            resourceImage[i].gameObject.SetActive(false);
        }

        numberText.gameObject.SetActive(false);
    }

    void ShowAllItems()
    {
        resourceText[0].text = ItemManager.Instance.GetItemNumber("i_Wood").ToString();
        resourceText[1].text = ItemManager.Instance.GetItemNumber("i_Stone").ToString();
        resourceText[2].text = ItemManager.Instance.GetItemNumber("i_Iron").ToString();
        resourceText[3].text = ItemManager.Instance.GetItemNumber("i_Plastic").ToString();
    }

    void ActivateItemUI()
    {
        resourceImage[ItemManager.Instance.items[ItemManager.Instance.lastUpdatedItem] - 1].gameObject.SetActive(true);
        numberText.text = "+1";
        numberText.gameObject.SetActive(true);
    }
}
