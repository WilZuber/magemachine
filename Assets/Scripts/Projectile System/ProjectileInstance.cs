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
    public bool precalculatedBounce; //used by LinearBlink to give the projectile a bounce direction based on the raycast
    public RaycastHit lastHit;


    public static ProjectileInstance CreateProjectile(ProjectileType projectileType, Vector3 position, Vector3 velocity,
            GameObject ignoreCollision, float damageMultiplier)
    {
        GameObject instance = Instantiate(projectileType.GetPrefab(), position, Quaternion.identity);
        ProjectileInstance newProjectile = instance.GetComponent<ProjectileInstance>();
        newProjectile.projectile = projectileType;
        newProjectile.rb = instance.GetComponent<Rigidbody>();
        newProjectile.lifetime = projectileType.lifetime;
        newProjectile.ignoreCollision = ignoreCollision;
        newProjectile.expired = false;
        newProjectile.damageMultiplier = damageMultiplier;

        // Set velocity, possibly dependent on an object the projectile bounced from
        if (ignoreCollision != null &&
                projectileType.AdditiveBounceVelocity() &&
                ignoreCollision.TryGetComponent(out Rigidbody bounceRB))
        {
            newProjectile.rb.velocity = velocity + bounceRB.velocity;
        }
        else
        {
            newProjectile.rb.velocity = velocity;
        }
        if (projectileType is Bullet) {
             Explosion.bulletFired = true;
        }
       
        return newProjectile;
    }

    public static void CreateProjectile(ProjectileType projectileType, Vector3 position, Vector3 velocity,
            GameObject ignoreCollision, float damageMultiplier, RaycastHit lastHit)
    {
        ProjectileInstance newProjectile = CreateProjectile(projectileType, position, velocity,
                ignoreCollision, damageMultiplier);
        if (projectileType is Explosion) {
            Explosion.explosionSound = true;
        }
        newProjectile.precalculatedBounce = true;
        newProjectile.lastHit = lastHit;
    }

    void FixedUpdate()
    {
        lifetime -= Time.fixedDeltaTime;
        if (lifetime <= 0)
        {
            projectile.Expire(this, transform.position, rb.velocity, null);
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
            (Vector3 bouncePosition, Vector3 bounceDirection) = Bounce();
            projectile.Hit(this, other.gameObject, bouncePosition, bounceDirection);
        }
    }

    private (Vector3, Vector3) Bounce()
    {
        if (!projectile.HasNext()) //skip calculation since it won't be used
        {
            return (Vector3.zero, Vector3.zero);
        }

        //Radius of current projectile to find entry path
        float radius = GetComponent<SphereCollider>().radius;
        Vector3 currentDirection = rb.velocity.normalized;

        RaycastHit hit;
        if (precalculatedBounce)
        {
            hit = lastHit;
        }
        else
        {
            //Position the projectile was in 2 physics updates ago (1 doesn't seem to work)
            Vector3 lastPosition = transform.position - rb.velocity * (Time.fixedDeltaTime * 2);
            Ray entryRay = new(lastPosition, rb.velocity);

            //Spherecast from the last position to find where the projectile's path makes contact
            Physics.SphereCast(entryRay, radius, out hit, Mathf.Infinity, -1, QueryTriggerInteraction.Ignore);
        }
        Vector3 hitPosition = hit.point + hit.normal * radius;

        //Reflect across the normal vector to bounce
        Vector3 newDirection = Vector3.Reflect(currentDirection, hit.normal);
        return (hitPosition, newDirection);
    }
}
