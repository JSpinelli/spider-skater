using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    // Start is called before the first frame update
    public float jumpForce = 100f;
    public float cooldownTimer = 10f;
    private bool activated = false;

    public bool isDirectional = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!activated && other.CompareTag("Skate"))
        {
            activated = true;
            var rigidBody = other.GetComponent<Rigidbody2D>();
            if (isDirectional)
            {
                var direction = Vector3.Dot(rigidBody.velocity.normalized, transform.up);
                if (direction > -0.5f)
                {
                    rigidBody.AddForce(transform.up * jumpForce);
                    StartCoroutine(RechargeAfterDelay());
                }
            }
            else
            {
                rigidBody.AddForce(transform.up * jumpForce);
                StartCoroutine(RechargeAfterDelay());
            }
            
        }
    }

    IEnumerator RechargeAfterDelay()
    {
        yield return new WaitForSeconds(cooldownTimer);
        activated = false;
    }
}
