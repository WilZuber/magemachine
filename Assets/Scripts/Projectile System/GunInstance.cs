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
            Debug.DrawRay(spawnPoint.position, 2*spawnPoint.forward, Color.yellow);
        }
    }

    public bool Fire()
    {
        return Fire(gunType.projectileType);
    }

    public bool Fire(ProjectileType projectile)
    {
        if (reloadTimer <= 0) //able to fire
        {
            //Vector3 firePosition = transform.GetChild(0).GetChild(0).position;
            //Vector3 fireDirection = transform.GetChild(0).GetChild(0).forward;
            Vector3 firePosition = spawnPoint.position;
            Vector3 fireDirection = spawnPoint.forward;
            reloadTimer = gunType.reloadDuration;
            projectile.Fire(firePosition, fireDirection);
            Debug.DrawRay(firePosition, 20*fireDirection, Color.blue, 0.4f);
            return true;
        }
        else
        {
            return false;
        }
    }
}
