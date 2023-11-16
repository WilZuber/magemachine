using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Projectile : ScriptableObject
{
    public float damage;
    public float speed; //used when firing from another projectile
    public float lifetime = 5.0f;
    public List<Projectile> next = new();

    //override and give the prefab to the projectile initializer object if the projectile needs a prefab
    public virtual void SetPrefab(GameObject prefab) {}
    public virtual GameObject GetPrefab() => null;

    public bool HasNext()
    {
        return next.Count != 0;
    }
    
    public virtual void Hit(GameObject self, GameObject other, Vector3 bounceDirection)
    {
        if (other.TryGetComponent(out HealthManager hp)) // if the object has a HealthManager
        {
            hp.TakeDamage(damage);
        }

        Expire(self, bounceDirection, other);
    }

    public void Expire(GameObject self, Vector3 nextDirection, GameObject ignoreCollision)
    {
        Destroy(self);
        if (HasNext()) //ignore if there aren't any projectiles to spawn
        {
            Vector3 position = self.transform.position;
            Expire(position, nextDirection, ignoreCollision);
        }
    }

    public void Expire(Vector3 position, Vector3 nextDirection, GameObject ignoreCollision)
    {
        foreach (Projectile nextProjectile in next)
        {
            nextProjectile.Fire(position, nextDirection, ignoreCollision);
        }
    }

    public virtual void UpdateProjectile(ProjectileInstance instance)
    {
        //do nothing by default, but can override
    }

    //Fire with the projectile's own speed
    public void Fire(Vector3 position, Vector3 direction)
    {
        Fire(position, direction, speed, null);
    }

    //Fire at the given speed
    public void Fire(Vector3 position, Vector3 direction, float speed)
    {
        Fire(position, direction, speed, null);
    }

    public void Fire(Vector3 position, Vector3 direction, GameObject ignoreCollision)
    {
        Fire(position, direction, speed, ignoreCollision);
    }

    //override when there isn't an instance
    public virtual void Fire(Vector3 position, Vector3 direction, float speed, GameObject ignoreCollision)
    {
        ProjectileInstance.CreateProjectile(this, position, speed * direction, ignoreCollision);
    }
}
