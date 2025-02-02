using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private static GunType[] guns; //all guns the player has
    private static int[] gunSelections; //left and right selections, -1 if unset
    public static Dictionary<PartType, int> weaponPartCounts; //number of each part type
    public Dictionary<PartType, GameObject> weaponPartDisplays;
    public static Dictionary<PartType, Sprite> currentWeaponPartSprites; //quick fix
    public Transform weaponPartPanel;
    public GameObject weaponPartPrefab;

    private static int soulRefills;
    private static int skillPoints;

    private static float currentSoul; //carry between levels
    private static float maxSoul;
    private static float maxStamina;
    private static float maxHealth;
    private WeaponHolder playerGuns;

    private static Inventory currentInventory;

    //Components
    public TextMeshProUGUI soulRefillCounter;
    public TextMeshProUGUI skillPointCounter;
    public Image[] gunSprites;
    public RectTransform[] gunSelectionHighlights; //left and right
    private SoulManager playerSoulManager;
    private HealthManager playerHealthManager;
    private StaminaManager playerStaminaManager;
    public GameObject player;
    public SkillTreeSkillPointCounterUpdater skillPointUpdater;
    public static bool menuSound;

    //called from InventoryManager to prevent load order errors
    public void Initialize()
    {
        playerSoulManager = player.GetComponent<SoulManager>();
        playerHealthManager = player.GetComponent<HealthManager>();
        playerStaminaManager = player.GetComponent<StaminaManager>();
        playerGuns = player.GetComponent<WeaponHolder>();

        playerHealthManager.SetMaxHealth(maxHealth); // health and stamina should reset every level
        playerSoulManager.SetMaxSoul(maxSoul);
        playerStaminaManager.SetMaxStamina(maxStamina);

        playerSoulManager.SetSoul(currentSoul);
        playerHealthManager.SetHealth(maxHealth); // we want it filled up

        playerGuns.SpawnGun(guns[gunSelections[0]], 0);
        playerGuns.SpawnGun(guns[gunSelections[1]], 1);

        currentInventory = this;

        for (int i = 0; i < guns.Length; i++)
        {
            UpdateGunSprite(i);
        }
        UpdateGunSelectionHighlight(gunSelectionHighlights[0], gunSelections[0]);
        UpdateGunSelectionHighlight(gunSelectionHighlights[1], gunSelections[1]);
        UpdateSoulRefillCounter();
        UpdateSkillPointCounter();
        AI.player = player;

        menuSound = false;


        weaponPartDisplays = new();
        foreach (PartType type in currentWeaponPartSprites.Keys)
        {
            AddWeaponPart(type, currentWeaponPartSprites[type]);
            UpdateWeaponPart(type);
        }

    }

    public static int GetSkillPoints()
    {
        return skillPoints;
    }

    //World interaction

    //Try to collect the given item. Returns whether it was picked up.
    public static bool CollectItem(Pickup item)
    {
        switch (item.type)
        {
            case PickupType.SoulRefillPotion:
                soulRefills++;
                currentInventory.UpdateSoulRefillCounter();
                return true;
            case PickupType.SkillPoint:
                skillPoints++;
                currentInventory.UpdateSkillPointCounter();
                return true;
            case PickupType.WeaponPart:
                return CollectWeaponPart((WeaponPartPickup)item);
            case PickupType.Gun:
                return CollectGun(((GunPickup)item).gunType);
        }
        //Switch should be exhaustive
        print("Error: item not implemented. Add it to the PickupType enum in Pickup, and include a case for it here.");
        return false;
    }

    public static bool CollectWeaponPart(WeaponPartPickup part)
    {
        WeaponPart weaponPart = part.GetPart();
        PartType type = weaponPart.type;
        if (weaponPartCounts.ContainsKey(type))
        {
            weaponPartCounts[type]++;
            currentInventory.UpdateWeaponPart(weaponPart.type);
        }
        else
        {
            weaponPartCounts.Add(type, 1);
            currentInventory.AddWeaponPart(weaponPart.type, weaponPart.inventorySprite);
        }
        return true;
    }

    private void AddWeaponPart(PartType type, Sprite sprite)
    {
        GameObject instance = Instantiate(weaponPartPrefab, weaponPartPanel);
        instance.GetComponent<Image>().sprite = sprite;
        if (!currentWeaponPartSprites.ContainsKey(type))
        {
            currentWeaponPartSprites.Add(type, sprite);
        }
        weaponPartDisplays.Add(type, instance);
    }

    private void UpdateWeaponPart(PartType type)
    {
        GameObject instance = weaponPartDisplays[type];
        TextMeshProUGUI text = instance.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        text.text = weaponPartCounts[type].ToString();
    }

    public static bool CollectGun(GunType gun)
    {
        int index = Array.IndexOf(guns, null);
        if (index == -1)
        {
            return false;
        }
        else
        {
            guns[index] = gun;
            currentInventory.UpdateGunSprite(index);
            HUDUpdateGunSprites.UpdateGunSprites();
            return true;
        }
    }

    public static void FinishLevel()
    {
        maxHealth = currentInventory.playerHealthManager.GetMaxHealth();
        maxSoul = currentInventory.playerSoulManager.GetMaxSoul();
        maxStamina = currentInventory.playerStaminaManager.GetMaxStamina();

        currentSoul = currentInventory.playerSoulManager.GetSoul();
    }
    public static void ResetInventory()
    {
        guns = new GunType[]
        {
            ScriptableObject.CreateInstance<TestGun1>(),
            ScriptableObject.CreateInstance<TestGun3>(),
            null,
            null
        };
        gunSelections = new int[] { 0, 1 };
        weaponPartCounts = new();
        currentWeaponPartSprites = new();

        soulRefills = 0;
        skillPoints = 0;
        LevelGenerator.level = 1; //move later
        LevelGenerator.CreateSpawnFunctions();
        currentSoul = 100;
        maxSoul = 100;
        maxStamina = 100;
        maxHealth = 20;
    }

    public static GunType GetLeftGun()
    {
        return guns[gunSelections[0]];
    }

    //Buttons
    public void SelectLeftGun(int index)
    {
        menuSound = true;
        SelectGun(index, 0);
    }
    public void SelectRightGun(int index)
    {
        menuSound = true;
        SelectGun(index, 1);
    }

    //Equips the gun at the given index on the given side, or puts it away if it is already selected.
    //Unequips the gun on the other side first if necessary.
    private void SelectGun(int index, int gunSideIndex)
    {
        //currently equipped gun on the given side
        int currentSelection = gunSelections[gunSideIndex];
        int otherSideIndex = 1 - gunSideIndex;
        int otherSelection = gunSelections[otherSideIndex];

        //deselect the gun
        if (currentSelection == index)
        {
            menuSound = true;
            UnsetGun(gunSideIndex);
        }
        //take the gun from the other side
        else if (otherSelection == index)
        {
            menuSound = true;
            UnsetGun(otherSideIndex);
            SetGun(index, gunSideIndex);
        }
        //select the gun normally
        else
        {
            menuSound = true;
            SetGun(index, gunSideIndex);
        }
        HUDUpdateGunSprites.UpdateGunSprites();
    }

    private void SetGun(int index, int gunSideIndex)
    {
        gunSelections[gunSideIndex] = index;
        playerGuns.SpawnGun(guns[index], gunSideIndex);
        UpdateGunSelectionHighlight(gunSelectionHighlights[gunSideIndex], index);
    }

    private void UnsetGun(int gunSideIndex)
    {
        gunSelections[gunSideIndex] = -1;
        playerGuns.SpawnGun(null, gunSideIndex);
        UpdateGunSelectionHighlight(gunSelectionHighlights[gunSideIndex], -1);
    }

    public void RefillSoul()
    {
        if (soulRefills <= 0)
        {
            return; // break out of function, as we dont want to refill
        }
        menuSound = true;
        soulRefills--;
        playerSoulManager.RefillSoulMax();
        UpdateSoulRefillCounter();
    }

    //Display
    private void UpdateGunSprite(int index)
    {
        if (guns[index] == null)
        {
            gunSprites[index].gameObject.SetActive(false);
        }
        else
        {
            gunSprites[index].sprite = guns[index].inventorySprite;
            gunSprites[index].gameObject.SetActive(true);
        }
    }

    private void UpdateGunSelectionHighlight(RectTransform highlight, int index)
    {
        if (index == -1)
        {
            highlight.gameObject.SetActive(false);
        }
        else
        {
            highlight.gameObject.SetActive(true);
            float x = highlight.anchorMin.x;
            float y = 4 - index;
            highlight.anchorMin = new Vector2(x, y);
            highlight.anchorMax = new Vector2(x + 1, y + 1);
        }
    }

    private void UpdateSoulRefillCounter()
    {
        soulRefillCounter.text = soulRefills.ToString();
    }

    private void UpdateSkillPointCounter()
    {
        skillPointCounter.text = skillPoints.ToString();
        skillPointUpdater.UpdateSkillTreeSkillPointCounter();
    }

    public static bool UseSkillPoints(int amount)
    {
        if (skillPoints < amount)
        {
            return false;
        }
        menuSound = true;
        skillPoints -= amount;
        currentInventory.UpdateSkillPointCounter();
        return true;
    }

    public static Sprite[] GetGunSprites()
    {
        Sprite[] sprites = new Sprite[2];

        for (int i = 0; i < gunSelections.Length; i++)
        {
            if (gunSelections[i] < 0) // gunSelections returns a -1 if no gun held
            {
                // dont do anything, as new array index already instantiated to null
            }
            else
            {
                sprites[i] = (guns[gunSelections[i]].inventorySprite);
            }
        }

        return sprites;
    }
}
