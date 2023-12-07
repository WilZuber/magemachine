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
        //print(playerStaminaManager.GetStamina());
        //print(playerStaminaManager.hasStamina);
    }

    public void UpdateStaminaMeterSprite()
    {
        float maxStamina = playerStaminaManager.GetMaxStamina();
        float currentStamina = playerStaminaManager.GetStamina();
        float currentStaminaUsedRatio;

        // stamina is intentionally set to a negative value if the player runs out of stamina,
        // to make the player wait a moment before stamina refill, so this makes the HUD element more accurate,
        // making the stamina meter empty if the player doesn't have any stamina yet
        if (currentStamina >= 0) {
            currentStaminaUsedRatio = 1-currentStamina/maxStamina;
        } else {
            currentStaminaUsedRatio = 1;
        }

        int currentSpriteIndex = ((int) (currentStaminaUsedRatio*18)); // should be a number between 0 and 17.

        if (currentSpriteIndex > 17) // it sometimes goes beyond 17, please catch
        {
            currentSpriteIndex = 17;
        }

        if (staminaMetreImage.sprite != staminaMetreSprites[currentSpriteIndex]) {
            staminaMetreImage.sprite = staminaMetreSprites[currentSpriteIndex]; // update sprite
        }
    }
}
