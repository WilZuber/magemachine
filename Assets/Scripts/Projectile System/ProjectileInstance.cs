using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInstance : MonoBehaviour
{

    public ProjectileType projectile;
    Rigidbody rb;
    public GameObject ignoreCollision; //ignore an object after bouncing from it
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

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == ignoreCollision)
        {
            ignoreCollision = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other.gameObject != ignoreCollision && !expired)
        {
            Vector3 bounceDirection = Bounce(other);
            projectile.Hit(this, other.gameObject, bounceDirection);
        }
    }

    private Vector3 Bounce(Collider other)
    {
        if (!projectile.HasNext()) //skip calculation since it won't be used
        {
            return Vector3.zero;
        }
        //as long as the projectile is a sphere, the closest point is the point of contact,
        //and the normal of the other surface at that point must pass through the center of the projectile
        Vector3 contactPoint = other.ClosestPoint(transform.position);
        Vector3 normal;
        if (contactPoint.Equals(transform.position))
        {
            //used if the projectile's origin is inside the other collider
            Physics.ComputePenetration(
                GetComponent<Collider>(), transform.position, transform.rotation,
                other, other.transform.position, other.transform.rotation,
                out normal, out _);
        }
        else
        {
            normal = (transform.position - contactPoint).normalized;
        }
        Vector3 currentDirection = rb.velocity.normalized;
        //reflect across the normal vector to bounce
        Vector3 newDirection = Vector3.Reflect(currentDirection, normal);
        return newDirection;
    }
}
