using System.Collections;
using UnityEngine;

public class EnemyControllerLab4 : MonoBehaviour
{
    public GameConstants gameConstants;
    private int moveRight;
    private float originalX;
    private Vector2 velocity;
    private Rigidbody2D enemyBody;
    private SpriteRenderer enemySprite;
    private bool move = true;

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();

        // get the starting position
        originalX = transform.position.x;

        // randomise initial direction
        moveRight = Random.Range(0, 2) == 0 ? -1 : 1;

        // compute initial velocity
        ComputeVelocity();
        // subscribe to player event
        GameManager.OnPlayerDeath += EnemyRejoice;
    }
    // animation when player is dead
    void EnemyRejoice()
    {
        Debug.Log("Enemy killed Mario");
        move = false;
        StartCoroutine(celebrate(enemySprite));
    }

    IEnumerator celebrate(SpriteRenderer sprite)
    {
        for (int i = 0; i < 20; i++)
        {
            Debug.Log("Celebrating");
            yield return new WaitForSeconds(0.1f);
            if (sprite.flipX)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
        }
        yield break;
    }

    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * gameConstants.maxOffset / gameConstants.enemyPatroltime, 0);
    }

    void MoveEnemy()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // check if it collides with Mario
        if (other.gameObject.tag == "Player")
        {
            // check if collides on top
            float yoffset = (other.transform.position.y - this.transform.position.y);
            if (yoffset > 0.75f)
            {
                KillSelf();
            }
            else
            {
                CentralManager.centralManagerInstance.damagePlayer();
            }
        }
    }

    void KillSelf()
    {
        // enemy dies
        CentralManager.centralManagerInstance.increaseScore();
        StartCoroutine(flatten());
        float rndFloat = Random.Range(-1f, 1f);
        if (rndFloat > 0)
        {
            CentralManager.centralManagerInstance.spawnFromPooler(ObjectType.goombaEnemy);
        }
        else
        {
            CentralManager.centralManagerInstance.spawnFromPooler(ObjectType.greenEnemy);
        }
        Debug.Log("Kill sequence ends");
    }

    IEnumerator flatten()
    {
        Debug.Log("Flatten starts");
        int steps = 5;
        float stepper = 1.0f / (float)steps;

        float originalScale = this.transform.localScale.y;
        for (int i = 0; i < steps; i++)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y - stepper, this.transform.localScale.z);

            // make sure enemy is still above ground
            this.transform.position = new Vector3(this.transform.position.x, gameConstants.groundSurface + GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
            yield return null;
        }
        Debug.Log("Flatten ends");
        this.gameObject.SetActive(false);
        this.transform.localScale = new Vector3(this.transform.localScale.x, originalScale, this.transform.localScale.z);
        Debug.Log("Enemy returned to pool");
        yield break;
    }

    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < gameConstants.maxOffset)
        {// move goomba
            if (move)
            {
                MoveEnemy();
            }
        }
        else
        {
            // change direction
            moveRight *= -1;
            ComputeVelocity();
            if (move)
            {
                MoveEnemy();
            }
        }
    }
}