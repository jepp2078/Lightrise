using UnityEngine;
using System.Collections;

public interface Item
{
    string getItemText();
    string getType();
    string getItemDescription();
    string getItemSlot();
    float[] getProtections();
    int getPrice();
    int getInventorySlot();
    void setInventorySlot(int id);
    int getInventoryID();
    void setInventoryID(int id);
    float getWeight();
    Texture getIcon();
    int getItemID();
}