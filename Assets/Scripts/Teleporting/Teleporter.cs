using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//teleports player to first obstacle
public class Teleporter : MonoBehaviour
{
     public static GameObject player;
     public void Awake() {
         player = GameObject.Find("/MainCharacter");
     }
    public void Teleport(Vector3 position, float speed, Vector3 direction, GameObject ignoreCollision)
    {
        Ray ray = new(position, direction);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            player.transform.position = hit.point;
        }
    }
}
