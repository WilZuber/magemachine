using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public static bool playDoorSounds;
    private Animator anim;
    private AudioSource buttonSound;
    // Start is called before the first frame update
    void Start()
    {
        playDoorSounds = false;
        anim = GetComponent<Animator>();
        //this script is attached to the doorface object which is a sibling of button
        //needs to be more specific if other door sounds added
        buttonSound = transform.parent.GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("InteractionPressed", Input.GetKeyDown(KeyCode.R));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetBool("PlayerInRange", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetBool("PlayerInRange", false);
        }
    }
    public void playButtonNoise()
    {
        //buttonSound.Play();
        playDoorSounds = true;
    }
}
