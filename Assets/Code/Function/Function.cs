using UnityEngine;
using System.Collections;

public class Function : MonoBehaviour {
    public Player playerInstance;

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
        if(instance is HotbarAble)
            playerInstance.player.hotbarAdd(instance, hotbarSlot);
        //if(hotbarSlot == 2)
        //    hotbarGuiFunction.instance.setHotbarIcon(instance.getIcon(), hotbarSlot);
    }

    public void removeFromHotbar(int hotbarSlot)
    {
        playerInstance.player.hotbarRemove(hotbarSlot);
    }

    public void hotbarUse(int hotbarSlot)
    {
        HotbarAble hotbarType = playerInstance.player.getHotbarType(hotbarSlot);
        if (hotbarType is Weapon)
        {
            if (playerInstance.player.getEquipmentIDinSlot(6) == -1 || hotbarType.getInventoryID() != playerInstance.player.getEquipmentIDinSlot(6))
            {
                Debug.Log(equipItem(playerInstance.player.getInventoryItem(hotbarType.getInventoryID())));
			}
        }
        else if (hotbarType is Castable)
        {
            playerInstance.player.setActiveSkill((Castable)hotbarType);
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
            tempOffset.Scale(new Vector3(1f, 0f, 1f));
            hitbox.transform.position += tempOffset;
            hitbox.transform.rotation = playerInstance.playerObject.transform.rotation;
            hitbox.GetComponent<Rigidbody>().AddForce(playerInstance.playerObject.GetComponentInChildren<Camera>().transform.forward * 1000f);
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
        playerInstance.player.setHealth(damage, 0, false, damageType);
    }
   
}
