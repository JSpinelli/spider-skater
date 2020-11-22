using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Fallable : MonoBehaviour
{
    public float timerToFall = 1;
    public float respawnTimer = 10;
    public Rigidbody2D body;
    private bool shouldFall = false;
    private Vector3 startPos;
    public void Start()
    {
        startPos = body.transform.position;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!shouldFall)
            {
                StartCoroutine(FallAfterDelay());
                StartCoroutine(RespawnAfterDelay());
            }
            shouldFall = true;
        }

    }

    IEnumerator FallAfterDelay()
    {
        yield return new WaitForSeconds(timerToFall);
        body.bodyType  = RigidbodyType2D.Dynamic;
    }

    IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnTimer);
        ResetPos();
    }

    public void ResetPos()
    {
        StopCoroutine(FallAfterDelay());
        shouldFall = false;
        body.transform.position = startPos;
        body.velocity = new Vector2(0, 0);
        body.bodyType  = RigidbodyType2D.Static;
    }
}
