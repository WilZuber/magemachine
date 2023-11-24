using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject InfoScreen;
    void Awake() {
        InfoScreen.SetActive(false);
    }
    // Update is called once per frame
    void Update() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ShowInfoScreen() {
        InfoScreen.SetActive(true);
    }

    public void InfoScreenBackButton() {
        InfoScreen.SetActive(false);
    }

    public void StartGame() {
        SceneManager.LoadScene("Level1");
        LevelExit.currentLevel = 2;
    }

    public void LoadTutorial() {
        SceneManager.LoadScene("Tutorial");
        LevelExit.currentLevel = 1;
    }

    public void ExitGame() {
        Application.Quit();
    }
}
