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

        private AudioSource source;
        public AudioClip deathSound;
        public AudioClip killSound;

        void Start()
        {
            currentHealth = maxHealth;
            source = player.GetComponent<AudioSource>();
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
               
                if (isLocalPlayer)
                {
                    source.clip = deathSound;
                    source.Play();
                }

                else
                {
                    source.clip = killSound;
                    source.Play();
                }
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

