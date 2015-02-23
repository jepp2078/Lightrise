using UnityEngine;
using System.Collections;

public class Item_Armor_Cloth_Pants : ItemEntity, Armor, Item, Equipable
{

	private static int id = 3;
	private static string itemSlot = "Legs";
	private static string armorType = "Cloth";
	private static string itemName = "Cloth Pants "+"(+1 Armor)";
	private static string itemDescription = "Old cloth pants.";
    private float[] protections = new float[15] { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
	private static int price = 20;
    private static int inventoryID = 999;

	
	public Item_Armor_Cloth_Pants(int x, int y) :
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
