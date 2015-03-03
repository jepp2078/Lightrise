using UnityEngine;
using System.Collections;

public class WeaponHitInfo : MonoBehaviour {
    public float damage;
    public string damageType;
	// Use this for initialization

    void OnTriggerStay(Collider other) {
        if (other.gameObject.name == "Test_Dummy")
        {
            Debug.Log("You hit " + other.gameObject.name + " for " + damage + " " + damageType + " damage!");
            other.gameObject.GetComponent<Function>().takeDamage(damage, damageType);
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
