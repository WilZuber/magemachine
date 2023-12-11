using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunEditorButton : MonoBehaviour
{
    public int x, y;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void New(int x, int y, PartType type, GameObject prefab, Transform parent)
    {
        GameObject instance = Instantiate(prefab, parent);
        GunEditorButton button = instance.GetComponent<GunEditorButton>();
        button.x = x;
        button.y = y;
        instance.GetComponent<Image>().sprite = WeaponPart.New(type).inventorySprite;
    }
}
