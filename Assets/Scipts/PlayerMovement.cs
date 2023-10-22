using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerBody;
    public float moveSpeed = 5f;
    public float lookSpeed = 5f;
    private Vector2 rotation = new Vector2(0, 0);

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerBody = gameObject.GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        // Horizontal Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * Time.deltaTime * moveSpeed);

        // Jumping (The 0.0005 thing allows jumping on top of spherical surfaces)
        if(Input.GetKeyDown(KeyCode.Space) && (Math.Abs(playerBody.velocity.y) <= 0.0005)) 
        {
            playerBody.AddForce(transform.up * 5, ForceMode.VelocityChange);
        }

        // Looking around
        rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
        transform.eulerAngles = rotation;

        // Running
        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 8f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 5f;
        }
    }
}
