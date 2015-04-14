using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Item_Weapon_Short_Bow : RangedWeapon
{
	private int id = 3;
	private string itemName = "Short Bow";
	private string itemDescription = "A beginners short bow";
    private int price = 20;
    private int hotbarSlot = 0;
    private float damage = 0.50f;
    private float attackSpeed = 0.50f;
    private float durability = 60f;
    private float weaponRank = 0.0f;
    private float weight = 3.3f;
    private int inventoryID = 999;
    private int inventorySlot = 999;
    private float projectileSpeed = 10; //Change this later
    private GameObject projectile;
    Texture texture;


    public Item_Weapon_Short_Bow()
    {
        projectile = (GameObject)Resources.Load("Archery_Projectile");
        texture = Resources.Load("short_bow", typeof(Texture)) as Texture;
	}

    public Texture getIcon()
    {
        return texture;
    }

    string Equipable.getItemSlot()
    {
        string type = "Main Hand";
        return type;
    }

    float Equipable.getDurability()
    {
        return durability;
    }

    bool Equipable.setDurability(float change)
    {
        durability -= change;
        if (durability <= 0)
        {
            return true;
        }
        return false;
    }

    string Weapon.getType()
    {
        string type = "bow";
        return type;
    }

    float Weapon.getDamage()
    {
        return damage;
    }

    float Weapon.getAttackspeed()
    {
        return attackSpeed;
    }

    string Weapon.getDamageType()
    {
        string damageType = "arrow";
        return damageType;
    }

    string Weapon.getItemDescription()
    {
        return itemDescription;
    }

    string Weapon.getItemSlot()
    {
        string type = "Main Hand";
        return type;
    }

    float Weapon.getWeaponRank()
    {
        return weaponRank;
    }


    public int getID()
    {
        return id;
    }

    string Item.getItemText()
    {
        return itemName;
    }

    string Item.getType()
    {
        string type = "bow";
        return type;
    }
    string Item.getItemDescription()
    {
        return itemDescription;
    }

    string Item.getItemSlot()
    {
        string type = "Main Hand";
        return type;
    }

    float[] Item.getProtections()
    {
        return new float[0];
    }

    int Item.getPrice()
    {
        return price;
    }

    int Item.getInventoryID()
    {
        return inventoryID;
    }

    void Item.setInventoryID(int id)
    {
        inventoryID = id;
    }

    float Item.getWeight()
    {
        return weight;
    }

    public void setHotbarSlot(int slot)
    {
        hotbarSlot = slot;
    }

    public int getHotbarSlot()
    {
        return hotbarSlot;
    }

    int HotbarAble.getInventoryID()
    {
        return inventoryID;
    }

    public int getSkillID()
    {
        return 999;
    }

    public float getProjectileSpeed()
    {
        return projectileSpeed;
    }

    public Object getProjectile()
    {
        return projectile;
    }

    public int getInventorySlot()
    {
        return inventorySlot;
    }

    public void setInventorySlot(int id)
    {
        inventorySlot = id;
    }


    public int getItemID()
    {
        return id;
    }
    public void setStartingDurability(float start)
    {
        durability = start;
    }
}