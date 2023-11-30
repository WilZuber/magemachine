using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    static bool isPaused = false;
    static float originalTimeScale;

    public static GameObject player;

    public static GameObject PausePanel;

    public static GameObject PauseCamera;

    public static GameObject InfoScreen;
    public static GameObject TutorialUI;
    void Awake() {
        InfoScreen = GameObject.Find("PauseScreen/Canvas/PausePanel/InfoScreen");
        PausePanel = GameObject.Find("PauseScreen/Canvas/PausePanel");
        PauseCamera = GameObject.Find("PauseScreen/Canvas/PauseCamera");
        player = GameObject.Find("/MainCharacter");

        PauseCamera.SetActive(false);
        InfoScreen.SetActive(false);
        PausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!isPaused)
            {
                Pause();
            } else {
                Unpause();
            }
        }
    }

    private void Pause() {
        isPaused = true;
        PausePanel.SetActive(true);
        PausePanel.SetActive(true);
        PausePanel.SetActive(true); // for some reason, spamming this makes the menu work consistently. :)

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        originalTimeScale = Time.timeScale;
        Time.timeScale = 0; // pause time

        PauseCamera.SetActive(true);
        player.SetActive(false);
    }


    public void Unpause() {
        isPaused = false;
        InfoScreen.SetActive(false);
        PausePanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = originalTimeScale;

        player.SetActive(true);
        PauseCamera.SetActive(false);
    }

    public void ShowInfoScreen() {
        InfoScreen.SetActive(true);
        PausePanel.SetActive(true);
    }

    public void InfoScreenBackButton() {
        InfoScreen.SetActive(false);
    }

    public void LoadMainMenu() {
        Time.timeScale = originalTimeScale;
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }
}
