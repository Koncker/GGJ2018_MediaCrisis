using Assets.Scripts.RSS;
using Assets.Scripts.ViewModel.News;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.RSS
{
    public class TryRssFeed : MonoBehaviour
    {
        public RSSReader rssReader = new RSSReader();
        public string link;


        public void GetRSS()
        {
            StartCoroutine(rssReader.GetRss(link, NewsType.Fake, ReceiveRSS));
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
