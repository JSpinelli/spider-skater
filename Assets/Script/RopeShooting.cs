﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeShooting : MonoBehaviour
{
    public Transform pointerPosition;
    public SpringJoint2D rope;

    public LineRenderer ropeRender;
    public float ropeDistance = 10000;

    private bool ropeAttached = false;

    private bool ropeClimbing = false;
    public float ropeTimer = 1;
    private float remainingRopeTimer = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ropeAttached)
        {
            ropeRender.SetPosition(0, transform.position);
            if (ropeClimbing){
                this.climb();
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] hits;
            Debug.DrawRay(transform.position, (pointerPosition.position - transform.position), Color.green);
            hits = Physics2D.RaycastAll(transform.position, (pointerPosition.position - transform.position), ropeDistance);
            int i = 0;
            bool hit = false;
            while (i < hits.Length && !hit)
            {
                if (hits[i].transform.CompareTag("Ropable"))
                {
                    hit = true;
                    rope.enabled = true;
                    rope.distance = hits[i].distance - 1;
                    rope.connectedAnchor = hits[i].point;
                    Vector3[] line = { transform.position, hits[i].point };
                    ropeRender.useWorldSpace = true;
                    ropeRender.SetPositions(line);
                    ropeRender.enabled = true;
                    ropeAttached = true;
                }
                i++;
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
            rope.distance = rope.distance - 1;
            remainingRopeTimer = ropeTimer;
        }else{
            remainingRopeTimer = remainingRopeTimer - Time.deltaTime;
        }
    }
}
