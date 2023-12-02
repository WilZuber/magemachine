using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunType : ScriptableObject
{
    public static GameObject[] prefabs;
    public static GameObject[] collectablePrefabs;
    public static Sprite[] inventorySprites;
    public GameObject prefab;
    public GameObject collectablePrefab;
    public Sprite inventorySprite;
    public ProjectileType projectileType;
    private float projectileSpeed;
    public float reloadDuration;

    // SetProjectileType
    // sets the type of the projectile
    // Parameters:
    //      ProjectileType: projectileType
    public void SetProjectileType(ProjectileType projectileType)
    {
        this.projectileType = projectileType;
    }
    
    public void SetProjectileType(ProjectileType projectileType, float speed)
    {
        this.projectileType = projectileType;
        projectileSpeed = speed;
    }

    public void SetModel(int modelIndex)
    {
        prefab = prefabs[modelIndex];
        collectablePrefab = collectablePrefabs[modelIndex];
        inventorySprite = inventorySprites[modelIndex];
    }

    public virtual (GameObject, GunInstance) Spawn(Transform parent, AimTarget target, WeaponHolder holder)
    {
        GameObject instance = Instantiate(prefab, parent);
        instance.GetComponent<Aiming>().target = target;
        GunInstance gun = instance.GetComponent<GunInstance>();
        gun.Initialize(this, target, holder);
        return (instance, gun);
    }

    public void SpawnPickup(Vector3 position, Transform parent)
    {
        GameObject instance = Instantiate(collectablePrefab, position, Quaternion.identity, parent);
        GameObject pickup = instance.transform.GetChild(0).gameObject;
        pickup.GetComponent<GunPickup>().gunType = this;
    }
}
