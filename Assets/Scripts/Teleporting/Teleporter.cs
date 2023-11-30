using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//teleports player to first obstacle
public class Teleporter : MonoBehaviour
{
     public static GameObject player;
     public Vector3 teleportPoint;
     public void Awake() {
         player = GameObject.Find("/MainCharacter");
     }
     //teleports player to line-of-sight with one button
    public void NewPosition(Vector3 position, float speed, Vector3 direction, GameObject ignoreCollision) {
        Ray ray = new(position, direction);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            player.transform.position = hit.point;
        }
    }

    // sets teleport point,
    public void SetTeleportPoint(Vector3 position, float speed, Vector3 direction, GameObject ignoreCollision) {
        Ray ray = new(position, direction);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // assumes the player is teleporting to a wall
            teleportPoint = hit.point + hit.normal; //* player.Collider.radius; <- find out how to do this
        }
        // next, write the above as an "else if," and have the first "if" check the raycast and if it's hitting an enemy
    }

    public void SwitchWithTeleportPoint(Vector3 position, float speed, Vector3 direction, GameObject ignoreCollision) {
        if (teleportPoint != null) {
            player.transform.position = teleportPoint;
        }
    }

    public static Teleporter New()
    {
        return CreateInstance<Teleporter>();
    }
}
