using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTextUpdater : MonoBehaviour
{
    private TMP_Text textObject;
    private string currentLevel;
    void Awake() {
        textObject = GetComponent<TMP_Text>();
    }
    // Update is called once per frame
    void Update()
    {
        // the -1 is due to an off by one error with LevelGenerator.level
        // not being equal to the current stage
        textObject.text = "level " + (LevelGenerator.level - 1).ToString();
    }
}
