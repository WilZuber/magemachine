using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaminaManager : MonoBehaviour
{
    private float stamina;
    public float maxStamina; // initialize in prefabs

    public bool hasStamina;

    // Start is called before the first frame update
    void Start()
    {
        stamina = maxStamina;
        hasStamina = true;
    }
    
    public void ReduceStamina(float reductionAmount)
    {
        if ((stamina > 0)) {
            hasStamina = true;
            stamina -= reductionAmount;
        } else {
            hasStamina = false;
        }
    }

    public void RefillStamina(float refill)
    {
        stamina += refill;
        if (stamina > maxStamina) {
            stamina = maxStamina;
        }
    }
}
