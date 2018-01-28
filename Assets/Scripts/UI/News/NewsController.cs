using Assets.Scripts.API;
using Assets.Scripts.GameLogic;
using Assets.Scripts.RSS;
using Assets.Scripts.UI.Timer;
using Assets.Scripts.Util;
using Assets.Scripts.ViewModel.News;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.UI.News
{
    public enum PlayerNumber
    {
        Player_1,
        Player_2
    }

    [Serializable]
    public struct PlayerPointsGained
    {
        public int Player1Culture;
        public int Player1Idiocracy;

        public int Player2Culture;
        public int Player2Idiocracy;
    }

    public class NewsController : MonoBehaviour
    {
        public RoundController RoundController;
        public List<string> FakeNewsRssLinkList = new List<string>();
        private string redditAfter = "";
        public RectTransform TransmitingPanel;
        private YieldInstruction WaitSeconds = new WaitForSeconds(1);

        public int minNews = 2;
        public string[] playerOneindexButtonList = new string[]{
            "Select X P1",
            "Select Y P1",
            "Select A P1",
            "Select B P1",
        };


        public string[] playerTwoindexButtonList = new string[]{
            "Select X P2",
            "Select Y P2",
            "Select A P2",
            "Select B P2",
        };

        public RSSReader RSSReader = new RSSReader();
        public RedditAPI RedditAPI = new RedditAPI();

        public List<NewsItem> NewsItemList = new List<NewsItem>();
        private List<NewsItemModel> _fakeNewsModelList = new List<NewsItemModel>();
        private List<NewsItemModel> _legitNewsModelList = new List<NewsItemModel>();


        private List<NewsItemModel> _usedFakeNewsModelList = new List<NewsItemModel>();
        private List<NewsItemModel> _usedLegitNewsModelList = new List<NewsItemModel>();

        //public List<string> LegitNewsLinks;
        //public List<string> FakeNewsLinks;

        private bool _showingResult = false;

        public NewsController()
        {
            FakeNewsRssLinkList.Shuffle();
        }

        public void GetNews(Action callback)
        {
            ResetNews();
            StartCoroutine(GetNewNews(callback));
        }


        public void Update()
        {
            if (GameManager.Instance.Paused)
                return;

            if (!_showingResult)
            {
                CheckPlayerSelect(PlayerNumber.Player_1);
                CheckPlayerSelect(PlayerNumber.Player_2);
            }
        }


        public PlayerPointsGained ShowResults(NewsItemModel player1NewsItem, NewsItemModel player2NewsItem)
        {
            _showingResult = true;
            PlayerPointsGained playerPoints = new PlayerPointsGained();

            foreach (NewsItem newsItem in NewsItemList)
            {
                newsItem.HideResult();
                newsItem.ShowResults();
                if (newsItem.Model == player1NewsItem)
                {
                    if (newsItem.Model.NewsType.Value == NewsType.Fake)
                        playerPoints.Player1Idiocracy++;

                    if (newsItem.Model.NewsType.Value == NewsType.Legit)
                        playerPoints.Player1Culture++;
                    newsItem.ShowPlayerChosen(PlayerNumber.Player_1);
                }

                if (newsItem.Model == player2NewsItem)
                {
                    if (newsItem.Model.NewsType.Value == NewsType.Fake)
                        playerPoints.Player2Idiocracy++;

                    if (newsItem.Model.NewsType.Value == NewsType.Legit)
                        playerPoints.Player2Culture++;
                    newsItem.ShowPlayerChosen(PlayerNumber.Player_2);
                }
            }

            return playerPoints;
        }

        private void ResetNews()
        {
            _showingResult = false;
            foreach (NewsItem newsItem in NewsItemList)
            {
                newsItem.ResetNews();
                newsItem.HideResult();
            }
        }

        private void CheckPlayerSelect(PlayerNumber playerNumber)
        {
            if (!RoundController.ActiveRoundProps.gameObject.activeInHierarchy)
                return;

            int index = 0;
            string[] buttonList;
            if (playerNumber == PlayerNumber.Player_1)
            {
                buttonList = playerOneindexButtonList;
            } else
            {
                buttonList = playerTwoindexButtonList;
            }

            foreach (var indexButton in buttonList)
            {
                if (Input.GetButtonDown(indexButton))
                {
                    GameManager.Instance.ButtonClick.Play();
                    RoundController.SelectNews(NewsItemList[index], playerNumber);
                    break;
                }
                index++;
            }
        }

        /* More news handling */

        private IEnumerator GetNewNews(Action callback)
        {
            if(NeedMoreNews())
            {
                TransmitingPanel.gameObject.SetActive(true);
                yield return CheckFakeNews();
                yield return CheckLegitNews();
                TransmitingPanel.gameObject.SetActive(false);
            } else
            {
                TransmitingPanel.gameObject.SetActive(true);
                yield return WaitSeconds;
                TransmitingPanel.gameObject.SetActive(false);
            }


            var fakeNewsCount = Mathf.CeilToInt(NewsItemList.Count / 2f);
            var legitNewsCount = Mathf.FloorToInt(NewsItemList.Count / 2f);
            var newNewsModelList = new List<NewsItemModel>();

            for(int i = 0; i < fakeNewsCount; i++)
            {
                var newsItem = _fakeNewsModelList[0];
                newNewsModelList.Add(newsItem);
                _usedFakeNewsModelList.Add(newsItem);
                _fakeNewsModelList.RemoveAt(0);
            }
            for (int i = 0; i < legitNewsCount; i++)
            {
                var newsItem = _legitNewsModelList[0];
                newNewsModelList.Add(newsItem);
                _usedLegitNewsModelList.Add(newsItem);
                _legitNewsModelList.RemoveAt(0);
            }

            newNewsModelList.Shuffle();
            for (int i = 0; i < newNewsModelList.Count; i++)
            {
                NewsItemList[i].Bind(newNewsModelList[i]);
            }

            if (callback != null)
                callback();
        }

    
        private IEnumerator CheckLegitNews()
        {
            yield return RedditAPI.GetRedditFeed(RedditAPI.NOT_THE_ONION_LINK, NewsType.Legit, ReceiveLegitNews, redditAfter);
        }

        private void ReceiveLegitNews(List<NewsItemModel> _newsList, string after)
        {
            _legitNewsModelList.AddRange(_newsList);
            _legitNewsModelList.Shuffle();
            redditAfter = after;
        }

        private IEnumerator CheckFakeNews()
        {
            if (FakeNewsRssLinkList.Count == 0)
            {
                Debug.Log("No more fake news links. Recycling!");
                _fakeNewsModelList.Clear();
                _fakeNewsModelList.AddRange(_usedFakeNewsModelList);
                _fakeNewsModelList.Shuffle();
                _usedFakeNewsModelList.Clear();
            }
            else
            {
                yield return GetFakeNews();
            }
        }


        private IEnumerator GetFakeNews()
        {
            var nextFakeNewsLink = FakeNewsRssLinkList.Last();
            yield return RSSReader.GetRss(nextFakeNewsLink, NewsType.Fake, ReceiveFakeNews);
        }

        private void ReceiveFakeNews(List<NewsItemModel> fakeNews)
        {
            _fakeNewsModelList.AddRange(fakeNews);
            _fakeNewsModelList.Shuffle();
        }

        private bool NeedMoreNews()
        {
            return _fakeNewsModelList.Count < minNews || _legitNewsModelList.Count < minNews;
        }

    }
}
