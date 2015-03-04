using UnityEngine;
using System.Collections;

public class Skill_Passive_Combat_Great_Sword_Mastery : SkillEntity, Skill, Learnable
{
    private static int id = 0;
    private static string type = "passive";
    private static string group = "combat";
    private static string skillName = "Great Sword Mastery";
    private static string skillDescription = "Further improves your combat efficiency with Great Swords";
    private static int price = 0;
    private static float skillLevel = 1f;
    private static float effect = 0f;
    public static Player playerInstance;
    private Npc npcInstance;

    public Skill_Passive_Combat_Great_Sword_Mastery() :
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
        effect = 0.03f * Mathf.Floor(skillLevel);
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
    public void setPlayerInstance(Player player, Npc npc)
    {
        playerInstance = player;
        npcInstance = npc;
    }

    public bool canLearn()
    {
        if (playerInstance.player.getSkill(17).getSkillLevel() == 100)
        {
            return true;
        }
        return false;
    }
}
