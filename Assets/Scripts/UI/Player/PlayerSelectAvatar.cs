using Assets.Scripts.GameLogic;
using Assets.Scripts.UI.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Player
{
    public enum AvatarType
    {
        Boy,
        Girl
    }

    [Serializable]
    public struct AvatarButton
    {
        public string InputButtonName;
        public Button Button;
        public AvatarType Avatar;
    }

    public class PlayerSelectAvatar : MonoBehaviour
    {
        public AvatarButton[] AvatarButtonList;
        public PlayerNumber playerNumber;
        public GameController GameController;

        private void Update()
        {
            if (GameManager.Instance.Paused)
                return;

            foreach (AvatarButton avatarButton in AvatarButtonList)
            {
                if(Input.GetButtonDown(avatarButton.InputButtonName))
                {
                    GameManager.Instance.ButtonClick.Play();
                    avatarButton.Button.onClick.Invoke();
                    SelectAvatar();
                }
            }
        }

        public void SelectAvatar()
        {
            GameController.SelectAvatar(playerNumber);
        }
    }
}
