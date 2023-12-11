using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPart
{
    public static Sprite[] miscSprites;
    public static Sprite[] generatorSprites;
    public static Sprite[] bufferSprites;
    public static Sprite[] modifierSprites;

    public Sprite inventorySprite; //sprite for this part
    public PartType type;

    //todo: action in gun
    public WeaponPart previous, next;
    public ProjectileType currentProjectile;
    public bool giveWhenReady; //give the next received projectile to the next part

    //take from this part
    public virtual ProjectileType TakeProjectile()
    {
        ProjectileType projectile = currentProjectile;
        currentProjectile = null;
        //currentProjectile = previous.TakeProjectile();
        return projectile;
    }
    
    //give to the next part
    public virtual void GiveProjectile()
    {
        next.GetProjectile(currentProjectile);
    }

    //get from the previous part
    public virtual bool GetProjectile(ProjectileType projectile)
    {
        /*if (giveWhenReady)
        {
            next.GetProjectile(projectile);
            giveWhenReady = false;
        }
        else
        {
            currentProjectile = projectile;
        }*/
        if (next != null)
        {
            return next.GetProjectile(projectile);
        }
        else currentProjectile = projectile;
        return true;
    }

    public virtual void Update(float time)
    {

    }

    //roll a random weapon part
    public static WeaponPart CreateWeaponPart()
    {
        return RollTier1();
    }

    private static WeaponPart RollTier1()
    {
        switch (Random.Range(0, 3))
        {
            default: return RollTier2();
            case 0: return new WPAccelerator();
        }
    }

    private static WeaponPart RollTier2()
    {
        switch (Random.Range(0, 3))
        {
            default: return RollTier3();
        }
    }

    private static WeaponPart RollTier3()
    {
        switch (Random.Range(0, 3))
        {
            default: return new WPGeneratorBasic();
        }
    }

    public static WeaponPart New(PartType type)
    {
        switch (type)
        {
            case PartType.accelerator: return new WPAccelerator();
            case PartType.generatorBasic: return new WPGeneratorBasic();
            case PartType.bufferBasic: return new WPBufferBasic();
        }
        Debug.Log("Error: " + type + " not implemented");
        return null;
    }
}

public enum PartType
{
    accelerator,
    generatorBasic,
    generatorExplosion,

    bufferBasic
}