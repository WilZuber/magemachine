using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun2 : GunType
{
    void Awake()
    {
        SetProjectileType(TestProjectile2());
        SetModel(2);
        reloadDuration = 0.5f;
    }

    private ProjectileType TestProjectile2()
    {
        LinearBlink blink = LinearBlink.New();
        Bullet bullet = Bullet.New();
        Bullet child = Bullet.New();
        child.damage = 5;
        Explosion explosion = Explosion.New();

        child.next.Add(explosion);
        bullet.next.Add(child);
        blink.next.Add(bullet);

        return blink;
    }
}
