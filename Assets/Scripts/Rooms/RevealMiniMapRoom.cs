using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealMiniMapRoom : MonoBehaviour
{
    public GameObject roomCover;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            roomCover.SetActive(false);
        }
    }
}
