using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private float health;
    public float maxHealth; // initialize in prefabs

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }
    
    public void TakeDamage(float damage)
    {
        if (health <= 0)
        {
            Destroy(this);
        }
    }

    public void RefillHealth(float refill)
    {
        health += refill;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
