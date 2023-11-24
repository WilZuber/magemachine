using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun3 : SoulMagicGunType
{
    void Awake()
    {
        SetProjectileType(TestProjectile3());
        prefab = prefabs[3];
        reloadDuration = 0.5f; // actually inverse fire rate, not reload duration
    }

    private ProjectileType TestProjectile3()
    {
        LinearBlink blink = LinearBlink.New();
        MagicExplosion explosion = MagicExplosion.New();

        blink.next.Add(explosion);

        Debug.Log("owo");
        return blink;
    }
}
