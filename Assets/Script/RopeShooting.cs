using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeShooting : MonoBehaviour
{
    public Transform pointerPosition;
    public SpringJoint2D rope;

    public LineRenderer ropeRender;
    public float ropeDistance = 10000;

    private bool ropeAttached = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ropeAttached){
            ropeRender.SetPosition(0,transform.position);
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] hits;
            Debug.DrawRay(transform.position, (pointerPosition.position - transform.position), Color.green);
            hits = Physics2D.RaycastAll(transform.position, (pointerPosition.position - transform.position), ropeDistance);
            foreach (var hit in hits)
            {
                if (hit.transform.CompareTag("Ropable"))
                {
                    rope.enabled = true;
                    rope.distance = hit.distance - 1;
                    rope.connectedAnchor = hit.point;
                    Vector3[] line = {transform.position, hit.point};
                    ropeRender.useWorldSpace = true;
                    ropeRender.SetPositions(line);
                    
                    ropeRender.enabled=true;
                    ropeAttached = true;
                }
            }

        }
        if (Input.GetMouseButtonDown(1)){ 
            ropeRender.enabled=false;
            rope.enabled = false;
            ropeAttached = false;
        }
        if (Input.GetKeyDown(KeyCode.Space)){
            rope.distance = rope.distance -1;
        }

        
    }
}
