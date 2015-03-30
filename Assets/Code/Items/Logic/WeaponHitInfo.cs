﻿using UnityEngine;
using System.Collections;

public class WeaponHitInfo : MonoBehaviour {
    public float damage;
    public string damageType;
    public Player playerInstance;
    public Weapon weapon;
    private GuiFunction gui;
	// Use this for initialization

    void OnTriggerStay(Collider other) {
        calculateDamage(other);
    }

    void calculateDamage(Collider other)
    {
        if (other.gameObject.tag == "npc" && playerInstance != null)
        {
            gui = playerInstance.gui;
            float armorValue = 0;
            switch (damageType)
            {
                case "slashing": armorValue = other.gameObject.GetComponent<NpcObject>().getProtection(damageType); break;
                case "arrow": armorValue = other.gameObject.GetComponent<NpcObject>().getProtection(damageType); break;
            }
            damage -= armorValue;
            gui.newTextLine("You hit " + other.gameObject.name + " for " + damage + " " + damageType + " damage!");
            other.gameObject.GetComponent<NpcFunction>().takeDamage(damage, damageType);

            if (weapon != null)
            {
                playerInstance.gainSkill((1.1f - (playerInstance.player.getSkillLevel(playerInstance.player.getWeaponSkillId(weapon.getType()) / 100))), playerInstance.player.getWeaponSkillId(weapon.getType()));

                if (playerInstance.player.getWeaponSkill(null, weapon.getType()) != 0)
                {
                    playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(playerInstance.player.getWeaponSkillId(weapon.getType()) / 100))), playerInstance.player.getWeaponMasterySkillId(weapon.getType()));
                }
            }

        }

        else if (other.gameObject.tag == "Player")
        {
            if (playerInstance != null)
            {
                if (playerInstance == other.gameObject.GetComponent<Player>())
                {
                    destroy();
                    return;
                }
                gui = playerInstance.gui;
                if (weapon != null)
                 playerInstance.gainSkill((1.1f - (playerInstance.player.getSkillLevel(playerInstance.player.getWeaponSkillId(weapon.getType()) / 100))), playerInstance.player.getWeaponSkillId(weapon.getType()));

                if (weapon != null)
                {
                    if (playerInstance.player.getWeaponSkill(null, weapon.getType()) != 0)
                    {
                        playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(playerInstance.player.getWeaponSkillId(weapon.getType()) / 100))), playerInstance.player.getWeaponMasterySkillId(weapon.getType()));
                    }
                }
            }

            float armorValue = 0;

            switch (damageType)
            {
                case "slashing": armorValue = other.gameObject.GetComponent<PlayerObject>().getProtection(damageType); break;
                case "arrow": armorValue = other.gameObject.GetComponent<PlayerObject>().getProtection(damageType); break;
            }
            damage -= armorValue;
            other.gameObject.GetComponent<Function>().takeDamage(damage, damageType);
        }

        destroy();
    }

    public void destroy()
    {
        if (this.transform.parent != null){
            Destroy(this.transform.parent.gameObject);
        }
        else
        {
            Destroy(this.transform.gameObject); 

        }
    }
}
