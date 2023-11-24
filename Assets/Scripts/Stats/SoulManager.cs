using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoulManager : MonoBehaviour
{
    private float soul;
    public float maxSoul; // initialize in prefabs

    // Start is called before the first frame update
    void Start()
    {
        soul = maxSoul;
    }

    public float getMaxSoul()
    {
        return this.maxSoul;
    }
    
    public float getSoul()
    {
        return this.soul;
    }

    public void UseSoul(float amount)
    {
        soul -= amount;
        if ((soul <= 0))
        {
            GetComponent<HealthManager>().DeathStart();
        }
    }

    public void RefillSoul(float refill)
    {
        soul += refill;
        if (soul > maxSoul)
        {
            soul = maxSoul;
        }
    }
}
