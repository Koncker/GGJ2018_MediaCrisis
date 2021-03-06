﻿using Assets.Scripts.ViewModel.News;
using System.Collections.Generic;
using System.Xml;

namespace Assets.Scripts.RSS
{
    public static class NewsRSSReader
    {
        public static List<NewsItemModel> ReadRSSText(string feed, NewsType newsType)
        {
            var newsList = new List<NewsItemModel>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(feed);
            XmlNodeList xmlNodeList = xmlDoc.SelectNodes("rss/channel/item");
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                var titleNode = xmlNode.SelectSingleNode("title");

                var newsItem = new NewsItemModel();
                newsItem.Title.Value = titleNode.InnerText;
                newsList.Add(newsItem);
            }

            return newsList;
        }
    }
}
