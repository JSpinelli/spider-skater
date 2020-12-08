using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRestriction : MonoBehaviour
{
    public string[] scenes;

    public GameObject obstacle;
    // Start is called before the first frame update
    void Start()
    {
        bool result = true;
        for (int i = 0; i < scenes.Length; i++)
        {
            var sceneStatus = PlayerPrefs.GetString(scenes[i],"not completed");
            Debug.Log(scenes[i]);
            Debug.Log(sceneStatus);
            result = result && (sceneStatus != "not completed");
        } 
        if (result){
            obstacle.SetActive(false);
        }else{
            obstacle.SetActive(true);
        }
    }
}
