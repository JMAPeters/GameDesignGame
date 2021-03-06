﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Prototype.NetworkLobby
{
    public class PlayerSetup : NetworkBehaviour
    {
        public GameObject player;
        static GameObject staticPlayer;
        public NetworkIdentity netID;
        public SpriteRenderer spriteRenderer;

        static GameObject spawnPoint;
        public GameObject spawnPoint_1;
        public GameObject spawnPoint_2;

        public Sprite spriteScout, spriteSoldier, spriteTank;
        [SyncVar]
        string localSprite;
        [SyncVar]
        string serverSprite;

        static string playerName = "";
        public Text playerNameText;
        static string team;
        static int deaths = 0;

        void Start()
        {
            if (player.GetComponent<NetworkIdentity>().isLocalPlayer) 
                staticPlayer = player;

            playerName = LobbyPlayer.GetPlayerName();
            SetSpawnPoint();
        }

        void Update()
        {
            if (isLocalPlayer)
            {
                localSprite = LobbyTopPanel.GetPlayerCharacter();
                CmdSetSprite(localSprite);
            }

            SpriteUpdate();

        }

        [Command]
        void CmdSetSprite(string sprite)
        {
            serverSprite = sprite;
        }

        
        void SpriteUpdate()
        {
            if (serverSprite == "Scout")
            {
                spriteRenderer.sprite = spriteScout;
                ArmRotation.SetSprite("Scout");
            }
            if (serverSprite == "Soldier")
            {
                spriteRenderer.sprite = spriteSoldier;
                ArmRotation.SetSprite("Soldier");
            }
            if (serverSprite == "Tank")
            {
                spriteRenderer.sprite = spriteTank;
                ArmRotation.SetSprite("Tank");
            }
        }

        public void SetSpawnPoint()
        {
            int nid = int.Parse(netID.netId.ToString());

            if (nid % 2 == 0)
            {
                if (isLocalPlayer)
                {
                    team = "red";
                    spawnPoint = spawnPoint_1;
                }
            }
            else
            {
                if (isLocalPlayer)
                {
                    team = "blue";
                    spawnPoint = spawnPoint_2;
                }
            }
        }

        public static GameObject GetSpawnPoint()
        {
            return spawnPoint;
        }

        public static void SetDeaths()
        {
            deaths++;
        }

        public static int GetDeaths()
        {
            return deaths;
        }

        public static string GetTeam()
        {
            return team;
        }

        public static GameObject GetPlayer()
        {
            return staticPlayer;
        }
    }
}

/*            if (!isLocalPlayer)
                return;
            playerNameText.text = playerName;
            if (team == "red")
                playerNameText.color = Color.red;
            if (team == "blue")
                playerNameText.color = Color.blue;
     * 
     CmdSetName(playerName, team); //START
     
     
        [Command]
        void CmdSetName(string name, string team)
        {
            playerName = name;

            RpcPlayerName(name, team);
        }

        [ClientRpc]
        void RpcPlayerName(string name, string team)
        {
            playerNameText.text = name;
            if (team == "red")
                playerNameText.color = Color.red;
            if (team == "blue")
                playerNameText.color = Color.blue;
        }

        void Update()
        {
            RpcPlayerName(playerName, team);
        }
     
     
     */
