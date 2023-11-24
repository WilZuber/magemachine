using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMagicGunInstance : GunInstance
{
    private float soulMagicDamage;
    private SoulManager soulManager;

    // Initialize (SoulMagic)
    // Creates an instance of a soul magic gun
    public override void Initialize(GunType gunType, AimTarget target, WeaponHolder holder)
    {
        base.Initialize(gunType, target, holder);
        soulManager = holder.GetComponent<soulManager>();
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
        UpdateProjectileDamageModifier();
        
        return base.Fire(projectile); // do whatever base fire does (should always return true)
    }

    // UpdateProjectileDamageModifier
    private void UpdateProjectileDamageModifier()
    {
        // taking a break for now this is too much
    }
}
