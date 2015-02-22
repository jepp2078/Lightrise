using UnityEngine;
using System.Collections;

public class Skill_Passive_General_Run : SkillEntity, Skill
{
    private static int id = 0;
    private static string type = "passive";
    private static string group = "general";
    private static string skillName = "Run";
    private static string skillDescription = "Run is a skill that increases your running speed";
    private static int price = 0;
    private static float skillLevel = 1f;
    private static float effect = 0f;

    public Skill_Passive_General_Run() :
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
            effect = 0.125f;
        }
        else if (skillLevel >= 50)
        {
            effect = 0.25f;
        }
        else if (skillLevel >= 75)
        {
            effect = 0.375f;
        }
        else if (skillLevel >= 100)
        {
            effect = 0.50f;
            skillLevel = 100;
            return false;
        }
        else
        {
            effect = 0f;
        }
        return true;
    }
}
