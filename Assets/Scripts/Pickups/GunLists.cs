using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLists
{
    public static GunType CreateGun()
    {
        GunType newGun = ScriptableObject.CreateInstance<TestGun2>();
        return newGun;
    }
}