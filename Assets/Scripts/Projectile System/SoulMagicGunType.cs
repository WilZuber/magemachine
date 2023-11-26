using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMagicGunType : GunType
{
    public float cost;
    public float shotsAfterDouble; //number of safe shots after and including the first one with at least double damage
    public float maxSafeMultiplier; //multiplier of the last shot that is guaranteed to be safe

    public override (GameObject, GunInstance) Spawn(Transform parent, AimTarget target, WeaponHolder holder)
    {
        GameObject instance = Instantiate(prefab, parent);
        instance.GetComponent<Aiming>().target = target;
        GunInstance gun = instance.GetComponent<SoulMagicGunInstance>();
        gun.Initialize(this, target, holder);
        return (instance, gun);
    }
}
