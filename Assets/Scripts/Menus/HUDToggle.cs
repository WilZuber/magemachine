using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDToggle : MonoBehaviour
{
    public GameObject[] HUDElements;
    private static HUDToggle self;

    // Start is called before the first frame update
    void Start()
    {
        self = this;
    }

    public static void Disable()
    {
        foreach (GameObject element in self.HUDElements)
        {
            element.SetActive(false);
        }
    }

    public static void Enable()
    {
        foreach (GameObject element in self.HUDElements)
        {
            element.SetActive(true);
        }
    }
}
