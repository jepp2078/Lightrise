using UnityEngine;
using System.Collections;

public class Item_Armor_Scale_Helm : ItemEntity, Armor, Item, Equipable
{

	private static int id = 999;
	private static string itemSlot = "Head";
	private static string itemName = "Scale Helm";
	private static string itemDescription = "Helm made out of scale armor";
    private float[] protections = new float[15] { 0, 0.92f, 1.03f, 1.15f, 2.18f, 0, 2.18f, 2.18f, 2.18f, 1.15f, 1.65f, 2.18f, 0, 0, 0 };
	private static int price = 20;
    private static int inventoryID = 999;
    private static float encumbrance = 7.0f;
    private static float weight = 3.0f;
    private static float durability = 40;

	
	public Item_Armor_Scale_Helm(int x, int y) :
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
