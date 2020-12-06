using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZonePointer : MonoBehaviour
{
    public GameObject pointer;
    private GameObject endZone;

    public string nameOfObjectToPoint = "EndZone";
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        endZone = GameObject.Find(nameOfObjectToPoint);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (endZone)
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(endZone.transform.position);
            if (pos.x > 0 && pos.x < 1 && pos.y < 1 && pos.y > 0)
            {
                pointer.SetActive(false);
            }
            else
            {
                pointer.SetActive(true);
                pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
                pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
                pos = Camera.main.ViewportToWorldPoint(pos);
                pointer.transform.position = Vector3.Lerp(pointer.transform.position, pos, 0.5f);
                Quaternion rotation = Quaternion.LookRotation(endZone.transform.position - pointer.transform.position, pointer.transform.TransformDirection(Vector3.up));
                pointer.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

                Vector3 theScale = transform.localScale;
                if ((endZone.transform.position.x - pointer.transform.position.x <= 0))
                {
                    if (theScale.x > 0)
                    {
                        theScale.x *= -1;
                        transform.localScale = theScale;
                    }
                }
                else
                {
                    if (theScale.x < 0)
                    {
                        theScale.x *= -1;
                        transform.localScale = theScale;
                    }
                }
            }
        }


        //pointer.transform.LookAt(endZone.transform.position);
    }
}
