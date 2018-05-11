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

	public void TakeDamage(int amount)
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
    }

    void OnChangeHealth(int health) {
        print("HP: " + currentHealth);
        currentHealth = health;     // Important !
        healthBar.localScale = new Vector3((float)currentHealth / maxHealth, 1f, 1f);        
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
            transform.position = Vector3.zero;
    }

}
