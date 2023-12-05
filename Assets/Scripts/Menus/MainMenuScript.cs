using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject ExpositionScreen;
    public GameObject SkipText;
    public bool HasPressedKeyOnce = false;
    public GameObject InfoScreen;
    void Awake() {
        ExpositionScreen.SetActive(true);
        ExpositionScreen.SetActive(true);
        SkipText.SetActive(false);
        InfoScreen.SetActive(false);
    }
    // Update is called once per frame
    void Update() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SkipIntroCheck(HasPressedKeyOnce);

        // SkipIntroCheck is called before this on purpose to not skip the intro with only one key press
        if (Input.anyKeyDown) {
            SkipText.SetActive(true);
            SkipText.SetActive(true);
            HasPressedKeyOnce = true;
        }
    }

    public void HideExpositionScreen() {
        ExpositionScreen.SetActive(false);
        ExpositionScreen.SetActive(false);
    }

    public void SkipIntroCheck(bool PressedKey) {
        if (PressedKey) {
            if (Input.anyKeyDown) {
                ExpositionScreen.SetActive(false);
                ExpositionScreen.SetActive(false);
            }
        }
    }

    public void ShowInfoScreen() {
        InfoScreen.SetActive(true);
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
