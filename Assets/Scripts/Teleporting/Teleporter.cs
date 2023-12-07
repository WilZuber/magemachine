using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : ProjectileType
{
    public TeleportPoint point;
    public GameObject player;
    public Vector3 point0;
    public Vector3 point1;

    public override void Fire(Vector3 position, float speed, Vector3 currentDirection, GameObject ignoreCollision, float damageMultiplier)
    {
        player = GameObject.Find("/MainCharacter");
        Ray ray = new(position, currentDirection);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // assumes the player is teleporting to a wall
            Vector3 teleportDestination = hit.point + hit.normal * 1f; // multiply by radius, experiment with this
            Collider hitCollider = hit.collider;

            // create new collider once the corner teleport glitch is fixed to replace player.GetComponent
            //Collider playerCollider = new CapsuleCollider();
            CapsuleCollider playerCollider = player.GetComponent<CapsuleCollider>();
            
            // hell (positioning the teleport point so the player doesn't clip through anything while teleporting)

            // source for next part https://roundwide.com/physics-overlap-capsule/
            // calculating point0 and point1 for the CapsuleCollider,
            // taking into account the direction property of the CapsuleCollider,
            // which can be 0, 1, or 2 corresponding to the x, y, or the z axis
            var playerDirection = new Vector3 {[playerCollider.direction] = 1};
            var offset = playerCollider.height / 2 - playerCollider.radius;
            var point0 = playerCollider.center - playerDirection * offset;
            var point1= playerCollider.center + playerDirection * offset;

            // creating array of colliders to iterate through and depenetrate if currently penetrated
                // using OverlapCapsuleNonAlloc instead of OverlapCapsule because OverlapCapsuleNonAlloc allocates the array in advance
            Collider[] colliders = Physics.OverlapCapsule(point0, point1, playerCollider.radius);
            foreach (Collider overlappingCollider in colliders) {
                if (!(overlappingCollider.isTrigger) && !(overlappingCollider.CompareTag("Player") || overlappingCollider.CompareTag("Enemy"))) {
                    Debug.Log(overlappingCollider.gameObject);
                    bool overlapping = Physics.ComputePenetration(playerCollider, teleportDestination, Quaternion.identity,
                                                                  overlappingCollider, overlappingCollider.transform.position, 
                                                                  overlappingCollider.transform.rotation, out Vector3 direction, out float distance);
                    if (overlapping) {
                        teleportDestination += distance * direction;
                    }
                }
            }

            // delete other instances of teleportpoint when a new one is fired
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