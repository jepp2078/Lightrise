using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Item_Resources_BaseResource : Item, Stackable
{
    protected string itemName = "Default resource name";
    protected string itemDescription = "Default resource description";
    protected int price = 1;
    protected float weight = 1f;
    protected int inventoryID = 999;
    protected int inventorySlot = 999;
    protected int _stackCount = 1;
    protected int _stackMax = 1024;
    protected Texture texture;

    public Item_Resources_BaseResource()
    {
        texture = Resources.Load("DefaultIcon", typeof(Texture)) as Texture;
    }

    public Texture getIcon()
    {
        return texture;
    }

    string Item.getItemText()
    {
        return itemName;
    }

    string Item.getItemDescription()
    {
        return itemDescription;
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
    public int getInventorySlot()
    {
        return inventorySlot;
    }
    public void setInventorySlot(int id)
    {
        inventorySlot = id;
    }
    public string getType()
    {
        throw new NotImplementedException();
    }

    public string getItemSlot()
    {
        throw new NotImplementedException();
    }

    public int stackCount
    {
        get
        {
            return _stackCount;
        }
        set
        {
            _stackCount = value;
        }
    }

    public int stackMax
    {
        get { return _stackMax; }
    }


    public float[] getProtections()
    {
        throw new NotImplementedException();
    }
}

