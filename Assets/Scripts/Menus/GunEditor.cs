using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunEditor : MonoBehaviour
{
    public GameObject editorPanel;
    public GameObject editorButtonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopulatePanel(TechGunType gun)
    {

        int L0 = gun.parts.GetLength(0);
        int L1 = gun.parts.GetLength(1);
        editorPanel.GetComponent<GridLayoutGroup>().constraintCount = L1;
        for (int i = 0; i < L0; i++)
        {
            for (int j = 0; j < L1; j++)
            {
                GunEditorButton.New(j, i, gun.parts[i, j], editorButtonPrefab, editorPanel.transform);
            }
        }
    }
}
