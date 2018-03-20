using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Prototype.NetworkLobby
{
    //Main menu, mainly only a bunch of callback called by the UI (setup throught the Inspector)
    public class LobbyMainMenu : MonoBehaviour 
    {
        public LobbyManager lobbyManager;

        public RectTransform lobbyServerList;
        public RectTransform lobbyPanel;

        public InputField matchNameInput;

        public Toggle Toggle1v1;
        public Toggle Toggle2v2;
        public Toggle ToggleTD;
        public Toggle ToggleFFA;

        uint roomSize = 0;
        static int gameMode = 0;

        public void OnEnable()
        {
            lobbyManager.topPanel.ToggleVisibility(true);


            matchNameInput.onEndEdit.RemoveAllListeners();
            matchNameInput.onEndEdit.AddListener(onEndEditGameName);
        }

        void Update()
        {
            ToggleMenu();
        }

        private void ToggleMenu()
        {
            if (Toggle1v1.isOn)
            {
                roomSize = 2;
                ToggleTD.interactable = false;
                ToggleFFA.interactable = true;
            }

            if (Toggle2v2.isOn)
            {
                roomSize = 4;
                ToggleTD.interactable = true;
                ToggleFFA.interactable = false;
            }

            if ((!Toggle1v1.isOn && !Toggle2v2.isOn) || Toggle1v1.isOn && Toggle2v2.isOn)
            {
                roomSize = 0;
                ToggleTD.interactable = false;
                ToggleFFA.interactable = false;
            }

            if (ToggleTD.isOn)
                gameMode = 2; // TeamDeathmatch

            if (ToggleFFA.isOn)
                gameMode = 1; //FreeForAll

            if (!ToggleTD.isOn && !ToggleFFA.isOn)
                gameMode = 0; //nonGamemode
        }

        public void OnClickHost()
        {
            lobbyManager.StartHost();
        }

        public void OnClickJoin()
        {
            lobbyManager.ChangeTo(lobbyPanel);

            lobbyManager.StartClient();

            lobbyManager.backDelegate = lobbyManager.StopClientClbk;
            lobbyManager.DisplayIsConnecting();

            lobbyManager.SetServerInfo("Connecting...", lobbyManager.networkAddress);
        }

        public void OnClickDedicated()
        {
            lobbyManager.ChangeTo(null);
            lobbyManager.StartServer();

            lobbyManager.backDelegate = lobbyManager.StopServerClbk;

            lobbyManager.SetServerInfo("Dedicated Server", lobbyManager.networkAddress);
        }

        public void OnClickCreateMatchmakingGame()
        {
            if (roomSize != 0 && gameMode != 0)
            {
                lobbyManager.StartMatchMaker();
                lobbyManager.matchMaker.CreateMatch(matchNameInput.text, roomSize, true, "", "", "", gameMode, 0, lobbyManager.OnMatchCreate);
                lobbyManager.backDelegate = lobbyManager.StopHost;
                lobbyManager._isMatchmaking = true;
                lobbyManager.DisplayIsConnecting();

                lobbyManager.SetServerInfo("Matchmaker Host", lobbyManager.matchHost);
            }
        }

        public void OnClickOpenServerList()
        {
            if (roomSize != 0 && gameMode != 0)
            {
                lobbyManager.StartMatchMaker();
                lobbyManager.backDelegate = lobbyManager.SimpleBackClbk;
                lobbyManager.ChangeTo(lobbyServerList);
            }
        }

        void onEndEditIP(string text)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                OnClickJoin();
            }
        }

        void onEndEditGameName(string text)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                OnClickCreateMatchmakingGame();
            }
        }

        public static int GetGameMode()
        {
            return gameMode;
        }

    }
}
