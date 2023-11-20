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
    public void SpawnGun(GunType gunType, int index)
    {
        //print("catch");
        //clear slot if it is already used
        if (gunModels[index] != null)
        {
            Destroy(gunModels[index]);
        }
        (gunModels[index], guns[index]) = gunType.Spawn(gunPositions[index], target);
    }

    public bool Fire(int index)
    {
        return guns[index].Fire();
    }
}
