using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{

    public int[] inventory;
    public Dictionary<string, int> items;

    public string lastUpdatedItem;
    public bool updatedItem;

    protected ItemManager() { }

    public void Initiate()
    {
        //Do nothing
    }

    // Start is called before the first frame update
    private void Start()
    {
        updatedItem = false;
        items = new Dictionary<string, int>();
        itemdef();

        inventory = new int[100];
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("[ItemManager] " + "debug item: " + inventory[items["debug"]]);   
    }

    public void UpdateItem(string tag, int value)
    {       
        inventory[items[tag]] += value;
        updatedItem = true;
        lastUpdatedItem = tag;
        Debug.Log("[ItemManager] " + tag + ": " + inventory[items[tag]]);
    }

    public int GetItemNumber(string tag)
    {
        return inventory[items[tag]];
    }

    private void itemdef()
    {
        items.Add("debug", 0);
        items.Add("i_Wood", 1);
        items.Add("i_Stone", 2);
        items.Add("i_Plastic", 3);
        items.Add("i_Iron", 4);
        items.Add("t_Sword", 98);
        items.Add("t_Staff", 99);
    }
}