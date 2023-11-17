using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun1 : GunType
{
    
    void Awake()
    {
        SetProjectileType(TestProjectile1());
        prefab = prefabs[4];
        reloadDuration = 0.25f;
    }

    private ProjectileType TestProjectile1()
    {
        Bullet bullet = Bullet.New();

        Bullet child = Bullet.New();
        child.damage = 5;

        bullet.next.Add(child);

        return bullet;
    }
}
