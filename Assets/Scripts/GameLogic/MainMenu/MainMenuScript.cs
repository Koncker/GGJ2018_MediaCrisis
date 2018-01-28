using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    public GameObject ExitGameSelected;
    public GameObject ExitGameSelectedText;
    public GameObject PlayGameSelected;
    public GameObject PlayGameSelectedText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxisRaw("Vertical") < 0 && PlayGameSelected.activeSelf)
        {
            GameManager.Instance.ButtonClick.Play();
            ExitGameSelected.SetActive(true);
            ExitGameSelectedText.SetActive(true);
            PlayGameSelected.SetActive(false);
            PlayGameSelectedText.SetActive(false);
        
        }

        if (Input.GetAxisRaw("Vertical") > 0 && ExitGameSelected.activeSelf)
        {
            GameManager.Instance.ButtonClick.Play();
            ExitGameSelected.SetActive(false);
            ExitGameSelectedText.SetActive(false);
            PlayGameSelected.SetActive(true);
            PlayGameSelectedText.SetActive(true);
        }

        LoadByIndex();
        ExitGame();
	}

    public void LoadByIndex()
        {
            if (Input.GetButtonDown("Submit") && PlayGameSelected.activeSelf)
            {
            GameManager.Instance.ButtonClick.Play();
            SceneManager.LoadScene (1);
            }
        }

    public void ExitGame()
    {
        if (Input.GetButtonDown("Submit") && ExitGameSelected.activeSelf)
        {
            GameManager.Instance.ButtonClick.Play();
            Application.Quit();
            Debug.Log("Game is exiting");
        }
    }
}
