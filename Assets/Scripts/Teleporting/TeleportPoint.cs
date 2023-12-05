using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : ProjectileType
{
    private static GameObject sharedPrefab;
    public override void SetPrefab(GameObject prefab) => sharedPrefab = prefab;
    public override GameObject GetPrefab() => sharedPrefab;

    // delete Awake if it doesn't break anything
    void Awake()
    {
        damage = 0f;
        speed = 0f;
    }

    public static Bullet New()
    {
        return CreateInstance<Bullet>();
    }
}
