using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInstance : MonoBehaviour
{
    public Transform spawnPoint;
    GunType gunType;
    float reloadTimer = 0;
    
    public void Initialize(GunType gunType, AimTarget target)
    {
        this.gunType = gunType;
        GetComponent<Aiming>().target = target;
    }
    void Update()
    {
        if (reloadTimer > 0)
        {
            reloadTimer -= Time.deltaTime;
        }
    }

    public void Fire()
    {
        Fire(gunType.projectileType);
    }

    public void Fire(ProjectileType projectile)
    {
        if (reloadTimer <= 0) //able to fire
        {
            reloadTimer = gunType.reloadDuration;
            projectile.Fire(spawnPoint.position, spawnPoint.forward);
        }
    }
}
