using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    float damage;
    public void Hit(GameObject other)
    {
        if (other.TryGetComponent(out HealthManager hp)) // if the object has a HealthManager
        {
            print("hit");
            hp.TakeDamage(damage);
        }
    }

    public static void CreateProjectile(GameObject bulletPrefab, float damage, Vector3 location, Vector3 velocity)
    {
        GameObject instance = Instantiate(bulletPrefab, location, Quaternion.identity);
        Bullet bullet = instance.GetComponent<Bullet>();
        bullet.damage = damage;
        Rigidbody rb = instance.GetComponent<Rigidbody>();
        rb.velocity = velocity;
    }

    public static void CreateProjectile(float damage, Vector3 origin, Vector3 direction)
    {
        
    }
}
