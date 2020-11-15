using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWallMovement : MonoBehaviour
{
    public float movementPerTick = 1;
    public float tickEverySeconds = 1;


    private float timer;

    void Start(){
        timer = tickEverySeconds;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            var newPosition = new Vector3(transform.position.x,transform.position.y,transform.position.z);
            newPosition.x = newPosition.x+ movementPerTick;
            gameObject.transform.position = newPosition;
            timer = tickEverySeconds;
        }
    }
}
