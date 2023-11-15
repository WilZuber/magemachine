using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bombPrefab;
    public GameObject explosionPrefab;
    public Transform projectileSpawn;
    public Bullet bullet;
    public Beam beam;
    public LinearBlink blink;
    public Bomb bomb;
    
    void Start()
    {
        TestProjectile1();
        TestProjectile2();
        TestProjectile3();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bullet.Fire(projectileSpawn.position, transform.forward);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            blink.Fire(projectileSpawn.position, transform.forward);
        }
        else if (Input.GetMouseButtonDown(2))
        {
            bomb.Fire(projectileSpawn.position, transform.forward);
        }
    }

    private void TestProjectile1()
    {
        bullet = Bullet.New();
        bullet.bulletPrefab = bulletPrefab;
        

        Bullet child = Bullet.New();
        child.bulletPrefab = bulletPrefab;
        child.damage = 5;

        bullet.next.Add(child);
    }

    private void TestProjectile2()
    {
        blink = LinearBlink.New();
        Bullet bullet2 = Bullet.New();
        bullet2.bulletPrefab = bulletPrefab;
        Bullet child = Bullet.New();
        child.bulletPrefab = bulletPrefab;
        child.damage = 5;
        Explosion explosion = Explosion.New();
        explosion.bulletPrefab = explosionPrefab;

        child.next.Add(explosion);
        bullet2.next.Add(child);
        blink.next.Add(bullet2);
    }

    private void TestProjectile3()
    {
        bomb = Bomb.New();
        bomb.bulletPrefab = bombPrefab;
        bomb.next[0].bulletPrefab = explosionPrefab;
    }
}
