using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public PickupType type;

    void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            if (Input.GetKey(KeyCode.R)) {
                Inventory.CollectItem(this);
                print(type);
                Destroy(gameObject);
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