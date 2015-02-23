using UnityEngine;
using System.Collections;

public interface Armor
{
    string getItemSlot();
    string getArmorType();
    float[] getProtections();
    string getName();
    string getItemDescription();
}

