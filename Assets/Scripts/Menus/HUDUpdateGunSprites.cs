using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDUpdateGunSprites : MonoBehaviour
{
    public Image[] gunSprites;
    static HUDUpdateGunSprites hudUpdateGunSprites;
    // Start is called before the first frame update
    void Start()
    {
        hudUpdateGunSprites = this;
    }
    public static void UpdateGunSprites()
    {
        Sprite[] currentGunSprites = Inventory.GetGunSprites(); // update current gun sprites
        {
            hudUpdateGunSprites.gunSprites[0].sprite = currentGunSprites[0];
            hudUpdateGunSprites.gunSprites[1].sprite = currentGunSprites[1];
        }
    }
}
