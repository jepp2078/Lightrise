using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Skill_Active_General_Revive : SkillEntity, Skill, HotbarAble, Castable  {

	private static int id = 14;
    private static string type = "active";
    private static string group = "general";
    private static string skillName = "Revive";
    private static string skillDescription = "Revives a fallen player";
    private static int price = 0;
    private static float skillLevel = 1f;
    private static float effect = 0f;
    private static int inventoryID = 9999;
    private static int hotbarSlot;
    private static float castingCost = 0;
    private static float duration = 0;
    private static float currentDuration = 0;
    private static bool activated = false;
    private static string castMsg = "You begin reviving";
    private static float gainPrCast = 1.0f;
    private static float cooldown = 20f;
    private static float currentCooldown = 0f;
    private static RawImage icon;
    Texture texture;

    public Skill_Active_General_Revive() :
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
        float oldSkillLevel = getSkillLevel();
        skillLevel += change;
        if (Mathf.Floor(oldSkillLevel) < Mathf.Floor(skillLevel))
        {
            Debug.Log("Skill level in " + getSkillText() + " has increased to " + Mathf.Floor(skillLevel)+"!");
        }
        if (skillLevel >= 100)
        {
            if (Mathf.Floor(oldSkillLevel) < 100)
            {
                Debug.Log(getSkillText() + " is surging!");
            }
            skillLevel = 100;
            return false;
        }
        else if (skillLevel >= 75)
        {
            if (Mathf.Floor(oldSkillLevel) < 75)
            {
                Debug.Log(getSkillText() + " has reached a new level!");
            }
        }
        else if (skillLevel >= 50)
        {
            if (Mathf.Floor(oldSkillLevel) < 50)
            {
                Debug.Log(getSkillText() + " has reached a new level!");
            }
        }
        else if (skillLevel >= 25)
        {
            if (Mathf.Floor(oldSkillLevel) < 25)
            {
                Debug.Log(getSkillText() + " has reached a new level!");
            } 
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
        
    }

    public void stopEffect()
    {
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

    public float getCurrentCooldown()
    {
        return currentCooldown;
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


    public bool setCurrentDuration(float durationChange)
    {
        if (durationChange == duration)
        {
            currentDuration = duration;
            return false;
        }
        else
        {
            currentDuration -= durationChange;
            if (currentDuration <= 0)
            {
                currentDuration = 0;
                stopEffect();
                return true;
            }
            else
            {
                return false;
            }
        }
    }


    public float getGainPrCast()
    {
        return gainPrCast;
    }

    public void updateGainPrCast()
    {
        gainPrCast = 1.1f - (getSkillLevel()/100);
    }
    public RawImage getIcon()
    {
        return icon;
    }
}
