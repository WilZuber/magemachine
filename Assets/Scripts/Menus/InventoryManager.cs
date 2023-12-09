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

    // Start is called before the first frame update
    void Start()
    {
        activePanel = inventoryPanel;
        inventoryCanvas.SetActive(false);
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

    //Buttons
    public void OpenGunEditor()
    {
        //GunLists.CreateGun().SpawnPickup(playerGuns.transform.position, null);
        SetPanel(gunEditorPanel);
    }

    public void OpenSkillTree()
    {
        SetPanel(skillTreePanel);
    }
}
