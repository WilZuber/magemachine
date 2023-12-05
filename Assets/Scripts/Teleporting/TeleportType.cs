using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportType : GunType
{
    void Awake()
    {
        SetProjectileType(TeleportProjectile());
        SetModel(4);
        reloadDuration = 0.5f;
    }

    private ProjectileType TeleportProjectile()
    {
        Teleporter teleport = Teleporter.New();
        TeleportPoint point = TeleportPoint.New();
        teleport.point = point;

        return teleport;
    }
}
