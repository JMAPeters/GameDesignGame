using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour
{
    NetworkStartPosition[] spawnPoints;

    public int maxHealth = 100;
    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth;
    public RectTransform healthBar;


    void Start()
    {
        if (isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!isServer)
            return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            RpcRespawn();
        }
    }

    void OnChangeHealth(int currentHealth)
    {
        healthBar.sizeDelta = new Vector2(currentHealth / 2, healthBar.sizeDelta.y); /////////////////////////////////////////////////////////////////////////////////////////////
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            Vector2 spawnPoint = Vector2.zero;

            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }
            transform.position = spawnPoint;
        }
        currentHealth = maxHealth;
    }
} 
