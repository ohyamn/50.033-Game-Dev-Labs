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
public Transform enemyLocation;
public Text scoreText   ;
private int score = 0;
private bool countScoreState = false;
public GameObject restartButton;
private Animator marioAnimator;
private AudioSource marioAudio;
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate =  30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAudio = GetComponent<AudioSource>();
        restartButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown("a") || Input.GetKeyDown("left")) && faceRightState){
            faceRightState = false;
            marioSprite.flipX = true;
            if (Mathf.Abs(marioBody.velocity.x) > 1.0)
                marioAnimator.SetTrigger("onSkid");
        }

        if ((Input.GetKeyDown("d") || Input.GetKeyDown("right")) && !faceRightState){
            faceRightState = true;
            marioSprite.flipX = false;
            if (Mathf.Abs(marioBody.velocity.x) > 1.0)
                marioAnimator.SetTrigger("onSkid");
        }

        if (!onGroundState && countScoreState){
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f){
                countScoreState = false;
                score++;
                Debug.Log(score);
            }
        }

        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        marioAnimator.SetBool("onGround", onGroundState);
        Debug.Log(onGroundState);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")){
            onGroundState = true;
            countScoreState = false; //reset score state
            scoreText.text = "Score:" + score.ToString();
        }

        if(col.gameObject.CompareTag("Obstacle")){
            onGroundState = true;
        }
    }
    void  FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0){
            Vector2 movement = new Vector2(moveHorizontal,0);
            if (marioBody.velocity.magnitude < maxSpeed){
                marioBody.AddForce(movement*speed);
            }
        }
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")){
            //stop
            marioBody.velocity = Vector2.zero;
        }
        if (Input.GetKeyDown("space") && onGroundState){
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            countScoreState = true; //check if gomba is below
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Enemy")){
            Debug.Log("Collided with Gomba!");
            Time.timeScale = 0.0f;
            restartButton.gameObject.SetActive(true);
            score = 0;
        }
    }

    void PlayJumpSound(){
        marioAudio.PlayOneShot(marioAudio.clip);
    }
}
