using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    // Start is called before the first frame update
    public float jumpForce = 100f;
    public float cooldownTimer = 10f;
    private bool activated = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!activated && other.CompareTag("Player"))
        {
            activated = true;
            var rigidBody = other.GetComponent<Rigidbody2D>();
            Debug.Log(rigidBody);
            rigidBody.AddForce(transform.up * jumpForce);
            StartCoroutine(RechargeAfterDelay());
        }
    }

    IEnumerator RechargeAfterDelay()
    {
        yield return new WaitForSeconds(cooldownTimer);
        Debug.Log("Should be relaoded");
        activated = false;
    }
}
