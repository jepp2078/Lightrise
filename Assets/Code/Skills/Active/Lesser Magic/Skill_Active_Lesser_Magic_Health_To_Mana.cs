﻿using UnityEngine;
using System.Collections;

public class Skill_Active_Lesser_Magic_Health_To_Mana : SkillEntity, Skill, HotbarAble, Castable, Spell
{

    private static int id = 22;
    private static string type = "active";
    private static string group = "lesser magic";
    private static string skillName = "Health To Mana";
    private static string skillDescription = "Caster increases his own Mana, at the cost of losing Health.";
    private static int price = 0;
    private static float skillLevel = 1f;
    private static float effect = 3f;
    private static int inventoryID = 9999;
    private static int hotbarSlot;
    private static float manaCost = 0.1f;
    private static float staminaCost = 0.1f;
    private static float healthCost = 7.5f;
    private static float duration = 2f;
    private static float currentDuration = 2f;
    private static bool activated = false;
    private static string castMsg = "Health To Mana";
    private static float gainPrCast = 1.0f;
    private static float cooldown = 7f;
    private static float currentCooldown = 0f;
    private static float castTime = 1.5f;
    Texture texture;
    GameObject particle;
    private Player playerInstance;
    private Npc npcInstance;
    private GuiFunction gui;


    public Skill_Active_Lesser_Magic_Health_To_Mana() :
        base(id, skillName)
    {
        texture = Resources.Load("health_to_mana", typeof(Texture)) as Texture;
        particle = Resources.Load("health2mana", typeof(GameObject)) as GameObject;

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
        if (Mathf.Floor(oldSkillLevel) < Mathf.Floor(skillLevel))
        {
            gui.newTextLine("Skill level in " + getSkillText() + " has increased to " + Mathf.Floor(skillLevel) + "!");
            effect += 0.02f;
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

    public GameObject cast()
    {
        playerInstance.player.setMana(0, effect);
        GameObject go = (GameObject)particle;
        particle.transform.position = playerInstance.playerObject.transform.position - (new Vector3(0, 1, 0));
        particle.transform.rotation = playerInstance.playerObject.transform.rotation;

        playerInstance.func.instanciateObject(go.name, go.transform.position, go.transform.rotation, Vector3.zero, 0f, "", 0, 0f, false);
        PhotonView viewThis = playerInstance.playerObject.GetComponent<PhotonView>();
        viewThis.RPC("instanciateObject", PhotonTargets.Others, go.name, go.transform.position, go.transform.rotation, Vector3.zero, 0f, "", 0, 0f, false);

        return null;
    }

    public void stopEffect()
    {
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
        float oldCurrentDuration = currentDuration;
        currentDuration -= durationChange;
        if (currentDuration < Mathf.Floor(oldCurrentDuration))
        {
            cast();
        }
        if (currentDuration <= 0)
        {
            currentDuration = duration;
            stopEffect();
            return true;
        }
        else
        {
            return false;
        }
    }


    public float getGainPrCast()
    {
        return gainPrCast;
    }

    public void updateGainPrCast()
    {
        gainPrCast = 1.1f - (getSkillLevel() / 100);
    }

    public Texture getIcon()
    {
        return texture;
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

    public float getCastTime()
    {
        return castTime;
    }


    public float getManaCost()
    {
        return manaCost;
    }

    public float getStaminaCost()
    {
        return staminaCost;
    }

    public float getHealthCost()
    {
        return healthCost;
    }
    public string getDamageType()
    {
        return "";
    }
}

