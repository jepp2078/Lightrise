using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Npc : MonoBehaviour
{
    public NpcObject npc;
    public GameObject npcObject;
    public NpcFunction func;
    private List<Castable> cooldownList = new List<Castable>();
    private List<Castable> spellDurationList = new List<Castable>();
    private float attackCooldown = 0f;
    public Npc instance;
    public int serverTicks = 0;

    // Use this for initialization and testing
    void Start()
    {
        npc.changeStats(5, 5, 5, 5, 5, 5, 0, 0, 0);
        npc.refillVitals();
        InvokeRepeating("serverTick", 0, 0.0825F); //TEMP value. We might need to change how fast the server ticks? 1/12 of a sec right now.
    }

    void Update()
    {
        
    }

    void gameLogic()
    {
        if (serverTicks % 48 == 0)
        {
            npc.setHealth(0, (0.66f + npc.getRegenModifiers(1)), true, "self"); // Think about skill to modify this?
            npc.setMana(0, (0.66f + npc.getRegenModifiers(3)));
        }
        if (serverTicks % 24 == 0)
        {
            npc.setStamina(0, (0.66f + npc.getRegenModifiers(2))); // Think about skill to modify this?
            Debug.Log(func.status());
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
        npc.skillGain(gain, skillID);
    }

    public void updateCooldowns()
    {
        for (int i = 0; i < cooldownList.Count; i++)
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

    //void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    //{
    //    Vector3 syncPosition = Vector3.zero;
    //    if (stream.isWriting)
    //    {
    //        syncPosition = this.gameObject.transform.position;
    //        stream.Serialize(ref syncPosition);
    //    }
    //    else
    //    {
    //        stream.Serialize(ref syncPosition);
    //        this.gameObject.transform.position = syncPosition;
    //    }
    //}
}
