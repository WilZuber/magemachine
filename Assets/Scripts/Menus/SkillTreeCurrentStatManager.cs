using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeCurrentStatManager : MonoBehaviour
{
    public TextMeshProUGUI currentSoulText;
    public TextMeshProUGUI currentStaminaText;
    public TextMeshProUGUI currentHealthText;
    private HealthManager playerHealthManager;
    private SoulManager playerSoulManager;
    private StaminaManager playerStaminaManager;

    void Start()
    {
        playerSoulManager = GameObject.Find("MainCharacter").GetComponent<SoulManager>();
        playerHealthManager = GameObject.Find("MainCharacter").GetComponent<HealthManager>();
        playerStaminaManager = GameObject.Find("MainCharacter").GetComponent<StaminaManager>();
    }

    public void UpdateCurrentSoulText()
    {
        currentSoulText.text = "Currently: " + playerSoulManager.GetMaxSoul();
    }

    public void UpdateCurrentHealthText()
    {
        currentHealthText.text = "Currently: " + playerHealthManager.GetMaxHealth();
    }

    public void UpdateCurrentStaminaText()
    {
        currentStaminaText.text = "Currently: " + playerStaminaManager.GetMaxStamina();
    }

}
