using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this has methods callable by players
public class CentralManager : MonoBehaviour
{
    public GameObject gameManagerObject;
    private GameManager gameManager;
    public static CentralManager centralManagerInstance;
    public GameObject powerupManagerObject;
    private PowerupManager powerUpManager;

    public GameObject spawnManagerObject;
    private SpawnManager spawnManager;


    void Awake()
    {
        centralManagerInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = gameManagerObject.GetComponent<GameManager>();
        powerUpManager = powerupManagerObject.GetComponent<PowerupManager>();
        spawnManager = spawnManagerObject.GetComponent<SpawnManager>();
    }
    public void damagePlayer()
    {
        gameManager.damagePlayer();
    }

    public void increaseScore()
    {
        gameManager.increaseScore();
    }
    public void consumePowerup(KeyCode k, GameObject g)
    {
        powerUpManager.consumePowerup(k, g);
    }

    public void addPowerup(Texture t, int i, ConsumableInterface c)
    {
        powerUpManager.addPowerup(t, i, c);
    }

    public void spawnFromPooler(ObjectType i)
    {
        spawnManager.spawnFromPooler(i);
    }
}