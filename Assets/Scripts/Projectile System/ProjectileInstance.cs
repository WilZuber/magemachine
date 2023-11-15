using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInstance : MonoBehaviour
{

public Projectile projectile;
Rigidbody rb;
public GameObject ignoreCollision; //ignore a wall after bouncing from it
private Vector3 initialVelocity;
float lifetime;

    public static ProjectileInstance CreateProjectile(Projectile projectileType, Vector3 position, Vector3 velocity)
    {
        GameObject instance = Instantiate(projectileType.bulletPrefab, position, Quaternion.identity);
        ProjectileInstance newProjectile = instance.GetComponent<ProjectileInstance>();
        newProjectile.projectile = projectileType;
        newProjectile.rb = instance.GetComponent<Rigidbody>();
        newProjectile.rb.velocity = velocity;
        newProjectile.initialVelocity = velocity;
        newProjectile.lifetime = projectileType.lifetime;
        return newProjectile;
    }
    void FixedUpdate()
    {
        lifetime -= Time.fixedDeltaTime;
        if (lifetime <= 0)
        {
            projectile.Expire(gameObject, rb.velocity, null);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject == ignoreCollision)
        {
            ignoreCollision = null;
            rb.velocity = initialVelocity;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject != ignoreCollision)
        {
            Vector3 bounceDirection = Bounce(other);
            projectile.Hit(this.gameObject, other.gameObject, bounceDirection);
        }
    }

    private Vector3 Bounce(Collision other)
    {
        if (!projectile.HasNext()) //skip calculation since it won't be used
        {
            return Vector3.zero;
        }
        Vector3 normal = other.GetContact(0).normal;
        //Vector3 currentDirection = rb.velocity.normalized;
        Vector3 currentDirection = initialVelocity.normalized;
        Vector3 newDirection = Vector3.Reflect(currentDirection, normal);
        return newDirection;
    }
}
