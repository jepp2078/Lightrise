﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    public static PlayerObject player;
    public static GameObject playerObject;
    private List<Castable> cooldownList = new List<Castable>();
    private List<Castable> spellDurationList = new List<Castable>();
    private float attackCooldown = 0f;
    public static Player instance;
    public int serverTicks = 0;
    private float guiHelper = 0.3333f;
    private float guiHelperNext = 0.0f;

	// Use this for initialization and testing
	void Start () {
        player = new PlayerObject(0, 0, 0);
        playerObject = GameObject.Find("PlayerChar");
        instance = this;
        player.changeStats(5, 5, 5, 5, 5, 5, 0, 0, 0);
        player.refillVitals();
        Function.putOnHotbar((HotbarAble)player.getInventoryItem(0), 0);
        Function.putOnHotbar((HotbarAble)player.getInventoryItem(1), 1);
        Function.putOnHotbar((HotbarAble)player.getSkill(1), 2);
        Function.putOnHotbar((HotbarAble)player.getSkill(14), 3);

        InvokeRepeating("serverTick", 0, 0.0825F); //TEMP value. We might need to change how fast the server ticks? 1/12 of a sec right now.
    }

    void Update()
    {
        if (Input.GetButton("toggleGUI") && Time.time > guiHelperNext)
        {
            guiHelperNext = Time.time + guiHelper;
            bool current = RPG_Camera.instance.getGuiMode();
            if (current)
            {
                RPG_Camera.instance.setGuiMode(false);
            }
            else
            {
                RPG_Camera.instance.setGuiMode(true);
            }
        }
        if (Input.GetButton("action") && Time.time > guiHelperNext)
        {
            guiHelperNext = Time.time + guiHelper;
            if (player.getActiveSkill() != null && !(player.getEquipment(6) is Weapon))
            {
                Debug.Log(Function.performAction());
            }
            else if (player.getEquipment(6) is Weapon && attackCooldown == 0)
            {
                Function.performAttack((Weapon)player.getEquipment(6));
                Debug.Log("attack");
            }

        }
        if (Input.GetButton("Hotbar1") && Time.time > guiHelperNext)
        {
            guiHelperNext = Time.time + guiHelper;
            Function.hotbarUse(0);
        }

        if (Input.GetButton("Hotbar2") && Time.time > guiHelperNext)
        {
            guiHelperNext = Time.time + guiHelper;
            Function.hotbarUse(1);
        }
        if (Input.GetButton("Hotbar3") && Time.time > guiHelperNext)
        {
            guiHelperNext = Time.time + guiHelper;
            Function.hotbarUse(2);
        }
        if (Input.GetButton("Hotbar4") && Time.time > guiHelperNext)
        {
            guiHelperNext = Time.time + guiHelper;
            Function.hotbarUse(3);
        }
    }

	void gameLogic () {
        if (Input.GetButton("Sprint") && !Input.GetButton("Crouch"))
        {
            if (player.setStamina(0.225f-player.getSkillEffect(2), 0))
            {
                Player.instance.gainSkill(0.0833f/60f, 2);
                RPG_Controller.instance.walkSpeed = 10;
            }
        }
        else if (Input.GetButton("Crouch") && RPG_Camera.instance.getGuiMode() == false)
        {
                Player.instance.gainSkill(0.0833f / 60f, 3);
                RPG_Controller.instance.walkSpeed = 4 + player.getSkillEffect(3);
        }
        else
        {
            RPG_Controller.instance.walkSpeed = RPG_Controller.instance.baseWalkSpeed + player.getSkillEffect(0); //See skillID identify pdf for skill id list
        }

        if (serverTicks % 48 == 0)
        {
            player.setHealth(0, (0.66f + player.getRegenModifiers(1)), true); // Think about skill to modify this?
            player.setMana(0, (0.66f + player.getRegenModifiers(3)));
        }
        if (serverTicks % 24 == 0)
        {
            player.setStamina(0, (0.66f + player.getRegenModifiers(2))); // Think about skill to modify this?
            Debug.Log(Function.status());
        }
	}

    void serverTick()
    {
        gameLogic();
        updateCooldowns();
        updateSpellDurations();
        updateAttackCooldown();
        serverTicks++;
        if (serverTicks > 100000)
        {
            serverTicks = 0;
        }

        
    }

    public void gainSkill(float gain, int skillID)
    {
        player.skillGain(gain, skillID);
    }

    public void updateCooldowns()
    {
        for (int i = 0; i < cooldownList.Count;i++)
        {
            if (cooldownList[i].setCurrentCooldown(0.0825F))
            {
                cooldownList.RemoveAt(i);
            }
        }
    }

    public void updateSpellDurations()
    {
        for (int i = 0; i < spellDurationList.Count; i++)
        {
            if (spellDurationList[i].setCurrentDuration(0.0825F))
            {
                spellDurationList.RemoveAt(i);
            }
        }
    }
    public void updateAttackCooldown()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= 0.0825F;
        }
        if (attackCooldown < 0)
        {
            attackCooldown = 0f;
        }
    }

    public void addCooldown(Castable skill)
    {
        cooldownList.Add(skill);
    }
    public void addAttackCooldown(float cooldown)
    {
        attackCooldown += cooldown;
    }
}
