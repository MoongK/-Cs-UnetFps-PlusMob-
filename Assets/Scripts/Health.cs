using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class Health : NetworkBehaviour
{
    public const int maxHealth = 100;

    public bool destroyOnDeath;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    public RectTransform healthBar;

    //public Camera SendmyCam()
    //{
    //    if (isLocalPlayer)
    //    {
    //        print("hi local! - " + transform.root.name);
    //        return transform.Find("Camera").GetComponent<Camera>();
    //    }
    //    else
    //    {
    //        print("Null Cam send : " + transform.root.name);
    //        return null;
    //    }
            
    //}

    public void TakeDamage(int amount, GameObject WhoShootedMe)
    {
        if (!isServer)
            return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            //currentHealth = 0;
            print("Dead!");

            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
            else
            {
                currentHealth = maxHealth;

                // called on the Server, will be invoked on the Clients
                RpcRespawn();
            }
        }
        if (transform.root.gameObject.layer == 12)
        {
            if ((WhoShootedMe != null) && (WhoShootedMe.layer == 11))
            {
                transform.GetComponent<Animator>().GetBehaviour<RoamingWalk>().targetPlayer = WhoShootedMe.transform;
            }
        }
    }

    void OnChangeHealth(int health) {
        currentHealth = health;     // Important !
        healthBar.localScale = new Vector3((float)currentHealth / maxHealth, 1f, 1f);        
        print("HP: " + currentHealth);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
            transform.position = Vector3.zero;
    }

}
