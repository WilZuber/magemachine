using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//teleports a projectile a fixed distance, including through obstacles
public class FixedBlink : Projectile
{
    float distance = 10.0f;
    public override void Fire(Vector3 position, Vector3 direction, float speed, GameObject ignoreCollision)
    {
        Vector3 nextPosition = position + distance * direction;
        Expire(nextPosition, direction, null);
    }

    public static FixedBlink New()
    {
        return CreateInstance<FixedBlink>();
    }
}
