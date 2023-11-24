using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class ProjectileType : ScriptableObject
{
    public float damage;
    public float speed; //used when firing from another projectile
    public float lifetime = 5.0f;
    public List<ProjectileType> next = new();

    //override and give the prefab to the projectile initializer object if the projectile needs a prefab
    public virtual void SetPrefab(GameObject prefab) {}
    public virtual GameObject GetPrefab() => null;

    public bool HasNext()
    {
        return next.Count != 0;
    }
    
    public virtual void Hit(ProjectileInstance self, GameObject other, Vector3 bounceDirection)
    {
        if (other.TryGetComponent(out HealthManager hp)) // if the object has a HealthManager
        {
            hp.TakeDamage(damage * self.damageMultiplier);
        }

        Expire(self, bounceDirection, other);
    }

    public void Expire(ProjectileInstance self, Vector3 nextDirection, GameObject ignoreCollision)
    {
        self.expired = true;
        Destroy(self.gameObject);
        if (HasNext()) //ignore if there aren't any projectiles to spawn
        {
            Vector3 position = self.transform.position;
            SpawnNext(position, nextDirection, ignoreCollision, self.damageMultiplier);
        }
    }

    public void SpawnNext(Vector3 position, Vector3 nextDirection, GameObject ignoreCollision, float damageMultiplier)
    {
        foreach (ProjectileType nextProjectile in next)
        {
            nextProjectile.Fire(position, nextDirection, ignoreCollision, damageMultiplier);
        }
    }

    public virtual void UpdateProjectile(ProjectileInstance instance)
    {
        //do nothing by default, but can override
    }

    //Fire with the projectile's own speed
    public void Fire(Vector3 position, Vector3 direction)
    {
        Fire(position, speed, direction, null, 1);
    }

    //Fire at the given speed
    public void Fire(Vector3 position, float speed, Vector3 direction)
    {
        Fire(position, speed, direction, null, 1);
    }

    public void Fire(Vector3 position, Vector3 direction, GameObject ignoreCollision)
    {
        Fire(position, speed, direction, ignoreCollision, 1);
    }

    public void Fire(Vector3 position, Vector3 direction, float damageMultiplier)
    {
        Fire(position, speed, direction, null, damageMultiplier);
    }

    public void Fire(Vector3 position, Vector3 direction, GameObject ignoreCollision, float damageMultiplier)
    {
        Fire(position, speed, direction, ignoreCollision, damageMultiplier);
    }

    //override when there isn't an instance
    public virtual void Fire(Vector3 position, float speed, Vector3 direction, GameObject ignoreCollision, float damageMultiplier)
    {
        ProjectileInstance.CreateProjectile(this, position, speed * direction, ignoreCollision, damageMultiplier);
    }
}
