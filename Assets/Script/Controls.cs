using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public Transform pointerPosition;
    public SpringJoint2D rope;

    public LineRenderer ropeRender;
    public float ropeDistance = 10000;

    private bool ropeAttached = false;

    private bool ropeClimbing = false;
    public float ropeTimer = 1;
    public float distanceToClimb = 1;
    private float remainingRopeTimer = 0;

    public float rotationSpeed = 5f;

    public float distanceRemovedOnContact = 2;

    public float maxVelocity = 10f;

    public Rigidbody2D body;
    public Rigidbody2D skate;

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        body.velocity = Vector2.ClampMagnitude(body.velocity, maxVelocity);
        skate.velocity = Vector2.ClampMagnitude(skate.velocity, maxVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        if (ropeAttached)
        {
            ropeRender.SetPosition(0, transform.position);
            if (ropeClimbing)
            {
                this.climb();
            }
        }
        float horizontal = Input.GetAxis("Horizontal");
        transform.Rotate(
            0f,
            0f,
            horizontal * rotationSpeed
        );
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] hits;
            Debug.DrawRay(transform.position, (pointerPosition.position - transform.position), Color.green);
            hits = Physics2D.RaycastAll(transform.position, (pointerPosition.position - transform.position), ropeDistance);
            int i = 0;
            bool hit = false;
            while (i < hits.Length && !hit)
            {
                //Debug.Log("TAG:" + hits[i].transform.tag);
                if (hits[i].transform.CompareTag("Ropable"))
                {
                    hit = true;
                    rope.enabled = true;
                    rope.distance = hits[i].distance - distanceRemovedOnContact;
                    rope.connectedAnchor = hits[i].point;
                    Vector3[] line = { transform.position, hits[i].point };
                    ropeRender.useWorldSpace = true;
                    ropeRender.SetPositions(line);
                    ropeRender.enabled = true;
                    ropeAttached = true;
                }
                else
                {
                    if (hits[i].transform.CompareTag("Mesh") || hits[i].transform.CompareTag("Player"))
                    {
                        i++;
                    }
                    else
                    {
                        hit = true;
                    }
                }


            }

        }
        if (Input.GetMouseButtonUp(0) && ropeAttached)
        {
            ropeRender.enabled = false;
            rope.enabled = false;
            ropeAttached = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ropeClimbing = true;
            remainingRopeTimer = ropeTimer;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ropeClimbing = false;
            remainingRopeTimer = 0;
        }
    }

    public void climb()
    {
        if (remainingRopeTimer < 0)
        {
            rope.distance = rope.distance - distanceToClimb;
            remainingRopeTimer = ropeTimer;
        }
        else
        {
            remainingRopeTimer = remainingRopeTimer - Time.deltaTime;
        }
    }
}
