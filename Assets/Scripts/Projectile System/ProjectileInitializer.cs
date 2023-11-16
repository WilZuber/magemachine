using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Put on a single game object and drag prefabs in, it then sets the static fields for projectiles to pull from
//The prefab and spript must have exactly the same name for this to work
public class ProjectileInitializer : MonoBehaviour
{
    public GameObject[] prefabs;
    void Start()
    {
        foreach (GameObject prefab in prefabs)
        {
            //get name of prefab, get projectile script with same name, instantiate, and set prefab for subclass
            string projectileName = prefab.name;
            Projectile projectileInstance = (Projectile)ScriptableObject.CreateInstance(projectileName);
            projectileInstance.SetPrefab(prefab);
        }
    }
}
