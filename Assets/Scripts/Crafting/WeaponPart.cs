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
            case 0: return new TestPart1();
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
            default: return new TestPart2();
        }
    }
}

public enum PartType
{
    accelerator,
    generatorBasic,
    generatorExplosion,

    bufferBasic
}