using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPart1 : WeaponPart
{
    public TestPart1()
    {
        type = PartType.accelerator;
        inventorySprite = miscSprites[0];
    }
}

public class TestPart2 : WeaponPart
{
    public TestPart2()
    {
        type = PartType.generatorBasic;
        inventorySprite = generatorSprites[0];
    }
}
