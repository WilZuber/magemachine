using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFXScript : MonoBehaviour
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
        AudioClip playSound = sounds[0];
        if (Pickup.pickupSound) {
            Pickup.pickupSound = false;
            source.PlayOneShot(sounds[0]);
        }
        else if (PlayerAttack.teleportSound) {
            PlayerAttack.teleportSound = false;
            source.PlayOneShot(sounds[1]);
        }
        else if (PlayerAttack.meleeSound) {
            PlayerAttack.meleeSound = false;
            source.PlayOneShot(sounds[2]);
        }
        else if (Explosion.explosionSound) {
            Explosion.explosionSound = false;
            source.PlayOneShot(sounds[3]);
        }
        
    }

    public void TechWeaponSound() {

    }

    public void SoulMagicSound() {

    }

    // public static void TeleportSound() {
    //     AudioClip teleportSound = sounds[1];
    //     source.PlayOneShot(teleportSound);
    //     PlayerAttack.teleportSound = false;
    // }

    public void AOESound() {

    }

    public void PlayerHit() {

    }

    public void PlayerDeath() {

    }

    public void PlayerMelee() {

    }

    public void PlayerJump() {

    }

    public void PlayerWalking() {

    }
}
