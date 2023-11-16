using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//creates an explosion after a fixed timer
public class Bomb : Projectile
{
    public static GameObject sharedPrefab;
    public override void SetPrefab(GameObject prefab)
    {
        sharedPrefab = prefab;
    }

    void Awake()
    {
        prefab = sharedPrefab;
        speed = 2.0f;
        next.Add(Explosion.New());
    }

    public override void Hit(GameObject self, GameObject other, Vector3 bounceDirection)
    {
        //do nothing
    }

    public static Bomb New()
    {
        return CreateInstance<Bomb>();
    }
}
