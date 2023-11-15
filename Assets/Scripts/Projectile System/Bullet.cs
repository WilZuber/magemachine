using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//basic projectile
public class Bullet : Projectile
{
    void Awake()
    {
        damage = 2.0f;
        speed = 5.0f;
    }

    public static Bullet New()
    {
        return CreateInstance<Bullet>();
    }
}
