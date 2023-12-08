using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : ProjectileType
{
    public TeleportPoint point;
    public GameObject player;
    public static bool isTeleportableEnemy;
    public static GameObject enemyToTeleport;
    public Vector3 point0;
    public Vector3 point1;
    public override void Fire(Vector3 position, float speed, Vector3 currentDirection, GameObject ignoreCollision, float damageMultiplier)
    {
        player = GameObject.Find("/MainCharacter");
        Ray ray = new(position, currentDirection);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, -1, QueryTriggerInteraction.Ignore))
        {   
            Vector3 teleportDestination = hit.point + hit.normal * 0.05f;
            Collider hitCollider = hit.collider;

            // code for setting up teleport point with enemy
            if (hitCollider.gameObject.name == "enemy1" || hitCollider.gameObject.name == "enemy3") {
                isTeleportableEnemy = true;
               
                // activates a teleport point above the enemy
                enemyToTeleport = hitCollider.gameObject;
                enemyToTeleport.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);

                // note : point.fire is still called after this if statement, but the teleport destination does not matter since the
                // teleportPoint does not set where the player goes, the enemy's position sets where the player goes
            } else {
                // this else statement assumes the player is teleporting to a surface

                isTeleportableEnemy = false;

                // checks if an enemy has already been marked for teleportation
                // since this else statement is for when the player wants to teleport to a surface instead of an enemy,
                // this if statement deactivates the teleport point above an enemy's head if there is one and resets the variable
                if (enemyToTeleport != null) {
                    enemyToTeleport.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                    enemyToTeleport = null;
                }
            
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

                // var trueRadius = playerCollider.radius / 0.6f;
                // var trueHeight = playerCollider.height / 0.6f;

                // var playerDirection = Vector3.up;
                // var offset = playerDirection * (trueHeight / 2 - trueRadius);
                // var point0 = hit.point - offset;
                // var point1 = hit.point + offset;

                // creating array of colliders to iterate through and depenetrate if currently penetrated
                    // using OverlapCapsuleNonAlloc instead of OverlapCapsule because OverlapCapsuleNonAlloc allocates the array in advance
                // Collider[] colliders = Physics.OverlapCapsule(point0, point1, trueRadius);
                Collider[] colliders = Physics.OverlapCapsule(point0, point1, playerCollider.radius);
                foreach (Collider overlappingCollider in colliders) {
                    //Debug.Log(overlappingCollider.gameObject);
                    if (!(overlappingCollider.isTrigger) && !(overlappingCollider.CompareTag("Player") || overlappingCollider.CompareTag("Enemy"))) {
                        //Debug.Log(overlappingCollider.gameObject);
                        bool overlapping = Physics.ComputePenetration(playerCollider, teleportDestination, Quaternion.identity,
                                                                    overlappingCollider, overlappingCollider.transform.position, 
                                                                    overlappingCollider.transform.rotation, out Vector3 direction, out float distance);
                        if (overlapping) {
                            Debug.Log(overlapping);
                            teleportDestination += distance * direction;
                        }
                    }
                }
            }
            point.Fire(teleportDestination, currentDirection);
        }
    }

    public static Teleporter New()
    {
        return CreateInstance<Teleporter>();
    }
}