using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NpcObject : MonoBehaviour
{
    private List<Item> inventory = new List<Item>();
    private List<Item> equipmentList = new List<Item>();
    private List<Skill> skillList = new List<Skill>();
    private List<HotbarAble> hotbar = new List<HotbarAble>();
    private Castable activeSkill = null;
    private float str = 0, dex = 0, intel = 0, vit = 0, wis = 0, quick = 0;
    private float health, tempHealth, mana, tempMana, stamina, tempStamina, baseHealth = 0, baseMana = 0, baseStamina = 0, bonusHealth = 0, bonusMana = 0, bonusStamina = 0;
    private float lungCapacity = 60;
    private int invSize, baseInvSize = 200, inventoryIDCount = 0;
    private float encumbrance = 0;
    private float[] protections = new float[15] { 1, 1, 1, 1, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0, 0, 0 }; //arrow, bludgeoning, piercing, slashing, acid, arcane, cold, fire, holy, impact, lightning, unholy, malediction, mental, infliction
    private float healthMod = 0, manaMod = 0, staminaMod = 0;
    private float rangedRange = 5;
    private float critChance = 1;
    private int arrows = 0, gold = 0;//, mandrake = 0, ash = 0, sulfur = 0, raisin = 0, nacra = 0, bone = 0;
    public Npc npcInstance;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            equipmentList.Add(null);
        }
        inventoryAdd(new Item_Weapon_Short_Bow(0, 0));
        inventoryAdd(new Item_Weapon_Troll_Clubber(0, 0));
        inventoryAdd(new Item_Armor_Scale_Helm(0, 0)); equip(2);
        inventoryAdd(new Item_Armor_Scale_Cuirass(0, 0)); equip(3);
        inventoryAdd(new Item_Armor_Scale_Leggings(0, 0)); equip(4);
        inventoryAdd(new Item_Armor_Scale_Boots(0, 0)); equip(5);

        for (int i = 0; i < 100; i++)
        {
            skillList.Add(null);
            switch (i)
            {
                case 0: skillList.Insert(0, new Skill_Passive_General_Run()); skillList[0].setPlayerInstance(null,npcInstance); break;
                case 1: skillList.Insert(1, new Skill_Active_General_Rest()); skillList[1].setPlayerInstance(null, npcInstance); break;
                case 2: skillList.Insert(2, new Skill_Passive_General_Sprint()); skillList[2].setPlayerInstance(null, npcInstance); break;
                case 3: skillList.Insert(3, new Skill_Passive_General_Crouch_Walk()); skillList[3].setPlayerInstance(null, npcInstance); break;
                case 4: skillList.Insert(4, new Skill_Passive_General_Constitution()); skillList[4].setPlayerInstance(null, npcInstance); break;
                case 5: skillList.Insert(5, new Skill_Passive_General_Defense()); skillList[5].setPlayerInstance(null, npcInstance); break;
                case 6: skillList.Insert(6, new Skill_Passive_General_Fortitude()); skillList[6].setPlayerInstance(null, npcInstance); break;
                //7 = Skill_Passive_General_Preseverance()
                case 8: skillList.Insert(8, new Skill_Passive_General_Reflex()); skillList[8].setPlayerInstance(null, npcInstance); break;
                case 9: skillList.Insert(9, new Skill_Passive_General_Rigor()); skillList[9].setPlayerInstance(null, npcInstance); break;
                //10 = Skill_Passive_General_Survivalist()
                //11 = Skill_Passive_General_Thoughness()
                case 12: skillList.Insert(12, new Skill_Passive_General_Willpower()); skillList[12].setPlayerInstance(null, npcInstance); break;
                case 13: skillList.Insert(13, new Skill_Passive_General_Diving()); skillList[13].setPlayerInstance(null, npcInstance); break;
                case 14: skillList.Insert(14, new Skill_Active_General_Revive()); skillList[14].setPlayerInstance(null, npcInstance); break;
                case 15: skillList.Insert(15, new Skill_Passive_General_Riding()); skillList[15].setPlayerInstance(null, npcInstance); break;
                case 16: skillList.Insert(16, new Skill_Passive_General_Swimming()); skillList[16].setPlayerInstance(null, npcInstance); break;
                case 17: skillList.Insert(17, new Skill_Passive_Weapon_Skill_Great_Sword()); skillList[17].setPlayerInstance(null,npcInstance); break;
                //18 = Skill_Passive_Combat_Great_Sword_Mastery()   
                case 19: skillList.Insert(19, new Skill_Passive_Weapon_Skill_Archery()); skillList[19].setPlayerInstance(null, npcInstance); break;

            }
        }

        for (int i = 0; i < 9; i++)
        {
            hotbar.Add(null);
        }
    }
    public float getWeaponSkillEffect(string weapon, string mastery)
    {
        if (weapon != null)
        {
            switch (weapon)
            {
                case "great sword": return getSkillEffect(17);
            }
        }
        else if (mastery != null)
        {
            switch (mastery)
            {
                case "great sword": if (getSkill(18) != null) return getSkillEffect(17); else return 0;
            }
        }
        return 0;
    }

    public float getWeaponSkill(string weapon, string mastery)
    {
        if (weapon != null)
        {
            switch (weapon)
            {
                case "great sword": return getSkillLevel(17);
            }
        }
        else if (mastery != null)
        {
            switch (mastery)
            {
                case "great sword": if (getSkill(18) != null) return getSkillLevel(18); else return 0;
            }
        }
        return 0;
    }
    public int getWeaponSkillId(string weapon)
    {
        if (weapon != null)
        {
            switch (weapon)
            {
                case "great sword": return 17;
            }
        }
        return 0;
    }

    public int getWeaponMasterySkillId(string weapon)
    {
        if (weapon != null)
        {
            switch (weapon)
            {
                case "great sword": return 18;
            }
        }
        return 0;
    }

    public float getInvSize()
    {
        float weight = 0;
        for (int i = 0; i < inventory.Count; i++)
        {
            weight += inventory[i].getWeight();
        }
        return weight;
    }

    public Item getEquipment(int index)
    {
        return equipmentList[index];
    }

    public Item getInventory(int index)
    {
        return inventory[index];
    }

    public string getHealth()
    {
        return tempHealth.ToString("0.00") + "/" + health;
    }

    public string getMana()
    {
        return tempMana.ToString("0.00") + "/" + mana;
    }

    public string getStamina()
    {
        return tempStamina.ToString("0.00") + "/" + stamina;
    }

    public float getStaminaFloat()
    {
        return stamina;
    }

    public float getHealthFloat()
    {
        return health;
    }

    public void setBonusStamina(float change)
    {
        changeStats(0, 0, 0, 0, 0, 0, 0, change, 0);
    }

    public float getManaInt()
    {
        return tempMana;
    }

    public void setEncumbrance(float change)
    {
        encumbrance += change;
    }

    public float getEncumbrance()
    {
        return encumbrance;
    }
    public float getRegenModifiers(int vital) //Vital 1 = health, Vital 2 = stamina, Vital 3 = mana
    {
        if (vital == 1)
        {
            return healthMod;
        }
        else if (vital == 2)
        {
            return staminaMod;
        }
        else if (vital == 3)
        {
            return manaMod;
        }
        else
        {
            return 0;
        }
    }

    public void setRegenModifiers(int vital, float modifier) //Vital 1 = health, Vital 2 = stamina, Vital 3 = mana
    {
        if (vital == 1)
        {
            healthMod += modifier;
        }
        else if (vital == 2)
        {
            staminaMod += modifier;
        }
        else if (vital == 3)
        {
            manaMod += modifier;
        }
    }

    public string getStatus()
    {
        string tempStats = getStats();
        string tempStatus = "Health: " + getHealth() + " " + "\nStamina: " + getStamina() + " " + "\nMana: " + getMana();
        return tempStatus + "\n" + tempStats + "\n" + "Gold: " + getGold();
    }

    public string getItemDescription(int index)
    {
        try
        {
            if (equipmentList[index].getItemDescription() != null)
            {
                return equipmentList[index].getItemDescription();
            }
        }
        catch
        {
            return "No item equipped in this slot";
        }
        return "";
    }

    public void changeStats(float tempStr, float tempDex, float tempIntel, float tempVit, float tempWis, float tempQuick, float tempBonusHealth, float tempBonusStamina, float tempBonusMana)
    {
        str += tempStr;
        dex += tempDex;
        intel += tempIntel;
        vit += tempVit;
        wis += tempWis;
        quick += tempQuick;
        bonusHealth += tempBonusHealth;
        bonusStamina += tempBonusStamina;
        bonusMana += tempBonusMana;
        health = baseHealth + (3 * str) + (7 * vit) + bonusHealth;
        mana = baseMana + (3 * wis) + (7 * intel) + bonusMana;
        stamina = baseStamina + (7 * quick) + (3 * vit) + bonusStamina;
    }

    public void refillVitals()
    {
        tempHealth = health;
        tempMana = mana;
        tempStamina = stamina;
    }
    public bool inventoryAdd(Item e)
    {
        if (e is Item)
        {
            if (getInvSize() < baseInvSize)
            {
                inventory.Add(e);
                if (e.getInventoryID() == 999)
                {
                    e.setInventoryID(inventoryIDCount);
                    inventoryIDCount++;
                }
                bool spaceLeft = true;
                return spaceLeft;
            }
        }
        return false;
    }

    public void skillListAdd(Skill inputSkill)
    {
        if (inputSkill is Skill)
        {
            skillList.Insert(inputSkill.getSkillID(), inputSkill);
        }
    }

    public void hotbarAdd(HotbarAble input, int hotbarSlot)
    {
        if (input is HotbarAble)
        {
            hotbar.Insert(hotbarSlot, input);
        }
    }

    public void hotbarRemove(int hotbarSlot)
    {
        hotbar.Insert(hotbarSlot, null);
    }

    public HotbarAble getHotbarType(int hotbarSlot)
    {
        return hotbar[hotbarSlot];
    }

    public bool inventoryRemove(int inventoryID)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].getInventoryID() == inventoryID)
            {
                inventory.RemoveAt(i);
                if (getInvSize() < baseInvSize)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public Item getInventoryItem(int inventoryID)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].getInventoryID() == inventoryID)
            {
                return inventory[i];
            }
        }
        return null;
    }

    public int getEquipmentIDinSlot(int slot)
    {
        try
        {
            return equipmentList[slot].getInventoryID();
        }
        catch
        {
            return -1;
        }

    }

    public string equip(int i)
    {
        try
        {
            Item equipmentIn = getInventoryItem(i);
            if (equipmentIn is Equipable)
            {
                if (((Equipable)equipmentIn).getItemSlot() == "Main Hand")
                {
                    if (equipmentList[6] == null)
                    {
                        equipmentList[6] = equipmentIn;
                        inventoryRemove(i);
                    }
                    else
                    {
                        inventoryRemove(i);
                        inventoryAdd(equipmentList[6]);
                        equipmentList[6] = equipmentIn;
                    }
                }
                else if (((Equipable)equipmentIn).getItemSlot() == "Off Hand")
                {
                    if (equipmentList[7] == null)
                    {
                        equipmentList[7] = equipmentIn;
                        setProtections(equipmentIn.getProtections(), true);
                        inventoryRemove(i);
                    }
                    else
                    {
                        inventoryRemove(i);
                        inventoryAdd(equipmentList[7]);
                        setProtections(equipmentList[7].getProtections(), false);
                        equipmentList[7] = equipmentIn;
                        setProtections(equipmentList[7].getProtections(), true);
                    }
                }
                else if (((Equipable)equipmentIn).getItemSlot() == "Head")
                {
                    if (equipmentList[0] == null)
                    {
                        equipmentList[0] = equipmentIn;
                        inventoryRemove(i);
                        setProtections(equipmentIn.getProtections(), true);
                        setEncumbrance(((Armor)equipmentIn).getEncumbrance());
                    }
                    else
                    {
                        inventoryRemove(i);
                        inventoryAdd(equipmentList[0]);
                        setProtections(equipmentList[0].getProtections(), false);
                        setEncumbrance(-((Armor)equipmentList[0]).getEncumbrance());
                        equipmentList[0] = equipmentIn;
                        setProtections(equipmentList[0].getProtections(), true);
                        setEncumbrance(((Armor)equipmentIn).getEncumbrance());
                    }
                }
                else if (((Equipable)equipmentIn).getItemSlot() == "Torso")
                {
                    if (equipmentList[1] == null)
                    {
                        equipmentList[1] = equipmentIn;
                        inventoryRemove(i);
                        setProtections(equipmentIn.getProtections(), true);
                        setEncumbrance(((Armor)equipmentIn).getEncumbrance());
                    }
                    else
                    {
                        inventoryRemove(i);
                        inventoryAdd(equipmentList[1]);
                        setProtections(equipmentList[1].getProtections(), false);
                        setEncumbrance(-((Armor)equipmentList[1]).getEncumbrance());
                        equipmentList[1] = equipmentIn;
                        setProtections(equipmentList[1].getProtections(), true);
                        setEncumbrance(((Armor)equipmentIn).getEncumbrance());
                    }
                }
                else if (((Equipable)equipmentIn).getItemSlot() == "Legs")
                {
                    if (equipmentList[4] == null)
                    {
                        equipmentList[4] = equipmentIn;
                        inventoryRemove(i);
                        setProtections(equipmentIn.getProtections(), true);
                        setEncumbrance(((Armor)equipmentIn).getEncumbrance());
                    }
                    else
                    {
                        inventoryRemove(i);
                        inventoryAdd(equipmentList[4]);
                        setProtections(equipmentList[4].getProtections(), false);
                        setEncumbrance(-((Armor)equipmentList[4]).getEncumbrance());
                        equipmentList[4] = equipmentIn;
                        setProtections(equipmentList[4].getProtections(), true);
                        setEncumbrance(((Armor)equipmentIn).getEncumbrance());
                    }
                }
                else if (((Equipable)equipmentIn).getItemSlot() == "Feet")
                {
                    if (equipmentList[5] == null)
                    {
                        equipmentList[5] = equipmentIn;
                        inventoryRemove(i);
                        setProtections(equipmentIn.getProtections(), true);
                        setEncumbrance(((Armor)equipmentIn).getEncumbrance());
                    }
                    else
                    {
                        inventoryRemove(i);
                        inventoryAdd(equipmentList[5]);
                        setProtections(equipmentList[5].getProtections(), false);
                        setEncumbrance(-((Armor)equipmentList[5]).getEncumbrance());
                        equipmentList[5] = equipmentIn;
                        setProtections(equipmentList[5].getProtections(), true);
                        setEncumbrance(((Armor)equipmentIn).getEncumbrance());
                    }
                }
                else if (((Equipable)equipmentIn).getItemSlot() == "Hands")
                {
                    if (equipmentList[3] == null)
                    {
                        equipmentList[3] = equipmentIn;
                        inventoryRemove(i);
                        setProtections(equipmentIn.getProtections(), true);
                        setEncumbrance(((Armor)equipmentIn).getEncumbrance());
                    }
                    else
                    {
                        inventoryRemove(i);
                        inventoryAdd(equipmentList[3]);
                        setProtections(equipmentList[3].getProtections(), false);
                        setEncumbrance(-((Armor)equipmentList[3]).getEncumbrance());
                        equipmentList[3] = equipmentIn;
                        setProtections(equipmentList[3].getProtections(), true);
                        setEncumbrance(((Armor)equipmentIn).getEncumbrance());
                    }
                }
                else if (((Equipable)equipmentIn).getItemSlot() == "Arms")
                {
                    if (equipmentList[2] == null)
                    {
                        equipmentList[2] = equipmentIn;
                        inventoryRemove(i);
                        setProtections(equipmentIn.getProtections(), true);
                        setEncumbrance(((Armor)equipmentIn).getEncumbrance());
                    }
                    else
                    {
                        inventoryRemove(i);
                        inventoryAdd(equipmentList[2]);
                        setProtections(equipmentList[2].getProtections(), false);
                        setEncumbrance(-((Armor)equipmentList[2]).getEncumbrance());
                        equipmentList[2] = equipmentIn;
                        setProtections(equipmentList[2].getProtections(), true);
                        setEncumbrance(((Armor)equipmentIn).getEncumbrance());
                    }
                }
                else if (((Equipable)equipmentIn).getItemSlot() == "Neck")
                {
                    if (equipmentList[8] == null)
                    {
                        equipmentList[8] = equipmentIn;
                        inventoryRemove(i);
                    }
                    else
                    {
                        inventoryRemove(i);
                        inventoryAdd(equipmentList[8]);
                        equipmentList[8] = equipmentIn;
                    }
                }
                else if (((Equipable)equipmentIn).getItemSlot() == "Fingers")
                {
                    if (equipmentList[9] == null)
                    {
                        equipmentList[9] = equipmentIn;
                        inventoryRemove(i);
                    }
                    else
                    {
                        inventoryRemove(i);
                        inventoryAdd(equipmentList[9]);
                        equipmentList[9] = equipmentIn;
                    }
                }
                return "You equip " + equipmentIn.getItemText() + " on your " + equipmentIn.getItemSlot() + "!";
            }
        }
        catch
        {
            return "Cant equip nothing";
        }
        return null;
    }

    public string dequip(int i)
    {
        Item item = equipmentList[i];
        if (item != null)
        {
            if (inventoryAdd(equipmentList[i]) == true)
            {
                equipmentList[i] = null;
                return "You take off " + item.getItemText();
            }
            else
            {
                return "No space in inventory";
            }
        }
        return "You can't take off nothing!";
    }

    public Item getWeapon()
    {
        Item weapon = null;
        try
        {
            weapon = equipmentList[6];
            if (weapon != null)
            {
                return weapon;
            }
            else
            {

            }
        }
        catch
        {
        }
        return weapon;
    }

    public float[] getProtections()
    {
        return protections;
    }
    public float getProtection(string protection)
    {
        switch (protection)
        {
            case "slashing": return protections[3];
        }
        return 0;
    }

    public void setProtections(float[] protectionsIn, bool add)
    {
        if (add)
        {
            for (int i = 0; i < protections.Length; i++)
            {
                protections[i] += protectionsIn[i];
            }
        }
        else
        {
            for (int i = 0; i < protections.Length; i++)
            {
                protections[i] -= protectionsIn[i];
            }
        }
    }

    public bool setHealth(float damage, float healing, bool regen, string type)
    {
        if (damage == 0 && !regen)
        {
        }
        else if (healing == 0 && !regen)
        {
        }
        tempHealth -= damage;
        tempHealth += healing;
        if (tempHealth <= 0)
        {
            tempHealth = 0;
            bool isDead = true;
            return isDead;
        }
        else if (tempHealth > health)
        {
            tempHealth = health;
        }
        return false;
    }

    public bool setMana(float manaCost, float manaRestore)
    {
        tempMana += manaRestore;
        if (tempMana >= manaCost && manaCost != 0)
        {
            tempMana -= manaCost;
            bool canCast = true;
            return canCast;
        }
        else if (tempMana > mana)
        {
            tempMana = mana;
        }
        return false;
    }

    public bool setStamina(float staminaCost, float staminaRestore)
    {
        tempStamina += staminaRestore;
        if (tempStamina >= staminaCost && staminaCost != 0)
        {
            tempStamina -= staminaCost;
            bool canSprint = true;
            return canSprint;
        }
        else if (tempStamina > stamina)
        {
            tempStamina = stamina;
        }
        return false;
    }

    public string getStats()
    {
        return "\nStrength: " + str + " " + "\nDexterity: " + dex + " " + "\nIntellect: " + intel + " " + "\nVitality: " + vit + " " + "\nWisdom: " + wis + "\nQuickness: " + quick;
    }

    public float getStat(string stat)
    {
        switch (stat)
        {
            case "str": return str;
            case "dex": return dex;
            case "int": return intel;
            case "vit": return vit;
            case "wis": return wis;
            case "quick": return quick;
        }
        return 0;
    }

    public double getCritChance()
    {
        return critChance;
    }

    public void setCritChance(float critChance)
    {
        this.critChance = critChance;
    }

    public float getRangedRange()
    {
        return rangedRange;
    }

    public void setRangedRange(float rangedRange)
    {
        this.rangedRange = rangedRange;
    }

    public int getArrows()
    {
        return arrows;
    }

    public void setArrows(int amount)
    {
        arrows += amount;
    }

    public float getGold()
    {
        return gold;
    }

    public bool setGold(int goldIn)
    {
        int tempGold = gold;
        gold += goldIn;
        if (gold < 0)
        {
            gold = tempGold;
            return false;
        }
        else
        {
            return true;
        }
    }

    public float getSkillEffect(int skillID)
    {
        return skillList[skillID].getEffect();
    }

    public bool skillGain(float change, int skillID)
    {
        return skillList[skillID].setSkillLevel(change);
    }

    public string getSkillName(int skillID)
    {
        return skillList[skillID].getSkillText();
    }

    public string getSkillType(int skillID)
    {
        return skillList[skillID].getType();
    }

    public float getSkillLevel(int skillID)
    {
        return skillList[skillID].getSkillLevel();
    }

    public Skill getSkill(int skillID)
    {
        return skillList[skillID];
    }

    public void setActiveSkill(Castable skill)
    {
        activeSkill = skill;
    }

    public Castable getActiveSkill()
    {
        return activeSkill;
    }

    public float getLungCapacity()
    {
        return lungCapacity;
    }

    public void setLungCapacity(float change)
    {
        lungCapacity += change;
    }
}
