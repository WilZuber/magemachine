using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBackButton : MonoBehaviour
{
    // the main menu info screen uses this to hide the info screen
    // becuase if you use the main menu script, Awake() will set the exposition screen to active
    // when the info screen is revealed
    public GameObject InfoScreen;
    public void HideInfoScreen() {
        InfoScreen.SetActive(false);
    }
}
