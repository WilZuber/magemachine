using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//teleports a projectile into the first obstacle
public class LinearBlink : ProjectileType
{
    public override void Fire(Vector3 position, float speed, Vector3 direction, GameObject ignoreCollision, float damageMultiplier)
    {
        Ray ray = new(position, direction);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, -1, QueryTriggerInteraction.Ignore))
        {
            Vector3 nextPosition = hit.point;
            SpawnNext(nextPosition, direction, null, damageMultiplier, hit);
        }
    }

    public static LinearBlink New()
    {
        return CreateInstance<LinearBlink>();
    }
}
