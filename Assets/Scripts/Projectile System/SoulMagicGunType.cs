using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMagicGunType : GunType
{
    public override (GameObject, GunInstance) Spawn(Transform parent, AimTarget target, WeaponHolder holder)
    {
        GameObject instance = Instantiate(prefab, parent);
        instance.GetComponent<Aiming>().target = target;
        GunInstance gun = instance.GetComponent<SoulMagicGunInstance>();
        gun.Initialize(this, target, holder);
        Debug.Log("uwu");
        return (instance, gun);
    }
}
