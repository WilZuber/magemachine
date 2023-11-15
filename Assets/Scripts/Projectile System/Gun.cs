using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform projectileSpawn;
    public Bullet bullet;
    public Beam beam;
    
    void Start()
    {
        bullet = ScriptableObject.CreateInstance<Bullet>();
        //bullet.Init(bulletPrefab);
        bullet.bulletPrefab = bulletPrefab;
        bullet.damage = 2;
        bullet.lifetime = 5.0f;

        Bullet child = ScriptableObject.CreateInstance<Bullet>();
        child.bulletPrefab = bulletPrefab;
        child.damage = 5;
        child.lifetime = 5.0f;
        child.speed = 5;

        bullet.next.Add(child);
        beam = ScriptableObject.CreateInstance<Beam>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Projectile.CreateProjectile(bulletPrefab, 2, projectileSpawn.position, transform.forward*5);
            bullet.Fire(projectileSpawn.position, transform.forward, 3);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            //Projectile.CreateProjectile(1, projectileSpawn.position, transform.forward);
            beam.Fire(projectileSpawn.position, transform.forward);
        }
    }
}
