using UnityEngine;
using System.Collections;
public interface Weapon
{
    string getType();
    float getDamage();
    float getAttackspeed();
    string getDamageType();
    string getItemDescription();
    string getItemSlot();
    float getWeaponRank();
}