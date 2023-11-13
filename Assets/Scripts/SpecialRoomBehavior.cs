using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialRoomBehavior : MonoBehaviour
{

    private int enemiesKilled;
    private int totalEnemies;
    private GameObject mysteryBox;
    private GameObject doorLock1;
    private GameObject doorLock2;
    private GameObject doorLock3;
    private GameObject doorLock4;
    private Animator doorLock1Animator;
    private Animator doorLock2Animator;
    private Animator doorLock3Animator;
    private Animator doorLock4Animator;

    // Start is called before the first frame update
    void Start()
    {
        enemiesKilled = 0;
        mysteryBox = this.gameObject.transform.GetChild(0).gameObject;
        doorLock1 = this.gameObject.transform.GetChild(1).gameObject;
        doorLock1 = this.gameObject.transform.GetChild(2).gameObject;
        doorLock1 = this.gameObject.transform.GetChild(3).gameObject;
        doorLock1 = this.gameObject.transform.GetChild(4).gameObject;

        doorLock1Animator = doorLock1.transform.GetChild(0).gameObject.GetComponent<Animator>();
        doorLock2Animator = doorLock2.transform.GetChild(0).gameObject.GetComponent<Animator>();
        doorLock3Animator = doorLock3.transform.GetChild(0).gameObject.GetComponent<Animator>();
        doorLock4Animator = doorLock4.transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesKilled >= totalEnemies)
        {
            OpenDoors();
            GivePrize();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LockDoors();
            SpawnEnemies(5); // five for now
        }
    }

    private void OpenDoors()
    {
        doorLock1Animator.SetBool("DoorLocked", false);
        doorLock2Animator.SetBool("DoorLocked", false);
        doorLock3Animator.SetBool("DoorLocked", false);
        doorLock4Animator.SetBool("DoorLocked", false);
    }

    private void LockDoors()
    {
        doorLock1Animator.SetBool("DoorLocked", true);
        doorLock2Animator.SetBool("DoorLocked", true);
        doorLock3Animator.SetBool("DoorLocked", true);
        doorLock4Animator.SetBool("DoorLocked", true);
    }

    private void SpawnEnemies(int numberOfEnemies)
    {
        totalEnemies = numberOfEnemies;
        // Spawn enemies
    }

    private void GivePrize()
    {
        // Animate mystery box upward.
    }
}
