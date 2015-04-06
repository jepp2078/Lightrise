using UnityEngine;
using System.Collections;

public class Skill_Passive_General_Perseverance : SkillEntity, Skill, Learnable
{

	private static int id = 7;
    private static string type = "passive";
    private static string group = "general";
    private static string skillName = "Perseverance";
    private static string skillDescription = "Perseverance is a skill that increases Stamina";
    private static int price = 500;
    private static float skillLevel = 1f;
    private static float effect = 0f;
    public static Player playerInstance;
    private Npc npcInstance;
    private GuiFunction gui;


    public Skill_Passive_General_Perseverance() :
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
        bool skillUp = false;
        switch (Mathf.FloorToInt(skillLevel))
        {
            case 10: if (effect < 1) effect = 1; playerInstance.player.changeStats(0, 0, 0, 0, 0, 0, 0, 1, 0); break;
            case 20: if (effect < 2) effect = 2; playerInstance.player.changeStats(0, 0, 0, 0, 0, 0, 0, 1, 0); break;
            case 30: if (effect < 3) effect = 3; playerInstance.player.changeStats(0, 0, 0, 0, 0, 0, 0, 1, 0); break;
            case 40: if (effect < 4) effect = 4; playerInstance.player.changeStats(0, 0, 0, 0, 0, 0, 0, 1, 0); break;
            case 50: if (effect < 5) effect = 5; playerInstance.player.changeStats(0, 0, 0, 0, 0, 0, 0, 1, 0); break;
            case 60: if (effect < 6) effect = 6; playerInstance.player.changeStats(0, 0, 0, 0, 0, 0, 0, 1, 0); break;
            case 70: if (effect < 7) effect = 7; playerInstance.player.changeStats(0, 0, 0, 0, 0, 0, 0, 1, 0); break;
            case 80: if (effect < 8) effect = 8; playerInstance.player.changeStats(0, 0, 0, 0, 0, 0, 0, 1, 0); break;
            case 90: if (effect < 9) effect = 9; playerInstance.player.changeStats(0, 0, 0, 0, 0, 0, 0, 1, 0); break;
            case 100: if (effect < 10) effect = 10; playerInstance.player.changeStats(0, 0, 0, 0, 0, 0, 0, 1, 0); break;

        }
        if (Mathf.Floor(oldSkillLevel) < Mathf.Floor(skillLevel))
        {
            gui.newTextLine("Skill level in " + getSkillText() + " has increased to " + Mathf.Floor(skillLevel) + "!");
            skillUp = true;
        }
        if (skillLevel >= 100)
        {
            if (Mathf.Floor(oldSkillLevel) < 100)
            {
                gui.newTextLine(getSkillText() + " is surging!");
            }
            skillLevel = 100;
        }
        else if (skillLevel >= 75)
        {
            if (Mathf.Floor(oldSkillLevel) < 75)
            {
                gui.newTextLine(getSkillText() + " has reached a new level!");
            }
        }
        else if (skillLevel >= 50)
        {
            if (Mathf.Floor(oldSkillLevel) < 50)
            {
                gui.newTextLine(getSkillText() + " has reached a new level!");
            }
        }
        else if (skillLevel >= 25)
        {
            if (Mathf.Floor(oldSkillLevel) < 25)
            {
                gui.newTextLine(getSkillText() + " has reached a new level!");
            }
        }
        return skillUp;
    }

    public bool canLearn()
    {
        if (playerInstance.player.getStaminaFloat() >= 300)
        {
            return true;
        }
        return false;
    }
    public void setPlayerInstance(Player player, Npc npc)
    {
        playerInstance = player;
        npcInstance = npc;
    }

    public void setGuiInstance(GuiFunction guiIn, bool player)
    {
        if (player)
            gui = guiIn;
    }
}