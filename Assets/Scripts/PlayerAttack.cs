using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    WeaponHolder guns;
    MeleeWeaponController melee;
    //teleport script
    public GameObject[] startingGuns;

    // Start is called before the first frame update
    void Start()
    {
        guns = GetComponent<WeaponHolder>();
        melee = GetComponent<MeleeWeaponController>();
        //set teleport script
        
        for (int i = 0; i < 2; i++)
        {
            GameObject model = startingGuns[i];
            Gun gun = model.GetComponent<Gun>();
            guns.SpawnGun(model, gun, i);
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
        else if (Input.GetKeyDown(KeyCode.F))
        {
            melee.Attack();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            //teleport set
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            //teleport use
        }
    }
}
