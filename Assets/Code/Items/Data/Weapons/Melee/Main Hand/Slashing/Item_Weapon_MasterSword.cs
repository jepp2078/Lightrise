using UnityEngine;
using System.Collections;

public class Item_Weapon_MasterSword : ItemEntity, Weapon, Item, Equipable, HotbarAble
{
	private static int id = 6;
	private static string itemName = "Master Sword "+"(+100-130)";
	private static string itemDescription = "If you have this you cheated. So there's that.";
	private static int price = 20;
    private static int hotbarSlot = 0;
    private static int inventoryID = 999;

	
	public Item_Weapon_MasterSword(string itemName, int x, int y) :
		base(id, itemName, x, y)
    {
    }
	
	
	public int getDamage(){
        int damage = Random.Range(130, 230);
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
	
	public int getArmor() {
		return 0;
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

