using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputToggle : MonoBehaviour
{
    private static PlayerInputToggle input;
    public MonoBehaviour[] inputScripts;
    
    // Start is called before the first frame update
    void Start()
    {
        input = this;
    }

    public static void Disable()
    {
        foreach (MonoBehaviour script in input.inputScripts)
        {
            script.enabled = false;
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public static void Enable()
    {
        foreach (MonoBehaviour script in input.inputScripts)
        {
            script.enabled = true;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
