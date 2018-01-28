using Assets.Scripts.API;
using Assets.Scripts.ViewModel.News;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.RSS
{
    public class TryRedditFeed : MonoBehaviour
    {
        public RedditAPI rssReader = new RedditAPI();

        public void GetRSS()
        {
            //StartCoroutine(rssReader.GetRedditFeed(ReceiveRSS));
        }

        private void ReceiveRSS(List<NewsItemModel> feed)
        {
            foreach (NewsItemModel item in feed)
            {
                Debug.Log(item.Title);
            }
        }
    }
}
