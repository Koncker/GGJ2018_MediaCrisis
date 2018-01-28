using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GameLogic
{
    public class QuitGame : MonoBehaviour
    {
        public int MainMenuIndex;
        public void Quit()
        {
            Time.timeScale = 1.0f;
            GameManager.Instance.BackgroundMusic.Play();

            StartCoroutine(QuitLater());
        }

        private IEnumerator QuitLater()
        {
            yield return null;
            GameManager.Instance.Paused = false;
    		SceneManager.LoadScene(MainMenuIndex);

        }
    }
}
