using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    public PlayerObject player;
    public GameObject playerObject;
    public Function func;
    public GuiFunction gui;
    private List<Castable> cooldownList = new List<Castable>();
    private List<Castable> spellDurationList = new List<Castable>();
    private float attackCooldown = 0f;
    private float castTime = 0f, totalCastTime = 0f;
    private bool casting;
    private Skill currentlyCasting;
    public bool doneCasting;
    public int serverTicks = 0;
    private float guiHelper = 0.05f;
    private float guiHelperNext = 0.0f;
    public AudioSource[] audioMisc = new AudioSource[1];
    public AudioSource[] audioMagicCharge = new AudioSource[1];
    public AudioSource[] audioArrow = new AudioSource[3];
    public AudioSource[] audioAmbience = new AudioSource[1];
    public RPG_Controller rpgController;
    public RPG_Camera rpgCamera;
    public networkPlayer networkPlayer;
    Camera tempCam;
    public PhotonView view;
    public float currentlyLooting;


	// Use this for initialization and testing
	void Start () {
        tempCam = playerObject.GetComponentInChildren<Camera>();
        player.changeStats(20, 20, 20, 20, 20, 20, 0, 0, 0);
        player.refillVitals();
        gui.init();
        gui.newTextLine("Welcome to lightrise!");
        player.setName("Bubbles");
        //audioAmbience[0].Play();
        doneCasting = false;
        casting = false;
        rpgCamera.setGuiMode(true);
        view.RPC("setName", PhotonTargets.AllBuffered, player.getName());
        view.RPC("setHealth", PhotonTargets.AllBuffered, 1f);
        func.putOnHotbar((HotbarAble)player.getInventoryItem(0), 0);
        func.putOnHotbar((HotbarAble)player.getInventoryItem(1), 1);
        func.putOnHotbar((HotbarAble)player.getInventoryItem(2), 2);
        func.putOnHotbar((HotbarAble)player.getInventoryItem(3), 3);
        func.putOnHotbar((HotbarAble)player.getSkill(24), 4);
        InvokeRepeating("serverTick", 0, 0.0825F); //TEMP value. We might need to change how fast the server ticks? 1/12 of a sec right now.
    }

    void Update()
    {
        if (Input.GetButton("toggleGUI") && Time.time > guiHelperNext)
        {
            guiHelperNext = Time.time + 0.3333f;
            bool current = rpgCamera.getGuiMode();
            if (current)
            {
                rpgCamera.setGuiMode(false);
            }
            else
            {
                rpgCamera.setGuiMode(true);
            }
        }

        if (Input.GetButton("bag") && Time.time > guiHelperNext)
        {
            guiHelperNext = Time.time + 0.3333f;
            if (gui.isInventoryShowing())
            {
                gui.hideInventory();
            }
            else
            {
                gui.showInventory();
                rpgCamera.setGuiMode(true);
            }
        }

        if (Input.GetButton("action") && Time.time > guiHelperNext && rpgCamera.getGuiMode() == false)
        {
            guiHelperNext = Time.time + 0.3333f;
            if (player.getEquipment(6) is Weapon && player.getEquipment(6) is MeleeWeapon && attackCooldown == 0 && !casting )
            {
                func.performAttack((Weapon)player.getEquipment(6));
                player.setStamina(4f, 0);
            }
            else if (player.getEquipment(6) != null && player.getEquipment(6).getType() == "bow")
            {
                if (!casting)
                {
                    if ((player.getActiveSkill()) == null || player.getActiveSkill().getCurrentCooldown() == 0)
                    {
                        if (!doneCasting)
                        {
                            castTime = func.getCastTime("ranged");
                            addCastTime(castTime);
                        }
                        //audioArrow[0].Play();
                        //audioArrow[1].PlayDelayed(1.247f);
                    }
                    else
                    {
                        gui.newTextLine("Skill " + ((Skill)player.getActiveSkill()).getSkillText() + " is on cooldown!");
                    }
                }
                else
                {
                    if (!(Input.GetButton("action")))
                        gui.newTextLine("Action already in progress!");
                }
            }
            else if (player.getEquipment(6) != null && player.getEquipment(6).getType() == "pickaxe")
            {
                if (!casting)
                {
                    if ((player.getActiveSkill()) == null || player.getActiveSkill().getCurrentCooldown() == 0)
                    {
                        if (!doneCasting)
                        {
                            castTime = 5 - player.getSkillEffect(25);
                            addCastTime(castTime);
                            //audioArrow[0].Play();
                            //audioArrow[1].PlayDelayed(1.247f);
                        }
                    }
                    else
                    {
                            gui.newTextLine("Skill " + ((Skill)player.getActiveSkill()).getSkillText() + " is on cooldown!");
                    }
                }
                else
                {
                    if (!(Input.GetButton("action")))
                        gui.newTextLine("Action already in progress!");
                }
            }
            else if (player.getActiveSkill() != null && ((Skill)player.getActiveSkill()).getSkillGroup() == "general" || player.getEquipment(6) != null && (player.getEquipment(6).getType() == "staff"))
            {
                if (!casting)
                {
                    if (player.getActiveSkill().getCurrentCooldown() == 0)
                    {
                        if (player.getActiveSkill() is Spell)
                        {
                            if (!doneCasting)
                            {
                                castTime = func.getCastTime("spell");
                                addCastTime(castTime);
                                currentlyCasting = (Skill)player.getActiveSkill();
                                //audioMagicCharge[0].Play();
                            }
                        }
                        else
                        {
                            gui.newTextLine(func.performAction((Skill)player.getActiveSkill()));
                        }
                    }
                    else
                    {
                        gui.newTextLine("Skill " + ((Skill)player.getActiveSkill()).getSkillText() + " is on cooldown!");
                    }
                }
                else
                {
                    if (!(Input.GetButton("action")))
                        gui.newTextLine("Action already in progress!");
                }
            }

        }
        if (doneCasting && !(Input.GetButton("action")))
        {
            if (player.getEquipment(6) != null && player.getEquipment(6).getType() == "bow")
            {
                func.performAttack((Weapon)player.getEquipment(6));
                player.setStamina(4f, 0);
                doneCasting = false;
                //audioArrow[1].Stop();
                //audioArrow[2].Play();
            }
            else if (player.getEquipment(6) != null && player.getEquipment(6).getType() == "staff")
            {
                gui.newTextLine(func.performAction(currentlyCasting));
                currentlyCasting = null;
                doneCasting = false;
                //audioMagicCharge[0].Stop();
            }
            else if (player.getEquipment(6) != null && player.getEquipment(6).getType() == "pickaxe")
            {
                func.harvest();
                doneCasting = false;
                gainSkill(0.333f, 25);
                player.setStamina(7f, 0);
                //audioMagicCharge[0].Stop();
            }
            else if (((Skill)player.getActiveSkill()).getSkillGroup() == "general")
            {
                gui.newTextLine(func.performAction(currentlyCasting));
                currentlyCasting = null;
                doneCasting = false;
            }
        }
        if (Input.GetButton("sheath") && Time.time > guiHelperNext && rpgCamera.getGuiMode() == false && doneCasting == false)
        {
            guiHelperNext = Time.time + 0.3333f;
            func.sheathWeapon();
            stopCasting();
        }
        if (Input.GetButton("Hotbar1") && Time.time > guiHelperNext && rpgCamera.getGuiMode() == false && !(Input.GetButton("action")))
        {
            guiHelperNext = Time.time + guiHelper;
            func.hotbarUse(0);
        }
        if (Input.GetButton("Hotbar2") && Time.time > guiHelperNext && rpgCamera.getGuiMode() == false && !(Input.GetButton("action")))
        {
            guiHelperNext = Time.time + guiHelper;
            func.hotbarUse(1);
        }
        if (Input.GetButton("Hotbar3") && Time.time > guiHelperNext && rpgCamera.getGuiMode() == false && !(Input.GetButton("action")))
        {
            guiHelperNext = Time.time + guiHelper;
            func.hotbarUse(2);
        }
        if (Input.GetButton("Hotbar4") && Time.time > guiHelperNext && rpgCamera.getGuiMode() == false && !(Input.GetButton("action")))
        {
            guiHelperNext = Time.time + guiHelper;
            func.hotbarUse(3);
        }
        if (Input.GetButton("Hotbar5") && Time.time > guiHelperNext && rpgCamera.getGuiMode() == false && !(Input.GetButton("action")))
        {
            guiHelperNext = Time.time + guiHelper;
            func.hotbarUse(4);
        }
        if (Input.GetButton("Hotbar6") && Time.time > guiHelperNext && rpgCamera.getGuiMode() == false && !(Input.GetButton("action")))
        {
            guiHelperNext = Time.time + guiHelper;
            func.hotbarUse(5);
        }
        if (Input.GetButton("Hotbar7") && Time.time > guiHelperNext && rpgCamera.getGuiMode() == false && !(Input.GetButton("action")))
        {
            guiHelperNext = Time.time + guiHelper;
            func.hotbarUse(6);
        }
        if (Input.GetButton("Hotbar8") && Time.time > guiHelperNext && rpgCamera.getGuiMode() == false && !(Input.GetButton("action")))
        {
            guiHelperNext = Time.time + guiHelper;
            func.hotbarUse(7);
        }
        if (Input.GetButton("Hotbar9") && Time.time > guiHelperNext && rpgCamera.getGuiMode() == false && !(Input.GetButton("action")))
        {
            guiHelperNext = Time.time + guiHelper;
            func.hotbarUse(8);
        }
        if (Input.GetButton("Hotbar0") && Time.time > guiHelperNext && rpgCamera.getGuiMode() == false && !(Input.GetButton("action")))
        {
            guiHelperNext = Time.time + guiHelper;
            func.hotbarUse(9);
        }

        if (Input.GetButton("clearActiveSkill") && Time.time > guiHelperNext && rpgCamera.getGuiMode() == false && !(Input.GetButton("action")))
        {
            guiHelperNext = Time.time + guiHelper;
            player.setActiveSkill(null);
            gui.setActiveSkillIcon(null, true);
        }

        if (Input.GetButton("debug") && Time.time > guiHelperNext && rpgCamera.getGuiMode() == false && !(Input.GetButton("action")))
        {
            guiHelperNext = Time.time + 0.66666f;
            func.takeDamage(210, "mental", 999f, view.viewID);
        }

        if (Input.GetButton("use") && Time.time > guiHelperNext && rpgCamera.getGuiMode() == false && !(Input.GetButton("action")))
        {
            guiHelperNext = Time.time + 0.66666f;

            int raylenght;
            if (rpgCamera.distance > 2.5)
                raylenght = 8;
            else
                raylenght = 3;

            RaycastHit hit;

            Ray ray = new Ray(tempCam.transform.position, tempCam.transform.forward);


            if (Physics.Raycast(ray, out hit, raylenght))
            {
                if (hit.transform.tag == "Interactable")
                {
                    if (hit.transform.name == "Spawn Stone")
                    {
                        if (player.setSpawnPoint(hit.transform.GetComponentInParent<SpawnPoint>()))
                        {
                            gui.newTextLine("You have bound to the Spawn Stone of " + hit.transform.GetComponentInParent<SpawnPoint>().getSpawnStoneName());
                        }
                        else
                        {
                            gui.newTextLine("You're already bound to the Spawn Stone of " + hit.transform.GetComponentInParent<SpawnPoint>().getSpawnStoneName());
                        }
                    }
                    else if (hit.transform.name == "Grave(Clone)")
                    {
                        if (hit.transform.GetChild(0).name == "PlayerTomb")
                        {
                            List<Item> grave = hit.transform.GetChild(0).gameObject.GetComponent<playerGrave>().getItems();
                            currentlyLooting = hit.transform.GetChild(0).gameObject.GetComponent<playerGrave>().getGraveId();
                            gui.clearPlayerLoot();
                            for (int i = 0; i < grave.Count; i++)
                            {
                                if (grave[i] != null)
                                {
                                    gui.setLootIcon(grave[i].getInventorySlot(), grave[i].getIcon(), false, grave[i]);
                                    gui.setLootStackCount(i, grave[i], false);
                                }
                            }
                            gui.showPlayerLoot();
                            gui.showInventory();
                            rpgCamera.setGuiMode(true);
                        }
                    }
                }
            }
            
        }

        if (Input.GetButton("clearGui") && Time.time > guiHelperNext && !(Input.GetButton("action")))
        {
            guiHelperNext = Time.time + 0.3333f;
            if (rpgCamera.getGuiMode() == false)
            {
                stopCasting();
            }
            else
            {
                gui.clearGui();
                rpgCamera.setGuiMode(false);
            }
        }

    }

	void gameLogic () {
        if (Input.GetButton("Sprint") && !Input.GetButton("Crouch") && rpgCamera.getGuiMode() == false)
        {
            if (player.setStamina(0.125f - player.getSkillEffect(2), 0) && rpgCamera.getGuiMode() == false)
            {
                gainSkill(0.0833f/60f, 2);
                rpgController.walkSpeedModifier = 3;
                player.changeStats(0.005f, 0f, 0f, 0f, 0f, 0.005f, 0f, 0f, 0f);
            }
        }
        else if (Input.GetButton("Crouch") && rpgCamera.getGuiMode() == false)
        {
                gainSkill(0.0833f / 60f, 3);
                rpgController.walkSpeedModifier = 0;
                player.changeStats(0.005f, 0.005f, 0f, 0f, 0f, 0f, 0f, 0f, 0f);
        }
        else
        {
            rpgController.walkSpeedModifier = player.getSkillEffect(0) +0.000000001f;
        }

        if (serverTicks % 24 == 0)
        {
            player.setHealth(0, (0.33f + player.getRegenModifiers(1)), true,"self"); // Think about skill to modify this?
            player.setMana(0, (0.33f + player.getRegenModifiers(3)));
            view.RPC("setHealth", PhotonTargets.AllBuffered, player.getHealthForTarget());
        }
        if (serverTicks % 12 == 0)
        {
            player.setStamina(0, (0.33f + player.getRegenModifiers(2))); // Think about skill to modify this?
        }
        if (serverTicks % 6 == 0)
        {
            checkForTarget();
        }

	}

    void serverTick()
    {
        gameLogic();
        updateCooldowns();
        updateSpellDurations();
        updateAttackCooldown();
        if (casting)
        {
            updateCastTime();
        }
        serverTicks++;
        if (serverTicks > 100000)
        {
            serverTicks = 0;
        }
    }

    public void gainSkill(float gain, int skillID)
    {
        if (player.skillGain(gain, skillID))
        {
            //audioMisc[0].Play();
        }
    }

    public void updateCooldowns()
    {
        for (int i = 0; i < cooldownList.Count;i++)
        {
            gui.setSkillCooldown(((Castable)cooldownList[i]).getCurrentCooldown(), ((Castable)cooldownList[i]).getCooldown(), ((HotbarAble)cooldownList[i]).getHotbarSlot());
            if (cooldownList[i].setCurrentCooldown(0.0825F))
            {
                gui.setSkillCooldown(0, ((Castable)cooldownList[i]).getCooldown(), ((HotbarAble)cooldownList[i]).getHotbarSlot());
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
        else if (attackCooldown <= 0)
        {
            attackCooldown = 0f;
        }
    }

    public void updateCastTime()
    {
        if (castTime > 0)
        {
            castTime -= 0.0825F;
            if (castTime <= 0)
            {
                gui.setCastTime(0, totalCastTime);
            }
            else
            {
                gui.setCastTime(castTime, totalCastTime);
            }
        }
        else if (castTime <= 0)
        {
            castTime = 0f;
            casting = false;
            gui.setCastTime(0,0);
            doneCasting = true;
        }
    }

    public void addCooldown(Castable skill)
    {
        cooldownList.Add(skill);
    }

    public void addSpellDuration(Castable skill)
    {
        spellDurationList.Add(skill);
    }

    public void addAttackCooldown(float cooldown)
    {
        attackCooldown += cooldown;
    }

    public void addCastTime(float time)
    {
        if (time != 0)
        {
            totalCastTime = time;
            castTime = time;
            casting = true;
            gui.setCastTime(time, time);
        }
        else
        {
            doneCasting = true;
            casting = false;
        }
    }

    public void stopCasting()
    {
        currentlyCasting = null;
        casting = false;
        doneCasting = false;
        gui.hideCastBar();
    }

    public void checkForTarget()
    {
        int raylenght = 50;
        RaycastHit hit;

        Ray ray = new Ray(tempCam.transform.position, tempCam.transform.forward);


        if (Physics.Raycast(ray, out hit, raylenght))
        {
            if (hit.transform && hit.transform.tag == "PlayerCapsule" && view.viewID != hit.transform.GetComponentInParent<PhotonView>().viewID)
            {
                gui.setTarget(hit.transform.GetComponentInParent<networkPlayer>().networkPlayerName, hit.transform.gameObject.GetComponentInParent<networkPlayer>().networkPlayerHealth);
            }
            else if (hit.rigidbody && hit.rigidbody.tag == "resource")
            {
                gui.setTarget(hit.transform.GetComponent<ResourceSource>().nodeName, 0);
            }
            else
            {
                gui.removeTarget();
            }
        }
        else
        {
            gui.removeTarget();
        }
    }

    public void stopActionsOnMovement()
    {
        gui.hidePlayerLoot();
        currentlyLooting = 0f;
    }
}
