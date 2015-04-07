using UnityEngine;
using System.Collections;

public class networkPlayer : MonoBehaviour {
    public float networkPlayerHealth;
    public string networkPlayerName;

    [RPC]
    public void setName(string nameIn)
    {
        networkPlayerName = nameIn;
    }

     [RPC]
    public void setHealth(float healthIn)
    {
        networkPlayerHealth = healthIn;
    }
}
