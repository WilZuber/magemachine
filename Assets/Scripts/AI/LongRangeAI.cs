using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeAI : AI
{
    public static GunType gunType;

    // Start is called before the first frame update
    void Start()
    {
        float minDistance = 0;//20.0f;
        float startDistance = 100.0f;
        float MaxDistance = 100.0f;
        WeaponHolder guns = GetComponent<WeaponHolder>();
        guns.SpawnGun(gunType, 0);
        wait = new AIWaitBehavior();
        chase = new AIChaseBehavior(startDistance);
        attack = new AIShootingBehavior(guns, minDistance, MaxDistance, 3, 8);
        Initialize();
    }

    public static void InitializeGun()
    {
        gunType = ScriptableObject.CreateInstance<LongRangeAIGun>();
    }
}

public class LongRangeAIGun : SoulMagicGunType
{
    void Awake()
    {
        Bullet bullet = RedBullet.New();
        bullet.next.Add(Explosion.New());
        SetProjectileType(bullet);
        prefab = prefabs[1];
        reloadDuration = 1.0f;
        cost = 1;
        shotsAfterDouble = 10;
        maxSafeMultiplier = 5;
    }
}
