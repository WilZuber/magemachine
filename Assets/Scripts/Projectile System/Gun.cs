using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform projectileSpawn;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Projectile.CreateProjectile(bulletPrefab, 2, projectileSpawn.position, transform.forward*5);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Projectile.CreateProjectile(1, projectileSpawn.position, transform.forward);
        }
    }
}
