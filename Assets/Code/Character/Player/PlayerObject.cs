using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerObject : MonoBehaviour {
    private string name;
    private List<Item> inventory = new List<Item>();
    private List<Item> equipmentList = new List<Item>();
    private List<Skill> skillList = new List<Skill>();
    private List<Skill> generalSkillList = new List<Skill>();
    private List<Skill> lesserMagicSkillList = new List<Skill>();
    private List<Skill> combatSkillList = new List<Skill>();
    private List<Skill> craftingSkillList = new List<Skill>();
    private List<Skill> weaponSkillList = new List<Skill>();
    private List<HotbarAble> hotbar = new List<HotbarAble>();
    private Castable activeSkill = null;
    private Weapon sheathedWeapon = null;
    private float str = 0, dex = 0, intel = 0, vit = 0, wis = 0, quick = 0;
    private float health, tempHealth, mana, tempMana, stamina, tempStamina, baseHealth = 0, baseMana = 0, baseStamina = 0,  bonusHealth = 0, bonusMana = 0, bonusStamina = 0;
    private float lungCapacity = 60;
    private int invSize, baseInvSize = 21, inventoryIDCount = 0;
    private float encumbrance = 0;
    private float[] protections = new float[15] { 1, 1, 1, 1, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0, 0, 0 }; //arrow, bludgeoning, piercing, slashing, acid, arcane, cold, fire, holy, impact, lightning, unholy, malediction, mental, infliction
    private float healthMod = 0, manaMod = 0, staminaMod = 0;  
    private float rangedRange = 5;
    private float critChance = 1;
    private int arrows = 0, gold = 0;//, mandrake = 0, ash = 0, sulfur = 0, raisin = 0, nacra = 0, bone = 0;
    public Player playerInstance;
    public GuiFunction gui;
    public RPG_Camera camera;
    public SpawnPoint spawnStone;

	// Use this for initialization
    void Start () 
    {
        for (int i = 0; i < 10; i++)
        {
            equipmentList.Add(null);
        }
        inventoryAdd(new Item_Weapon_Mirdain_Spellstaff());
        inventoryAdd(new Item_Weapon_Short_Bow());
        inventoryAdd(new Item_Weapon_Troll_Clubber());

        for (int i = 0; i < 100; i++)
        {
            skillList.Add(null);
            switch (i)
            {
                case 0: addSkill(i, new Skill_Passive_General_Run()); break;
                case 1: addSkill(i, new Skill_Active_General_Rest()); break;
                case 2: addSkill(i, new Skill_Passive_General_Sprint()); break;
                case 3: addSkill(i, new Skill_Passive_General_Crouch_Walk()); break;
                case 4: addSkill(i, new Skill_Passive_General_Constitution()); break;
                case 5: addSkill(i, new Skill_Passive_General_Defense()); break;
                case 6: addSkill(i, new Skill_Passive_General_Fortitude()); break;
                //7 = Skill_Passive_General_Preseverance()
                case 8: addSkill(i, new Skill_Passive_General_Reflex()); break;
                case 9: addSkill(i, new Skill_Passive_General_Rigor()); break;
                //10 = Skill_Passive_General_Survivalist()
                //11 = Skill_Passive_General_Thoughness()
                case 12: addSkill(i, new Skill_Passive_General_Willpower()); break;
                //case 13: addSkill(i, new Skill_Passive_General_Diving()); break;
                //case 14: addSkill(i, new Skill_Active_General_Revive()); break;
                //case 15: addSkill(i, new Skill_Passive_General_Riding()); break;
                //case 16: addSkill(i, new Skill_Passive_General_Swimming()); break;
                case 17: addSkill(i, new Skill_Passive_Weapon_Skill_Great_Sword()); break;
                //18 = Skill_Passive_Combat_Great_Sword_Mastery()     
                case 19: addSkill(i, new Skill_Passive_Weapon_Skill_Archery()); break;
                case 20: addSkill(i, new Skill_Active_Lesser_Magic_Heal_Self()); break;
                case 21: addSkill(i, new Skill_Active_Lesser_Magic_Mana_to_Stamina()); break;
                case 22: addSkill(i, new Skill_Active_Lesser_Magic_Health_To_Mana()); break;
                case 23: addSkill(i, new Skill_Active_Lesser_Magic_Stamina_To_Health()); break;
                case 24: addSkill(i, new Skill_Active_Lesser_Magic_Mana_Missle()); break;
                case 25: addSkill(i, new Skill_Passive_Crafting_Skill_Mining()); break;
                case 26: addSkill(i, new Skill_Passive_Lesser_Magic()); break;

            }
        }

        for (int i = 0; i < 9; i++)
        {
            hotbar.Add(null);
        }
    }

    public void setSpawnPoint(SpawnPoint spawnPoint)
    {
        spawnStone = spawnPoint;
    }

    public string getName()
    {
        return name;
    }

    public void setName(string nameIn)
    {
        name = nameIn;
        gui.setName(name);
    }

    public bool isWeaponSheathed()
    {
        if (sheathedWeapon != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void sheathWeapon()
    {
        if (equipmentList[6] != null)
        {
            sheathedWeapon = (Weapon) equipmentList[6];
            equipmentList[6] = null;
            gui.newTextLine("You sheath your " + ((Item) sheathedWeapon).getItemText() + "!");
            camera.viewMode("thirdPerson");
        }
    }

    public void unSheathWeapon()
    {
        if (sheathedWeapon != null)
        {
            equipmentList[6] = (Item)sheathedWeapon;
            sheathedWeapon = null;
            gui.newTextLine("You unsheathe your " + equipmentList[6].getItemText() + "!");
            if (equipmentList[6] is RangedWeapon)
            {
                camera.viewMode("firstPerson");
            }
            else
            {
                camera.viewMode("thirdPerson");
            }
        }
    }

    public float getWeaponSkillEffect(string weapon, string mastery)
    {
        if (weapon != null)
        {
            switch (weapon)
            {
                case "great sword": return getSkillEffect(17);
                case "bow": return getSkillEffect(19);
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
                case "bow": return getSkillLevel(19);
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
                case "bow": return 19;
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

    public int getInvSize()
    {
        return inventory.Count;
    }

    public float getInvWeight()
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
    public float getTempStaminaFloat()
    {
        return tempStamina;
    }


    public float getHealthFloat()
    {
        return health;
    }
    public float getTempHealthFloat()
    {
        return tempHealth;
    }
    public float getManaFloat()
    {
        return mana;
    }

    public float getTempManaFloat()
    {
        return tempMana;
    }

    public void setBonusStamina(float change)
    {
        changeStats(0, 0, 0, 0, 0, 0, 0, change, 0);
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

    public void changeStats(float tempStr, float tempDex, float tempIntel, float tempVit, float tempWis, float tempQuick, float tempBonusHealth,float tempBonusStamina,float tempBonusMana)
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
        stamina = baseStamina + (7 * quick) + (3 * vit)+ bonusStamina;
    }

    public void refillVitals()
    {
        tempHealth = health;
        tempMana = mana;
        tempStamina = stamina;
    }
    public bool inventoryAdd(Item e){
		if(e is Item){
            if (e is Stackable)
            {
                string tempName = e.getItemText();
                bool stacked = false;
                for (int i = 0; i < inventory.Count; i++)
                {
                    if (inventory[i].getItemText().Equals(tempName))
                    {
                        ((Stackable)inventory[i]).stackCount++;
                        stacked = true;
                    }
                }
                if (!stacked)
                {
                    if (getInvSize() < baseInvSize)
                    {
                        if (e.getInventorySlot() == 999)
                        {
                            if (inventory.Count == 0)
                            {
                                e.setInventorySlot(0);
                            }
                            else
                            {
                                e.setInventorySlot(inventory.Count);
                            }
                            gui.setInventoryIcon(e.getInventorySlot(), e.getIcon(), false, e);
                        }
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
            }
            else
            {
                if (getInvSize() < baseInvSize)
                {
                    if (e.getInventorySlot() == 999)
                    {
                        if (inventory.Count == 0)
                        {
                            e.setInventorySlot(0);
                        }
                        else
                        {
                            e.setInventorySlot(inventory.Count);
                        }
                        gui.setInventoryIcon(e.getInventorySlot(), e.getIcon(), false, e);
                    }
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
		}
		return false;
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
        hotbar[hotbarSlot] = null;
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
                gui.setInventoryIcon(inventory[i].getInventorySlot(), null, true, null);
                inventory[i].setInventorySlot(999);
                inventory.RemoveAt(i);
                for (int f = 0; f < inventory.Count; f++)
                {
                    inventory[f].setInventorySlot(f);
                    gui.setInventoryIcon(inventory[f].getInventorySlot(), inventory[f].getIcon(), false, inventory[f]);
                }
                for (int x = inventory.Count; x<21 ; x++)
                {
                    gui.setInventoryIcon(x, null, true, null);
                }
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
		try{
		return equipmentList [slot].getInventoryID();
		}catch
		{
			return -1;
		}

	}

    public string equip(int i){
		try{
		Item equipmentIn = getInventoryItem(i);
		if(equipmentIn is Equipable){
			if(((Equipable) equipmentIn).getItemSlot() == "Main Hand"){
				if(equipmentList[6] == null){
                    if (isWeaponSheathed())
                    {
                        sheathedWeapon = (Weapon) equipmentIn;
                    }
                    else 
                    { 
				        equipmentList[6] = equipmentIn;
                    }
                    if (equipmentList[6] is RangedWeapon)
                    {
                        camera.viewMode("firstPerson");
                    }
                    else
                    {
                        camera.setDesiredDistance(6);
                    }
                    inventoryRemove(i);
				}else{
					inventoryRemove(i);
					inventoryAdd(equipmentList[6]);
                    equipmentList[6] = equipmentIn;
                    if (equipmentList[6] is RangedWeapon)
                    {
                        camera.viewMode("firstPerson");
                    }
                    else
                    {
                        camera.viewMode("thirdPerson");
                    }
				}
                setActiveSkill(null);
                gui.setActiveSkillIcon(null, true);
			}else if(((Equipable) equipmentIn).getItemSlot() == "Off Hand"){
				if(equipmentList[7] == null){
                    equipmentList[7] = equipmentIn;
                    setProtections(equipmentIn.getProtections(), true);
                    inventoryRemove(i);
				}else{
					inventoryRemove(i);
					inventoryAdd(equipmentList[7]);
                    setProtections(equipmentList[7].getProtections(), false);
                    equipmentList[7] = equipmentIn;
                    setProtections(equipmentList[7].getProtections(),true);
				}
			}else if(((Equipable) equipmentIn).getItemSlot() == "Head"){
				if(equipmentList[0] == null){
                    equipmentList[0] = equipmentIn; 
                    inventoryRemove(i);
                    setProtections(equipmentIn.getProtections(), true);
                    setEncumbrance(((Armor)equipmentIn).getEncumbrance());
				}else{
					inventoryRemove(i);
					inventoryAdd(equipmentList[0]);
                    setProtections(equipmentList[0].getProtections(), false);
                    setEncumbrance(-((Armor)equipmentList[0]).getEncumbrance());
                    equipmentList[0] = equipmentIn;
                    setProtections(equipmentList[0].getProtections(),true);
                    setEncumbrance(((Armor)equipmentIn).getEncumbrance());
                }
			}else if(((Equipable) equipmentIn).getItemSlot() == "Torso"){
				if(equipmentList[1] == null){
                    equipmentList[1] = equipmentIn;
					inventoryRemove(i);
                    setProtections(equipmentIn.getProtections(), true);
                    setEncumbrance(((Armor)equipmentIn).getEncumbrance());
				}else{
					inventoryRemove(i);
					inventoryAdd(equipmentList[1]);
                    setProtections(equipmentList[1].getProtections(), false);
                    setEncumbrance(-((Armor)equipmentList[1]).getEncumbrance());
                    equipmentList[1] = equipmentIn;
                    setProtections(equipmentList[1].getProtections(),true);
                    setEncumbrance(((Armor)equipmentIn).getEncumbrance());
				}
			}else if(((Equipable) equipmentIn).getItemSlot() == "Legs"){
				if(equipmentList[4] == null){
                    equipmentList[4] = equipmentIn;
                    inventoryRemove(i);
                    setProtections(equipmentIn.getProtections(), true);
                    setEncumbrance(((Armor)equipmentIn).getEncumbrance());
				}else{
					inventoryRemove(i);
                    inventoryAdd(equipmentList[4]);
                    setProtections(equipmentList[4].getProtections(), false);
                    setEncumbrance(-((Armor)equipmentList[4]).getEncumbrance());
                    equipmentList[4] = equipmentIn;
                    setProtections(equipmentList[4].getProtections(), true);
                    setEncumbrance(((Armor)equipmentIn).getEncumbrance());
				}
			}else if(((Equipable) equipmentIn).getItemSlot() == "Feet"){
				if(equipmentList[5] == null){
                    equipmentList[5] = equipmentIn;
                     inventoryRemove(i);
                     setProtections(equipmentIn.getProtections(), true);
                     setEncumbrance(((Armor)equipmentIn).getEncumbrance());
				}else{
					inventoryRemove(i);
                    inventoryAdd(equipmentList[5]);
                    setProtections(equipmentList[5].getProtections(), false);
                    setEncumbrance(-((Armor)equipmentList[5]).getEncumbrance());
                    equipmentList[5] = equipmentIn;
                    setProtections(equipmentList[5].getProtections(), true);
                    setEncumbrance(((Armor)equipmentIn).getEncumbrance());
				}
			}else if(((Equipable) equipmentIn).getItemSlot() == "Hands"){
				if(equipmentList[3] == null){
                    equipmentList[3] = equipmentIn;
                    inventoryRemove(i);
                    setProtections(equipmentIn.getProtections(), true);
                    setEncumbrance(((Armor)equipmentIn).getEncumbrance());
				}else{
                    inventoryRemove(i);
                    inventoryAdd(equipmentList[3]);
                    setProtections(equipmentList[3].getProtections(), false);
                    setEncumbrance(-((Armor)equipmentList[3]).getEncumbrance());
                    equipmentList[3] = equipmentIn;
                    setProtections(equipmentList[3].getProtections(), true);
                    setEncumbrance(((Armor)equipmentIn).getEncumbrance());
                }
			}else if(((Equipable) equipmentIn).getItemSlot() == "Arms"){
				if(equipmentList[2] == null){
                    equipmentList[2] = equipmentIn;
                    inventoryRemove(i);
                    setProtections(equipmentIn.getProtections(), true);
                    setEncumbrance(((Armor)equipmentIn).getEncumbrance());
				}else{
                    inventoryRemove(i);
                    inventoryAdd(equipmentList[2]);
                    setProtections(equipmentList[2].getProtections(), false);
                    setEncumbrance(-((Armor)equipmentList[2]).getEncumbrance());
                    equipmentList[2] = equipmentIn;
                    setProtections(equipmentList[2].getProtections(), true);
                    setEncumbrance(((Armor)equipmentIn).getEncumbrance());
				}
			}else if(((Equipable) equipmentIn).getItemSlot() == "Neck"){
				if(equipmentList[8] == null){
                    equipmentList[8] = equipmentIn;
                     inventoryRemove(i);
				}else{
					inventoryRemove(i);
					inventoryAdd(equipmentList[8]);
                    equipmentList[8] = equipmentIn;
				}
			}else if(((Equipable) equipmentIn).getItemSlot() == "Fingers"){
				if(equipmentList[9] == null){
                    equipmentList[9] = equipmentIn;
                     inventoryRemove(i);
				}else{
					inventoryRemove(i);
					inventoryAdd(equipmentList[9]);
                    equipmentList[9] = equipmentIn;
				}
			}
			return "You equip "+ equipmentIn.getItemText()+" on your "+equipmentIn.getItemSlot()+"!";
		}
		}catch{
			return "Cant equip nothing";
		}
		return null;
	}

    public string dequip(int i){
		Item item = equipmentList[i];
		if(item != null){
			if(inventoryAdd(equipmentList[i]) == true){
				equipmentList[i] = null;
				return "You take off "+item.getItemText();
			}else{
				return "No space in inventory";
			}
		}
		return "You can't take off nothing!";
	}

    public Item getWeapon(){
		Item weapon=null;
		try{
			weapon=equipmentList[6];
			if(weapon!=null){
				return weapon;
			}else{

			}
		}
		catch {
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
            case "arrow": return protections[0];
        }
        return 0;
    }

    public void setProtections(float[] protectionsIn, bool add)
    {
        if(add){
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
            gui.newTextLine("You were healed for <color=lime>" + healing.ToString("0.00")+"</color> ");
        }
        else if(healing == 0 && !regen)
        {
            gui.newTextLine("You took <color=red>" + damage.ToString("0.00") + "</color> "+ type +" damage!");
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
        if (!regen)
            playerInstance.view.RPC("setHealth", PhotonTargets.AllBuffered, getHealthForTarget());
        
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

    public void skillGainGroup(float change, string skillGroup)
    {
        switch (skillGroup)
        {
            case "lesser magic": skillList[26].setSkillLevel(change/skillList[26].getSkillLevel()); break;
            default: break;
        }
    }

    public void addSkill(int id, Skill skillIn)
    {
        skillList.Insert(id, skillIn);
        skillList[id].setPlayerInstance(playerInstance, null);
        skillList[id].setGuiInstance(gui, true);

        switch (skillIn.getSkillGroup())
        {
            case "combat": combatSkillList.Add(skillIn);break;
            case "crafting": craftingSkillList.Add(skillIn); break;
            case "general": generalSkillList.Add(skillIn); break;
            case "lesser magic": lesserMagicSkillList.Add(skillIn); break;
            case "weapon skill": weaponSkillList.Add(skillIn); break;
        }
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
        if (activeSkill != null)
            return activeSkill;
        else
            return null;
    }

    public float getLungCapacity()
    {
        return lungCapacity;
    }

    public void setLungCapacity(float change)
    {
        lungCapacity += change;
    }

    public void makeSkillWindow(string type)
    {
        switch (type)
        {
            case "combat": gui.makeSkillWindow(type,combatSkillList); break;
            case "crafting": gui.makeSkillWindow(type,craftingSkillList); break;
            case "general": gui.makeSkillWindow(type,generalSkillList); break;
            case "lesser magic": gui.makeSkillWindow(type,lesserMagicSkillList); break;
            case "weapon skill": gui.makeSkillWindow(type, weaponSkillList); break;
        }
    }

    public float getHealthForTarget()
    {
        return tempHealth / health;
    }
}
