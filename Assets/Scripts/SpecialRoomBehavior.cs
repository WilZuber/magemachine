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
    public GameObject enemy;

    private bool playerEntered;

    // Start is called before the first frame update
    void Start()
    {
        enemiesKilled = 0;
        mysteryBox = this.gameObject.transform.GetChild(0).gameObject;
        doorLock1 = this.gameObject.transform.GetChild(1).gameObject;
        doorLock2 = this.gameObject.transform.GetChild(2).gameObject;
        doorLock3 = this.gameObject.transform.GetChild(3).gameObject;
        doorLock4 = this.gameObject.transform.GetChild(4).gameObject;

        doorLock1Animator = doorLock1.transform.GetChild(0).gameObject.GetComponent<Animator>();
        doorLock2Animator = doorLock2.transform.GetChild(0).gameObject.GetComponent<Animator>();
        doorLock3Animator = doorLock3.transform.GetChild(0).gameObject.GetComponent<Animator>();
        doorLock4Animator = doorLock4.transform.GetChild(0).gameObject.GetComponent<Animator>();

        playerEntered = false;
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
        if (other.gameObject.CompareTag("Player") && !playerEntered)
        {
            playerEntered = true;
            LockDoors();
            SpawnEnemies(5); // five for now
        }
    }

    private void OpenDoors()
    {
        doorLock1Animator.SetBool("DoorsLocked", false);
        doorLock2Animator.SetBool("DoorsLocked", false);
        doorLock3Animator.SetBool("DoorsLocked", false);
        doorLock4Animator.SetBool("DoorsLocked", false);
    }

    private void LockDoors()
    {
        doorLock1Animator.SetBool("DoorsLocked", true);
        doorLock2Animator.SetBool("DoorsLocked", true);
        doorLock3Animator.SetBool("DoorsLocked", true);
        doorLock4Animator.SetBool("DoorsLocked", true);
    }

    private void SpawnEnemies(int numberOfEnemies)
    {
        totalEnemies = numberOfEnemies;
        for (int i = 0; i < numberOfEnemies; i++)
        {
             Instantiate(enemy, transform.position, Quaternion.identity);
        }
    }

    private void GivePrize()
    {
        // Animate mystery box upward.
    }
}
