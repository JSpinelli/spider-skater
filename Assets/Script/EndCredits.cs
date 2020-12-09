using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndCredits : MonoBehaviour
{
    public float FadeRate;
    public Image image;
    public float targetAlpha = 0f;

    public float titleTime = 10f;
    public GameObject title;
    public float creditsTime = 15f;
    public GameObject credits;

    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        Color curColor = this.image.color;
        if (curColor.a > 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, this.FadeRate * Time.deltaTime);
            Debug.Log(curColor);
            this.image.color = curColor;
        }
        else
        {
            if (titleTime > 0)
            {
                titleTime -= Time.deltaTime;
            }
            else
            {
                title.SetActive(false);
                credits.SetActive(true);
                if (creditsTime > 0)
                {
                    creditsTime -= Time.deltaTime;
                }
                else
                {
                    SceneManager.LoadScene("Climb Hub");
                }
            }
        }
    }

    public void FadeOut()
    {
        this.targetAlpha = 0.0f;
    }

    public void FadeIn()
    {
        this.targetAlpha = 1.0f;
    }
}