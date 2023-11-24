using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInstance : MonoBehaviour
{

    public ProjectileType projectile;
    Rigidbody rb;
    public GameObject ignoreCollision; //ignore a wall after bouncing from it
    private Vector3 initialVelocity;
    float lifetime;
    public bool expired;
    public float damageMultiplier; // instance-specific damage multiplier

    public static void CreateProjectile(ProjectileType projectileType, Vector3 position, Vector3 velocity,
            GameObject ignoreCollision, float damageMultiplier)
    {
        GameObject instance = Instantiate(projectileType.GetPrefab(), position, Quaternion.identity);
        ProjectileInstance newProjectile = instance.GetComponent<ProjectileInstance>();
        newProjectile.projectile = projectileType;
        newProjectile.rb = instance.GetComponent<Rigidbody>();
        newProjectile.rb.velocity = velocity;
        newProjectile.initialVelocity = velocity;
        newProjectile.lifetime = projectileType.lifetime;
        newProjectile.ignoreCollision = ignoreCollision;
        newProjectile.expired = false;
        newProjectile.damageMultiplier = damageMultiplier;
    }
    void FixedUpdate()
    {
        lifetime -= Time.fixedDeltaTime;
        if (lifetime <= 0)
        {
            projectile.Expire(this, rb.velocity, null);
        }
        else
        {
            projectile.UpdateProjectile(this);
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
        if (other.gameObject != ignoreCollision && !expired)
        {
            Vector3 bounceDirection = Bounce(other);
            projectile.Hit(this, other.gameObject, bounceDirection);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && !expired)
        {
            projectile.Hit(this, other.gameObject, Vector3.zero);
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
