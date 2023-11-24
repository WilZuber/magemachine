using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//basic projectile
public class PurpleBullet : Bullet
{
    private static GameObject childPrefab;
    public override void SetPrefab(GameObject prefab) => childPrefab = prefab;
    public override GameObject GetPrefab() => childPrefab;

    public static new PurpleBullet New()
    {
        return CreateInstance<PurpleBullet>();
    }
}
