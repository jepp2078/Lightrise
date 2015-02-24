using UnityEngine;
using System.Collections;

public interface Armor
{
    string getItemSlot();
    float[] getProtections();
    string getName();
    string getItemDescription();
    float getEncumbrance();
}

