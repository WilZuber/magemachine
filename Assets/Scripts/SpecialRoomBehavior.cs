using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialRoomBehavior : MonoBehaviour
{

    private int enemiesKilled;
    private int totalEnemies;
    // Start is called before the first frame update
    void Start()
    {
        enemiesKilled = 0;
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
        // Open the doors
    }

    private void LockDoors()
    {
        // Lock the doors
        // Player should not be allowed to open them
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
