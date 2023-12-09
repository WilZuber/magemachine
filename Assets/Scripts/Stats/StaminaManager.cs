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

    public void SetMaxStamina(float maxStamina)
    {
        this.maxStamina = maxStamina;
    }

    public float GetStamina()
    {
        return this.stamina;
    }

    public float GetMaxStamina()
    {
        return this.maxStamina;
    }

    public bool StaminaNotFull() {
        return stamina != maxStamina;
    }
    
    public void ReduceStamina(float reductionAmount)
    {
        if ((stamina >= 0)) {
            hasStamina = true;
            stamina -= reductionAmount;
        } else {
            hasStamina = false;

            // intentionally setting stamina negative so the player has to wait a moment when they're out of stamina
            stamina = -50;
        }
    }

    public void RefillStamina(float refill)
    {
        stamina += refill;
        if (stamina >= maxStamina) {
            stamina = maxStamina;
        }
        if (stamina >= 0) {
            hasStamina = true;
        }
    }
}
