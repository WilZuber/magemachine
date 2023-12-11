using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortRangeAI : AI
{
    public static GunType gunType;

    // Start is called before the first frame update
    void Start()
    {
        float minDistance = 0; // stop attacking and run away
        float startDistance = 15.0f; // start attacking
        float MaxDistance = 20.0f; // Stop attacking and chase
        WeaponHolder guns = GetComponent<WeaponHolder>();
        //print("try");
        guns.SpawnGun(gunType, 0);
        wait = new AIWaitBehavior();
        chase = new AIChaseBehavior(startDistance);
        attack = new AIShootingBehavior(guns, minDistance, MaxDistance, 4, 4);
        Initialize();
    }

    public static void InitializeGun()
    {
        gunType = ScriptableObject.CreateInstance<ShortRangeAIGun>();
    }
}

public class ShortRangeAIGun : GunType
{
    void Awake()
    {
        SetProjectileType(Bullet.New());
        prefab = prefabs[5];
        reloadDuration = 0.5f;
    }
}
