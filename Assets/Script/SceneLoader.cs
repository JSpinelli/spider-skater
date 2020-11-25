using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public string nextLevel;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.N))
        {
           SceneManager.LoadScene(nextLevel);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
