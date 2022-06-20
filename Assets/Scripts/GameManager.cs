using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public Text score;
    public GameObject Panel;
    public GameObject RestartButton; 
    private int playerScore = 0;
    private static GameManager _instance;
    private static GameManager Instance{
        get{return _instance;}
    }
    override  public  void  Awake(){
		base.Awake();
		Debug.Log("awake called");
		// other instructions...
	}
    public void increaseScore()
    {
        playerScore += 1;
        score.text = "SCORE: " + playerScore.ToString();
        Debug.Log("Score: " + playerScore.ToString());
    }
    public void damagePlayer()
    {
        OnPlayerDeath();
    }
    public delegate void gameEvent();
    public static event gameEvent OnPlayerDeath;

}