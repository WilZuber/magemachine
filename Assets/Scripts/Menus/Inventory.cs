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

    private static float currentSoul; //carry between levels
    private WeaponHolder playerGuns;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("MainCharacter");
        //player.GetComponent<SoulManager>().SetSoul(currentSoul);
        playerGuns = player.GetComponent<WeaponHolder>();

        leftGunSelection = 0;
        rightGunSelection = 1;
        playerGuns.SpawnGun(guns[leftGunSelection], 0);
        playerGuns.SpawnGun(guns[rightGunSelection], 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static void CollectItem(Pickup item)
    {
        switch (item.type)
        {
            case PickupType.SoulRefillPotion: soulRefills++; break;
            case PickupType.SkillPoint: skillPoints++; break;
            case PickupType.WeaponPart: CollectWeaponPart((WeaponPartPickup)item); break;
            case PickupType.Gun: guns.Add(((GunPickup)item).gunType); break;
        }
    }

    public static void CollectWeaponPart(WeaponPartPickup part)
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
