using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : ProjectileType
{
    private static GameObject sharedPrefab;
    public override void SetPrefab(GameObject prefab) => sharedPrefab = prefab;
    public override GameObject GetPrefab() => sharedPrefab;

    public static bool isTeleportableEnemy;
    public override void Fire(Vector3 position, float speed, Vector3 direction, GameObject ignoreCollision, float damageMultiplier) {
        if (TeleportGun.teleportPoint != null) {
            Destroy(TeleportGun.teleportPoint);
        }
        Debug.Log("TeleportPoint isTeleportableEnemy check: " + isTeleportableEnemy);
        if (!isTeleportableEnemy) {
            ProjectileInstance instance = ProjectileInstance.CreateProjectile(this, position, Vector3.zero, ignoreCollision, 0f);
            TeleportGun.teleportPoint = instance.gameObject;
        } else {
            Debug.Log("projectile intentionally not created");
        }
    }

    public static TeleportPoint New()
    {
        return CreateInstance<TeleportPoint>();
    }
}
