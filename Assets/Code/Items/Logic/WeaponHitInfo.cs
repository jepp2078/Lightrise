using UnityEngine;
using System.Collections;

public class WeaponHitInfo : MonoBehaviour {
    public float damage;
    public string damageType;
    public Player playerInstance;
    public Weapon weapon;
	// Use this for initialization

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "npc")
        {
            Debug.Log("You hit " + other.gameObject.name + " for " + damage + " " + damageType + " damage!");
            other.gameObject.GetComponent<NpcFunction>().takeDamage(damage, damageType);

            playerInstance.gainSkill((1.1f - (playerInstance.player.getSkillLevel(playerInstance.player.getWeaponSkillId(weapon.getType()) / 100))), playerInstance.player.getWeaponSkillId(weapon.getType()));

            if (playerInstance.player.getWeaponSkill(null, weapon.getType()) != 0)
            {
                playerInstance.gainSkill((1.05f - (playerInstance.player.getSkillLevel(playerInstance.player.getWeaponSkillId(weapon.getType()) / 100))), playerInstance.player.getWeaponMasterySkillId(weapon.getType()));
            }
        }else if (other.gameObject.tag == "Player")
        {
            Debug.Log("You hit " + other.gameObject.name + " for " + damage + " " + damageType + " damage!");
            other.gameObject.GetComponent<Function>().takeDamage(damage, damageType);
        }

        destroy();
    }

    public void destroy()
    {
        Destroy(this.transform.parent.gameObject);
    }
}
