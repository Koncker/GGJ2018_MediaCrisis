using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public GameObject Pause;
    public Button DefaultButton;

    void Start()
    {
        Pause.gameObject.SetActive(false);
//        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            if (GameManager.Instance.Paused == true)
            {
                GameManager.Instance.ButtonClick.Play();
                Pause.gameObject.SetActive(false);
                GameManager.Instance.Paused = false;
                Time.timeScale = 1.0f;
                GameManager.Instance.BackgroundMusic.Play();
            }
            else
            {
                Pause.gameObject.SetActive(true);
                GameManager.Instance.Paused = true;
                DefaultButton.Select();
                Time.timeScale = 0f;

                GameManager.Instance.BackgroundMusic.Pause();
            }
        }
    }

    public void Resume()
    {
        GameManager.Instance.ButtonClick.Play();
        Pause.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        GameManager.Instance.BackgroundMusic.Play();

        StartCoroutine(ResumeLater());
    }

    private IEnumerator ResumeLater()
    {
        yield return null;
        GameManager.Instance.Paused = false;
    }
}