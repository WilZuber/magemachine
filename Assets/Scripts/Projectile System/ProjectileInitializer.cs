using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Put on a single game object and drag prefabs in, it then sets the static fields for projectiles to pull from
//The prefab and spript must have exactly the same name for this to work
public class ProjectileInitializer : MonoBehaviour
{
    public GameObject[] projectilePrefabs;
    public GameObject[] gunPrefabs;
    void Start()
    {
        foreach (GameObject prefab in projectilePrefabs)
        {
            //get name of prefab, get projectile script with same name, instantiate, and set prefab for subclass
            string projectileName = prefab.name;
            ScriptableObject instance = ScriptableObject.CreateInstance(projectileName);
            ProjectileType projectileInstance = (ProjectileType)instance;
            projectileInstance.SetPrefab(prefab);
        }

        GunType.prefabs = gunPrefabs;
        ShortRangeAI.InitializeGun();
        LongRangeAI.InitializeGun();

        Inventory.ResetInventory();
    }
}
