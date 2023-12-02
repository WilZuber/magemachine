using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInstance : MonoBehaviour
{
    public Transform spawnPoint;
    public GunType gunType;
    public float reloadTimer = 0;
    
    // Initialize
    // initializes a instance of a gun of gunType
    // Parameters:
    //      gunType: GunType representing the type of gun
    //      target: AimTarget gets information on where the gun should point
    //      holder: WeaponHolder is the holder for the gun (used by a subclass)
    // Pre:
    // Post: an instance of a gun of gunType is initialized with a specified AimTarget
    public virtual void Initialize(GunType gunType, AimTarget target, WeaponHolder holder)
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

    // Fire
    // delegates to Fire
    // Parameters: none
    // Pre:
    // Post: if it is possible for the gun to fire, return true and fire. Else, returns false and does not fire
    public bool Fire()
    {
        return Fire(gunType.projectileType);
    }

    // Fire
    // checks if possible to fire gun, fires if so
    // Parameters:
    //      projecile: ProjectileType which represents a type of projectile
    // Pre:
    // Post: if possible to fire, fire and returns true, else returns false.
    public virtual bool Fire(ProjectileType projectile)
    {
        if (reloadTimer <= 0) //able to fire
        {
            Vector3 firePosition = spawnPoint.position;
            Vector3 fireDirection = spawnPoint.forward;
            reloadTimer = gunType.reloadDuration;
            projectile.Fire(firePosition, fireDirection);
            return true;
        }
        else
        {
            return false;
        }
    }
}
