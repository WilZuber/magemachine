using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    //Components
    public GameObject inventoryCanvas;
    public GameObject inventoryPanel;
    public GameObject gunEditorPanel;
    public GameObject skillTreePanel;
    private static GameObject activePanel;
    public static bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        activePanel = inventoryPanel;
        inventoryPanel.GetComponent<Inventory>().Initialize();
        inventoryCanvas.SetActive(false);
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape))
            {
                CloseInventory();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Tab) && !PauseScript.isPaused)
            {
                OpenInventory();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseScript.Toggle();
            }
        }
    }

    private void OpenInventory()
    {
        if (PauseScript.characterIsAlive)
        {
            SetPanel(inventoryPanel);
            inventoryCanvas.SetActive(true);
            isOpen = true;
            PlayerInputToggle.Disable();
        }
    }

    private void CloseInventory()
    {
        if (PauseScript.characterIsAlive)
        {
            inventoryCanvas.SetActive(false);
            isOpen = false;
            PlayerInputToggle.Enable();
        }
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

    //Buttons
    public void OpenGunEditor()
    {
        //GunLists.CreateGun().SpawnPickup(playerGuns.transform.position, null);
        GunType leftGun = Inventory.GetLeftGun();
        if (leftGun is TechGunType)
        {
            gunEditorPanel.GetComponent<GunEditor>().PopulatePanel((TechGunType) leftGun);
            SetPanel(gunEditorPanel);
        }
    }

    public void OpenSkillTree()
    {
        SetPanel(skillTreePanel);
    }
}
