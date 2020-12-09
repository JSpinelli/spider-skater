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

    public float respawnTimer = 2f;

    private GameObject currentPlayer = null;

    public TextMeshProUGUI flipCounter;
    public TextMeshProUGUI discoveredPrompt;

    private JointDetacher jointDetacher;
    private Controls playercontrols;
    [System.NonSerialized] public bool respawning = false;

    private Vector3 currentRespawnSet;

    // Start is called before the first frame update
    public void RespawnPlayer(Vector3 position)
    {
        if (!respawning)
        {
            respawning = true;
            if (position == Vector3.zero)
            {
                StartCoroutine(Respawn(spawnTransform.position));
            }
            else
            {
                StartCoroutine(Respawn(position));
            }
            currentRespawnSet = position;

        }
    }

    IEnumerator Respawn(Vector3 position)
    {
        if (currentPlayer)
        {
            playercontrols.DisableControls();
            jointDetacher.detachThings();
            yield return new WaitForSeconds(respawnTimer);
            Destroy(currentPlayer);
        }
        currentPlayer = Instantiate(player, position, Quaternion.identity);
        playerTrack.player = currentPlayer.transform;
        playercontrols = currentPlayer.GetComponent<Controls>();
        var ropeJoint = currentPlayer.GetComponent<SpringJoint2D>();
        var hitBox = currentPlayer.GetComponentInChildren<SpiderDeadHitbox>();
        jointDetacher = currentPlayer.GetComponent<JointDetacher>();
        hitBox.cameraTracker = playerTrack;
        playercontrols.ropeRender = ropeRender;
        playercontrols.rope = ropeJoint;
        playercontrols.pointerPosition = pointerPosition;
        respawning = false;
        if (flipCounter)
        {
            flipCounter.text = "Flips: 0";
        }
        DeathWallMovement deathWall = FindObjectOfType<DeathWallMovement>();
        if (deathWall)
        {
            deathWall.Respawn();
        }

    }
    public void updateFlips(float flips)
    {
        if (flipCounter)
            flipCounter.text = "Flips: " + flips;
    }
    void Start()
    {
        RespawnPlayer(spawnTransform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Backspace))
        {
            PlayerPrefs.DeleteAll();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            currentRespawnSet = spawnTransform.position;
        }
    }
}
