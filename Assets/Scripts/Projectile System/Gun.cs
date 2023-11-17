using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public Transform projectileSpawn;
    public Projectile projectileType;
    private float projectileSpeed;
    public float reloadDuration;
    public float reloadTimer = 0;
    
    void Update()
    {
        if (reloadTimer > 0)
        {
            reloadTimer -= Time.deltaTime;
        }
    }

    public void SetProjectileType(Projectile projectileType)
    {
        this.projectileType = projectileType;
    }
    
    public void SetProjectileType(Projectile projectileType, float speed)
    {
        this.projectileType = projectileType;
        projectileSpeed = speed;
    }

    public void Fire()
    {
        Fire(projectileType);
    }

    public void Fire(Projectile projectile)
    {
        if (reloadTimer <= 0) //able to fire
        {
            reloadTimer = reloadDuration;
            projectile.Fire(projectileSpawn.position, transform.forward);
        }
    }
}
