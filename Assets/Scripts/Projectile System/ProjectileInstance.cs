using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInstance : MonoBehaviour
{

public Projectile projectile;
Rigidbody rb;
float lifetime;

    public static void CreateProjectile(Projectile projectileType, Vector3 position, Vector3 velocity)
    {
        GameObject instance = Instantiate(projectileType.bulletPrefab, position, Quaternion.identity);
        ProjectileInstance newProjectile = instance.GetComponent<ProjectileInstance>();
        newProjectile.projectile = projectileType;
        newProjectile.rb = instance.GetComponent<Rigidbody>();
        newProjectile.rb.velocity = velocity;
        newProjectile.lifetime = projectileType.lifetime;
    }
    void FixedUpdate()
    {
        lifetime -= Time.fixedDeltaTime;
        if (lifetime <= 0)
        {
            projectile.Expire(gameObject, rb.velocity);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Vector3 bounceDirection = Bounce(other);
        projectile.Hit(this.gameObject, other.gameObject, bounceDirection);
    }

    private Vector3 Bounce(Collision other)
    {
        if (!projectile.HasNext()) //skip calculation since it won't be used
        {
            return Vector3.zero;
        }
        Vector3 normal = other.GetContact(0).normal;
        Vector3 currentVelocity = rb.velocity;
        return Vector3.Reflect(currentVelocity, normal);
    }
}
