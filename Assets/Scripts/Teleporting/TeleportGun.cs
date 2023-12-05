using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGun : MonoBehaviour
{
    public GameObject teleportPoint;
    public void SwitchWithTeleportPoint(Vector3 position, float speed, Vector3 direction, GameObject ignoreCollision) {
        if (teleportPoint != null) {
            player.transform.position = teleportPoint.transform.position;
            Destroy(teleportPoint);
        }
    }
}
