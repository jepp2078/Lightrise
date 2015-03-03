using UnityEngine;
using System.Collections;

public class NpcFunction : MonoBehaviour
{
    public Npc npcInstance;

    public string status()
    {
        string tempStats = npcInstance.npc.getStatus();
        return tempStats;
    }

    public void hotbarUse(int hotbarSlot)
    {
        HotbarAble hotbarType = npcInstance.npc.getHotbarType(hotbarSlot);
        if (hotbarType is Castable)
        {
            npcInstance.npc.setActiveSkill((Castable)hotbarType);
        }
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
        if (weapon is Melee) //damage formula weapon [ (0.2 * MS + 0.05 * WS + 0.03 * WM) + WD - AR ]
        {
            GameObject reach = (GameObject)Instantiate(((Melee)weapon).getWeaponReach());
            reach.transform.parent = npcInstance.npcObject.transform;
            reach.transform.position = npcInstance.npcObject.transform.position;
            reach.transform.rotation = npcInstance.npcObject.transform.rotation;
            float damage = (0.2f * npcInstance.npc.getStat("str") + 0.05f * 100 + 0.03f * 100) + weapon.getDamage(); //the two 100's are weapon skills and mastery
            string damageType = weapon.getDamageType();
            WeaponHitInfo info = reach.GetComponentInChildren<WeaponHitInfo>();
            info.damage = damage;
            info.damageType = damageType;
            float speed = ((weapon.getAttackspeed() * 5) - (0.008f * npcInstance.npc.getStat("quick") + 0.002f * 100)); //The 100 is weapon mastery
            npcInstance.instance.addAttackCooldown(speed);
        }

        return "";
    }

    public void takeDamage(float damage, string damageType)
    {
        npcInstance.npc.setHealth(damage, 0, false, damageType);
    }

}
