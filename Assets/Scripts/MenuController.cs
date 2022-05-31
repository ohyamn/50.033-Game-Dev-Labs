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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake(){
        if (playerStates.oldStart){
            startButton.SetActive(false);
            panel.SetActive(false);
            Time.timeScale = 1.0f;
        }else{
            Time.timeScale = 0.0f;
        }
    }

    public void StartButtonClicked(){
        foreach (Transform eachChild in transform){
            if (eachChild.name != "Score"){
                Debug.Log("Child found. Name:" + eachChild.name);
                eachChild.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
                playerStates.oldStart = true;
            }
        }
    }

    public void RestartButtonClicked(){
        foreach (Transform eachChild in transform){
            if (eachChild.name == "Restart"){
                Time.timeScale = 1.0f;
                eachChild.gameObject.SetActive(false);
                UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
            }
        }
    }
}
