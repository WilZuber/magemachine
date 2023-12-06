using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDUpdateSoulSprite : MonoBehaviour
{
    public Sprite[] soulMetreSprites;
    public Image soulMetreImage;
    private SoulManager playerSoulManager;


    void Start()
    {
        playerSoulManager = GameObject.Find("MainCharacter").GetComponent<SoulManager>();
    }
    void Update()
    {
        UpdateSoulMeterSprite();
    }

    public void UpdateSoulMeterSprite()
    {
        float maxSoul = playerSoulManager.GetMaxSoul();
        float currentSoul = playerSoulManager.GetSoul();
        float currentSoulUsedRatio = 1-currentSoul/maxSoul;

        int currentSpriteIndex = ((int) (currentSoulUsedRatio*18)); // should be a number between 0 and 17.

        if (currentSpriteIndex > 17) // it sometimes goes beyond 17, please catch
        {
            currentSpriteIndex = 17;
        }

        soulMetreImage.sprite = soulMetreSprites[currentSpriteIndex]; // update sprite
    }
}
