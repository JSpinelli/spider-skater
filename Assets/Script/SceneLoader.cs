using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    public string nextLevel;
    public bool automatic;
    public GameObject keyPrompt;

    private bool isIn = false;

    public bool isEnd = false;

    public bool isRespawn = false;
    public int respawnLevel = 1;

    private LevelLogic levelLogic;

    private bool imCompleted = false;

    private void Awake()
    {
        levelLogic = GameObject.Find("Logic").GetComponent<LevelLogic>();
        var status = PlayerPrefs.GetString(nextLevel, "notcompleted");
        imCompleted = status == "completed";
        Debug.Log(nextLevel+"Is completed? "+imCompleted);
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && isIn)
        {
            this.loadStage();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && respawnLevel==1 )
        {
            levelLogic.RespawnPlayer(transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && respawnLevel==2)
        {
            levelLogic.RespawnPlayer(transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && respawnLevel==3 )
        {
            levelLogic.RespawnPlayer(transform.position);
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
                if (isRespawn)
                {
                    //levelLogic.respawnActivated(respawnLevel, transform.position);
                }
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
