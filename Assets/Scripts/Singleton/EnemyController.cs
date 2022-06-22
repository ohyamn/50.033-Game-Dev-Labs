using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float originalX;
    public float maxOffset = 5.0f;
    public float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;
    private SpriteRenderer enemySprite;
    private Rigidbody2D enemyBody;
    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();
        originalX = -transform.position.x;
        enemySprite.flipX = true;
        ComputeVelocity();
    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }

    void MoveGomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }
    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {
            MoveGomba();
        }
        else
        {
            if (enemySprite.flipX)
            {
                enemySprite.flipX = false;
            }
            else
            {
                enemySprite.flipX = true;
            }
            moveRight *= -1;
            ComputeVelocity();
            MoveGomba();
        }
    }
}
