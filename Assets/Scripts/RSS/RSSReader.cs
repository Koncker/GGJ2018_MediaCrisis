using Assets.Scripts.ViewModel.News;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.RSS
{
    public enum FeedType
    {
        News,
    }

    public class RSSReader
    {
        public IEnumerator GetRss(string link, NewsType newsType, Action<List<NewsItemModel>> callback)
        {
            var webRequest = UnityWebRequest.Get(link);
            var operation = webRequest.SendWebRequest();

            yield return operation;
            
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log(webRequest.error);
                Debug.Log(webRequest.responseCode); 
            }
            else
            {
                var result = webRequest.downloadHandler.text;
                callback(NewsRSSReader.ReadRSSText(result, newsType));
            }
        }
    }
}
