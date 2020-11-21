using UnityEngine;
using System.Collections;
using TMPro;
public class LevelLogic : MonoBehaviour
{
    public GameObject player;
    public LineRenderer ropeRender;
    public Transform pointerPosition;
    public Transform spawnTransform;

    public PlayerTrack playerTrack;

    private GameObject currentPlayer = null;

    public TextMeshProUGUI flipCounter;

    private JointDetacher jointDetacher;
    private bool respawning = false;

    // Start is called before the first frame update
    public void RespawnPlayer()
    {
        if (!respawning)
        {   
            respawning = true;
            StartCoroutine(Example());
        }


    }

    IEnumerator Example()
    {
        if (currentPlayer)
        {
            Debug.Log("WE HAVE SPIDER");
            jointDetacher.detachThings();
            yield return new WaitForSeconds(5);
            Destroy(currentPlayer);
        }
        Debug.Log("TRIGGERED");
        currentPlayer = Instantiate(player, spawnTransform.position, Quaternion.identity);
        playerTrack.player = currentPlayer.transform;
        var controls = player.GetComponent<Controls>();
        var ropeJoint = player.GetComponent<SpringJoint2D>();
        var hitBox = player.GetComponentInChildren<SpiderDeadHitbox>();
        jointDetacher = currentPlayer.GetComponent<JointDetacher>();
        hitBox.cameraTracker = playerTrack;
        controls.ropeRender = ropeRender;
        controls.rope = ropeJoint;
        controls.pointerPosition = pointerPosition;
        respawning = false;
        flipCounter.text = "Flips: 0";
    }
    public void updateFlips(float flips)
    {
        Debug.Log(flips);
        flipCounter.text = "Flips: " + flips;
    }
    void Start()
    {
        RespawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
