using UnityEngine;
using System.Collections;

public class Item_Armor_Cloth_Sack : ItemEntity, Armor, Item, Equipable
{
private static int id = 2;
	private static string itemSlot = "Head";
	private static string armorType = "Cloth";
	private static string itemName = "Cloth Sack "+"(+1 Armor)";
	private static string itemDescription = "Old potato sack. It's on your head.";
	private static int armor = 1;
	private static int price = 20;
    private static int inventoryID = 999;

	
	public Item_Armor_Cloth_Sack(int x, int y) : 
        base(id, itemName, x, y)
	{
    }
	
	public string getItemSlot(){
		return itemSlot;
	}

	public string getArmorType() {
		return armorType;
	}

	public int getArmor() {
		return armor;
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
