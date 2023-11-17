using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HealthManager hp)) // if the object has a HealthManager
        {
            hp.TakeDamage(damage);
        }
    }
}
