using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerObject : PlayerEntity {
    private List<Item> inventory = new List<Item>();
    private List<Item> equipmentList = new List<Item>();
    private List<Skill> skillList = new List<Skill>();
    private List<HotbarAble> hotbar = new List<HotbarAble>();
    private float str = 5, dex = 5, intel = 5, vit = 5, wis = 5;
    private float health, tempHealth, mana, tempMana, stamina, tempStamina, baseHealth = 100, baseMana = 100, baseStamina = 100,  bonusHealth = 0, bonusMana = 0, bonusStamina = 0;
    private int invSize, baseInvSize = 200, inventoryIDCount = 0;
    private float meleeWepMod, spellMod, rangedWepMod, healthMod = 0, manaMod = 0, staminaMod = 0;
    private float unarmedDmg, armedDmg, magicDmg, rangedDmg;
    private float rangedRange = 5;
    private float critChance = 1;
    private int arrows = 0, gold = 0, mandrake = 0, ash = 0, sulfur = 0, raisin = 0, nacra = 0, bone = 0;

	// Use this for initialization
    public PlayerObject(int id, int x, int y) : base(id, x, y)
    {
        for (int i = 0; i < 10; i++)
        {
            equipmentList.Add(null);
            switch (i)
            {
                case 0: equipmentList[0] = new Item_Armor_Cloth_Sack(0, 0); break;
                case 1: equipmentList[1] = new Item_Armor_Cloth_West(0, 0); break;
                case 4: equipmentList[4] = new Item_Armor_Cloth_Pants(0, 0); break;
                case 5: equipmentList[5] = new Item_Armor_Leather_Sandals(0, 0); break;
                case 6: inventoryAdd(new Item_Weapon_GreatBow(0, 0)); inventoryAdd(new Item_Weapon_GreatSword(0, 0)); break;
            }
        }

        for (int i = 0; i < 100; i++)
        {
            skillList.Add(null);
            switch (i)
            {
                case 0: skillList.Insert(0, new Skill_Passive_General_Run()); break;
            }
        }

        for (int i = 0; i < 9; i++)
        {
            hotbar.Add(null);
        }
    }

    public int getInvSize()
    {
        invSize = inventory.Count;
        return invSize;
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

    public float getManaInt()
    {
        return tempMana;
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

    public void changeStats(float tempStr, float tempDex, float tempIntel, float tempVit, float tempWis)
    {
        str += tempStr;
        dex += tempDex;
        intel += tempIntel;
        vit += tempVit;
        wis += tempWis;
        health = baseHealth + (3 * str) + (7 * vit) + bonusHealth;
        mana = baseMana + (3 * wis) + (7 * intel) + bonusMana;
        stamina = baseStamina + (3 * wis) + (7 * dex) + bonusStamina;

        unarmedDmg = (int)((0.5 * str) + (0.2 * dex));
        armedDmg = (int)(meleeWepMod + (0.5 * str) + (0.2 * dex));
        magicDmg = (int)(spellMod + (0.5 * intel) + (0.2 * wis));
        rangedDmg = (int)(rangedWepMod + (0.5 * dex) + (0.2 * str));
    }

    public void refillVitals()
    {
        tempHealth = health;
        tempMana = mana;
        tempStamina = stamina;
    }
    public bool inventoryAdd(Item e){
		if(e is Item){
			if(inventory.Count < baseInvSize){
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

    public void inventoryRemove(int inventoryID)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].getInventoryID() == inventoryID)
            {
                inventory.RemoveAt(i);
            }
        }
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

    public float getWeaponMod(string type)
    {
        float damageMod = 0;
        switch (type)
        {
            case "melee":
                damageMod = armedDmg;
                break;
            case "ranged":
                damageMod = rangedDmg;
                break;
            case "spell":
                damageMod = magicDmg;
                break;
            default:
                damageMod = unarmedDmg;
                break;
        }
        return damageMod;
    }

    public string equip(int i){
		try{
		Item equipmentIn = getInventoryItem(i);

		if(equipmentIn is Equipable){
			if(((Equipable) equipmentIn).getItemSlot() == "Main Hand"){
				if(equipmentList[6] == null){
				 equipmentList[6] = equipmentIn;
                 inventoryRemove(i);
				}else{
					inventoryRemove(i);
					inventoryAdd(equipmentList[6]);
                    equipmentList[6] = equipmentIn;
				}
			}else if(((Equipable) equipmentIn).getItemSlot() == "Off Hand"){
				if(equipmentList[7] == null){
                    equipmentList[7] = equipmentIn;
                     inventoryRemove(i);
					}else{
						inventoryRemove(i);
						inventoryAdd(equipmentList[7]);
                        equipmentList[7] = equipmentIn;
					}
			}else if(((Equipable) equipmentIn).getItemSlot() == "Head"){
				if(equipmentList[0] == null){
                    equipmentList[0] = equipmentIn; inventoryRemove(i);
					}else{
						inventoryRemove(i);
						inventoryAdd(equipmentList[0]);
                        equipmentList[0] = equipmentIn;
                    }
			}else if(((Equipable) equipmentIn).getItemSlot() == "Torso"){
				if(equipmentList[1] == null){
                      equipmentList[1] = equipmentIn;
					  inventoryRemove(i);
					}else{
						inventoryRemove(i);
						inventoryAdd(equipmentList[1]);
                        equipmentList[1] = equipmentIn;
					}
			}else if(((Equipable) equipmentIn).getItemSlot() == "Legs"){
				if(equipmentList[4] == null){
                    equipmentList[4] = equipmentIn;
                     inventoryRemove(i);
					}else{
						inventoryRemove(i);
						inventoryAdd(equipmentList[4]);
                        equipmentList[4] = equipmentIn;
					}
			}else if(((Equipable) equipmentIn).getItemSlot() == "Feet"){
				if(equipmentList[5] == null){
                    equipmentList[5] = equipmentIn;
                     inventoryRemove(i);
					}else{
						inventoryRemove(i);
						inventoryAdd(equipmentList[5]);
                        equipmentList[5] = equipmentIn;
					}
			}else if(((Equipable) equipmentIn).getItemSlot() == "Hands"){
				if(equipmentList[3] == null){
                    equipmentList[3] = equipmentIn;
                     inventoryRemove(i);
					}else{
						inventoryRemove(i);
						inventoryAdd(equipmentList[3]);
                        equipmentList[3] = equipmentIn;
					}
			}else if(((Equipable) equipmentIn).getItemSlot() == "Arms"){
				if(equipmentList[2] == null){
                    equipmentList[2] = equipmentIn;
                     inventoryRemove(i);
					}else{
						inventoryRemove(i);
						inventoryAdd(equipmentList[2]);
                        equipmentList[2] = equipmentIn;
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
				Item weapon_unarmed = new Item_Weapon_Unarmed(0,0);
				weapon = weapon_unarmed;
			}
		}
		catch {
		}
		return weapon;
	}

    public float getArmor()
    {
        int armorInt = 0;
        try
        {
            for (int i = 0; i < 5; i++)
                armorInt += equipmentList[i].getArmor();
        }
        catch 
        {

        }
        return (float)(1 - armorInt * 0.01);
    }

    public bool setHealth(float damage, float healing)
    {
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
        return "\nStrength: " + str + " " + "\nDexterity: " + dex + " " + "\nIntellect: " + intel + " " + "\nVitality: " + vit + " " + "\nWisdom: " + wis;
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

    public float getMagicMod()
    {
        return magicDmg;
    }

    public float getGold()
    {
        return gold;
    }

    public void setGold(int gold)
    {
        this.gold += gold;
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

    public string getSkillLevel(int skillID)
    {
        string skillLevel = skillList[skillID].getSkillLevel().ToString("0.00");
        return skillLevel;
    }
}
