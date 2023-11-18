using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunType : ScriptableObject
{
    public static GameObject[] prefabs;
    public GameObject prefab;
    public ProjectileType projectileType;
    private float projectileSpeed;
    public float reloadDuration;

    public void SetProjectileType(ProjectileType projectileType)
    {
        this.projectileType = projectileType;
    }
    
    public void SetProjectileType(ProjectileType projectileType, float speed)
    {
        this.projectileType = projectileType;
        projectileSpeed = speed;
    }

    public (GameObject, GunInstance) Spawn(Transform parent, AimTarget target)
    {
        GameObject instance = Instantiate(prefab, parent);
        instance.GetComponent<Aiming>().target = target;
        GunInstance gun = instance.GetComponent<GunInstance>();
        gun.Initialize(this, target);
        return (instance, gun);
    }
}
