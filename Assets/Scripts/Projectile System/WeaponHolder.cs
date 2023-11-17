using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public Transform[] gunPositions;
    public GameObject[] gunModels;
    public Gun[] guns;
    public AimTarget target;

    void Start()
    {
        int length = gunPositions.Length;
        gunModels = new GameObject[length];
        guns = new Gun[length];
    }
    public void SpawnGun(GameObject gunModel, Gun gunType, int index)
    {
        //clear slot if it is already used
        if (gunModels[index] != null)
        {
            Destroy(gunModels[index]);
        }

        gunModels[index] = Instantiate(gunModel, gunPositions[index]);
        gunModels[index].GetComponent<Aiming>().target = target;
        //guns[index] = gunType;
        guns[index] = gunModel.GetComponent<Gun>();
        //the first child of the gun model should be the projectile spawn point
        guns[index].projectileSpawn = gunModel.transform.GetChild(0);
    }

    public void Fire(int index)
    {
        //print("holder: " + guns[index].projectileType);
        //guns[index].Fire();
        gunModels[index].GetComponent<Gun>().Fire();
    }
}
