using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanelScaling : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RectTransform rect = GetComponent<RectTransform>();
        float scale;
        //Panels designed for 16:9
        if (Screen.width * 1f / Screen.height > 16f/9f)
        {
            //Fit height of wide screen
            scale = Screen.height / 1080f;
        } else{
            //Fit width of tall screen
            scale = Screen.width / 1920f;
        }
        rect.localScale *= scale;
    }
}
