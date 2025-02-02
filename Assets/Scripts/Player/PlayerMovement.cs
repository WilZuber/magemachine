using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    // do not try to change these in this script, change them in unity,
    // they're public values and they'll be rewritten with 
    // whatever unity has for the gameobject.
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpSpeed = 7.5f;
    private bool jumpPressed;
    private bool jumping;
    private float jumpTime;
    public float jumpTimeout = 0.5f;
    public float extraGravity = 20f;
    private Animator playerAnimator;
    private StaminaManager stamina;
    

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        playerAnimator = gameObject.GetComponent<Animator>();
        stamina = GetComponent<StaminaManager>();
    }
    
    void FixedUpdate()
    {
        // Running
        float moveSpeed = (Input.GetKey(KeyCode.LeftShift) && (stamina.hasStamina)) ? runSpeed : walkSpeed;
        
        // Stamina Management
        if (moveSpeed == runSpeed) {
            stamina.ReduceStamina(2);
        } else if (stamina.StaminaNotFull() && !(Input.GetKey(KeyCode.LeftShift))){
            stamina.RefillStamina(1);
        }

        // Horizontal Movement
        float moveX = moveSpeed * Input.GetAxis("Horizontal");
        float moveZ = moveSpeed * Input.GetAxis("Vertical");

        // Jumping
        float moveY = rb.velocity.y + Jump();

        // Set velocity
        rb.velocity = (moveX * transform.right) + (moveZ * transform.forward) + (moveY * Vector3.up);

        playerAnimator.SetBool("IsJumping", jumping);
    }

    // Added to vertical velocity if the player is jumping
    private float Jump()
    {
        
        if (Input.GetButton("Jump"))
        {
            if (jumping) // Player is already jumping
            {
                jumpTime += Time.fixedDeltaTime;
                // Limit jump height
                if (jumpTime >= jumpTimeout)
                {
                    jumping = false;
                }
                return 0; // maintain vertical velocity
            }
            else if (!jumpPressed) // Don't jump again until button is released
            {
                jumpPressed = true;
                Vector3 origin = transform.position + 0.005f * Vector3.up; // start above ground
                // Whether the player can actually jump
                bool isGrounded = Physics.Raycast(origin, Vector3.down, 0.01f);
                if (isGrounded)
                {
                    // Start jump
                    jumping = true;
                    jumpTime = 0;
                    return jumpSpeed;
                }
            }
        }
        else
        {
            jumping = false;
            jumpPressed = false;
        }

        return -extraGravity; // fall
    }
}
