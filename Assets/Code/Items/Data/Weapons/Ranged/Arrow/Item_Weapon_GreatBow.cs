using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Item_Weapon_GreatBow : ItemEntity, Weapon, Item, Equipable, HotbarAble
{
	private static int id = 999;
	private static string itemName = "Great Bow";
	private static string itemDescription = "A beginners great bow.";
    private static int price = 20;
    private static int hotbarSlot = 0;
    private static float damage = 0.30f;
    private static float attackSpeed = 0.60f;
    private static float durability = 60f;
    private static float weaponRank = 0.0f;
    private static float weight = 3.3f;
    private static int inventoryID = 999;
    private static RawImage icon;
    Texture texture;
	
	public Item_Weapon_GreatBow(int x, int y) :
		base(id, itemName, x, y)
    {
	}
	
	public float getDamage(){
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
		string damageType = "arrow";
		return damageType;
	}

	public string getItemText() {
		return itemName;
	}

	public string getItemDescription() {
		return itemDescription;
	}

	public float[] getProtections() {
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


    public float getAttackspeed()
    {
        return attackSpeed;
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


    public float getWeaponRank()
    {
        return weaponRank;
    }

    public RawImage getIcon()
    {
        return icon;
    }
}
