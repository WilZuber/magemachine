using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public Transform[] gunPositions;
    private GameObject[] guns;

    public void SpawnGun(GameObject gun, int index)
    {
        //clear slot if it is already used
        if (guns[index] != null)
        {
            Destroy(guns[index]);
        }

        guns[index] = Instantiate(gun, gunPositions[index]);
    }

    public void Fire(int index)
    {

    }
}
