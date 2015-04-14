using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerGrave : MonoBehaviour{
    public List<Item> inventory = new List<Item>();
    public float baseInvSize;
    public float graveID;
    private float lifeTime = 5;
    private int inventoryCount;

    void Update()
    {
        if (inventoryCount == 0)    
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
                Destroy(this.gameObject.transform.parent.gameObject);
        } 
    }
    
    public void addItem(List<Item> e)
    {
        inventory.AddRange(e);
        for (int i = 0; i < e.Count; i++)
        {
            inventory[i].setInventoryID(i);
            inventory[i].setInventorySlot(i);
        }
        inventoryCount = inventory.Count;
    }

    [RPC]
    public bool takePlayerItem(int index)
    {
        inventory[index] = null;
        inventoryCount--;
        if (inventoryCount == 0)
            return true;
        else
            return false;
    }

    public List<Item> getItems()
    {
        return inventory;
    }

    public void setGraveId(float id)
    {
        graveID = id;
    }

    public float getGraveId()
    {
        return graveID;
    }

}
