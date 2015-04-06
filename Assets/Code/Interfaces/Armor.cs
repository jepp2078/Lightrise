using UnityEngine;
using System.Collections;

public interface Armor : Equipable
{
    string getItemSlot();
    float[] getProtections();
    string getName();
    string getItemDescription();
    float getEncumbrance();
}

