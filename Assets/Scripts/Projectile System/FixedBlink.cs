using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//teleports a projectile a fixed distance, including through obstacles
public class FixedBlink : ProjectileType
{
    float distance = 10.0f;
    public override void Fire(Vector3 position, float speed, Vector3 direction, GameObject ignoreCollision, float damageMultiplier)
    {
        Vector3 nextPosition = position + distance * direction;
        SpawnNext(nextPosition, direction, null, damageMultiplier);
    }

    public static FixedBlink New()
    {
        return CreateInstance<FixedBlink>();
    }
}
