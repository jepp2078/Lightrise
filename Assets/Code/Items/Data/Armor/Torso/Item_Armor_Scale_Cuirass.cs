using UnityEngine;
using System.Collections;

public class Item_Armor_Scale_Cuirass : ItemEntity, Armor, Item, Equipable
{

    private static int id = 999;
	private static string itemSlot = "Torso";
	private static string itemName = "Scale Cuirass";
	private static string itemDescription = "Chest armor made out of scale";
    private float[] protections = new float[15] { 0, 1.84f, 2.07f, 2.30f, 4.45f, 0, 4.45f, 4.45f, 4.45f, 2.30f, 3.31f, 4.45f, 0, 0, 0 };
	private static int price = 20;
    private static int inventoryID = 999;
    private static float encumbrance = 28.0f;
    private static float weight = 3.0f;
    private static float durability = 40;

	
	public Item_Armor_Scale_Cuirass(int x, int y) :
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
