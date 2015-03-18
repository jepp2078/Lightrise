using UnityEngine;
using System.Collections;

public class Function : MonoBehaviour {
    public Player playerInstance;
    public GuiFunction gui;

    public string equipItem(Item item)
    {
        return playerInstance.player.equip(item.getInventoryID());
    }

    public string dequipItem(int equipSlot)
    {
        return playerInstance.player.dequip(equipSlot);
    }

    public string status(){
        string tempStats = playerInstance.player.getStatus();
        return tempStats;
	}

    public void putOnHotbar(HotbarAble instance, int hotbarSlot)
    {
        if (instance is HotbarAble)
        {
            playerInstance.player.hotbarAdd(instance, hotbarSlot);
            gui.setHotbarIcon(hotbarSlot, instance.getIcon(), false);
        }
    }

    public void removeFromHotbar(int hotbarSlot)
    {
        playerInstance.player.hotbarRemove(hotbarSlot);
    }

    public void sheathWeapon()
    {
        if (playerInstance.player.isWeaponSheathed())
        {
            playerInstance.player.unSheathWeapon();
            gui.setActiveWeaponIcon(null, false, true);
        }
        else
        {
            playerInstance.player.sheathWeapon();
            gui.setActiveWeaponIcon(null, true, true);
        }
        playerInstance.player.setActiveSkill(null);
        gui.setActiveSkillIcon(null, true);
    }

    public void hotbarUse(int hotbarSlot)
    {
        HotbarAble hotbarType = playerInstance.player.getHotbarType(hotbarSlot);
        if (hotbarType is Weapon)
        {
            if (playerInstance.player.getEquipmentIDinSlot(6) == -1 || hotbarType.getInventoryID() != playerInstance.player.getEquipmentIDinSlot(6))
            {
                gui.newTextLine(equipItem(playerInstance.player.getInventoryItem(hotbarType.getInventoryID())));
                gui.setActiveWeaponIcon(hotbarType.getIcon(), playerInstance.player.isWeaponSheathed(), false);
			}
        }
        else if (hotbarType is Castable)
        {
            playerInstance.player.setActiveSkill((Castable)hotbarType);
            gui.setActiveSkillIcon(hotbarType.getIcon(), false);
        }
        else
        {
            playerInstance.player.setActiveSkill(null);
            gui.setActiveSkillIcon(null, true);
        }
    }

    public string performAction()
    {
        Castable skill = playerInstance.player.getActiveSkill();
        if (skill.getCurrentCooldown() == 0)
        {
            skill.cast();
            skill.setCurrentCooldown(skill.getCooldown());
            playerInstance.instance.addCooldown(skill);
            skill.updateGainPrCast();
            playerInstance.instance.gainSkill(skill.getGainPrCast(), ((Skill)skill).getSkillID());
            playerInstance.player.setActiveSkill(null);
            gui.setActiveSkillIcon(null, true);
            return skill.getCastMsg();
        }
        else
        {
            return "Skill " + ((Skill)skill).getSkillText() + " is on cooldown!";
        }
    }

    public void performAttack(Weapon weapon)
    {
        if (weapon is Melee) //damage formula weapon [ (0.2 * MS + 0.05 * WS + 0.03 * WM) + >((WD*10) - (AR*2)) ]
        {
            GameObject hitbox = (GameObject)Instantiate(((Melee)weapon).getWeaponHitbox());
            hitbox.transform.parent = playerInstance.playerObject.transform;
            hitbox.transform.position = playerInstance.playerObject.transform.position;
            Vector3 tempOffset = playerInstance.playerObject.transform.forward;
            tempOffset.Scale(new Vector3(1f, 0f, 1f));
            hitbox.transform.position += tempOffset;
            hitbox.transform.rotation = playerInstance.playerObject.transform.rotation;
            float damage = (0.2f * playerInstance.player.getStat("str") + playerInstance.player.getWeaponSkillEffect(weapon.getType(), null) + playerInstance.player.getWeaponSkillEffect(null,weapon.getType())) * weapon.getDamage()*10;
            string damageType = weapon.getDamageType();
            WeaponHitInfo info = hitbox.GetComponentInChildren<WeaponHitInfo>();
            info.damage = damage;
            info.damageType = damageType;
            info.playerInstance = playerInstance;
            info.weapon = weapon;
            float speed = ((weapon.getAttackspeed() * 5) - (0.008f * playerInstance.player.getStat("quick") + 0.003f * playerInstance.player.getWeaponSkill(null, weapon.getType())));
            playerInstance.instance.addAttackCooldown(speed);
         }
        else if (weapon is Ranged) //damage formula weapon [ (0.2 * MS + 0.05 * WS + 0.03 * WM) + >((WD*10) - (AR*2)) ]
        {
            GameObject hitbox = (GameObject)Instantiate(((Ranged)weapon).getProjectile());
            hitbox.transform.position = playerInstance.playerObject.transform.position;
            Vector3 tempOffset = playerInstance.playerObject.transform.forward;
            hitbox.transform.position += tempOffset;
            hitbox.transform.position += new Vector3(0f, 0.60f, 0f);
            hitbox.transform.rotation = playerInstance.playerObject.transform.rotation;
            hitbox.GetComponent<Rigidbody>().AddForce(playerInstance.playerObject.GetComponentInChildren<Camera>().transform.forward * 3000f);
            float damage = (0.2f * playerInstance.player.getStat("dex") + playerInstance.player.getWeaponSkillEffect(weapon.getType(), null) + playerInstance.player.getWeaponSkillEffect(null, weapon.getType())) * weapon.getDamage() * 10;
            string damageType = weapon.getDamageType();


            WeaponHitInfo info = hitbox.GetComponentInChildren<WeaponHitInfo>();
            info.damage = damage;
            info.damageType = damageType;
            info.playerInstance = playerInstance;
            info.weapon = weapon;

            float speed = ((weapon.getAttackspeed() * 5) - (0.008f * playerInstance.player.getStat("quick") + 0.003f * playerInstance.player.getWeaponSkill(null, weapon.getType())));
            playerInstance.instance.addAttackCooldown(speed);
        }

    }

    public void takeDamage(float damage, string damageType)
    {
        playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(9) / 100)), 9);
        switch (damageType)
        {
            case "mental": playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(12) / 100)), 12); break;
            case "infliction": playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(6) / 100)), 6); break;
            case "arrow": playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(5) / 100)), 5); break;
            case "piercing": playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(5) / 100)), 5); break;
            case "slashing": playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(5) / 100)), 5); break;
            case "bludgeoning": playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(5) / 100)), 5); break;
            case "acid": playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(4) / 100)), 4); break;
            case "unholy": playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(4) / 100)), 4); break;
            case "fire": playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(8) / 100)), 8); break;
            case "cold": playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(8) / 100)), 8); break;
            case "impact": playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(8) / 100)), 8); break;
            case "lightning": playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(8) / 100)), 8); break;
        }
        playerInstance.player.setHealth(damage, 0, false, damageType);
    }
   
}
