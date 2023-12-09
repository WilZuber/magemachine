using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeRerollScript : MonoBehaviour
{
    private float rerollCost;
    public TextMeshProUGUI priceText;
    public SkillTreeStaminaUpgrade staminaUpgrade;
    // Start is called before the first frame update
    public void Reroll()
    {
        rerollCost++;
        priceText.text = rerollCost + "";
        staminaUpgrade.Reroll();
    }
}
