using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGun : GunInstance
{
    private static GameObject player;
    public static GameObject teleportPoint;

    void Awake() {
        player = GameObject.Find("/MainCharacter");
    }
    // public void SwitchWithTeleportPoint(Vector3 position, float speed, Vector3 direction, GameObject ignoreCollision) {
    //     if (teleportPoint != null) {
    //         player.transform.position = teleportPoint.transform.position;
    //         Destroy(teleportPoint);
    //     }
    // }

    public void SwitchWithTeleportPoint() {
        if (teleportPoint != null) {
            player.transform.position = teleportPoint.transform.position;
            Destroy(teleportPoint);
        }
    }
}
