using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun1 : TechGunType
{
    
    void Awake()
    {
        //SetProjectileType(TestProjectile1());
        SetModel(0);
        parts = new[,]{{PartType.generatorBasic, PartType.bufferBasic, PartType.accelerator}};
        //LoadFromArray(testParts);
        //reloadDuration = 0.25f;

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
