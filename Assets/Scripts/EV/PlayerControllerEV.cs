using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerEV : MonoBehaviour
{
    private float force;
    public IntVariable marioUpSpeed;
    public IntVariable marioMaxSpeed;
    public GameConstants gameConstants;
    private SpriteRenderer marioSprite;
    private Rigidbody2D marioBody;
    public AudioSource marioJumpAudio;
    private Animator marioAnimator;
    private bool faceRightState;
    private bool isDead;
    private bool isSpacebarUp = true;
    private bool isADKeyUp = true;
    private bool onGroundState = true;
    public ParticleSystem dustCloud;
    public CustomCastEvent onCast;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        marioSprite = GetComponent<SpriteRenderer>();
        marioBody = GetComponent<Rigidbody2D>();
        marioAnimator = GetComponent<Animator>();
        marioUpSpeed.SetValue(gameConstants.playerMaxJumpSpeed);
        marioMaxSpeed.SetValue(gameConstants.playerStartingMaxSpeed);
        force = gameConstants.playerDefaultForce;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            faceRightState = false;
            marioSprite.flipX = true;
            isADKeyUp = false;
        }

        if (Input.GetKeyDown("d"))
        {
            faceRightState = true;
            marioSprite.flipX = false;
            isADKeyUp = false;
        }
        if ((Input.GetKeyUp("a") || Input.GetKeyUp("d")))
        {
            if (!(Input.GetKey("a") || Input.GetKey("d")))
                isADKeyUp = true;
        }
        if (Input.GetKeyDown("space"))
        {
            isSpacebarUp = false;
            // onGroundState = false;
        }
        if (Input.GetKeyUp("space"))
        {
            isSpacebarUp = true;
        }
        if (Input.GetKeyDown("z"))
        {
            onCast.Invoke(KeyCode.Z);
        }
        if (Input.GetKeyDown("x"))
        {
            onCast.Invoke(KeyCode.X);
        }
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        marioAnimator.SetBool("onGround", onGroundState);
    }
    void FixedUpdate()
    {
        if (!isDead)
        {
            //check if a or d is pressed currently
            if (!isADKeyUp)
            {
                float direction = faceRightState ? 1.0f : -1.0f;
                Vector2 movement = new Vector2(force * direction, 0);
                if (marioBody.velocity.magnitude < marioMaxSpeed.Value)
                    marioBody.AddForce(movement);
            }

            if (!isSpacebarUp && onGroundState)
            {
                marioBody.AddForce(Vector2.up * marioUpSpeed.Value, ForceMode2D.Impulse);
                onGroundState = false;
                // part 2
                marioAnimator.SetBool("onGround", onGroundState);
                // countScoreState = true; //check if goomba is underneath
            }
        }

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
    void PlayJumpSound()
    {
        marioJumpAudio.PlayOneShot(marioJumpAudio.clip);
    }
    public void PlayerDiesSequence()
    {
        isDead = true;
        marioAnimator.SetBool("isDead", true);
        // GetComponent<Collider2D>().enabled = false;
    }
}

