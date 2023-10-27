using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelExit: MonoBehaviour
{
    public static string[] levelList = {"Main Menu", "Tutorial", "Level1", "Level2"};

    // This assumes the script is being used on the Tutorial level first.
    public static int currentLevel = 1;
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
            if (Input.GetKeyCode(KeyCode.R)) {
                if (currentLevel < 3) {
                    currentLevel += 1;
                    SceneManager.LoadScene(levelList[currentLevel]);
                } else {
                    print("ERROR: There are only four levels currently.");
                }
                
            }
        }
    }
}