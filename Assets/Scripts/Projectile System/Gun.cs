using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    public Transform projectileSpawn;
    private Projectile projectileType;
    private float projectileSpeed;
    
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
        projectile.Fire(projectileSpawn.position, transform.forward);
    }
}
