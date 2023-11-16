using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private float health;
    public float maxHealth; // initialize in prefabs
    public IDeathListener deathListener = null;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        if ((health <= 0) && !isDead)
        {
            isDead = true;
            Die();
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

    public void Die()
    {
        if (deathListener != null)
        {
            deathListener.DeathTrigger();
        }
        Destroy(this.gameObject);
    }
}
