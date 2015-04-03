using UnityEngine;
using System.Collections;

public class WeaponHitInfo : MonoBehaviour {
    public float damage;
    public string damageType;
    public Weapon weapon;
    public int viewID;
    public float hitID;
    public Vector3 force;
	// Use this for initialization

    void OnTriggerStay(Collider other) {
        calculateDamage(other);
    }

    void calculateDamage(Collider other)
    {
        if (other.gameObject.tag == "npc")
        {
            float armorValue = 0;
            switch (damageType)
            {
                case "slashing": armorValue = other.gameObject.GetComponent<NpcObject>().getProtection(damageType); break;
                case "arrow": armorValue = other.gameObject.GetComponent<NpcObject>().getProtection(damageType); break;
            }
            damage -= armorValue;
            PhotonView.Find(viewID).RPC("writeToGui", PhotonTargets.All, "You hit <color=blue>" + other.gameObject.name + "</color> for <color=maroon>" + damage + "</color> " + damageType + " damage!");
            other.gameObject.GetComponent<NpcFunction>().takeDamage(damage, damageType);
            destroy();
        }

        else if (other.gameObject.tag == "Player" && other.gameObject.GetComponentInChildren<PhotonView>().viewID != viewID)
        {
            float armorValue = 0;

            switch (damageType)
            {
                case "slashing": armorValue = other.gameObject.GetComponent<PlayerObject>().getProtection(damageType); break;
                case "arrow": armorValue = other.gameObject.GetComponent<PlayerObject>().getProtection(damageType); break;
            }
            damage -= armorValue;
            other.gameObject.GetComponentInChildren<PhotonView>().RPC("takeDamage", PhotonTargets.All, damage, damageType, hitID);//takeDamage(damage, damageType);
            PhotonView.Find(viewID).RPC("writeToGui", PhotonTargets.All, "You hit <color=blue>" + other.gameObject.name + "</color> for <color=maroon>" + damage + "</color> " + damageType + " damage!", hitID);
            destroy();
        }
        if (force == Vector3.zero)
        {
            destroy();
        }
    }

    public void destroy()
    {
        if (this.transform.parent != null){
            Destroy(this.transform.parent.gameObject);
        }
        else
        {
            Destroy(this.transform.gameObject); 

        }
    }
}
