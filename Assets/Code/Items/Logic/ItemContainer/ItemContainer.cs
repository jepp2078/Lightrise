using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemContainer : MonoBehaviour{
    public List<Item> inventory = new List<Item>();
    public float baseInvSize;
    //public int test;
	// Use this for initialization
	void Start () {
        Item test = new Item_Armor_Scale_Helm(0,0);
        inventoryAdd(test);
	}
	
	// Update is called once per frame
    public Item getInventory(int index)
    {
        return inventory[index];
    }

    public bool inventoryAdd(Item e)
    {
        if (e is Item)
        {
            if (inventory.Count < baseInvSize)
            {
                inventory.Add(e);
                bool spaceLeft = true;
                return spaceLeft;
            }
        }
        return false;
    }

    public void inventoryRemove(int index)
    {
        inventory.RemoveAt(index);
    }


}
