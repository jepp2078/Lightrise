using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Function : MonoBehaviour {
    public Player playerInstance;
    public GuiFunction gui;
    public AudioSource[] audioSwing = new AudioSource[4];
    public AudioSource[] audioDamage = new AudioSource[11];
    public AudioSource[] audioMagic = new AudioSource[20];
    private float objectID = 0.1f;
    private float hitDetection = 0;
    private float lastMsg = 0;


    void Start()
    {

    }

    public string equipItem(Item item)
    {
        return playerInstance.player.equip(item.getInventoryID());
    }

    public string dequipItem(int equipSlot)
    {
        return playerInstance.player.dequip(equipSlot);
    }

    public string status(){
        string tempStats = playerInstance.player.getStatus();
        return tempStats;
	}

    public void putOnHotbar(HotbarAble instance, int hotbarSlot)
    {
        if (instance is HotbarAble)
        {
            playerInstance.player.hotbarAdd(instance, hotbarSlot);
            instance.setHotbarSlot(hotbarSlot);
            if(instance is Item)
                gui.setHotbarIcon(hotbarSlot, instance.getIcon(), false, ((Item)instance),null);
            else
                gui.setHotbarIcon(hotbarSlot, instance.getIcon(), false, null, ((Skill)instance));
        }
    }

    public void removeFromHotbar(int hotbarSlot)
    {
        playerInstance.player.hotbarRemove(hotbarSlot);
    }

    public void sheathWeapon()
    {
        if (playerInstance.player.isWeaponSheathed())
        {
            playerInstance.player.unSheathWeapon();
            gui.setActiveWeaponIcon(null, false, true);
        }
        else
        {
            playerInstance.player.sheathWeapon();
            gui.setActiveWeaponIcon(null, true, true);
        }
        playerInstance.player.setActiveSkill(null);
        gui.setActiveSkillIcon(null, true);
    }

    public void hotbarUse(int hotbarSlot)
    {
        HotbarAble hotbarType = playerInstance.player.getHotbarType(hotbarSlot);
        if (hotbarType is Weapon)
        {
            if (playerInstance.player.getEquipmentIDinSlot(6) == -1 || hotbarType.getInventoryID() != playerInstance.player.getEquipmentIDinSlot(6))
            {
                gui.newTextLine(equipItem(playerInstance.player.getInventoryItem(hotbarType.getInventoryID())));
                gui.setActiveWeaponIcon(hotbarType.getIcon(), playerInstance.player.isWeaponSheathed(), false);
			}
        }
        else if (hotbarType is Castable)
        {
            playerInstance.player.setActiveSkill((Castable)hotbarType);
            gui.setActiveSkillIcon(hotbarType.getIcon(), false);
        }
        else
        {
            playerInstance.player.setActiveSkill(null);
            gui.setActiveSkillIcon(null, true);
        }
    }

    public void harvest()
    {
        int raylenght = 10;
        Camera tempCam = playerInstance.playerObject.GetComponentInChildren<Camera>();
        RaycastHit hit;
        
        Ray ray = new Ray (tempCam.transform.position, tempCam.transform.forward);


        if (Physics.Raycast(ray, out hit, raylenght))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green, 10, true);
            
            if (hit.rigidbody && hit.rigidbody.tag == "resource" )
            {
                playerInstance.player.inventoryAdd(hit.rigidbody.transform.GetComponentInParent<ResourceSource>().harvest);
                gui.newTextLine("You manage to extract some resources!");
            }
        }

    }

    public string performAction(Skill skillIn)
    {
        Castable skill = (Castable)skillIn;
        if(playerInstance.player.setMana(skill.getManaCost(), 0)){
            if (playerInstance.player.setStamina(skill.getStaminaCost(), 0))
            {
                if (!playerInstance.player.setHealth(skill.getHealthCost(), 0,true,""))
                {
                    if (skill.getDuration() != 0)
                    {
                        playerInstance.addSpellDuration(skill);
                    }
                    else if (skillIn.getSkillGroup() == "general")
                    {
                        skill.cast();
                    }
                    else
                    {
                        GameObject tempProjectile = skill.cast();
                        float tempEffect = ((Skill)skill).getEffect() + ((0.2f * playerInstance.player.getStat("int")) * ((Weapon)playerInstance.player.getEquipment(6)).getDamage() * 5);
                        Vector3 pos;
                        pos = playerInstance.playerObject.transform.position;
                        Vector3 tempOffset = playerInstance.playerObject.transform.forward;
                        pos += tempOffset;
                        pos += new Vector3(0f, 0.45f, 0f);
                        Quaternion rot = playerInstance.playerObject.transform.rotation;
                        tempProjectile.transform.position = pos;
                        tempProjectile.transform.rotation = rot;
                        PhotonView viewThis = this.gameObject.GetComponent<PhotonView>();
                        Vector3 forward = playerInstance.playerObject.GetComponentInChildren<Camera>().transform.forward;
                        Vector3 force = forward * 1500f;

                        instanciateObject(tempProjectile.name, tempProjectile.transform.position, tempProjectile.transform.rotation, force, tempEffect, ((Spell)skill).getDamageType(), viewThis.viewID, objectID, true);
                        viewThis.RPC("instanciateObject", PhotonTargets.All, tempProjectile.name, tempProjectile.transform.position, tempProjectile.transform.rotation, force, tempEffect, ((Spell)skill).getDamageType(), viewThis.viewID, objectID, false);
                        viewThis.RPC("increaseObjectID", PhotonTargets.All);
                    }
                    skill.setCurrentCooldown(skill.getCooldown());
                    playerInstance.addCooldown(skill);
                    skill.updateGainPrCast();
                    playerInstance.gainSkill(skill.getGainPrCast(), ((Skill)skill).getSkillID());
                    //if (audioMagic[((Skill)skill).getSkillID() - 1] != null)
                    //    audioMagic[((Skill)skill).getSkillID() - 1].Play();
                    return "You cast " + skill.getCastMsg();
                }
                else
                {
                    return "Not enough Health to cast " + skill.getCastMsg();
                }
            }
            else
            {
                return "Not enough stamina to cast " + skill.getCastMsg();
            }
        }else{
            return "Not enough mana to cast "+skill.getCastMsg();
        }
    }

    public void performAttack(Weapon weapon)
    {
        if (weapon is MeleeWeapon) //damage formula weapon [ (0.2 * MS + 0.05 * WS + 0.03 * WM) + >((WD*10) - (AR*2)) ]
        {
            GameObject hitbox = (GameObject)((MeleeWeapon)weapon).getWeaponHitbox();
            hitbox.transform.position = playerInstance.playerObject.transform.position;
            Vector3 tempOffset = playerInstance.playerObject.transform.forward;
            tempOffset.Scale(new Vector3(1f, 0f, 1f));
            hitbox.transform.position += tempOffset;
            hitbox.transform.rotation = playerInstance.playerObject.transform.rotation;
            float damage = (0.2f * playerInstance.player.getStat("str") + playerInstance.player.getWeaponSkillEffect(weapon.getType(), null) + playerInstance.player.getWeaponSkillEffect(null,weapon.getType())) * weapon.getDamage()*10;
            string damageType = weapon.getDamageType();
            PhotonView viewThis = this.gameObject.GetComponent<PhotonView>();

            instanciateObject(hitbox.name, hitbox.transform.position, hitbox.transform.rotation, Vector3.zero, damage, damageType, viewThis.viewID, objectID, true);
            viewThis.RPC("instanciateObject", PhotonTargets.Others, hitbox.name, hitbox.transform.position, hitbox.transform.rotation, Vector3.zero, damage, damageType, viewThis.viewID, objectID, false);
            viewThis.RPC("increaseObjectID", PhotonTargets.All);

            switch (damageType)
            {
                //case "slashing": audioSwing[Random.Range(0, 4)].Play(); break;
            }
            float speed = ((weapon.getAttackspeed() * 5) - (0.008f * playerInstance.player.getStat("quick") + 0.003f * playerInstance.player.getWeaponSkill(null, weapon.getType())));
            playerInstance.addAttackCooldown(speed);
            playerInstance.gainSkill((1.1f - (playerInstance.player.getSkillLevel(playerInstance.player.getWeaponSkillId(weapon.getType()) / 100))), playerInstance.player.getWeaponSkillId(weapon.getType()));

            if (playerInstance.player.getWeaponSkill(null, weapon.getType()) != 0)
            {
                playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(playerInstance.player.getWeaponSkillId(weapon.getType()) / 100))), playerInstance.player.getWeaponMasterySkillId(weapon.getType()));
            }
         }
        else if (weapon is RangedWeapon) //damage formula weapon [ (0.2 * MS + 0.05 * WS + 0.03 * WM) + >((WD*10) - (AR*2)) ]
        {
            GameObject hitbox = (GameObject)((RangedWeapon)weapon).getProjectile();
            hitbox.transform.position = playerInstance.playerObject.transform.position;
            Vector3 tempOffset = playerInstance.playerObject.transform.forward;
            hitbox.transform.position += tempOffset;
            hitbox.transform.position += new Vector3(0f, 0.60f, 0f);
            hitbox.transform.rotation = playerInstance.playerObject.transform.rotation;
            float damage = (0.2f * playerInstance.player.getStat("dex") + playerInstance.player.getWeaponSkillEffect(weapon.getType(), null) + playerInstance.player.getWeaponSkillEffect(null, weapon.getType())) * weapon.getDamage() * 10;
            string damageType = weapon.getDamageType();
            PhotonView viewThis = this.gameObject.GetComponent<PhotonView>();
            Vector3 forward = playerInstance.playerObject.GetComponentInChildren<Camera>().transform.forward;
            Vector3 force = forward * 3000f;

            instanciateObject(hitbox.name, hitbox.transform.position, hitbox.transform.rotation, force, damage, damageType, viewThis.viewID, objectID, true);
            viewThis.RPC("instanciateObject", PhotonTargets.Others, hitbox.name, hitbox.transform.position, hitbox.transform.rotation, force, damage, damageType, viewThis.viewID, objectID, false);
            viewThis.RPC("increaseObjectID", PhotonTargets.All);

            playerInstance.gainSkill((1.1f - (playerInstance.player.getSkillLevel(playerInstance.player.getWeaponSkillId(weapon.getType()) / 100))), playerInstance.player.getWeaponSkillId(weapon.getType()));

            if (playerInstance.player.getWeaponSkill(null, weapon.getType()) != 0)
            {
                playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(playerInstance.player.getWeaponSkillId(weapon.getType()) / 100))), playerInstance.player.getWeaponMasterySkillId(weapon.getType()));
            }
        }

    }

    [RPC]
    public void takeDamage(float damage, string damageType, float hitID, int viewID)
    {
        bool hasHit = false;
        if (hitDetection == hitID)
        {
            hasHit = true;
        }
        if (!hasHit)
        {
            if(viewID != null)
                PhotonView.Find(viewID).RPC("writeToGui", PhotonTargets.All, "You hit for <color=maroon>" + damage + "</color> " + damageType + " damage!", hitID);
            playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(9) / 100)), 9);
            switch (damageType)
            {
                case "mental": playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(12) / 100)), 12); break;
                case "infliction": playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(6) / 100)), 6); break;
                case "arrow": playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(5) / 100)), 5); break;
                case "piercing": playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(5) / 100)), 5); break;
                case "slashing": playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(5) / 100)), 5); break;
                case "bludgeoning": playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(5) / 100)), 5); break;
                case "acid": playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(4) / 100)), 4); break;
                case "unholy": playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(4) / 100)), 4); break;
                case "fire": playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(8) / 100)), 8); break;
                case "cold": playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(8) / 100)), 8); break;
                case "impact": playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(8) / 100)), 8); break;
                case "lightning": playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(8) / 100)), 8); break;
                case "arcane": break;
            }
            hitDetection = hitID;
            playerInstance.player.setHealth(damage, 0, false, damageType);
        }
        //audioDamage[Random.Range(4, 6)].Play();objectID
    }

    [RPC]
    public void instanciateObject(string objectIn, Vector3 pos, Quaternion rot, Vector3 force, float damage, string damagetype, int view, float objectID, bool collide)
    {
        GameObject go =(GameObject) Instantiate(Resources.Load(objectIn), pos, rot);
        if (go.GetComponent<WeaponHitInfo>() != null)
        {
           WeaponHitInfo hit;
           hit = go.GetComponent<WeaponHitInfo>();
           hit.damage = damage;
           hit.damageType = damagetype;
           hit.viewID = view;
           hit.hitID = objectID;
           hit.force = force;
           hit.collider = collide;
        }
        else if (go.GetComponentInChildren<WeaponHitInfo>() != null)
        {
            WeaponHitInfo hit;
            hit = go.GetComponentInChildren<WeaponHitInfo>();
            hit.damage = damage;
            hit.damageType = damagetype;
            hit.viewID = view;
            hit.hitID = objectID;
            hit.force = force;
            hit.collider = collide;
        }
        if(force != Vector3.zero)
            go.GetComponent<Rigidbody>().AddForce(force);
    }

    [RPC]
    public void writeToGui(string msg, float id)
    {
        bool hasWritten = false;

        if (lastMsg == id)
        {
            hasWritten = true;
        }
        if (!hasWritten)
        {
            gui.newTextLine(msg);
            lastMsg = id;
        }
    }
    public float getCastTime(string type)
    {
        float speed;
        Weapon weapon = (Weapon) playerInstance.player.getEquipment(6);
        switch (type)
        {
            case "ranged": speed = ((weapon.getAttackspeed() * 5) - (0.008f * playerInstance.player.getStat("quick") + 0.003f * playerInstance.player.getWeaponSkill(null, weapon.getType()))); return speed;
            case "spell": speed = ((Spell)playerInstance.player.getActiveSkill()).getCastTime()-((0.008f * playerInstance.player.getStat("int") + 0.003f )); return speed;
        }

        return 0;
    }

    [RPC]
    public void increaseObjectID()
    {
        objectID += 0.1f;
    }

}
