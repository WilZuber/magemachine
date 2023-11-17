using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun1 : Gun
{
    
    void Start()
    {
        SetProjectileType(TestProjectile1());
        reloadDuration = 0.25f;
    }

    private Projectile TestProjectile1()
    {
        Bullet bullet = Bullet.New();

        Bullet child = Bullet.New();
        child.damage = 5;

        bullet.next.Add(child);

        return bullet;
    }
}
