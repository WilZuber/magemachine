using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//teleports a projectile into the first obstacle
public class LinearBlink : Projectile
{
    public override void Fire(Vector3 position, Vector3 direction, float speed, GameObject ignoreCollision)
    {
        Ray ray = new(position, direction);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 nextPosition = hit.point;
            Expire(nextPosition, direction, null);
        }
    }

    public static LinearBlink New()
    {
        return CreateInstance<LinearBlink>();
    }
}
