using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportType : GunType
{
    void Awake()
    {
        SetProjectileType(TeleportProjectile());
        SetModel(4);
        reloadDuration = 1f;
    }

    private ProjectileType TeleportProjectile()
    {
        Teleporter teleport = Teleporter.New();
        TeleportPoint point = TeleportPoint.New();
        teleport.point = point;

        return teleport;
    }
}
