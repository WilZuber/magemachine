using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float lookSpeed = 5f;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float lookX = lookSpeed * Input.GetAxis("Mouse X");
        float lookY = lookSpeed * Input.GetAxis("Mouse Y");
        player.transform.Rotate(0, lookX, 0);
        transform.Rotate(-lookY, 0, 0);
    }
}
