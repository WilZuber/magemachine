using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static List<GunType> guns;
    private static int leftGunSelection;
    private static int rightGunSelection;

    private static int soulRefills;
    private static int skillPoints;
    private WeaponHolder playerGuns;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("MainCharacter");
        playerGuns = player.GetComponent<WeaponHolder>();
        playerGuns.SpawnGun(guns[leftGunSelection], 0);
        playerGuns.SpawnGun(guns[rightGunSelection], 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ResetInventory()
    {
        guns = new()
        {
            ScriptableObject.CreateInstance<TestGun1>(),
            ScriptableObject.CreateInstance<TestGun3>()
        };
    }
}
