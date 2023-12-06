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
        UpdateGunSprites();
    }
    public static void UpdateGunSprites()
    {
        Sprite[] currentGunSprites = Inventory.GetGunSprites(); // update current gun sprites
        {
            if (currentGunSprites[0] == null) // if no gun selected, turn off image
            {
                hudUpdateGunSprites.gunSprites[0].gameObject.SetActive(false);
            } else {
                hudUpdateGunSprites.gunSprites[0].sprite = currentGunSprites[0];
                hudUpdateGunSprites.gunSprites[0].gameObject.SetActive(true);
            }
            
            if (currentGunSprites[1] == null) // if no gun selected, turn off image
            {
                hudUpdateGunSprites.gunSprites[1].gameObject.SetActive(false);
            } else {
                hudUpdateGunSprites.gunSprites[1].sprite = currentGunSprites[1];
                hudUpdateGunSprites.gunSprites[1].gameObject.SetActive(true);
            }
        }
    }
}
