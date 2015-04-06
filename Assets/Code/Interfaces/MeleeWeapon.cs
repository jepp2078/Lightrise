using UnityEngine;
using System.Collections;

public interface MeleeWeapon : Weapon
{
    Object getWeaponHitbox();
    float getWeaponReachFloat();
}
