using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public AudioSource coinSound;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void consumedBy()
    {
        // give player score
        CentralManager.centralManagerInstance.increaseScore();
        coinSound.PlayOneShot(coinSound.clip);
        float rndFloat = Random.Range(-1f, 1f);
        if (rndFloat > 0)
        {
            CentralManager.centralManagerInstance.spawnFromPooler(ObjectType.goombaEnemy);
        }
        else
        {
            CentralManager.centralManagerInstance.spawnFromPooler(ObjectType.greenEnemy);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // update UI
            consumedBy();
            GetComponent<Collider2D>().enabled = false;
            this.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
