using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerGrave : MonoBehaviour{
    public List<Item> inventory = new List<Item>();
    public float baseInvSize;

    public void addItem(List<Item> e)
    {
        inventory.AddRange(e);
        for (int i = 0; i < e.Count; i++)
        {
            e[i].setInventoryID(999);
            e[i].setInventorySlot(999);
        }
    }

    public Item takeItem(int index)
    {
        Item tempItem = inventory[index];
        inventory.RemoveAt(index);
        return tempItem;
    }


}
