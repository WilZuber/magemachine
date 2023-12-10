using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public static bool isPaused = false;
    private static PauseScript self;
    static float originalTimeScale;

    public static GameObject player;

    public static GameObject PausePanel;

    public static GameObject PauseCamera;

    public static GameObject InfoScreen;
    public static GameObject TutorialUI;
    void Awake() {
        self = this;
        InfoScreen = GameObject.Find("PauseScreen/Canvas/PausePanel/InfoScreen");
        PausePanel = GameObject.Find("PauseScreen/Canvas/PausePanel");
        PauseCamera = GameObject.Find("PauseScreen/Canvas/PauseCamera");
        player = GameObject.Find("/MainCharacter");

        PauseCamera.SetActive(false);
        InfoScreen.SetActive(false);
        PausePanel.SetActive(false);
    }

    // Called from InventoryManager to prevent conflicts involving escape key
    public static void Toggle()
    {
        if (isPaused)
        {
            self.Unpause();
        }
        else
        {
            self.Pause();
        }
    }

    private void Pause() {
        isPaused = true;
        PausePanel.SetActive(true);
        PausePanel.SetActive(true);
        PausePanel.SetActive(true); // for some reason, spamming this makes the menu work consistently. :)

        originalTimeScale = Time.timeScale;
        Time.timeScale = 0; // pause time

        PlayerInputToggle.Disable();
        HUDToggle.Disable();
    }


    public void Unpause() {
        isPaused = false;
        InfoScreen.SetActive(false);
        PausePanel.SetActive(false);

        Time.timeScale = originalTimeScale;

        PlayerInputToggle.Enable();
        HUDToggle.Enable();
    }

    public void ShowInfoScreen() {
        InfoScreen.SetActive(true);
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
