using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace Prototype.NetworkLobby
{
    public class MatchManager : NetworkBehaviour
    {

        LobbyManager lobbyManager;

        float timeLeft;
        public Text timer;
        static int totalDeathsRed = 0;
        static int totalDeathsBlue = 0;
        public Text deathCountRed;
        public Text deathCountBlue;
        int teamDeaths;
        public Text playerStats;

        public GameObject DisplayWon;
        public Text WonText;

        void Start()
        {
            lobbyManager = LobbyManager.s_Singleton;
            if (LobbyMainMenu.GetGameMode() == 1) //FFA
            {
                timeLeft = 180;
                teamDeaths = 15;
            }
            if (LobbyMainMenu.GetGameMode() == 2) //TD
            {
                timeLeft = 600;
                teamDeaths = 50;
            }
        }

        private void FixedUpdate()
        {
            Timer();
            CheckWinGame();
            UpdateTeamDeathCount();
            PlayerStats();
        }

        void Timer()
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                int minutes = Mathf.FloorToInt(timeLeft / 60f);
                int seconds = Mathf.RoundToInt(timeLeft % 60);

                timer.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            }
            else
            {
                if (totalDeathsRed == totalDeathsBlue)
                    GameWon("draw");
                if (totalDeathsRed < totalDeathsBlue)
                    GameWon("red");
                if (totalDeathsRed > totalDeathsBlue)
                    GameWon("blue");
            }
        }

        void CheckWinGame()
        {
            if (totalDeathsRed == teamDeaths)
                GameWon("blue");
            if (totalDeathsBlue == teamDeaths)
                GameWon("red");
        }

        void GameWon(string team)
        {
            PlayerMovement.ToggleMenu();
            DisplayWon.SetActive(true);
            if (team == "draw")
                WonText.text = "It's a draw!";

            if (team == "red")
                WonText.text = "Team RED won!";

            if (team == "blue")
                WonText.text = "Team BLUE won!";
        }

        void GoBackToLobby()
        {
            lobbyManager.ServerReturnToLobby();
        }

        public static void TeamDeathCount(string team)
        {
            if (team == "red")
                totalDeathsRed++;
            if (team == "blue")
                totalDeathsBlue++;
        }

        void UpdateTeamDeathCount()
        {
            deathCountRed.text = "" + totalDeathsRed;
            deathCountBlue.text = "" + totalDeathsBlue;
        }

        void PlayerStats()
        {
            int deathCount = PlayerSetup.GetDeaths();
            int ammo = GunSpecs.ammo;

            playerStats.text = "Deaths: " + deathCount + " Ammo: " + ammo;
        }
    }
}
