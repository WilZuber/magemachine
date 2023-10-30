using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        // Running
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // Horizontal Movement
        float moveX = moveSpeed * Input.GetAxis("Horizontal");
        float moveZ = moveSpeed * Input.GetAxis("Vertical");
        float moveY = rb.velocity.y;
        rb.velocity = (moveX * transform.right) + (moveZ * transform.forward) + (moveY * Vector3.up);

        // Jumping (The 0.0005 thing allows jumping on top of spherical surfaces)
        if(Input.GetKeyDown(KeyCode.Space) && (Math.Abs(rb.velocity.y) <= 0.0005)) 
        {
            rb.AddForce(transform.up * 5, ForceMode.VelocityChange);
        }
    }
}
