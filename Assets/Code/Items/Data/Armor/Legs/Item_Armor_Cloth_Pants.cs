using UnityEngine;
using System.Collections;

public class Item_Armor_Cloth_Pants : ItemEntity, Armor, Item, Equipable
{

	private static int id = 999;
	private static string itemSlot = "Legs";
	private static string itemName = "Cloth Pants";
	private static string itemDescription = "Old cloth pants";
    private float[] protections = new float[15] { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
	private static int price = 20;
    private static int inventoryID = 999;
    private static float encumbrance = 1.0f;
    private static float weight = 3.0f;
    private static float durability = 40;

	
	public Item_Armor_Cloth_Pants(int x, int y) :
		base(id, itemName, x, y)
	{
    }
	
	public string getItemSlot(){
		return itemSlot;
	}

	public float[] getProtections() {
		return protections;
	}

	public string getItemText() {
		return itemName;
	}

	public string getItemDescription() {
		return itemDescription;
	}

	public string getType() {
		return "armor";
	}
	
	public int getPrice() {
		return price;
	}
    public int getInventoryID()
    {
        return inventoryID;
    }

    public void setInventoryID(int id)
    {
        inventoryID = id;
    }

    public float getEncumbrance()
    {
        return encumbrance;
    }


    public float getWeight()
    {
        return weight;
    }


    public float getDurability()
    {
        return durability;
    }

    public bool setDurability(float change)
    {
        durability -= change;
        if (durability <= 0)
        {
            return true;
        }
        return false;
    }
}
