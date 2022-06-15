using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Rigidbody2D coin;
    private Vector3 scaler;

    // Start is called before the first frame update
    void Start()
    {
        scaler = transform.localScale / (float) 10;
        coin = GetComponent<Rigidbody2D>();
        StartCoroutine("ScaleOut");
    }

    IEnumerator ScaleOut(){
        Vector2 direction = new Vector2(Random.Range(-1.0f, 1.0f), 1);
        coin.AddForce(direction.normalized * 10, ForceMode2D.Impulse);
        coin.AddTorque(10, ForceMode2D.Impulse);

        yield return null;

        for (int step =0; step < 30; step++){
            this.transform.localScale = this.transform.localScale - scaler;
            yield return null;
        }

        Destroy(gameObject);
    }
}
