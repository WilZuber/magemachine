using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySFXScript : MonoBehaviour
{
    // Start is called before the first frame update
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
        if (Explosion.bulletFired) {
            source.PlayOneShot(sounds[0]);
            Explosion.bulletFired = false;
        }
    }

    public void EnemyGrunt(string enemy) {
        // bunson grunt
        // ophelia grunt
        // cameron grunt
    }

    public void EnemyAttack(string enemy) {
        // bunson attack
        // ophelia attack
        // cameron attack
    }
}
