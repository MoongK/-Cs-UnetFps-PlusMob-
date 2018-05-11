using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Mobspawner : NetworkBehaviour {

    public GameObject mobPrefab;
    public int numberOfMobs;


    public override void OnStartServer()
    {
        var mob = Instantiate(mobPrefab, transform.position, Quaternion.identity);
        NetworkServer.Spawn(mob);
    }
}
