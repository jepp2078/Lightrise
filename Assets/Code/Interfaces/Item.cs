using UnityEngine;
using System.Collections;

public interface Item
{

    string getItemText();
    string getType();
    int getID();
    string getItemDescription();
    string getItemSlot();
    int getArmor();
    int getPrice();
}