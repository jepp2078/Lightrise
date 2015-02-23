using UnityEngine;
using System.Collections;

public class Skill_Active_General_Rest : SkillEntity, Skill, HotbarAble, Castable  {

	private static int id = 1;
    private static string type = "active-passive";
    private static string group = "general";
    private static string skillName = "Rest";
    private static string skillDescription = "Run is a skill that increases resting regeneration";
    private static int price = 0;
    private static float skillLevel = 1f;
    private static float effect = 0.125f;
    private static int inventoryID = 9999;
    private static int hotbarSlot;
    private static float castingCost = 0;
    private static float duration = 0;
    private static bool activated = false;
    private static string castMsg = "You begin resting";
    private static float cooldown = 5f;
    private static float currentCooldown = 0f;

    public Skill_Active_General_Rest() :
		base(id, skillName)
	{
    }

    public int getSkillID()
    {
        return id;
    }
    public string getSkillText()
    {
        return skillName;
    }

    public string getType()
    {
        return type;
    }

    public string getSkillDescription()
    {
        return skillDescription;
    }

    public string getSkillGroup()
    {
        return group;
    }

    public int getPrice()
    {
        return price;
    }

    public float getSkillLevel()
    {
        return skillLevel;
    }

    public float getEffect()
    {
        return effect;
    }

    public bool setSkillLevel(float change)
    {
        skillLevel += change;
        if (skillLevel >= 25)
        {
            effect = 0.25f;
        }
        else if (skillLevel >= 50)
        {
            effect = 0.375f;
        }
        else if (skillLevel >= 75)
        {
            effect = 0.50f;
        }
        else if (skillLevel >= 100)
        {
            effect = 0.625f;
            skillLevel = 100;
            return false;
        }
        else
        {
            effect = 0.125f;
        }
        return true;
    }

    public void setHotbarSlot(int slot)
    {
        hotbarSlot = slot;
    }

    public int getHotbarSlot()
    {
        return hotbarSlot;
    }

    public int getInventoryID()
    {
        return inventoryID;
    }

    public void cast()
    {
        Player.player.setRegenModifiers(1, effect);
        Player.player.setRegenModifiers(2, effect);
        Player.player.setRegenModifiers(3, effect);
    }

    public void stopEffect()
    {
        Player.player.setRegenModifiers(1, -effect);
        Player.player.setRegenModifiers(2, -effect);
        Player.player.setRegenModifiers(3, -effect);
    }

    public float getCastingCost()
    {
        return castingCost;
    }

    public float getDuration()
    {
        return duration;
    }

    public bool getState()
    {
        return activated;
    }

    public float getCooldown()
    {
        return cooldown;
    }


    public void setState(bool state)
    {
        activated = state;
    }

    public string getCastMsg()
    {
        return castMsg;
    }


    public bool setCurrentCooldown(float cooldownChange)
    {
        if (cooldownChange == cooldown)
        {
            currentCooldown = cooldown;
            return false;
        }
        else
        {
            currentCooldown -= cooldownChange;
            if (currentCooldown <= 0)
            {
                currentCooldown = 0;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
