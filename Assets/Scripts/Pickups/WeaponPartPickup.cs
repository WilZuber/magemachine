using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPartPickup : Pickup
{
    public WeaponPart GetPart()
    {
        return WeaponPart.CreateWeaponPart();
    }
}
