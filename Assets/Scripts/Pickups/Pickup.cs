using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public static bool pickupSound;
    public PickupType type;

    void Start() {
        pickupSound = false;
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            if (Input.GetKey(KeyCode.R)) {
                if (Inventory.CollectItem(this))
                {
                    pickupSound = true;
                    Destroy(gameObject);
                }
            }
        }
    }
}

public enum PickupType
{
    SoulRefillPotion,
    SkillPoint,
    WeaponPart,
    Gun
}