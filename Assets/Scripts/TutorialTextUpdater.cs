using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialTextUpdater : MonoBehaviour
{
    public TMP_Text textMesh;
    public string prompt;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textMesh.text = prompt;
        }
    }
}
