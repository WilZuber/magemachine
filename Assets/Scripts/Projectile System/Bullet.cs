using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Data", menuName ="ScriptableObjects/Bullet",order =1)]

//basic projectile
public class Bullet : Projectile
{
    public static GameObject sharedPrefab;
    public override void SetPrefab(GameObject prefab)
    {
        sharedPrefab = prefab;
    }

    void Awake()
    {
        prefab = sharedPrefab;
        damage = 2.0f;
        speed = 5.0f;
    }

    public static Bullet New()
    {
        return CreateInstance<Bullet>();
    }

    public Bullet create(){
        return CreateInstance<Bullet>();
    }
}
