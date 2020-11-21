using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderDeadHitbox : MonoBehaviour
{
    // Start is called before the first frame update
    private LevelLogic levelLogic;
    public Rigidbody2D rb;
    public float velocityTreshold = 2f;

    public PlayerTrack cameraTracker;
    void Awake()
    {
        levelLogic = GameObject.Find("Logic").GetComponent<LevelLogic>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Skate"))
        {
            cameraTracker.ScreenShake(rb.velocity.magnitude);
            if (rb.velocity.magnitude > velocityTreshold){
                levelLogic.RespawnPlayer();
            }
        }
    }
}
