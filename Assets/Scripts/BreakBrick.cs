using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrick : MonoBehaviour
{
    private bool broken = false;
    public GameObject debrisPrefab;

    public GameObject coinPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){
        Debug.Log("help");
        if (col.gameObject.CompareTag("Player") && !broken){
            broken = true;

            for (int x = 0; x<5; x++){
                Instantiate(debrisPrefab, transform.position, Quaternion.identity);
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
            }
            gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
<<<<<<< Updated upstream
            GetComponent<EdgeCollider2D>().enabled= false;
=======
            GetComponent<EdgeCollider2D>().enabled = false;
            CentralManager.centralManagerInstance.increaseScore();
>>>>>>> Stashed changes
        }
        Destroy(gameObject);
    }
}
