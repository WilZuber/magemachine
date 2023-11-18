using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialRoomBehavior : MonoBehaviour, IDeathListener
{
    private int enemiesRemaining;
    private GameObject mysteryBox;
    private GameObject doorLock1;
    private GameObject doorLock2;
    private GameObject doorLock3;
    private GameObject doorLock4;
    private Animator doorLock1Animator;
    private Animator doorLock2Animator;
    private Animator doorLock3Animator;
    private Animator doorLock4Animator;
    private Animator mysteryBoxAnimator;
    public GameObject enemy;
    public int quantity;

    private bool playerEntered;

    // Start is called before the first frame update
    void Start()
    {
        mysteryBox = this.gameObject.transform.GetChild(0).gameObject;
        doorLock1 = this.gameObject.transform.GetChild(1).gameObject;
        doorLock2 = this.gameObject.transform.GetChild(2).gameObject;
        doorLock3 = this.gameObject.transform.GetChild(3).gameObject;
        doorLock4 = this.gameObject.transform.GetChild(4).gameObject;

        doorLock1Animator = doorLock1.transform.GetChild(0).gameObject.GetComponent<Animator>();
        doorLock2Animator = doorLock2.transform.GetChild(0).gameObject.GetComponent<Animator>();
        doorLock3Animator = doorLock3.transform.GetChild(0).gameObject.GetComponent<Animator>();
        doorLock4Animator = doorLock4.transform.GetChild(0).gameObject.GetComponent<Animator>();

        mysteryBoxAnimator = mysteryBox.GetComponent<Animator>();

        playerEntered = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !playerEntered)
        {
            playerEntered = true;
            LockDoors();
            SpawnEnemies(quantity);
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

        int enemySpawnX = 2;
        enemiesRemaining = numberOfEnemies;
        for (int i = 0; i < numberOfEnemies; i++)
        {

            GameObject instance = Instantiate(enemy, transform.position + new Vector3(enemySpawnX - i, 0, 0), Quaternion.identity);
            instance.GetComponent<HealthManager>().deathListener = this;
        }
    }

    private void GivePrize()
    {
        mysteryBoxAnimator.SetBool("PrizeGiven", true); // animates mysterybox upward
    }

    public void DeathTrigger()
    {
        if (--enemiesRemaining == 0)
        {
            OpenDoors();
            GivePrize();
        }
    }

}
