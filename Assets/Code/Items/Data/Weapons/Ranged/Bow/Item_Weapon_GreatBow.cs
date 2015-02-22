using UnityEngine;
using System.Collections;

public class Item_Weapon_GreatBow : ItemEntity, Weapon, Item, Equipable
{
	private static int id = 7;
	private static string itemName = "Great Bow "+"(+10-25 Damage)";
	private static string itemDescription = "Plain old Great Bow.";
	private static int price = 20;
	
	public Item_Weapon_GreatBow(int x, int y) :
		base(id, itemName, x, y)
    {
	}
	
	public int getDamage(){
		int damage = Random.Range(15, 25);
		return damage;
		
	}
	
	public string getType(){
		string type = "ranged";
		return type;
	}
	
	public string getItemSlot(){
		string type = "Main Hand";
		return type;
	}
	
	public string getDamageType(){
		string damageType = "piercing";
		return damageType;
	}

	public string getItemText() {
		return itemName;
	}

	public string getItemDescription() {
		return itemDescription;
	}

	public int getArmor() {
		return 0;
	}
	
	public int getPrice() {
		return price;
	}
}
