using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    WeaponHolder guns;
    MeleeWeaponController melee;
    //teleport script
    Teleporter teleport;

    // Start is called before the first frame update
    void Start()
    {
        guns = GetComponent<WeaponHolder>();
        melee = GetComponent<MeleeWeaponController>();
        teleport = GetComponent<Teleporter>();
        
        GunType[] newGuns = {ScriptableObject.CreateInstance<TestGun1>(), ScriptableObject.CreateInstance<TestGun3>()};
        for (int i = 0; i < 2; i++)
        {
            guns.SpawnGun(newGuns[i], i);
        }
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
            melee.Attack();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            //switch with teleport point if set
            teleport.SetTeleportPoint();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            //teleport use
                //test basic teleporting before testing full mechanic
            teleport.NewPosition();
            //teleport.SwitchWithTeleportPoint
        }
    }
}
