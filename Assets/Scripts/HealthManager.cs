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
            DeathStart();
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

    private void DeathStart()
    {
        if (deathListener != null)
        {
            deathListener.DeathTrigger();
        }
        if (TryGetComponent(out Animator anim))
        {
            anim.SetTrigger("Die");
            RemoveComponents();
            Invoke(nameof(DeathFinish), 1.0f);
        }
        else
        {
            DeathFinish();
        }
    }

    private void RemoveComponents()
    {
        if (TryGetComponent(out AI ai))
        {
            Destroy(ai);
            if (TryGetComponent(out MeleeWeaponController melee))
            {
                Destroy(melee);
            }
            else
            {
                Destroy(GetComponent<WeaponHolder>());
                Destroy(GetComponent<AIAimTarget>());
            }
        }
        else if (TryGetComponent(out PlayerMovement move))
        {
            PlayerAttack attack = GetComponent<PlayerAttack>();
            Destroy(move);
            Destroy(attack);
        }
        Destroy(GetComponent<Collider>());
        Destroy(GetComponent<Rigidbody>());
    }

    private void DeathFinish()
    {
        Destroy(gameObject);
    }
}
