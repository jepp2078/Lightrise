using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Item_Resources_Iron_Ore : Item_Resources_BaseResource
{
    public Item_Resources_Iron_Ore()
    {
        texture = Resources.Load("DefaultIcon", typeof(Texture)) as Texture;
        price = 1;
        weight = 1.0f;
        inventoryID = 999;
        inventorySlot = 999;
        itemName = "Iron Ore";
        itemDescription = "This usefull ore can be smelted into iron";
    }
}