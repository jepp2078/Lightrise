using UnityEngine;
using System.Collections;

public class Item_Weapon_Unarmed : ItemEntity, Weapon, Item, Equipable, HotbarAble
{
    private static int id = 8;
    private static string itemName = "Unarmed " + "(+4-7)";
    private static string itemDescription = "Those are your hands. Email jepp2078@gmail.com if you see these";
    private static int price = 20;
    private static int hotbarSlot = 0;
    private static int inventoryID = 999;


    public Item_Weapon_Unarmed(int x, int y) :
        base(id, itemName, x, y)
    {
    }

    public int getDamage()
    {
        int damage = Random.Range(3, 7);
        return damage;

    }

    public string getType()
    {
        string type = "melee";
        return type;
    }

    public string getItemSlot()
    {
        string type = "Main Hand";
        return type;
    }

    public string getDamageType()
    {
        string damageType = "blunt";
        return damageType;
    }

    public string getItemText()
    {
        return itemName;
    }

    public string getItemDescription()
    {
        return itemDescription;
    }

    public int getArmor()
    {
        return 0;
    }

    public int getPrice()
    {
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