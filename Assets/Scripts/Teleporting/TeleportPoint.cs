using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : ProjectileType
{
    private static GameObject sharedPrefab;
    public override void SetPrefab(GameObject prefab) => sharedPrefab = prefab;
    public override GameObject GetPrefab() => sharedPrefab;

    public override void Fire(Vector3 position, float speed, Vector3 direction, GameObject ignoreCollision) {
        ProjectileInstance.CreateProjectile(this, position, 0, ignoreCollision, 0);
    }

    public static Bullet New()
    {
        return CreateInstance<Bullet>();
    }
}
