using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelExit: MonoBehaviour 
{
    public gameObject endLevelText;

    void Start() {
        endLevelText.SetActive(false);
    }

    // Attach this script to the hitbox of the level exit.
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            // Displays text on-screen.
            endLevelText.SetActive(true);
            
            // Add code here for a prompt to start the next level.
        }
    }
}