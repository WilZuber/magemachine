using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    WeaponHolder guns;
    MeleeWeaponController melee;
    TeleportGun teleporter;
    public static bool meleeSound;
    public static bool teleportSound;
    //public static bool isTeleportableEnemy;

    // Start is called before the first frame update
    void Start()
    {
        guns = GetComponent<WeaponHolder>();
        melee = GetComponent<MeleeWeaponController>();
        
        TeleportType type = ScriptableObject.CreateInstance<TeleportType>();
        guns.SpawnGun(type, 2);
        teleporter = (TeleportGun)guns.guns[2];

        meleeSound = false;
        teleportSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            guns.Fire(0);
        }
        else if (Input.GetMouseButton(1))
        {
            guns.Fire(1);
        }
        else if (Input.GetKey(KeyCode.F))
        {
            meleeSound = true;
            melee.Attack();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            //teleport set
            teleporter.Fire();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            //teleport use
            teleportSound = true;
            teleporter.SwitchWithTeleportPoint();
        }
    }
}
