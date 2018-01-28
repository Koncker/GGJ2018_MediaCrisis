using Assets.Scripts.UI.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    [ExecuteInEditMode]
    public class KeyImageChanger : MonoBehaviour
    {
        public Image KeyImage;
        public Sprite KeyboardImage;
        public Sprite ControllerImage;

        public PlayerNumber playerNum;


        private void Update()
        {
            var controlerInputs = Input.GetJoystickNames();
            var controlerInputCount = 0;
            foreach (var controlerInput in controlerInputs)
            {
                if (!string.IsNullOrEmpty(controlerInput))
                    controlerInputCount++;
            }

            if (playerNum == PlayerNumber.Player_1)
            {
                if (controlerInputCount > 1)
                {
                    KeyImage.sprite = ControllerImage;
                } else
                {
                    KeyImage.sprite = KeyboardImage;
                }
            } else if (playerNum == PlayerNumber.Player_2)
            {
                if (controlerInputCount > 0)
                {
                    KeyImage.sprite = ControllerImage;
                }
                else
                {
                    KeyImage.sprite = KeyboardImage;
                }
            }
        }
    }
}
