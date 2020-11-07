using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLogic : MonoBehaviour
{
    public GameObject player;
    public Transform spawnTransform;
    // Start is called before the first frame update
    public void RespawnPlayer(){
        player.transform.position = spawnTransform.position;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
