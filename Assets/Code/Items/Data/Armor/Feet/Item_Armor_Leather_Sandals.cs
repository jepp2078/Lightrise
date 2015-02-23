using UnityEngine;
using System.Collections;

public class Item_Armor_Leather_Sandals : ItemEntity, Armor, Item, Equipable
{

	private static int id = 0;
	private static string itemSlot = "Feet";
	private static string armorType = "Leather";
	private static string itemName = "Leather Sandals";
	private static string itemDescription = "Fashionable leather sandals";
	private static int price = 20;
    private static int inventoryID = 999;
    private float[] protections = new float[15] { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
	
	public Item_Armor_Leather_Sandals(int x, int y) :
		base(id, itemName, x, y)
	{
    }
	
	public string getItemSlot(){
		return itemSlot;
	}

	public string getArmorType() {
		return armorType;
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
}
