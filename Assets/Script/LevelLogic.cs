using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    // Start is called before the first frame update
    public void RespawnPlayer(){
        if (currentPlayer){
            Destroy(currentPlayer);
        }
        currentPlayer = Instantiate(player, spawnTransform.position, Quaternion.identity);
        playerTrack.player = currentPlayer.transform;
        var controls = player.GetComponent<Controls>();
        var ropeJoint = player.GetComponent<SpringJoint2D>();
        controls.ropeRender = ropeRender;
        controls.rope = ropeJoint;
        controls.pointerPosition = pointerPosition;
        flipCounter.text = "Flips: 0";
    }
    public void updateFlips(float flips){
        Debug.Log(flips);
        flipCounter.text = "Flips: "+flips;
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
