using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMagicGunInstance : GunInstance
{
    private SoulManager soulManager;

    // Initialize (SoulMagic)
    // Creates an instance of a soul magic gun
    public override void Initialize(GunType gunType, AimTarget target, WeaponHolder holder)
    {
        base.Initialize(gunType, target, holder);
        soulManager = holder.GetComponent<SoulManager>();
    }

    // Fire (SoulMagic)
    // Fires a projectile and updates the damage of the projectile based on the soul amount
    public override bool Fire(ProjectileType projectile)
    {
        if (reloadTimer > 0) // unable to fire
        {
            return false;
        }

        soulManager.UseSoul(5); // deplenish soul meter by 5

        float damageModifier = GetProjectileDamageModifier();
        Vector3 firePosition = spawnPoint.position;
        Vector3 fireDirection = spawnPoint.forward;
        reloadTimer = gunType.reloadDuration;
        projectile.Fire(firePosition, fireDirection, damageModifier);
        return true;
    }

    // UpdateProjectileDamageModifier
    private float GetProjectileDamageModifier()
    {
        float maxSoul = soulManager.getMaxSoul();
        float soulAmount = soulManager.getSoul();
        float damageMultiplier = maxSoul/soulAmount;
        return damageMultiplier;
    }
}
