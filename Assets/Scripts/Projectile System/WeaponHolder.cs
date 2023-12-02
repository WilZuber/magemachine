using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public Transform[] gunPositions;
    public GameObject[] gunModels;
    public GunInstance[] guns;
    public AimTarget target;

    void Start()
    {
        if (gunModels.Length == 0)
        {
            int length = gunPositions.Length;
            gunModels = new GameObject[length];
            guns = new GunInstance[length];
        }
    }

    // SpawnGun
    // spawns in a gun of a gunType which exists at index
    // Parameters:
    //      gunType: GunType that represents the type of the gun
    //      index: Int representing the index of the weapon holder to which the gun belongs
    // Pre: 
    // Post: creates gun model??? idk I give up on documenting this stuff -W
    public void SpawnGun(GunType gunType, int index)
    {
        if (gunType == null)
        {
            Destroy(gunModels[index]);
            guns[index] = null;
            return;
        }

        //clear slot if it is already used
        if (gunModels[index] != null)
        {
            Destroy(gunModels[index]);
        }
        (gunModels[index], guns[index]) = gunType.Spawn(gunPositions[index], target, this);
    }


    // Fire
    // delegates firing to a gun at specified index
    // Parameters:
    //      index: int that represents the index of a gun in weapon holder
    // Pre:
    // Post: Fires the gun if it is possible to fire, return true. If not possible fire, returns false.
    public bool Fire(int index)
    {
        if (guns[index] != null)
        {
            return guns[index].Fire();
        }
        else
        {
            return false;
        }
    }
}
