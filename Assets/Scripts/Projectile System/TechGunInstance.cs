using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechGunInstance : GunInstance
{
    public WeaponPart[,] parts;
    public WPAccelerator barrel;
    private TechGunType type;

    public override void Initialize(GunType gunType, AimTarget target, WeaponHolder holder)
    {
        base.Initialize(gunType, target, holder);
        type = (TechGunType)gunType;

        PartType[,] partTypes = type.parts;
        int L0 = partTypes.GetLength(0);
        int L1 = partTypes.GetLength(1);
        parts = new WeaponPart[L0, L1];
        for (int i = 0; i < L0; i++)
        {
            for (int j = 0; j < L1; j++)
            {
                parts[i, j] = WeaponPart.New(partTypes[i, j]);
                if (partTypes[i, j] == PartType.accelerator)
                {
                    barrel = (WPAccelerator)parts[i, j];
                }
            }
        }
        
        for (int i = 0; i < L0; i++)
        {
            for (int j = 0; j < L1-1; j++)
            {
                parts[i, j].next = parts[i, j+1];
            }
        }
    }

    void Update()
    {
        //no reload timer
    }

    void FixedUpdate()
    {
        foreach (WeaponPart part in parts)
        {
            part?.Update(Time.fixedDeltaTime);
        }
    }

    public override bool Fire(ProjectileType projectile)
    {
        //if (reloadTimer <= 0) //able to fire
        if (barrel?.currentProjectile != null)
        {
            Vector3 firePosition = spawnPoint.position;
            Vector3 fireDirection = spawnPoint.forward;
            //reloadTimer = gunType.reloadDuration;
            projectile = barrel.TakeProjectile();
            projectile.Fire(firePosition, fireDirection);
            return true;
        }
        return false;
    }
}
