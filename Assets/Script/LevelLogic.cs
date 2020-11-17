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
        player.transform.rotation = Quaternion.identity;
        var rb2d = player.GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0,0);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
