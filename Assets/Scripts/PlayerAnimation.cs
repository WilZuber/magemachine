using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("HasLeftGun", true);
        anim.SetBool("HasRightGun", true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        anim.SetFloat("HVelocity", Input.GetAxis("Horizontal"));
        anim.SetFloat("VVelocity", Input.GetAxis("Vertical"));
    }
}
