using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//basic projectile
public class Bullet : Projectile
{
    void Awake()
    {
        prefab = sharedBulletPrefab;
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
