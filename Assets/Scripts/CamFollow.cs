using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

namespace Prototype.NetworkLobby
{
    public class CamFollow : NetworkBehaviour
    {

        GameObject[] players;
        GameObject player, mainCamera;
        public float Xmin, Xmax, Ymin, Ymax;
        private float magnitude;
        public float MaxdistanceBetweenPlayerAndCam;
        private float mouseAngle;
        Vector2 mousePos;
        Vector2 playerPosition;
        Vector2 offset, camPosition;


        // Update is called once per frame
        void FixedUpdate()
        {
            if (LobbyTopPanel.IsInGame()) 
                {
                

                

                if (player != null)
                {
                    transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -10);

                  
                }
                else
                {
                    player = PlayerSetup.GetPlayer();
                }
            }
        }
    }
}


