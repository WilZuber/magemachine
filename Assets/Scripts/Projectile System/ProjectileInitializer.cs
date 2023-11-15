using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Put on a single game object and drag prefabs in, it then sets the static fields for projectiles to pull from
public class ProjectileInitializer : MonoBehaviour
{
    
    public GameObject bulletPrefab;
    public GameObject bombPrefab;
    public GameObject explosionPrefab;
    void Start()
    {
        Projectile.sharedBulletPrefab = bulletPrefab;
        Projectile.sharedBombPrefab = bombPrefab;
        Projectile.sharedExplosionPrefab = explosionPrefab;
    }
}
