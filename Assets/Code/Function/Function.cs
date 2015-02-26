using UnityEngine;
using System.Collections;

public class Function : MonoBehaviour {
    public static Function instance;
    void Start()
    {
        instance = this;
    }
    public static string equipItem(Item item)
    {
        if (item is Weapon)
        {
            GameObject reach = (GameObject)Instantiate(((Weapon)item).getWeaponReach());
            reach.transform.parent = Player.playerObject.transform;
            reach.transform.position = Player.playerObject.transform.position;
            reach.transform.rotation = Player.playerObject.transform.rotation;
        }
        return Player.player.equip(item.getInventoryID());
    }

    public static string dequipItem(int equipSlot)
    {
        return Player.player.dequip(equipSlot);
    }

    public static string showInventory(){
		string inventory ="Inventory:\n";
		PlayerEntity player = Player.player;
		if(player is PlayerObject){
			PlayerObject temptempPlayer = (PlayerObject)player;
			try{
				for(int i = 0 ; i<temptempPlayer.getInvSize() ; i++){
                    inventory += temptempPlayer.getInventory(i).getItemText() + " " + temptempPlayer.getInventory(i).getInventoryID() + "\n";
				}
				return inventory;
			}catch{
				return inventory;
			}
		}
		return null;
	}

    public static string showEquipment(){
		string equipment = "Equipment: \n";
		PlayerEntity player = Player.player;
		if(player is PlayerObject){
			PlayerObject temptempPlayer = (PlayerObject)player;
			for(int i=0;i<10;i++){
				switch(i){
				case 0:
					if(temptempPlayer.getEquipment(i)==null){
						equipment += "Head: None\n";
					}else{
					equipment +="Head: " + temptempPlayer.getEquipment(i).getItemText()+"\n";
					}
				break;
				case 1:
					if(temptempPlayer.getEquipment(i)==null){
						equipment += "Torso: None\n";
					}else{
					equipment +="Torso: " + temptempPlayer.getEquipment(i).getItemText()+"\n";
					}
				break;
				case 2:
					if(temptempPlayer.getEquipment(i)==null){
						equipment += "Arms: None\n";
					}else{
					equipment +="Arms: " + temptempPlayer.getEquipment(i).getItemText()+"\n";
					}
				break;
				case 3:
					if(temptempPlayer.getEquipment(i)==null){
						equipment += "Hands: None\n";
					}else{
					equipment +="Hands: " + temptempPlayer.getEquipment(i).getItemText()+"\n";
					}
				break;
				case 4:
					if(temptempPlayer.getEquipment(i)==null){
						equipment += "Legs: None\n";
					}else{
					equipment +="Legs: " + temptempPlayer.getEquipment(i).getItemText()+"\n";
					}
				break;
				case 5:
					if(temptempPlayer.getEquipment(i)==null){
						equipment += "Feet: None\n";
					}else{
					equipment +="Feet: " + temptempPlayer.getEquipment(i).getItemText()+"\n";
					}
				break;
				case 6:
					if(temptempPlayer.getEquipment(i)==null){
						equipment += "Main Hand: None\n";
					}else{
					equipment +="Main Hand: " + temptempPlayer.getEquipment(i).getItemText()+"\n";
					}
				break;
				case 7:
					if(temptempPlayer.getEquipment(i)==null){
						equipment += "Off Hand: None\n";
					}else{
					equipment +="Off Hand: " + temptempPlayer.getEquipment(i).getItemText()+"\n";
					}
				break;
				case 8:
					if(temptempPlayer.getEquipment(i)==null){
						equipment += "Neck: None\n";
					}else{
					equipment +="Neck: " + temptempPlayer.getEquipment(i).getItemText()+"\n";
					}
				break;
				case 9:
					if(temptempPlayer.getEquipment(i)==null){
						equipment += "Fingers: None\n";
					}else{
					equipment +="Fingers: " + temptempPlayer.getEquipment(i).getItemText()+"\n";
					}
				break;
				
				}
			}
		}
		return equipment;
	}

    public static string status(){
		PlayerEntity player = Player.player;
		if(player is PlayerObject){
			PlayerObject temptempPlayer = (PlayerObject)player;
			string tempStats = temptempPlayer.getStatus();
			return tempStats;
		}
		return null;
	}

    public static void putOnHotbar(HotbarAble instance, int hotbarSlot)
    {
        if(instance is HotbarAble)
            Player.player.hotbarAdd(instance, hotbarSlot);
        //if(hotbarSlot == 2)
        //    hotbarGuiFunction.instance.setHotbarIcon(instance.getIcon(), hotbarSlot);
    }

    public static void removeFromHotbar(int hotbarSlot)
    {
        Player.player.hotbarRemove(hotbarSlot);
    }

    public static void hotbarUse(int hotbarSlot)
    {
        HotbarAble hotbarType = Player.player.getHotbarType(hotbarSlot);
        if (hotbarType is Weapon)
        {
			if(Player.player.getEquipmentIDinSlot(6) == -1 || hotbarType.getInventoryID() != Player.player.getEquipmentIDinSlot(6)){
            	Debug.Log(equipItem(Player.player.getInventoryItem(hotbarType.getInventoryID())));
			}
        }
        else if (hotbarType is Castable)
        {
            Player.player.setActiveSkill((Castable)hotbarType);
        }
    }

    public static string performAction()
    {
        Castable skill = Player.player.getActiveSkill();
        if (skill.getCurrentCooldown() == 0)
        {
            skill.cast();
            skill.setCurrentCooldown(skill.getCooldown());
            Player.instance.addCooldown(skill);
            skill.updateGainPrCast();
            Player.instance.gainSkill(skill.getGainPrCast(), ((Skill)skill).getSkillID());
            return skill.getCastMsg();
        }
        else
        {
            return "Skill " + ((Skill)skill).getSkillText() + " is on cooldown!";
        }
    }

    public static string performAttack(Weapon weapon)
    {
        GameObject collision = GameObject.Find("Reach");
        Collider[] hitColliders = Physics.OverlapSphere(collision.transform.position, weapon.getWeaponReachFloat());
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.name == "Player2")
            {
                GameObject playerHit = hitColliders[i].gameObject;
                Function controller = (Function) playerHit.GetComponent("Function");
                GameObject test = GameObject.Find("Player2");
            }
        }
        
       return "";
    }

    public static void takeDamage(float damage)
    {
        Player.player.setHealth(damage,0, false);
    }
   
}
