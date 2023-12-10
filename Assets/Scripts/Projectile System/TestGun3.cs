using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun3 : SoulMagicGunType
{
    void Awake()
    {
        SetProjectileType(TestProjectile3());
        SetModel(3);
        reloadDuration = 0.5f; // actually inverse fire rate, not reload duration

        cost = 10.0f;
        shotsAfterDouble = 5;
        maxSafeMultiplier = 16;
    }

    private ProjectileType TestProjectile3()
    {
        LinearBlink blink = LinearBlink.New();
        MagicExplosion explosion = MagicExplosion.New();
        explosion.damage = 4;

        blink.next.Add(explosion);

        return blink;
    }
}
