using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : ProjectileType
{
    private static GameObject sharedPrefab;
    public override void SetPrefab(GameObject prefab) => sharedPrefab = prefab;
    public override GameObject GetPrefab() => sharedPrefab;
    public override void Fire(Vector3 position, float speed, Vector3 direction, GameObject ignoreCollision, float damageMultiplier) {
        if (TeleportGun.teleportPoint != null) {
            Destroy(TeleportGun.teleportPoint);
        }

        // if the player wants to teleport an enemy, a projectile is intentionally not shot 
        // since the teleport point is created by activating a gameobject on the enemy
        if (!Teleporter.isTeleportableEnemy) {
            ProjectileInstance instance = ProjectileInstance.CreateProjectile(this, position, Vector3.zero, ignoreCollision, 0f);
            TeleportGun.teleportPoint = instance.gameObject; 
        } 
    }

    public static TeleportPoint New()
    {
        return CreateInstance<TeleportPoint>();
    }
}
