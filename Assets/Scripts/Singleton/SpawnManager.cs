using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameConstants gameConstants;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        // spawn two goombaEnemy
        for (int j = 0; j < gameConstants.goombaEnemyNumber; j++)
            spawnFromPooler(ObjectType.goombaEnemy);
        // spawn two greenEnemy
        for (int j = 0; j < gameConstants.greenEnemyNumber; j++)
            spawnFromPooler(ObjectType.greenEnemy);
    }
    public void spawnFromPooler(ObjectType i)
    {
        // Debug.Log("Spawn from pooler called");
        // static method access
        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(i);
        if (item != null)
        {
            //set position, and other necessary states
            item.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), gameConstants.groundSurface + 0.5f, -1f);
            item.SetActive(true);
        }
        else
        {
            Debug.Log("not enough items in the pool.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
