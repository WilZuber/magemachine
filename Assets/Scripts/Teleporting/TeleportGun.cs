using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGun : GunInstance
{
    private static GameObject player;
    public static GameObject teleportPoint;
    public CapsuleCollider playerCollider;

    void Awake() {
        player = GameObject.Find("/MainCharacter");
        playerCollider = player.GetComponent<CapsuleCollider>();
    }

    public void SwitchWithTeleportPoint() {
        if (teleportPoint != null) {
            float halfHeight = playerCollider.height / 2;

            player.transform.position = teleportPoint.transform.position + halfHeight * Vector3.down;
            Destroy(teleportPoint);
        }
    }
}
