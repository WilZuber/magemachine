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

    public void SetTeleportPoint(Vector3 position, float speed, Vector3 direction, GameObject ignoreCollision) {
        Ray ray = new(position, direction);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            teleportPoint = hit.point;
        }
        // write else if for if the reticle is on a standard enemy
    }

    public void SwitchWithTeleportPoint(Vector3 position, float speed, Vector3 direction, GameObject ignoreCollision) {
        if (teleportPoint != null) {
            player.transform.position = teleportPoint;
        }
    }
}
