using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//basic projectile
public class Bullet : ProjectileType
{
    private static GameObject sharedPrefab;
    public override void SetPrefab(GameObject prefab) => sharedPrefab = prefab;
    public override GameObject GetPrefab() => sharedPrefab;

    void Awake()
    {
        damage = 2.0f;
        speed = 5.0f;
    }

    public static Bullet New()
    {
        return CreateInstance<Bullet>();
    }
}
