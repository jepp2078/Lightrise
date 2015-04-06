using UnityEngine;
using System.Collections;

public interface RangedWeapon : Weapon
{
    float getProjectileSpeed();
    Object getProjectile();
}