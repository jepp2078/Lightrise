using UnityEngine;
using System.Collections;

public interface Weapon : Equipable, HotbarAble
{
    string getType();
    float getDamage();
    float getAttackspeed();
    string getDamageType();
    string getItemDescription();
    string getItemSlot();
    float getWeaponRank();
}