using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    public static PlayerObject player;
    private List<Castable> cooldownList = new List<Castable>();
    public static Player instance;
    public int serverTicks = 0;
    private float guiHelper = 0.5f;
    private float guiHelperNext = 0.0f;
	// Use this for initialization
	void Start () {
        player = new PlayerObject(0, 0, 0);
        instance = this;
        player.changeStats(5, 5, 5, 5, 5);
        //player.refillVitals();
        player.skillGain(23.80f, 0); //See skillID identify pdf for skill id list
        player.hotbarAdd((HotbarAble)player.getInventoryItem(0), 0);
        player.hotbarAdd((HotbarAble)player.getInventoryItem(1), 1);
        Player.player.getSkill(1).cast(); //Testing rest skill
        Player.player.getSkill(1).setCurrentCooldown(Player.player.getSkill(1).getCooldown());
        cooldownList.Add(Player.player.getSkill(1));

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
    }

	void gameLogic () {
        if (Input.GetButton("Sprint"))
        {
            if (player.setStamina(0.125f, 0)) //sprint skill needs to modify this (Stamina cost)s
            {
                RPG_Controller.instance.walkSpeed = 10;
            }
            else
            {
                RPG_Controller.instance.walkSpeed = RPG_Controller.instance.baseWalkSpeed+player.getSkillEffect(0); //See skillID identify pdf for skill id list
            }
        }
        else
        {
            RPG_Controller.instance.walkSpeed = RPG_Controller.instance.baseWalkSpeed + player.getSkillEffect(0); //See skillID identify pdf for skill id list
        }
	}

    void serverTick()
    {
        if(serverTicks%48 == 0){
            player.setHealth(0, (0.66f + player.getRegenModifiers(1))); // Think about skill to modify this?
            player.setMana(0, (0.66f + player.getRegenModifiers(3))); 
        }
        if (serverTicks % 24 == 0)
        {
            player.setStamina(0, (0.66f + player.getRegenModifiers(2))); // Think about skill to modify this?
        }
        gameLogic();
        serverTicks++;
        if (serverTicks > 100000)
        {
            serverTicks = 0;
        }

        Debug.Log(Function.status());
    }

    public void gainSkill(float gain, int skillID)
    {
        player.skillGain(gain, skillID);
        Debug.Log("Gained " + gain + " in skill " + player.getSkillName(skillID));
        Debug.Log("Run: " + player.getSkillLevel(skillID));
    }

    public void updateCooldowns()
    {
        for (int i = 0; i < cooldownList.Count;i++)
        {
            if (cooldownList[i].setCurrentCooldown(0.0825F))
            {
                Debug.Log("Skill came off cooldown");
                cooldownList.RemoveAt(i);
            }
        }
    }
}
