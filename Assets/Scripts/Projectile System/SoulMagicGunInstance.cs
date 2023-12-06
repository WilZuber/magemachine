using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMagicGunInstance : GunInstance
{
    private SoulManager soulManager;
    private SoulMagicGunType type;
    private float factor;
    private float exponent;

    // Initialize (SoulMagic)
    // Creates an instance of a soul magic gun
    public override void Initialize(GunType gunType, AimTarget target, WeaponHolder holder)
    {
        base.Initialize(gunType, target, holder);
        soulManager = holder.GetComponent<SoulManager>();
        type = (SoulMagicGunType)gunType;
        PrepareFormula();
    }

    // Set the formula parameters so that f(2*cost) = maxSafeMultiplier and f((shotsAfterDouble + 1)*cost) = 2
    private void PrepareFormula()
    {
        factor = 1.0f / (soulManager.maxSoul - (type.shotsAfterDouble + 1) * type.cost);
        exponent = Mathf.Log(type.maxSafeMultiplier - 1, (soulManager.maxSoul - 2*type.cost) * factor);
    }

    // Fire (SoulMagic)
    // Fires a projectile and updates the damage of the projectile based on the soul amount
    public override bool Fire(ProjectileType projectile)
    {
        if (reloadTimer > 0) // unable to fire
        {
            return false;
        }

        float damageModifier = GetProjectileDamageModifier();
        soulManager.UseSoul(type.cost); // deplenish soul meter by 5

        Vector3 firePosition = spawnPoint.position;
        Vector3 fireDirection = spawnPoint.forward;
        reloadTimer = type.reloadDuration;
        projectile.Fire(firePosition, fireDirection, damageModifier);
        return true;
    }

    // GetProjectileDamageModifier
    private float GetProjectileDamageModifier()
    {
        float maxSoul = soulManager.GetMaxSoul();
        float soulAmount = soulManager.GetSoul();
        // damage increases closer to 0 soul, start at 1 multiplier
        float damageMultiplier = Mathf.Pow((maxSoul - soulAmount) * factor, exponent) + 1;
        return damageMultiplier;
    }
}
