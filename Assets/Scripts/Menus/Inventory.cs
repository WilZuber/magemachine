using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private static Inventory currentInventory;
    private bool inventoryOpen;

    //Components
    public GameObject inventoryCanvas;
    public TextMeshProUGUI soulRefillCounter;
    public TextMeshProUGUI skillPointCounter;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("MainCharacter");
        //player.GetComponent<SoulManager>().SetSoul(currentSoul);
        playerGuns = player.GetComponent<WeaponHolder>();

        playerGuns.SpawnGun(guns[leftGunSelection], 0);
        playerGuns.SpawnGun(guns[rightGunSelection], 1);

        currentInventory = this;
        inventoryOpen = false;
        inventoryCanvas.SetActive(false);
        UpdateSoulRefillCounter();
        UpdateSkillPointCounter();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!inventoryOpen)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }
    }

    private void OpenInventory()
    {
        inventoryOpen = true;
        inventoryCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void CloseInventory()
    {
        inventoryOpen = false;
        inventoryCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public static void CollectItem(Pickup item)
    {
        switch (item.type)
        {
            case PickupType.SoulRefillPotion:
                soulRefills++;
                currentInventory.UpdateSoulRefillCounter();
                break;
            case PickupType.SkillPoint:
                skillPoints++;
                currentInventory.UpdateSkillPointCounter();
                break;
            case PickupType.WeaponPart:
                CollectWeaponPart((WeaponPartPickup)item);
                break;
            case PickupType.Gun:
                guns.Add(((GunPickup)item).gunType);
                break;
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
        leftGunSelection = 0;
        rightGunSelection = 1;

        soulRefills = 0;
        skillPoints = 0;
    }

    private void UpdateSoulRefillCounter()
    {
        soulRefillCounter.text = soulRefills.ToString();
    }

    private void UpdateSkillPointCounter()
    {
        skillPointCounter.text = skillPoints.ToString();
    }
}
