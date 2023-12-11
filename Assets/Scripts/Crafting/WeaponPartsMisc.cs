using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPAccelerator : WeaponPart
{
    public WPAccelerator()
    {
        type = PartType.accelerator;
        inventorySprite = miscSprites[0];
    }

    public override bool GetProjectile(ProjectileType projectile)
    {
        if (currentProjectile == null)
        {
            currentProjectile = projectile;
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class WPGeneratorBasic : WeaponPart
{
    float generationTime = 1f;
    float timer;
    public WPGeneratorBasic()
    {
        type = PartType.generatorBasic;
        inventorySprite = generatorSprites[0];
        currentProjectile = Bullet.New();
    }

    public override void Update(float time)
    {
        timer -= time;
        if (timer <= 0)
        {
            timer = generationTime;
            GiveProjectile();
        }
    }

    public override void GiveProjectile()
    {
        next?.GetProjectile(currentProjectile);
    }
}

public class WPBufferBasic : WeaponPart
{
    int maxQuantity = 16;
    int quantity;
    float reloadTime = 0.33f;
    float timer;
    public WPBufferBasic()
    {
        type = PartType.bufferBasic;
        inventorySprite = bufferSprites[0];
    }

    /*public override ProjectileType TakeProjectile()
    {
        if (quantity == 0)
        {
            //giveWhenReady = true;
            return null;
        }
        else
        {
            quantity--;
            Debug.Log("buffer: -" + quantity);
            return currentProjectile;
        }
    }*/

    public override void Update(float time)
    {
        timer -= time;
        if (timer <= 0)
        {
            timer = reloadTime;
            if (quantity > 0 && next != null && next.GetProjectile(currentProjectile))
            {
                quantity--;
            }
        }
    }

    public override bool GetProjectile(ProjectileType projectile)
    {
        if (quantity < maxQuantity)
        {
            currentProjectile = projectile;
            quantity++;
            //Debug.Log("buffer: +" + quantity);
            return true;
        }
        else
        {
            return false;
        }
    }
}

public class WPRight : WeaponPart
{

}

public class WPUp : WeaponPart
{
    public override (int, int) NextRelativeLocation()
    {
        return (0, -1);
    }
}

public class WPDown : WeaponPart
{
    public override (int, int) NextRelativeLocation()
    {
        return (0, 1);
    }
}