using UnityEngine;
using System.Collections;

public class Item_Armor_Leather_Sandals : ItemEntity, Armor, Item, Equipable
{

	private static int id = 0;
	private static string itemSlot = "Feet";
	private static string armorType = "Leather";
	private static string itemName = "Leather Sandals "+"(+0 Armor)";
	private static string itemDescription = "Fasionable leather sandals";
	private static int armor = 0;
	private static int price = 20;
	
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
}
