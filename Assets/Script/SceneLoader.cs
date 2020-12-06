using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public string nextLevel;
    public bool automatic;
    public GameObject keyPrompt;

    private bool isIn = false;

    public bool isEnd = false;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && isIn)
        {
            this.loadStage();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (automatic)
            {
                this.loadStage();
            }
            else
            {
                isIn = true;
                keyPrompt.SetActive(true);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isIn = false;
            keyPrompt.SetActive(false);
        }
    }

    private void loadStage()
    {
        if (isEnd)
        {
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name, "completed");
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
            SceneManager.LoadScene(nextLevel);
        }

    }
}
