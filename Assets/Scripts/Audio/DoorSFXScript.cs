using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSFXScript : MonoBehaviour
{
    public AudioSource source;
    public List<AudioClip> sounds = new List<AudioClip>();
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update() {
        if (DoorOpen.playDoorSounds) {
            DoorSounds();
        }
    }
    public void DoorSounds() {
        AudioClip doorButton = sounds[0];
        AudioClip doorOpen = sounds[1];
        source.PlayOneShot(doorButton);
        source.PlayOneShot(doorOpen);
        DoorOpen.playDoorSounds = false;
    }

}
