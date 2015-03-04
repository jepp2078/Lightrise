using UnityEngine;
using System.Collections;

public class Skill_Passive_Weapon_Skill_Archery : SkillEntity, Skill
{
    private static int id = 0;
    private static string type = "passive";
    private static string group = "weapon skill";
    private static string skillName = "archery";
    private static string skillDescription = "Improves your combat efficiency with Bows";
    private static int price = 0;
    private static float skillLevel = 1f;
    private static float effect = 0f;
    public static Player playerInstance;
    private Npc npcInstance;

    public Skill_Passive_Weapon_Skill_Archery() :
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
        effect = 0.05f * Mathf.Floor(skillLevel);
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
}
