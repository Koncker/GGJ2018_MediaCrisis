using Assets.Scripts.ViewModel.News;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.API
{
    [Serializable]
    public struct RedditDataContainer
    {
        public RedditData data;
    }


    [Serializable]
    public struct RedditData
    {
        public string after;
        public RedditPost[] children;
    }

    [Serializable]
    public struct RedditPost
    {
        public RedditPostData data;
    }

    [Serializable]
    public struct RedditPostData
    {
        public string domain;
        public string title;
        public string distinguished;
    }

    public class RedditAPI
    {
        public const string NOT_THE_ONION_LINK = "https://www.reddit.com/r/nottheonion/.json";

        public IEnumerator GetRedditFeed(string link, NewsType newsType, Action<List<NewsItemModel>, string> callback, string after="")
        {
            if (!string.IsNullOrEmpty(after))
            {
                link += "?after=" + after;
            }

            var webRequest = UnityWebRequest.Get(link);
            webRequest.SetRequestHeader("user-agent", "unity:com.mediacrisis:v0.1(by /u/xnape)");
            var operation = webRequest.SendWebRequest();

            yield return operation;
            

            if (webRequest.isDone)
            {
                var newsList = new List<NewsItemModel>();
                var result = webRequest.downloadHandler.text;
                var redditData = JsonUtility.FromJson<RedditDataContainer>(result);
                foreach(RedditPost redditPost in redditData.data.children)
                {
                    if (redditPost.data.domain == "self.nottheonion")
                        continue;

                    var newsItem = new NewsItemModel();
                    newsItem.Title.Value = redditPost.data.title;
                    newsItem.NewsType.Value = newsType;
                    newsList.Add(newsItem);
                }
                callback(newsList, redditData.data.after);
            }
            else
                Debug.Log(webRequest.error);

        }
        
    }
}
