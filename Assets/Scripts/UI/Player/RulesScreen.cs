using Assets.Scripts.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.UI.Player
{
    public class RulesScreen : MonoBehaviour
    {
        public RectTransform RulesPanel;
        public GameController GameController;

        public bool PlayerOnePressed;
        public bool PlayerTwoPressed;

        public string PlayerOneButton = "Select A P1";
        public string PlayerTwoButton = "Select A P2";

        public void Update()
        {
            if (GameManager.Instance.Paused)
                return;
            if (Input.GetButtonDown(PlayerOneButton))
            {
                GameManager.Instance.ButtonClick.Play();
                PlayerOnePressed = true;
            }

            if (Input.GetButtonDown(PlayerTwoButton))
            {
                GameManager.Instance.ButtonClick.Play();
                PlayerTwoPressed = true;
            }

            if (PlayerOnePressed && PlayerTwoPressed)
                StartGame();
        }

        private void StartGame()
        {
            RulesPanel.gameObject.SetActive(false);
            GameController.StartGame();
        }
    }
}
