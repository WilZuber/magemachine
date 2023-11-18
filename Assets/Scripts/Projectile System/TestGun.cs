using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun : GunType
{
    public Bullet bullet;
    public Beam beam;
    public LinearBlink blink;
    public Bomb bomb;
    
    void Start()
    {
        TestProjectile1();
        TestProjectile2();
        TestProjectile3();
        reloadDuration = 0.5f;
    }

    // Update is called once per frame
    /*void Update()
    {
        if (reloadTimer > 0)
        {
            reloadTimer -= Time.deltaTime;
        }
        //should be able to remove the rest of this and only have the base class update
        if (Input.GetMouseButton(0))
        {
            Fire(bullet);
        }
        else if (Input.GetMouseButton(1))
        {
            Fire(blink);
        }
        else if (Input.GetMouseButtonDown(2))
        {
            Fire(bomb);
        }
    }*/

    private void TestProjectile1()
    {
        bullet = Bullet.New();

        Bullet child = Bullet.New();
        child.damage = 5;

        bullet.next.Add(child);
    }

    private void TestProjectile2()
    {
        blink = LinearBlink.New();
        Bullet bullet2 = Bullet.New();
        Bullet child = Bullet.New();
        child.damage = 5;
        Explosion explosion = Explosion.New();

        child.next.Add(explosion);
        bullet2.next.Add(child);
        blink.next.Add(bullet2);
    }

    private void TestProjectile3()
    {
        bomb = Bomb.New();
    }
}
