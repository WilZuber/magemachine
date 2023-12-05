using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : ProjectileType
{
    public TeleportPoint point;

    public override void Fire(Vector3 position, float speed, Vector3 direction, GameObject ignoreCollision) {
        Ray ray = new(position, direction);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // assumes the player is teleporting to a wall
            Vector3 teleportDestination = hit.point + hit.normal * 1f; // multiply by radius, experiment with this
            point.Fire(teleportDestination, direction);
        }
        // next, write the above as an "else if," and have the first "if" check the raycast and if it's hitting an enemy
        // then after that, write a script checking if it's a small enemy or big enemy (big enemies can't be teleported)
    }

    public static Teleporter New()
    {
        return CreateInstance<Teleporter>();
    }
}