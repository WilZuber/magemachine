using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelExit: MonoBehaviour
{
    public static string[] levelList = {"MainMenu", "Tutorial", "Level1", "Level2"};
    private AudioSource levelExitAudio;
    private bool audioPlayed;
    // This assumes the script is being used on the Tutorial level first.
    public static int currentLevel;
    //public GameObject endLevelText;

    void Start() {
    //    endLevelText.SetActive(false);
        levelExitAudio = GetComponent<AudioSource>();
        audioPlayed = false;
    }

    // Attach this script to the hitbox of the level exit.
    void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            // Displays text on-screen.
            //endLevelText.SetActive(true);
            
          
            // Add a prompt here to start the next level.
            if (Input.GetKey(KeyCode.R)) {
                if (!audioPlayed)
                {
                    audioPlayed = true;
                    levelExitAudio.Play();
                }
                Inventory.FinishLevel();
                if (currentLevel == 1)
                {
                    SceneManager.LoadScene("MainMenu");
                }
                else if ((currentLevel > 1) && (currentLevel < 3)) {
                    currentLevel += 1;
                    SceneManager.LoadScene(levelList[currentLevel]);
                } else {
                    //print("ERROR: There are only four levels currently.");
                    SceneManager.LoadScene("Level1"); //rework once procedural levels are fully implemented
                }
            }
        }
    }
}