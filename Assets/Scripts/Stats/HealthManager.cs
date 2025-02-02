using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    private float health;
    public float maxHealth; // initialize in prefabs
    public IDeathListener deathListener = null;
    private bool isDead = false;
    public static bool enemyIsDead;
    public static bool takeDamage;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        takeDamage = false;
    }
    
    public float GetHealth()
    {
        return this.health;
    }

    public float GetMaxHealth()
    {
        return this.maxHealth;
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public void SetHealth(float health)
    {
        this.health = health;
    }

    public void TakeDamage(float damage)
    {
        takeDamage = true;
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

    public void DeathStart()
    {
        if (deathListener != null)
        {
            deathListener.DeathTrigger();
        }
        if (TryGetComponent(out Animator anim))
        {
            anim.SetTrigger("Die");
            RemoveComponents();
            //Invoke(nameof(DeathFinish), 5f);
        }
        else
        {
            DeathFinish();
        }
    }

    private void RemoveComponents()
    {
        gameObject.transform.Find("Minimap").gameObject.SetActive(false);
        if (TryGetComponent(out AI ai))
        {
            // add enemy death
            enemyIsDead = true;
            ai.agent.isStopped = true;
            Destroy(ai);
            if (TryGetComponent(out MeleeWeaponController melee))
            {
                Destroy(melee.weapon.GetComponent<Collider>());
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
            // activate death screen
            GameObject.Find("/MainCharacter/CameraPivot/Main Camera/DeathScreenCanvas").SetActive(true);
            DeathScreen.playerDead = true;

            PlayerAttack attack = GetComponent<PlayerAttack>();
            Destroy(move);
            Destroy(attack);

            Transform camera = transform.GetChild(2);
            GameObject empty = Instantiate(new GameObject("Empty"), transform);
            camera.gameObject.GetComponent<MouseLook>().player = empty;//camera.gameObject;
            camera.SetParent(empty.transform);
            Invoke(nameof(GoToMenu), 4.0f);
        }
        Destroy(GetComponent<Collider>());
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        Destroy(rb);
    }

    private void DeathFinish()
    {
        Destroy(gameObject);
    }

    private void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
