using UnityEngine;
using System.Collections;
public interface Weapon
{
    string getType();
    int getDamage();
    string getDamageType();
    string getItemDescription();
    string getItemSlot();
}