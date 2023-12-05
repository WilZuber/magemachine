using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : ProjectileType
{
    public TeleportPoint point;
    public GameObject player;

    public override void Fire(Vector3 position, float speed, Vector3 currentDirection, GameObject ignoreCollision, float damageMultiplier)
    {
        player = GameObject.Find("/MainCharacter");
        Ray ray = new(position, currentDirection);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // assumes the player is teleporting to a wall
            Vector3 teleportDestination = hit.point + hit.normal * 1f; // multiply by radius, experiment with this
            Collider hitCollider = hit.collider;

            // create new collider once the corner teleport glitch is fixed
            //Collider playerCollider = new CapsuleCollider();
            Collider playerCollider = player.GetComponent<Collider>();
            // hell (positioning the teleport point so the player doesn't clip through anything while teleporting)
            if (Physics.ComputePenetration(playerCollider, teleportDestination, Quaternion.identity,
                                      hitCollider, hitCollider.transform.position, hitCollider.transform.rotation,
                                      out Vector3 direction, out float distance))
            {
                teleportDestination += distance * direction;
            }

            point.Fire(teleportDestination, currentDirection);
        }
        // next, write the above as an "else if," and have the first "if" check the raycast and if it's hitting an enemy
        // then after that, write a script checking if it's a small enemy or big enemy (big enemies can't be teleported)
    }

    public static Teleporter New()
    {
        return CreateInstance<Teleporter>();
    }
}