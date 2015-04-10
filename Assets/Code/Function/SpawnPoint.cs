using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

    public GameObject[] spawnPoints = new GameObject[5];
    public string SpawnStoneName;

    public Vector3 getSpawnPoint()
    {
        return spawnPoints[Random.Range(0, 4)].transform.position;
    }

    public string getSpawnStoneName()
    {
        return SpawnStoneName;
    }
}
