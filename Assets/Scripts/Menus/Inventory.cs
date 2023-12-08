using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private static GunType[] guns;
    //private static int leftGunSelection;
    //private static int rightGunSelection;
    private static int[] gunSelections;

    private static int soulRefills;
    private static int skillPoints;

    private static float currentSoul; //carry between levels
    private WeaponHolder playerGuns;

    private static Inventory currentInventory;

    //Components
    public GameObject inventoryCanvas;
    public GameObject inventoryPanel;
    public GameObject gunEditorPanel;
    public GameObject skillTreePanel;
    private GameObject activePanel;
    public TextMeshProUGUI soulRefillCounter;
    public TextMeshProUGUI skillPointCounter;
    public Image[] gunSprites;
    //public RectTransform leftSelectionHighlight;
    //public RectTransform rightSelectionHighlight;
    public RectTransform[] gunSelectionHighlights;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("MainCharacter");
        //player.GetComponent<SoulManager>().SetSoul(currentSoul);
        playerGuns = player.GetComponent<WeaponHolder>();

        //playerGuns.SpawnGun(guns[leftGunSelection], 0);
        //playerGuns.SpawnGun(guns[rightGunSelection], 1);
        playerGuns.SpawnGun(guns[gunSelections[0]], 0);
        playerGuns.SpawnGun(guns[gunSelections[1]], 1);

        currentInventory = this;
        activePanel = inventoryPanel;
        inventoryCanvas.SetActive(false);

        for (int i = 0; i < guns.Length; i++)
        {
            UpdateGunSprite(i);
        }
        UpdateGunSelectionHighlight(gunSelectionHighlights[0], gunSelections[0]);
        UpdateGunSelectionHighlight(gunSelectionHighlights[1], gunSelections[1]);
        UpdateSoulRefillCounter();
        UpdateSkillPointCounter();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryCanvas.activeSelf)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
    }

    private void OpenInventory()
    {
        SetPanel(inventoryPanel);
        inventoryCanvas.SetActive(true);
        PlayerInputToggle.Disable();
    }

    private void CloseInventory()
    {
        inventoryCanvas.SetActive(false);
        PlayerInputToggle.Enable();
    }

    private void SetPanel(GameObject newPanel)
    {
        if (newPanel != activePanel)
        {
            activePanel.SetActive(false);
            newPanel.SetActive(true);
            activePanel = newPanel;
        }

    }

    //World interaction

    //Try to collect the givenitem. Returns whether it was picked up.
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
        return true;
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

        soulRefills = 0;
        skillPoints = 0;
        LevelGenerator.level = 1; //move later
    }

    //Buttons
    public void SelectLeftGun(int index)
    {
        SelectGun(index, 0);
    }
    public void SelectRightGun(int index)
    {
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
            UnsetGun(gunSideIndex);
        }
        //take the gun from the other side
        else if (otherSelection == index)
        {
            UnsetGun(otherSideIndex);
            SetGun(index, gunSideIndex);
        }
        //select the gun normally
        else
        {
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

    }

    public void OpenGunEditor()
    {
        //GunLists.CreateGun().SpawnPickup(playerGuns.transform.position, null);
        SetPanel(gunEditorPanel);
    }

    public void OpenSkillTree()
    {
        SetPanel(skillTreePanel);
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
