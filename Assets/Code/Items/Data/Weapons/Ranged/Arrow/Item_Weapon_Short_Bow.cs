using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Item_Weapon_Short_Bow : ItemEntity, Weapon, Item, Equipable, HotbarAble, Ranged
{
	private static int id = 999;
	private static string itemName = "Short Bow";
	private static string itemDescription = "A beginners short bow";
    private static int price = 20;
    private static int hotbarSlot = 0;
    private static float damage = 0.50f;
    private static float attackSpeed = 0.50f;
    private static float durability = 60f;
    private static float weaponRank = 0.0f;
    private static float weight = 3.3f;
    private static int inventoryID = 999;
    private static float projectileSpeed = 10; //Change this later
    private static GameObject projectile;
    Texture texture;


    public Item_Weapon_Short_Bow(int x, int y) :
		base(id, itemName, x, y)
    {
        projectile = (GameObject)Resources.Load("Archery_Projectile");
        texture = Resources.Load("short_bow", typeof(Texture)) as Texture;
	}
	
	public float getDamage(){
		return damage;
		
	}
	
	public string getType(){
		string type = "bow";
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

    public Texture getIcon()
    {
        return texture;
    }

    public float getProjectileSpeed()
    {
        return projectileSpeed;
    }

    public Object getProjectile()
    {
        return projectile;
    }

}
