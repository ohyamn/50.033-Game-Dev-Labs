using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
<<<<<<< Updated upstream
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
public ParticleSystem dustCloud;
=======
    public float speed;
    private Rigidbody2D marioBody;
    private bool onGroundState = true;
    public float maxSpeed = 10;
    public float upSpeed = 5;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    public Transform enemyLocation;
    public Text scoreText;
    private int score = 0;
    private bool countScoreState = false;
    public GameObject restartButton;
    private Animator marioAnimator;
    private AudioSource marioAudio;
    public ParticleSystem dustCloud;
    private Vector3 scaler;

>>>>>>> Stashed changes
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate =  30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
        marioAudio = GetComponent<AudioSource>();
        restartButton.SetActive(false);
        GameManager.OnPlayerDeath  +=  PlayerDiesSequence;
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

        if (Input.GetKeyDown("z")){
            CentralManager.centralManagerInstance.consumePowerup(KeyCode.Z,this.gameObject);
        }

        if (Input.GetKeyDown("x")){
            CentralManager.centralManagerInstance.consumePowerup(KeyCode.X,this.gameObject);
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
            // scoreText.text = "Score:" + score.ToString();
            dustCloud.Play();
        }

        if(col.gameObject.CompareTag("Obstacle")){
            onGroundState = true;
            dustCloud.Play();
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
<<<<<<< Updated upstream
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Enemy")){
            Debug.Log("Collided with Gomba!");
            Time.timeScale = 0.0f;
            restartButton.gameObject.SetActive(true);
            score = 0;
        }
=======
    void OnTriggerEnter2D(Collider2D other)
    {

>>>>>>> Stashed changes
    }

    void PlayJumpSound(){
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    void  PlayerDiesSequence(){
	    // Mario dies
	    Debug.Log("Mario dies");
	    // do whatever you want here, animate etc
	    // ...
        scaler = transform.localScale / (float) 10;
        StartCoroutine("ScaleOut");
        Time.timeScale = 0.0f;
        // restartButton.gameObject.SetActive(true);
        // score = 0;
    }
    IEnumerator ScaleOut(){
        Vector2 direction = new Vector2(Random.Range(-1.0f, 1.0f), 1);
        marioBody.AddForce(direction.normalized * 10, ForceMode2D.Impulse);

        yield return null;

        for (int step =0; step < 30; step++){
            this.transform.localScale = this.transform.localScale - scaler;
            yield return null;
        }

        Destroy(gameObject);
    }
}
