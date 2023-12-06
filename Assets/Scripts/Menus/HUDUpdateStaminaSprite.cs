using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDUpdateStaminaSprite : MonoBehaviour
{
    public Sprite[] staminaMetreSprites;
    public Image staminaMetreImage;
    private StaminaManager playerStaminaManager;


    void Start()
    {
        playerStaminaManager = GameObject.Find("MainCharacter").GetComponent<StaminaManager>();
    }
    void Update()
    {
        UpdateStaminaMeterSprite();
        print(playerStaminaManager.GetStamina());
    }

    public void UpdateStaminaMeterSprite()
    {
        float maxStamina = playerStaminaManager.GetMaxStamina();
        float currentStamina = playerStaminaManager.GetStamina();
        float currentStaminaUsedRatio = 1-currentStamina/maxStamina;

        int currentSpriteIndex = ((int) (currentStaminaUsedRatio*18)); // should be a number between 0 and 17.

        if (currentSpriteIndex > 17) // it sometimes goes beyond 17, please catch
        {
            currentSpriteIndex = 17;
        }

        staminaMetreImage.sprite = staminaMetreSprites[currentSpriteIndex]; // update sprite
    }
}
