using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    private Rigidbody2D shroomBody;
    public float speed;
    private Vector2 currentPosition;
    private Vector2 currentDirection;
    private Vector2 nextPosition;
    private bool collected = false;
    // Start is called before the first frame update
    void Start()
    {
        shroomBody = GetComponent<Rigidbody2D>();
        // Set direction for shroom
        currentDirection = new Vector2(Random.Range(-1f, 1f), 0);
        // Allow mushroom to spring out of box
        shroomBody.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = shroomBody.position;
        // currentDirection = new Vector2(Random.Range(-1f, 1f), 0);
        nextPosition = currentPosition + speed * currentDirection.normalized * Time.fixedDeltaTime;
        shroomBody.MovePosition(nextPosition);
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.CompareTag("Pipe") || col.gameObject.CompareTag("Wall")){
            currentDirection = -currentDirection;
        }
        if(col.gameObject.CompareTag("Player")){
            speed = 0;
            shroomBody.velocity = Vector2.zero;
            consume();
            collected = true;

        }
    }

    IEnumerator consume(){
        gameObject.transform.localScale = new  Vector3(this.transform.localScale.x*2f, this.transform.localScale.y*2f, this.transform.localScale.z);
        yield break;
    }
    void OnBecameInvisible(){
        // Destroy(gameObject);
    }
}
