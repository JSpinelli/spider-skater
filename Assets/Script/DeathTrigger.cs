using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private LevelLogic levelLogic;
    private void Awake()
    {

        levelLogic = GameObject.Find("Logic").GetComponent<LevelLogic>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            levelLogic.RespawnPlayer(Vector3.zero);
        }
    }
}
