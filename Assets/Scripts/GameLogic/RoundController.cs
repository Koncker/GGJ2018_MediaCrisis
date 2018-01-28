using Assets.Scripts.GameLogic.District;
using Assets.Scripts.UI.News;
using Assets.Scripts.UI.Timer;
using Assets.Scripts.ViewModel.News;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameLogic
{
    public class RoundController : MonoBehaviour
    {
        private YieldInstruction _waitSecond = new WaitForSeconds(1);
        public Button StartRoundButton;

        public GameController GameController;
        public RectTransform Panel;
        public NewsController NewsController;
        public TimerClock RoundTimer;
        public TimerClock ResultTimer;
        public TimerClock DistrictTimer;

        public RectTransform ActiveRoundProps;
        public RectTransform ResultRoundProps;

        public int RoundTime = 30;
        public int ResultsTime = 5;
        public int DistrictTime = 5;

        private NewsItemModel _player1NewsItem;
        private NewsItemModel _player2NewsItem;

        public void StartRound()
        {
            DistrictTimer.gameObject.SetActive(false);
            StartRoundButton.gameObject.SetActive(false);

            Panel.gameObject.SetActive(true);

            NewsController.GetNews(() => {
                ActiveRoundProps.gameObject.SetActive(true);
                RoundTimer.StartTimer(RoundTime, EndRound);
            });
        }


        public void EndRound()
        {
            ActiveRoundProps.gameObject.SetActive(false);
            ResultRoundProps.gameObject.SetActive(true);
            var playerPoints = NewsController.ShowResults(_player1NewsItem, _player2NewsItem);

            GameController.GivePoints(playerPoints);
            ResultTimer.StartTimer(ResultsTime, ShowDistricts);
        }

        public void CheckNextRound()
        {
            if (GameController.GameEnded())
            {
                GameController.EndGame();
            } else
            {
                ResetRound();
                StartRound();
            }
        }

        public void ShowDistricts()
        {
            ResultRoundProps.gameObject.SetActive(false);
            Panel.gameObject.SetActive(false);
            DistrictTimer.gameObject.SetActive(true);
            DistrictTimer.StartTimer(DistrictTime, CheckNextRound);
        }

        public void SelectNews(NewsItem newsItem, PlayerNumber playerNumber)
        {
            if (playerNumber == PlayerNumber.Player_1 && _player1NewsItem == null)
            {
                _player1NewsItem = newsItem.Model;
                if (GameController.Player2DisctrictRules.HasDistrictType(DistrictType.Intelligence)) {
                    newsItem.ShowPlayerChosen(PlayerNumber.Player_1);

                    if (GameController.Player1DisctrictRules.HasDistrictType(DistrictType.CounterIntel))
                    {
                        var newsItemListCopy = NewsController.NewsItemList.GetRange(0, NewsController.NewsItemList.Count);
                        newsItemListCopy.Remove(newsItem);

                        int randomInt = Random.Range(0, newsItemListCopy.Count);
                        var randomNewsItem = newsItemListCopy[randomInt];
                        randomNewsItem.ShowPlayerChosen(PlayerNumber.Player_1);
                    }
                }
            }
            else if (playerNumber == PlayerNumber.Player_2 && _player2NewsItem == null)
            {
                _player2NewsItem = newsItem.Model;
                if (GameController.Player1DisctrictRules.HasDistrictType(DistrictType.Intelligence))
                {
                    newsItem.ShowPlayerChosen(PlayerNumber.Player_2);

                    if (GameController.Player2DisctrictRules.HasDistrictType(DistrictType.CounterIntel))
                    {
                        var newsItemListCopy = NewsController.NewsItemList.GetRange(0, NewsController.NewsItemList.Count);
                        newsItemListCopy.Remove(newsItem);

                        int randomInt = Random.Range(0, newsItemListCopy.Count);
                        var randomNewsItem = newsItemListCopy[randomInt];
                        randomNewsItem.ShowPlayerChosen(PlayerNumber.Player_2);
                    }
                }
            }

            if (IsRoundFinished())
                EndRound();
        }

        private bool IsRoundFinished()
        {
            if (true || Input.GetJoystickNames().Count() > 1)
                return _player1NewsItem != null && _player2NewsItem != null;
            else
                return _player1NewsItem != null;
        }

        private void ResetRound()
        {
            _player1NewsItem = null;
            _player2NewsItem = null;
            ResultRoundProps.gameObject.SetActive(false);
        }
        
    }
}
