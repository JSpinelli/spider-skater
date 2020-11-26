using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWallMovement : MonoBehaviour
{
    public float movementPerTick = 1;
    public float tickEverySeconds = 1;


    private float timer;

    public bool right = false;
    public bool left = false;
    public bool up = false;
    public bool down = false;

    private Vector3 respawnPosition;

    void Start()
    {
        timer = tickEverySeconds;
        respawnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    public void Respawn(){
        gameObject.transform.position = respawnPosition;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            var newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            if (right)
            {
                newPosition.x = newPosition.x + movementPerTick;
            }
            if (left)
            {
                newPosition.x = newPosition.x - movementPerTick;
            }
            if (up)
            {
                newPosition.y = newPosition.y + movementPerTick;
            }
            if (down)
            {
                newPosition.y = newPosition.y - movementPerTick;
            }
            gameObject.transform.position = newPosition;
            timer = tickEverySeconds;
        }
    }
}
