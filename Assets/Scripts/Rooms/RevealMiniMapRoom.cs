using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealMiniMapRoom : MonoBehaviour
{
    private GameObject roomCover;

    void Start()
    {
        roomCover = this.gameObject.transform.GetChild(0).gameObject;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            roomCover.SetActive(false);
        }
    }
}
