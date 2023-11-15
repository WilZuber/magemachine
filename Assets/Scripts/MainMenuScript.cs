using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Update is called once per frame
    void Update() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ShowInfoScreen() {
        // show info screen with keyboard, mouse, and overlayed controls
        print("Show info screen with controls");
    }

    public void StartGame() {
        SceneManager.LoadScene("Level1");
    }

    public void LoadTutorial() {
        SceneManager.LoadScene("Tutorial");
    }
}
