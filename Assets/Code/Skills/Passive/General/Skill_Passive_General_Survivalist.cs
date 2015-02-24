using UnityEngine;
using System.Collections;

public class Skill_Passive_General_Survivalist : SkillEntity, Skill, Learnable
{

	private static int id = 10;
    private static string type = "passive";
    private static string group = "general";
    private static string skillName = "Survivalist";
    private static string skillDescription = "Survivalist is a skill that increases the duration of the nourishment bonus from food";
    private static int price = 200;
    private static float skillLevel = 1f;
    private static float effect = 0f;

    public Skill_Passive_General_Survivalist() :
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
        switch (Mathf.FloorToInt(skillLevel))
        {
            case 10: if (effect < 1.0375) effect = 1.0375f; break;
            case 20: if (effect < 1.075) effect = 1.075f; break;
            case 30: if (effect < 1.1125) effect = 1.1125f; break;
            case 40: if (effect < 1.15) effect = 1.15f; break;
            case 50: if (effect < 1.1875) effect = 1.1875f; break;
            case 60: if (effect < 1.225) effect = 1.225f; break;
            case 70: if (effect < 1.2625) effect = 1.2625f; break;
            case 80: if (effect < 1.3) effect = 1.3f; break;
            case 90: if (effect < 1.3375) effect = 1.3375f; break;
            case 100: if (effect < 1.375) effect = 1.375f; break;

        }
        if (Mathf.Floor(oldSkillLevel) < Mathf.Floor(skillLevel))
        {
            Debug.Log("Skill level in " + getSkillText() + " has increased to " + Mathf.Floor(skillLevel) + "!");
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

    public bool canLearn()
    {
        if (Player.player.getStat("wis") >= 30)
        {
            return true;
        }
        return false;
    }
}