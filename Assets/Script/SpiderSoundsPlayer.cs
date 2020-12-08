using UnityEngine;

public class SpiderSoundsPlayer : MonoBehaviour
{
    public Rigidbody2D spiderBody;
    public GroundedDetection gDetector;
    public Controls controls;

    public float speedTreshold =1f;

    public AudioSource skateSound;
    public AudioSource landSound;
    public AudioSource deathSound;
    public AudioSource jumpSound;
    public AudioSource airSound;

    private bool isPlayingSkateSound = false;
    private bool isPlayingWindSound = false;
    private bool deathSoundPlayed = false;


    // Update is called once per frame
    void Update()
    {
        //Play Skate Rolling Sound
        if (!controls.spiderDead && gDetector.m_Grounded && !isPlayingSkateSound && spiderBody.velocity.magnitude > speedTreshold)
        {
            skateSound.volume = spiderBody.velocity.magnitude /80;
            skateSound.Play();
            isPlayingSkateSound = true;
        }

        if (isPlayingSkateSound){
            skateSound.volume = spiderBody.velocity.magnitude / 80;
        }
        if (isPlayingWindSound){
            airSound.volume = spiderBody.velocity.magnitude / 150;
        }


        //Stop Skate Rolling Sound and Play Jump sound
        if (!controls.spiderDead && !gDetector.m_Grounded && isPlayingSkateSound)
        {
            skateSound.Stop();
            isPlayingSkateSound = false;
            //jumpSound.Play();
        }

        //Stop Skate Rolling Sound because speed
        if (!controls.spiderDead && spiderBody.velocity.magnitude < speedTreshold && isPlayingSkateSound)
        {
            skateSound.Stop();
            isPlayingSkateSound = false;
        }

        //Play Wind Blowing Sound
        if (!controls.spiderDead && !gDetector.m_Grounded && !isPlayingWindSound && spiderBody.velocity.magnitude > speedTreshold)
        {
            isPlayingWindSound = true;
            airSound.Play();
        }

        //Stop Wind Blowing Sound and Land sound
        if (!controls.spiderDead && gDetector.m_Grounded && isPlayingWindSound)
        {
            Debug.Log("Playing Land Sound");
            isPlayingWindSound = false;
            airSound.Stop();
            landSound.Play();
        }

        //Play Death Sound
        if (controls.spiderDead && !deathSoundPlayed)
        {
            deathSoundPlayed = true;
            deathSound.Play();
        }
    }
}
