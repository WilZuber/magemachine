using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeStaminaUpgrade : MonoBehaviour
{
    public TextMeshProUGUI modifierText;
    public TextMeshProUGUI priceText;
    private StaminaManager playerStaminaManager;
    private float modifier;
    private bool isMultiplier; // if this is false, addition instead.
    private int price;
    void Start()
    {
        playerStaminaManager = GameObject.Find("MainCharacter").GetComponent<StaminaManager>();
    }

    // sets the text on the button
    public void UpdateButtonText()
    {
        if (isMultiplier)
        {
            modifierText.text = "x" + modifier + "\nStamina";
        } else
        {
            modifierText.text = "+" + modifier + "\nStamina";
        }
        priceText.text = "x" + price;
    }
    
    // update player stamina
    public void UpdatePlayerStamina()
    {
        if (isMultiplier)
        {
            playerStaminaManager.SetMaxStamina(playerStaminaManager.GetMaxStamina() * modifier);
        } else
        {
            playerStaminaManager.SetMaxStamina(playerStaminaManager.GetMaxStamina() + modifier);
        }
    }

    public void RerollModifer()
    {
        isMultiplier = (Random.value < 0.3); // 30% chance to be a multiplier value
        
        if (isMultiplier) // multiplier values should be small
        {
            modifier = ((Random.Range(11, 31)))/10; // value between 1.1 and 3.0
            price = Random.Range(3, 6); // multipliers are more costly
        } else // addition values should be somewhat bigger
        {
            modifier = ((Random.Range(10, 30)));
            price = Random.Range(1, 4); // could get cheap good rolls, or pricey bad rolls
        }
    }
}
