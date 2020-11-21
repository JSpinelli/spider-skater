using UnityEngine;

public class PlayerTrack : MonoBehaviour
{

    public Transform player;

    public float smoothSpeed = 10f;

    public Vector3 offset = new Vector3(0, 2, -20);

    public float shakeDuration = 3f;
    public float shakeMagnitude = 3f;
    private float shakeTimer = 0f;

    private bool screenShakeTriggered = false;


    private void LateUpdate()
    {
        Vector3 desiredPos = player.transform.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        transform.position = smoothPos;
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            transform.localPosition = transform.localPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeTimer -= Time.deltaTime;
        }
        else
        {
            shakeTimer = 0f;
            screenShakeTriggered = false;
        }
    }

    public void ScreenShake(float force)
    {
        if (!screenShakeTriggered)
        {
            shakeTimer = shakeDuration;
            screenShakeTriggered = true;
        }

    }
}
