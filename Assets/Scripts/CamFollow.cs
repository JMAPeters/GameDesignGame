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
        void Update()
        {
            if (LobbyTopPanel.IsInGame()) 
                {
                camPosition = new Vector2(GameObject.Find("Main Camera").transform.position.x, GameObject.Find("Main Camera").transform.position.y);
                mainCamera = GameObject.Find("Main Camera");

                if (player != null)
                {
                    if (!LobbyTopPanel.IsDisplayed())
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        Vector2 prevMousePos = mousePos;
                        mousePos = new Vector2(GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition).x, GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition).y);
                        mouseAngle = Vector2.Angle(prevMousePos, mousePos);
                        float mouseMovementHorizontal = Input.GetAxis("Mouse X");
                        float mouseMovementVertical = Input.GetAxis("Mouse Y");
                        Vector2 movement = new Vector2(mouseMovementHorizontal, mouseMovementVertical);
                        Vector2 camPosition = new Vector2(GameObject.Find("Main Camera").transform.position.x, GameObject.Find("Main Camera").transform.position.y);
                        playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);
                        offset = camPosition - playerPosition;
                        magnitude = offset.magnitude;
                        if (magnitude <= MaxdistanceBetweenPlayerAndCam)
                        {
                            camPosition += movement * 0.3f;
                            mainCamera.transform.position = new Vector3(camPosition.x, camPosition.y, mainCamera.transform.position.z);
                        }
                        else
                        {
                            Vector2 mouseToPlayer = new Vector2(player.transform.position.x - mainCamera.transform.position.x, player.transform.position.y - mainCamera.transform.position.y);
                            Debug.Log(mouseToPlayer);
                            mouseToPlayer.Normalize();
                            camPosition += mouseToPlayer * 1 / (MaxdistanceBetweenPlayerAndCam * (MaxdistanceBetweenPlayerAndCam - mouseToPlayer.magnitude));
                            mainCamera.transform.position = new Vector3(camPosition.x, camPosition.y, mainCamera.transform.position.z);
                        }
                    }
                }
                else
                {
                    player = PlayerSetup.GetPlayer();
                }
            }
        }
    }
}


