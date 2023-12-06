using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDUpdateHealthSprite : MonoBehaviour
{
    public Sprite[] healthMetreSprites;
    public Image healthMetreImage;
    private HealthManager playerHealthManager;


    void Start()
    {
        playerHealthManager = GameObject.Find("MainCharacter").GetComponent<HealthManager>();
    }
    void Update()
    {
        UpdateHealthMeterSprite();
    }

    public void UpdateHealthMeterSprite()
    {
        float maxHealth = playerHealthManager.GetMaxHealth();
        float currentHealth = playerHealthManager.GetHealth();
        float currentHealthUsedRatio = 1-currentHealth/maxHealth;

        int currentSpriteIndex = ((int) (currentHealthUsedRatio*18)); // should be a number between 0 and 17.

        if (currentSpriteIndex > 17) // it sometimes goes beyond 17, please catch
        {
            currentSpriteIndex = 17;
        }

        healthMetreImage.sprite = healthMetreSprites[currentSpriteIndex]; // update sprite
    }
}
