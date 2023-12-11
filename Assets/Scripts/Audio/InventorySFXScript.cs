using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySFXScript : MonoBehaviour
{
    public AudioSource source;
    public List<AudioClip> sounds = new List<AudioClip>();
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Inventory.menuSound) {
            MenuSound();
        }
    }

    public void MenuSound() {
        AudioClip sound = sounds[0];
        source.PlayOneShot(sound);
        Inventory.menuSound = false;
    }
}
