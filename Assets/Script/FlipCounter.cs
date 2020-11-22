using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCounter : MonoBehaviour
{
    public float flips;
    float deltaRotation = 0;
    float currentRotation = 0;
    float WindupRotation = 0;
    private LevelLogic levelLogic;
    private float flipCounter = 0;
    // Start is called before the first frame update
    void Awake()
    {
        levelLogic = GameObject.Find("Logic").GetComponent<LevelLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        deltaRotation = (currentRotation - transform.eulerAngles.z);
        currentRotation = transform.eulerAngles.z;
        if (deltaRotation >= 300)
            deltaRotation -= 360;
        if (deltaRotation <= -300)
            deltaRotation += 360;
        WindupRotation += (deltaRotation);

        flips = WindupRotation / 360;
        if (Mathf.Abs(flips) > 1)
        {
            if (flips > 0)
            {
                flipCounter += Mathf.Abs(Mathf.Floor(flips));
                WindupRotation = WindupRotation - (Mathf.Floor(flips) * 360);
            }
            else
            {
                flipCounter += Mathf.Abs(Mathf.Ceil(flips));
                WindupRotation = WindupRotation - (Mathf.Ceil(flips) * 360);
            }
            levelLogic.updateFlips(flipCounter);
        }
    }
}
