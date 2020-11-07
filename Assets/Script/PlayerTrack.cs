using UnityEngine;

public class PlayerTrack : MonoBehaviour {

    public Transform player;

    public float smoothSpeed=10f;

    public Vector3 offset = new Vector3(0,2,-10);

    private void LateUpdate()
    {
        Vector3 desiredPos = player.transform.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        transform.position= smoothPos;
    }
}
