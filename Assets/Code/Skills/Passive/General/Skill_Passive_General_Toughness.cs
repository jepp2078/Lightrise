using UnityEngine;
using System.Collections;

public class Skill_Passive_General_Toughness : SkillEntity, Skill, Learnable
{

	private static int id = 11;
    private static string type = "passive";
    private static string group = "general";
    private static string skillName = "Toughness";
    private static string skillDescription = "Toughness is a skill that increases Health";
    private static int price = 500;
    private static float skillLevel = 1f;
    private static float effect = 0f;

    public Skill_Passive_General_Toughness() :
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
            case 10: if (effect < 1) effect = 1; Player.player.changeStats(0, 0, 0, 0, 0, 0, 1, 0, 0); break;
            case 20: if (effect < 2) effect = 2; Player.player.changeStats(0, 0, 0, 0, 0, 0, 1, 0, 0); break;
            case 30: if (effect < 3) effect = 3; Player.player.changeStats(0, 0, 0, 0, 0, 0, 1, 0, 0); break;
            case 40: if (effect < 4) effect = 4; Player.player.changeStats(0, 0, 0, 0, 0, 0, 1, 0, 0); break;
            case 50: if (effect < 5) effect = 5; Player.player.changeStats(0, 0, 0, 0, 0, 0, 1, 0, 0); break;
            case 60: if (effect < 6) effect = 6; Player.player.changeStats(0, 0, 0, 0, 0, 0, 1, 0, 0); break;
            case 70: if (effect < 7) effect = 7; Player.player.changeStats(0, 0, 0, 0, 0, 0, 1, 0, 0); break;
            case 80: if (effect < 8) effect = 8; Player.player.changeStats(0, 0, 0, 0, 0, 0, 1, 0, 0); break;
            case 90: if (effect < 9) effect = 9; Player.player.changeStats(0, 0, 0, 0, 0, 0, 1, 0, 0); break;
            case 100: if (effect < 10) effect = 10; Player.player.changeStats(0, 0, 0, 0, 0, 0, 1, 0, 0); break;

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
        if (Player.player.getHealthFloat() >= 250)
        {
            return true;
        }
        return false;
    }
}