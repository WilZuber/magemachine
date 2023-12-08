using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGun : GunInstance
{
    private static GameObject player;
    public static GameObject teleportPoint;
    public CapsuleCollider playerCollider;
    public float halfHeight;

    void Awake() {
        player = GameObject.Find("/MainCharacter");
        playerCollider = player.GetComponent<CapsuleCollider>();
        halfHeight = playerCollider.height / 2;
    }

    public void SwitchWithTeleportPoint() {
        if (Teleporter.enemyTeleportPoint != null && Teleporter.enemyTeleportPoint.activeSelf) {
            SwitchWithEnemy();
        } else {
            SwitchWithSurface();
        }
    }

    public void SwitchWithEnemy() {
        // saves player position, sends player to enemy position, sends enemy to previous player position
        Vector3 tempPosition = player.transform.position;
        player.transform.position = Teleporter.enemyToTeleport.transform.position  - (0.25f * halfHeight) * Vector3.down;
        Teleporter.enemyToTeleport.transform.position = tempPosition;

        // deactivates teleport point above enemy's head
        Teleporter.enemyTeleportPoint.SetActive(false);
        Teleporter.enemyTeleportPoint.SetActive(false);
    }

    public void SwitchWithSurface() {
        if (teleportPoint != null) {
            // if the teleportation point is high, teleport a higher position of the player
            // if the teleportation point is low, teleport a lower position of the player
            if (HighCheck(teleportPoint.transform.position.y)) {
                Vector3 moveDown = new Vector3(0, -2f, 0);
                player.transform.position = teleportPoint.transform.position + halfHeight * moveDown;
            } else {
                player.transform.position = teleportPoint.transform.position - (0.25f * halfHeight) * Vector3.down;
            }
                
            Destroy(teleportPoint);
        }
    }

    // this is a quickfix to prevent the player from teleporting out of bounds or into walls
        // if this is still here after the class is over and levels have more verticality,
        // rewrite this code so that "3.875" here is replaced with the height of any room the player is currently in.
        // maybe have a raycast check the closest thing above the player and get the y value of the collided object?
    public bool HighCheck(float yValue) {
        return yValue >= (3.875 - halfHeight);
    }
}
