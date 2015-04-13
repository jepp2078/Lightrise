using UnityEngine;
using System.Collections;

public class Item_Tool_Pickaxe : Tool
{
    private int id = 999;
    private string itemName = "Pickaxe";
	private string itemDescription = "A tool used to extract minerals from resource nodes";
	private int price = 20;
    private int hotbarSlot = 0;
    private float durability = 60f;
    private float weight = 1.5f;
    private int inventoryID = 999;
    private int inventorySlot = 999;
    Texture texture;


    public Item_Tool_Pickaxe()
    {
        texture = Resources.Load("defaultIcon", typeof(Texture)) as Texture;
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
        string type = "pickaxe";
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


    public int getInventorySlot()
    {
        return inventorySlot;
    }

    public void setInventorySlot(int id)
    {
        inventorySlot = id;
    }
}