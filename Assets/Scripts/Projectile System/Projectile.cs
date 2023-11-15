using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : ScriptableObject
{
    public float damage;
    public float speed; //used when firing from another projectile
    public float lifetime = 30.0f;
    public GameObject bulletPrefab;
    public List<Projectile> next = new();

    public bool HasNext()
    {
        return next.Count != 0;
    }
    
    public void Hit(GameObject self, GameObject other, Vector3 bounceDirection)
    {
        if (other.TryGetComponent(out HealthManager hp)) // if the object has a HealthManager
        {
            //print("hit");
            hp.TakeDamage(damage);
        }

        //self.transform.position += 5*Time.deltaTime * bounceDirection;
        Expire(self, bounceDirection, other);
    }

    public void Expire(GameObject self, Vector3 nextDirection, GameObject ignoreCollision)
    {
        Destroy(self);
        if (HasNext()) //ignore if there aren't any projectiles to spawn
        {
            Vector3 position = self.transform.position;
            foreach (Projectile nextProjectile in next)
            {
                ProjectileInstance newProjectile = nextProjectile.Fire(position, nextDirection);
                newProjectile.ignoreCollision = ignoreCollision;
            }
        }
    }

    //Fire with the projectile's own speed
    public ProjectileInstance Fire(Vector3 position, Vector3 direction)
    {
        return Fire(position, direction, speed);
    }

    //Fire at the given speed - overridden when there isn't an instance
    public ProjectileInstance Fire(Vector3 position, Vector3 direction, float speed)
    {
        return ProjectileInstance.CreateProjectile(this, position, speed * direction);
    }
}
