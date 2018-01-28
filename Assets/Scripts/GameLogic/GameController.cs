using Assets.Scripts.GameLogic.District;
using Assets.Scripts.UI.News;
using Assets.Scripts.ViewModel;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameLogic
{
    public class GameController : MonoBehaviour
    {
        public bool Player1Selected = false;
        public bool Player2Selected = false;

        public RectTransform RulesPanel;
        public RoundController RoundController;

        public DistrictRules Player1DisctrictRules;
        public DistrictRules Player2DisctrictRules;
        public AudioSource HammerSFX;

        public RectTransform PauseMenu;
        public RectTransform GameOverMenu;
        public Button RestartButton;
        public Text GameOverText;

        public int Player1BreakPoints = 0;
        public int Player2BreakPoints = 0;

        public int Player1RecoverPoints = 0;
        public int Player2RecoverPoints = 0;

        public PlayerModel Player1 = new PlayerModel();
        public PlayerModel Player2 = new PlayerModel();

        public Text Player1PointsText;
        public Text Player2PointsText;

        public bool GameEnded()
        {
            var player1Lost = !Player1DisctrictRules.HasFineDistricts();
            var player2Lost = !Player2DisctrictRules.HasFineDistricts();

            return (player1Lost || player2Lost);
            
        }

        public void EndGame()
        {
            var player1Lost = !Player1DisctrictRules.HasFineDistricts();
            var player2Lost = !Player2DisctrictRules.HasFineDistricts();

            if (player1Lost && player2Lost)
            {
                GameOverText.text = "Both player lost!";
            } else if(player1Lost)
            {
                GameOverText.text = "Player 2 WON!";
            } else if(player2Lost)
            {
                GameOverText.text = "Player 1 WON!";
            }

            GameOverMenu.gameObject.SetActive(true);
            RestartButton.Select();
        }

        public void SelectAvatar(PlayerNumber playerNumber)
        {
            if (playerNumber == PlayerNumber.Player_1)
                Player1Selected = true;

            if (playerNumber == PlayerNumber.Player_2)
                Player2Selected = true;

            if (Player1Selected && Player2Selected)
            {
                RulesPanel.gameObject.SetActive(true);
            }
        }

        public void StartGame()
        {
            RoundController.StartRound();
        }

        public void GivePoints(PlayerPointsGained playerPointsGained)
        {
            Player1.PlayerCulture += playerPointsGained.Player1Culture;
            Player1.PlayerIdiocracy += playerPointsGained.Player1Idiocracy;
            var p1built = Player1DisctrictRules.SetDistrictPoints(Player1.PlayerCulture);
            if (p1built)
            {
                Debug.Log("dada");
                HammerSFX.Play();
            }
            if (playerPointsGained.Player1Culture > 0)
            {
                Player1RecoverPoints++;
                var hasSchool = Player1DisctrictRules.HasDistrictType(DistrictType.School);

                int minRecoverPoint;
                if (hasSchool)
                    minRecoverPoint = 3;
                else
                    minRecoverPoint = 5;

                if (Player1RecoverPoints >= minRecoverPoint)
                {
                    Player1DisctrictRules.RecoverDistrict();
                    Player1RecoverPoints = 0;
                }
            }


            if (playerPointsGained.Player1Idiocracy > 0)
            {
                var hasFactChecker = Player1DisctrictRules.HasDistrictType(DistrictType.Fact);
                if (hasFactChecker)
                {
                    Player1BreakPoints++;
                    if (Player1BreakPoints >= 2)
                    {
                        Player1DisctrictRules.BreakDistrict();
                        Player1BreakPoints = 0;
                    }
                } else {
                    Player1DisctrictRules.BreakDistrict();
                    Player1BreakPoints = 0;
                }
            }
            Player1PointsText.text = "Culture: " + Player1.PlayerCulture + " Idiocracy: " + Player1.PlayerIdiocracy;

            Player2.PlayerCulture += playerPointsGained.Player2Culture;
            Player2.PlayerIdiocracy += playerPointsGained.Player2Idiocracy;
            var p2built = Player2DisctrictRules.SetDistrictPoints(Player2.PlayerCulture);
            if (p2built)
            {
                Debug.Log("dada");
                HammerSFX.Play();

            }

            if (playerPointsGained.Player2Idiocracy > 0)
            {
                var hasFactChecker = Player2DisctrictRules.HasDistrictType(DistrictType.Fact);
                if (hasFactChecker)
                {
                    Player2BreakPoints++;
                    if (Player2BreakPoints >= 2)
                    {
                        Player2DisctrictRules.BreakDistrict();
                        Player2BreakPoints = 0;
                    }
                } else {
                    Player2DisctrictRules.BreakDistrict();
                    Player2BreakPoints = 0;
                }
            }

            if (playerPointsGained.Player2Culture > 0)
            {
                Player2RecoverPoints++;
                var hasSchool = Player2DisctrictRules.HasDistrictType(DistrictType.School);

                int minRecoverPoint;
                if (hasSchool)
                    minRecoverPoint = 3;
                else
                    minRecoverPoint = 5;

                if (Player2RecoverPoints >= minRecoverPoint)
                {
                    Player2DisctrictRules.RecoverDistrict();
                    Player2RecoverPoints = 0;
                }
            }
            Player2PointsText.text = "Culture: " + Player2.PlayerCulture + " Idiocracy: " + Player2.PlayerIdiocracy;
        }
    }
}
