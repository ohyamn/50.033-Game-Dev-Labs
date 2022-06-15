using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public AudioSource bumpAudio;
    public SpringJoint2D springJoint;
    public GameObject orangeMushroomPrefab;//mushroom prefab
    public GameObject redMushroomPrefab;//mushroom prefab
    public SpriteRenderer spriteRenderer;
    public Sprite usedQuestionBox;//the sprite that indicates empty box instead
    private bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        bumpAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(hit){
        //     spriteRenderer.sprite = usedQuestionBox;
        // }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && !hit)
        {
            hit = true;
            bumpAudio.PlayOneShot(bumpAudio.clip);
            // ensure we move the object sufficiently
            rigidBody.AddForce(new Vector2(0, rigidBody.mass * 20), ForceMode2D.Impulse);
            // spawn the mushroom prefab slightly above the box
            float rndOutput = Random.Range(-1f, 1f);
            if (rndOutput > 0)
            {
                Instantiate(orangeMushroomPrefab, new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z), Quaternion.identity);
            }
            else
            {
                Instantiate(redMushroomPrefab, new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z), Quaternion.identity);
            }
            StartCoroutine(DisableHittable());
        }
    }

    bool ObjectMovedAndStopped()
    {
        return Mathf.Abs(rigidBody.velocity.magnitude) < 0.01;
    }
    IEnumerator DisableHittable()
    {
        if (!ObjectMovedAndStopped())
        {
            yield return new WaitUntil(() => ObjectMovedAndStopped());
        }

        //continues here when the ObjectMovedAndStopped() returns true
        spriteRenderer.sprite = usedQuestionBox;
        rigidBody.bodyType = RigidbodyType2D.Static;

        //reset box position
        this.transform.localPosition = Vector3.zero;
        springJoint.enabled = false;
    }
}
