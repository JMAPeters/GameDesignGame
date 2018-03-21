using UnityEngine;
using UnityEngine.Networking;

namespace Prototype.NetworkLobby
{
    public class PlayerHealth : NetworkBehaviour
    {
        public GameObject player;
        public int maxHealth = 100;
        [SyncVar(hook = "OnChangeHealth")]
        public int currentHealth;
        public RectTransform healthBar;

        static GameObject spawnPoint;
        static bool firstSpawn;

        void Start()
        {
            currentHealth = maxHealth;
        }

        void Update()
        {
            if (firstSpawn)
            {
                if (isLocalPlayer)
                {
                    transform.position = spawnPoint.transform.localPosition;
                    firstSpawn = false;
                }
            }
        }

        public static void FirstSpawn() 
        {
            spawnPoint = PlayerSetup.GetSpawnPoint();
            firstSpawn = true;
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
                transform.position = spawnPoint.transform.position;
                PlayerSetup.SetDeaths();
                MatchManager.TeamDeathCount(PlayerSetup.GetTeam());
            }
            else
            {
                if (PlayerSetup.GetTeam() == "red")
                    MatchManager.TeamDeathCount("blue");
                if (PlayerSetup.GetTeam() == "blue")
                    MatchManager.TeamDeathCount("red");
            }

            currentHealth = maxHealth;
        }
    }
}

