using UnityEngine;
using System.Collections;

public class Item_Weapon_GreatSword : ItemEntity, Weapon, Item, Equipable, HotbarAble
{
	private static int id = 5;
	private static string itemName = "Great Sword "+"(+13-34 Damage)";
	private static string itemDescription = "Plain old Great Sword.";
	private static int price = 20;
    private static int hotbarSlot = 0;
    private static int inventoryID = 999;

	
	public Item_Weapon_GreatSword(int x, int y) : base(id, itemName, x, y)
    {
    }
	
	
	public int getDamage(){
        int damage = Random.Range(21, 34);
		return damage;
		
	}
	
	public string getType(){
		string type = "melee";
		return type;
	}
	
	public string getItemSlot(){
		string type = "Main Hand";
		return type;
	}
	
	public string getDamageType(){
		string damageType = "slashing";
		return damageType;
	}

	public string getItemText() {
		return itemName;
	}

	public string getItemDescription() {
		return itemDescription;
	}

    public float[] getProtections()
    {
        return new float[0];
	}
	
	public int getPrice() {
		return price;
	}

    public void setHotbarSlot(int slot)
    {
        hotbarSlot = slot;
    }

    public int getHotbarSlot()
    {
        return hotbarSlot;
    }
    public int getInventoryID()
    {
        return inventoryID;
    }

    public void setInventoryID(int id)
    {
        inventoryID = id;
    }

    public int getSkillID()
    {
        return 999;
    }
}
