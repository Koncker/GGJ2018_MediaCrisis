using System;
using Assets.Scripts.ViewModel.News;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.News
{
    public class NewsItem : MonoBehaviour
    {
        public NewsItemModel Model;

        public Text NewsTitle;
        public Image Background;

        public Color NeutralNewsColor;
        public Color FakeNewsColor;
        public Color LegitNewsColor;

        public RectTransform PlayerChoiceContainer;
        public RectTransform Player1Choice;
        public RectTransform Player2Choice;

        public void Bind(NewsItemModel model)
        {
            if (Model != model)
            {
                Unbind();
                Model = model;
                if (Model != null)
                {
                    model.Title.Bind(NewsTitleBinding);
                }
            }
        }
        
        public void ShowResults()
        {
            if (Model.NewsType.Value == NewsType.Fake)
                Background.color = FakeNewsColor;
            else
                Background.color = LegitNewsColor;

        }

        public void HideResult()
        {
            Background.color = NeutralNewsColor;
            PlayerChoiceContainer.gameObject.SetActive(false);
            Player1Choice.gameObject.SetActive(false);
            Player2Choice.gameObject.SetActive(false);
        }

        private void NewsTitleBinding(string newsTitle)
        {
            NewsTitle.text = newsTitle;
        }

        public void Unbind()
        {
            if (Model != null)
            {
                Model.Title.Unbind(NewsTitleBinding);
                Model = null;
            }
        }

        public void ResetNews()
        {
            NewsTitle.text = "";
        }

        public void ShowPlayerChosen(PlayerNumber playerNumber)
        {
            if(!PlayerChoiceContainer.gameObject.activeSelf)
                PlayerChoiceContainer.gameObject.SetActive(true);
           
            if (playerNumber == PlayerNumber.Player_1)
                Player1Choice.gameObject.SetActive(true);

            if (playerNumber == PlayerNumber.Player_2)
                Player2Choice.gameObject.SetActive(true);
        }
    }
}
