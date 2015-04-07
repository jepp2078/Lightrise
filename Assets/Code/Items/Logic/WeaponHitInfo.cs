using UnityEngine;
using System.Collections;

public class WeaponHitInfo : MonoBehaviour {
    public float damage;
    public string damageType;
    public Weapon weapon;
    public int viewID;
    public float hitID;
    public Vector3 force;
    private float lifeTime = 10;
    public bool collider = false;
	// Use this for initialization

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
            destroy();
    }

    void OnTriggerStay(Collider other) {
        calculateDamage(other);
    }

    void calculateDamage(Collider other)
    {
        if (other.gameObject.tag == "npc")
        {
            if (collider)
            {
                float armorValue = 0;
                switch (damageType)
                {
                    case "slashing": armorValue = other.gameObject.GetComponent<NpcObject>().getProtection(damageType); break;
                    case "arrow": armorValue = other.gameObject.GetComponent<NpcObject>().getProtection(damageType); break;
                }
                damage -= armorValue;
                other.gameObject.GetComponent<NpcFunction>().takeDamage(damage, damageType);
            }
            other.gameObject.GetComponentInParent<PhotonView>().RPC("takeDamage", PhotonTargets.All, damage, damageType, hitID, viewID);//takeDamage(damage, damageType);
            destroy();
        }
        else if (other.gameObject.tag == "PlayerCapsule" && other.gameObject.GetComponentInParent<PhotonView>().viewID != viewID)
        {
            if (collider)
            {
                float armorValue = 0;
                switch (damageType)
                {
                    case "slashing": armorValue = other.gameObject.GetComponentInParent<PlayerObject>().getProtection(damageType); break;
                    case "arrow": armorValue = other.gameObject.GetComponentInParent<PlayerObject>().getProtection(damageType); break;
                }
                damage -= armorValue;
                other.gameObject.GetComponentInParent<PhotonView>().RPC("takeDamage", PhotonTargets.All, damage, damageType, hitID, viewID);//takeDamage(damage, damageType);
            }
            destroy();
        }
        if (force == Vector3.zero)
        {
            destroy();
        }
        else if (force != Vector3.zero && other.gameObject.tag != "nonCollide")
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
