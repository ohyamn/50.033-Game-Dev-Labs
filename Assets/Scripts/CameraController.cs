using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform endLimit;
    private float offset;
    private float startX;
    private float endX;
    private float viewportHalfWidth;
    public AudioSource gameMusic;
    // Start is called before the first frame update
    void Start()
    {
        // get coordinate of the bottomleft of the viewport
        // z doesnt matter sine the camera is orthographic
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        viewportHalfWidth = Mathf.Abs(bottomLeft.x - this.transform.position.x);

        offset = this.transform.position.x - player.position.x;
        startX = this.transform.position.x;
        endX = endLimit.transform.position.x - viewportHalfWidth;
        GameManager.OnPlayerDeath += stopMusic;
    }

    // Update is called once per frame
    void Update()
    {
        float desiredX = player.position.x + offset;
        // check if desiredX is within startX and endX
        if (desiredX > startX && desiredX < endX)
            this.transform.position = new Vector3(desiredX, this.transform.position.y, this.transform.position.z);
    }

    void stopMusic()
    {
        gameMusic.Stop();
    }

    void OnDestroy()
    {
        GameManager.OnPlayerDeath -= stopMusic;
    }
}
