using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject startButton;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        // GameManager.OnPlayerDeath += playerDiesMenu;
        if (playerStates.oldStart)
        {
            startButton.SetActive(false);
            panel.SetActive(false);
            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        // if (playerStates.oldStart)
        // {
        //     startButton.SetActive(false);
        //     panel.SetActive(false);
        //     Time.timeScale = 1.0f;
        // }
        // else
        // {
        //     Time.timeScale = 0.0f;
        // }
    }

    public void StartButtonClicked()
    {
        foreach (Transform eachChild in transform)
        {
            if (eachChild.name != "Score" & eachChild.name != "Powerups")
            {
                // Debug.Log("Child found. Name:" + eachChild.name);
                eachChild.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
                playerStates.oldStart = true;
            }
        }
    }

    public void RestartButtonClicked()
    {
        Time.timeScale = 1.0f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
        // foreach (Transform eachChild in transform)
        // {
        //     if (eachChild.name == "Restart")
        //     {
        //         eachChild.gameObject.SetActive(false);
        //     }
        // }
    }
}
