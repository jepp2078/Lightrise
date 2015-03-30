using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Item_Weapon_Troll_Clubber : Weapon, Item, Equipable, HotbarAble, Melee
{
	private int id = 999;
    private string itemName = "Troll Clubber";
	private string itemDescription = "A great sword made for slashing trolls!";
	private int price = 20;
    private int hotbarSlot = 0;
    private float damage = 0.42f;
    private float attackSpeed = 0.40f;
    private float durability = 60f;
    private float weaponRank = 0.0f;
    private float weight = 3.3f;
    private int inventoryID = 999;
    private int inventorySlot = 999;
    private float reachFloat = 0.5f;
    private GameObject reach;
    Texture texture;

	
	public Item_Weapon_Troll_Clubber()
    {
        reach = (GameObject) Resources.Load("GreatSword_Reach");
        texture = Resources.Load("troll_clubber", typeof(Texture)) as Texture;
    }
	
    public Texture getIcon()
    {
        return texture;
    }

    public Object getWeaponHitbox()
    {
        return reach;
    }

    public float getWeaponReachFloat()
    {
        return reachFloat;
    }

    Object Melee.getWeaponHitbox()
    {
        return reach;
    }

    float Melee.getWeaponReachFloat()
    {
        return reachFloat;
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
        string type = "great sword";
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
        string damageType = "slashing";
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
        string type = "great sword";
        return type;
    }

    int Item.getID()
    {
        return id;
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


    public int getInventorySlot()
    {
        return inventorySlot;
    }

    public void setInventorySlot(int id)
    {
        inventorySlot = id;
    }
}
