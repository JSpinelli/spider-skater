using UnityEngine.SceneManagement;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public Transform pointerPosition;
    public SpringJoint2D rope;

    public LineRenderer ropeRender;
    public float ropeDistance = 10000;

    private bool ropeAttached = false;

    private bool ropeClimbing = false;
    public float ropeTimer = 0.2f;
    public float distanceToClimb = 1;
    private float remainingRopeTimer = 0;

    public float rotationSpeed = 5f;

    public float distanceRemovedOnContact = 2;

    public float maxVelocity = 10f;

    public float swingForce = 1f;

    public Rigidbody2D body;
    public Rigidbody2D skate;


    // Rope Animation variables
    public float timeToCastRope = 0.5f;
    private float timerToCastRope;
    private RaycastHit2D attachPoint;
    private bool ropeAnimTriggered = false;
    private bool shouldAttach = false;

    private bool mouseIsDown = false;

    public GroundedDetection gDetector;

    [System.NonSerialized] public bool spiderDead = false;


    private bool controlsDisabled = false;

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

    public void DisableControls()
    {
        spiderDead = true;
        controlsDisabled = true;
        ropeRender.enabled = false;
        rope.enabled = false;
        ropeAttached = false;
    }
    public void EnableControls()
    {
        controlsDisabled = false;
        spiderDead = false;
    }
    void RopeAnimation()
    {
        if (ropeAnimTriggered)
        {
            if (timerToCastRope > 0)
            {
                timerToCastRope -= Time.deltaTime;
                Vector3[] line = { transform.position, this.GetPointDistanceFromObject(1 - (timerToCastRope / timeToCastRope)) };
                ropeRender.SetPositions(line);
            }
            else
            {
                ropeAnimTriggered = false;
                if (shouldAttach && mouseIsDown)
                {
                    rope.enabled = true;
                    rope.connectedAnchor = attachPoint.point;
                    rope.distance = attachPoint.distance - distanceRemovedOnContact;
                    Vector3[] line = { transform.position, attachPoint.point };
                    ropeRender.SetPositions(line);
                    ropeAttached = true;

                }
                else
                {
                    ropeRender.enabled = false;
                    Vector3[] line = { transform.position, transform.position };
                    ropeRender.SetPositions(line);
                }

            }
        }

    }
    Vector2 GetPointDistanceFromObject(float percentageTraveled)
    {
        Vector2 directionOfTravel = attachPoint.point - new Vector2(transform.position.x, transform.position.y);
        float dist = Vector3.Distance(new Vector2(transform.position.x, transform.position.y), attachPoint.point);
        Vector2 finalDirection = directionOfTravel.normalized * (percentageTraveled * dist);
        Vector2 targetPosition = new Vector2(transform.position.x, transform.position.y) + finalDirection;
        return targetPosition;
    }
    void Update()
    {
        RopeAnimation();
        if (!controlsDisabled)
        {
            if (ropeAttached)
            {
                ropeRender.SetPosition(0, transform.position);
                ropeRender.SetPosition(1, rope.connectedAnchor);
                this.climb();
                body.AddForce(new Vector2(Input.GetAxis("Horizontal") * swingForce, 0));
            }
            else
            {
                float horizontal = -Input.GetAxis("Horizontal");
                transform.Rotate(
                    0f,
                    0f,
                    horizontal * rotationSpeed
                );
            }

            if (Input.GetMouseButtonDown(0))
            {
                mouseIsDown = true;
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
                        shouldAttach = true;
                    }
                    else
                    {
                        if (hits[i].transform.CompareTag("Mesh") || hits[i].transform.CompareTag("Player") || hits[i].transform.CompareTag("Skate") || hits[i].transform.CompareTag("Object"))
                        {
                            i++;
                        }
                        else
                        {
                            hit = true;
                            shouldAttach = false;
                        }
                    }
                }
                if (hit)
                {
                    attachPoint = hits[i];
                    ropeAnimTriggered = true;
                    ropeRender.useWorldSpace = true;
                    timerToCastRope = timeToCastRope;
                    ropeRender.enabled = true;
                }

            }
            if (Input.GetMouseButtonUp(0))
            {
                mouseIsDown = false;
                ropeRender.enabled = false;
                Vector3[] line = { transform.position, transform.position };
                ropeRender.SetPositions(line);
                rope.enabled = false;
                ropeAttached = false;
            }
            if (Input.GetKeyUp(KeyCode.R))
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }

    }

    public void climb()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            if (remainingRopeTimer <= 0)
            {
                if (Input.GetAxis("Vertical") > 0)
                {
                    rope.distance = rope.distance - distanceToClimb;
                }
                else
                {
                    rope.distance = rope.distance + distanceToClimb;
                }

                remainingRopeTimer = ropeTimer;
            }
            else
            {
                remainingRopeTimer = remainingRopeTimer - Time.deltaTime;
            }
        }
        else
        {
            remainingRopeTimer = ropeTimer;
        }

    }
}
