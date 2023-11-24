using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicExplosion : Explosion
{
    private static GameObject childPrefab;
    public override void SetPrefab(GameObject prefab) => childPrefab = prefab;
    public override GameObject GetPrefab() => childPrefab;

    public static new MagicExplosion New()
    {
        return CreateInstance<MagicExplosion>();
    }
}
