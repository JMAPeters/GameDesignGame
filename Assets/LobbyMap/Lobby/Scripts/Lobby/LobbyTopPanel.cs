using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Prototype.NetworkLobby
{
    public class LobbyTopPanel : MonoBehaviour
    {
        public bool isInGame = false;
        public static bool staticIsInGame;

        protected static bool isDisplayed = true;
        protected Image panelImage;

        protected bool chooseCharacter = false;
        protected bool firstSpawn = true;

        public Toggle ToggleScout, ToggleSoldier, ToggleTank;
        static string playerCharacter;
        public Button PlayCharacterButton;

        void Start()
        {
            panelImage = GetComponent<Image>();
        }


        void Update()
        {
            staticIsInGame = isInGame;
            if (!isInGame)
                return;
        
            if (!chooseCharacter)
            {
                if (!ToggleScout.IsActive())
                {
                    ToggleVisibility(!isDisplayed);
                }
            }
            else
            {
                if (firstSpawn)
                {
                    PlayerHealth.FirstSpawn();
                    firstSpawn = false;
                }
            }

            ToggleCharacter();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleVisibility(!isDisplayed);
            }

        }

        public void SetPlayerCharacter()
        {
            ToggleVisibility(!isDisplayed);
        }

        public void ToggleVisibility(bool visible)
        {
            isDisplayed = visible;
            PlayerMovement.ToggleMenu();
            foreach (Transform t in transform)
            {
                t.gameObject.SetActive(isDisplayed);
            }

            if (panelImage != null)
            {
                panelImage.enabled = isDisplayed;
            }
        }

        void ToggleCharacter()
        {
            if (ToggleScout.isOn)
                if (!ToggleSoldier.isOn || !ToggleTank.isOn)
                {
                    playerCharacter = "Scout";
                    ToggleSoldier.interactable = false;
                    ToggleTank.interactable = false;
                    chooseCharacter = true;
                    //change sprite voorbeeld
                }

            if (ToggleSoldier.isOn)
                if (!ToggleScout.isOn || !ToggleTank.isOn)
                {
                    playerCharacter = "Soldier";
                    ToggleScout.interactable = false;
                    ToggleTank.interactable = false;
                    chooseCharacter = true;
                    //change sprite voorbeeld
                }

            if (ToggleTank.isOn)
                if (!ToggleSoldier.isOn || !ToggleScout.isOn)
                {
                    playerCharacter = "Tank";
                    ToggleSoldier.interactable = false;
                    ToggleScout.interactable = false;
                    chooseCharacter = true;
                    //change sprite voorbeeld
                }

            if (!ToggleScout.isOn && !ToggleSoldier.isOn && !ToggleTank.isOn)
            {
                playerCharacter = null;
                ToggleScout.interactable = true;
                ToggleSoldier.interactable = true;
                ToggleTank.interactable = true;
                chooseCharacter = false;
            }

            if (playerCharacter != null)
                PlayCharacterButton.interactable = true;
            else
                PlayCharacterButton.interactable = false;
        }

        public static bool IsInGame()
        {
            return staticIsInGame;
        }
        
        public static bool IsDisplayed()
        {
            return isDisplayed;
        }

        public static string GetPlayerCharacter()
        {
            return playerCharacter;
        }
    }
}