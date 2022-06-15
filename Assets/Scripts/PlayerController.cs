using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Rigidbody2D marioBody;
    private bool onGroundState = true;
    public float maxSpeed = 10;
    public float upSpeed = 5;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    public GameObject restartButton;
    private Animator marioAnimator;
    public AudioSource marioJumpAudio;
    public AudioSource marioDieAudio;
    public ParticleSystem dustCloud;
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        // marioAudio = GetComponent<AudioSource>();
        restartButton.SetActive(false);

        GameManager.OnPlayerDeath += PlayerDiesSequence;
    }

    void OnDestroy()
    {
        GameManager.OnPlayerDeath -= PlayerDiesSequence;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown("a") || Input.GetKeyDown("left")) && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (Mathf.Abs(marioBody.velocity.x) > 1.0)
                marioAnimator.SetTrigger("onSkid");
        }

        if ((Input.GetKeyDown("d") || Input.GetKeyDown("right")) && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (Mathf.Abs(marioBody.velocity.x) > 1.0)
                marioAnimator.SetTrigger("onSkid");
        }

        if (Input.GetKeyDown("z"))
        {
            CentralManager.centralManagerInstance.consumePowerup(KeyCode.Z, this.gameObject);
        }

        if (Input.GetKeyDown("x"))
        {
            CentralManager.centralManagerInstance.consumePowerup(KeyCode.X, this.gameObject);
        }

        // if (!onGroundState && countScoreState)
        // {
        //     if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
        //     {
        //         countScoreState = false;
        //         score++;
        //     }
        // }

        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        marioAnimator.SetBool("onGround", onGroundState);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            onGroundState = true;
            dustCloud.Play();
        }

        if (col.gameObject.CompareTag("Obstacle"))
        {
            onGroundState = true;
            dustCloud.Play();
        }
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed)
            {
                marioBody.AddForce(movement * speed);
            }
        }
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            //stop
            marioBody.velocity = Vector2.zero;
        }
        if (Input.GetKeyDown("space") && onGroundState)
        {
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
        }
    }

    void PlayerDiesSequence()
    {
        // Mario dies
        Debug.Log("Mario dies");
        marioDieAudio.PlayOneShot(marioDieAudio.clip);
        // Time.timeScale = 0.0f;
        marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
        marioBody.SetRotation(90);
        GetComponent<Collider2D>().enabled = false;
        restartButton.gameObject.SetActive(true);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.gameObject.CompareTag("Enemy"))
        // {
        //     Debug.Log("Collided with Gomba!");
        //     Time.timeScale = 0.0f;
        //     restartButton.gameObject.SetActive(true);
        //     score = 0;
        // }
    }

    void PlayJumpSound()
    {
        marioJumpAudio.PlayOneShot(marioJumpAudio.clip);
    }
}
