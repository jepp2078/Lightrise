using UnityEngine;
using System.Collections;

public class NpcFunction : MonoBehaviour
{
    public Npc npcInstance;
    public string equipItem(Item item)
    {
        return npcInstance.npc.equip(item.getInventoryID());
    }
    public string status()
    {
        string tempStats = npcInstance.npc.getStatus();
        return tempStats;
    }

    public void hotbarUse(int hotbarSlot)
    {
        HotbarAble hotbarType = npcInstance.npc.getHotbarType(hotbarSlot);
        if (hotbarType is Weapon)
        {
            if (npcInstance.npc.getEquipmentIDinSlot(6) == -1 || hotbarType.getInventoryID() != npcInstance.npc.getEquipmentIDinSlot(6))
            {
                equipItem(npcInstance.npc.getInventoryItem(hotbarType.getInventoryID()));
            }
        }
        else if (hotbarType is Castable)
        {
            npcInstance.npc.setActiveSkill((Castable)hotbarType);
        }
    }
    public void putOnHotbar(HotbarAble instance, int hotbarSlot)
    {
        if (instance is HotbarAble)
        {
            npcInstance.npc.hotbarAdd(instance, hotbarSlot);
        }
        //if(hotbarSlot == 2)
        //    hotbarGuiFunction.instance.setHotbarIcon(instance.getIcon(), hotbarSlot);
    }

    public string performAction()
    {
        Castable skill = npcInstance.npc.getActiveSkill();
        if (skill.getCurrentCooldown() == 0)
        {
            skill.cast();
            skill.setCurrentCooldown(skill.getCooldown());
            npcInstance.instance.addCooldown(skill);
            skill.updateGainPrCast();
            npcInstance.instance.gainSkill(skill.getGainPrCast(), ((Skill)skill).getSkillID());
            return skill.getCastMsg();
        }
        else
        {
            return "Skill " + ((Skill)skill).getSkillText() + " is on cooldown!";
        }
    }

    public string performAttack(Weapon weapon)
    {
        if (weapon is Melee) //damage formula weapon [ (0.2 * MS + 0.05 * WS + 0.03 * WM) * (WD*10) - (AR) ]
        {
            GameObject reach = (GameObject)Instantiate(((Melee)weapon).getWeaponReach());
            reach.transform.parent = npcInstance.npcObject.transform;
            reach.transform.position = npcInstance.npcObject.transform.position;
            reach.transform.position += new Vector3(0f, 0f, 0.54f);
            reach.transform.rotation = npcInstance.npcObject.transform.rotation;
            float damage = (0.2f * npcInstance.npc.getStat("str") + npcInstance.npc.getWeaponSkillEffect(weapon.getType(), null) + npcInstance.npc.getWeaponSkillEffect(null, weapon.getType())) * weapon.getDamage()*10;
            string damageType = weapon.getDamageType();
            WeaponHitInfo info = reach.GetComponentInChildren<WeaponHitInfo>();
            info.damage = damage;
            info.damageType = damageType;
            float speed = ((weapon.getAttackspeed() * 5) - (0.008f * npcInstance.npc.getStat("quick") + 0.003f * npcInstance.npc.getWeaponSkill(null,weapon.getType())));
            npcInstance.instance.addAttackCooldown(speed);
        }

        return "";
    }

    public void takeDamage(float damage, string damageType)
    {
        npcInstance.npc.setHealth(damage, 0, false, damageType);
    }

}
