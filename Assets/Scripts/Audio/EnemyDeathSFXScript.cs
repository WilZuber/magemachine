using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathSFXScript : MonoBehaviour
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
        if (HealthManager.enemyIsDead) {
            source.PlayOneShot(sounds[0]);
            HealthManager.enemyIsDead = false;
        }
    }
}
