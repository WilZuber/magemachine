using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    bool isPaused = false;
    float pausedTimeScale;
    public GameObject PausePanel;
    void Awake() {
        PausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused && Input.GetKeyDown(KeyCode.Escape)) {
            Pause();
        } else if (isPaused && Input.GetKeyDown(KeyCode.Escape)) {
            Unpause();
        }
    }

    private void Pause() {
        isPaused = true;
        PausePanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        pausedTimeScale = Time.timeScale;
        Time.timeScale = 0; // pause time
    }

    public void Unpause() {
        if (isPaused) {
            isPaused = false;
            PausePanel.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Time.timeScale = pausedTimeScale;
        }
    }

    public void ShowInfoScreen() {
        // show info screen
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
