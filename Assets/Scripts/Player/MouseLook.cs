using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float lookSpeedX = 5f;
    public float lookSpeedY = 3f;
    public float minV = -90f;
    public float maxV = 90f;
    public Vector3 angles;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //minV += 360;
    }

    // Update is called once per frame
    void Update()
    {
        float lookX = lookSpeedX * Input.GetAxis("Mouse X");
        float lookY = lookSpeedY * Input.GetAxis("Mouse Y");
        player.transform.Rotate(0, lookX, 0);

        angles = transform.rotation.eulerAngles;
        //flip if necessary
        float currentV = (angles.z < 170) ? angles.x : 180 - angles.x;
        //Map from  [0, 360) to (-180, 180]
        if (currentV > 180)
        {
            currentV -= 360;
        }
        float nextV = currentV - lookY;
        nextV = Mathf.Clamp(nextV, minV, maxV);
        transform.Rotate(nextV - currentV, 0, 0);
    }
}
