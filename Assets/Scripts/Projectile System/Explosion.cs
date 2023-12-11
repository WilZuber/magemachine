using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : ProjectileType
{
    private static GameObject sharedPrefab;
    public override void SetPrefab(GameObject prefab) => sharedPrefab = prefab;
    public override GameObject GetPrefab() => sharedPrefab;

    public override bool AdditiveBounceVelocity() => false;
    public static bool explosionSound;
    public static bool onlyExplodeOnce;

    void Awake()
    {
        damage = 10.0f;
        lifetime = 0.375f;
        explosionSound = false;
    }

    public override void Hit(ProjectileInstance self, GameObject other, Vector3 bouncePosition, Vector3 bounceDirection)
    {
        
        if (other.TryGetComponent(out HealthManager hp)) // if the object has a HealthManager
        {
            hp.TakeDamage(damage);
        }
    }

    public static Explosion New()
    {
        return CreateInstance<Explosion>();
    }

    public override void UpdateProjectile(ProjectileInstance instance)
    {
        instance.gameObject.transform.localScale += 10.0f * Time.fixedDeltaTime * Vector3.one;
    }
}
