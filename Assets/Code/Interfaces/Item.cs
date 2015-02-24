using UnityEngine;
using System.Collections;

public interface Item
{
    string getItemText();
    string getType();
    int getID();
    string getItemDescription();
    string getItemSlot();
    float[] getProtections();
    int getPrice();
    int getInventoryID();
    void setInventoryID(int id);
    float getWeight();
}