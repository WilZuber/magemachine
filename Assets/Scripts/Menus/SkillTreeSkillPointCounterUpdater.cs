using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeSkillPointCounterUpdater : MonoBehaviour
{
    public TextMeshProUGUI skillPointCounterText;

    public void UpdateSkillTreeSkillPointCounter()
    {
        skillPointCounterText.text = "x" + Inventory.GetSkillPoints();
    }
}
