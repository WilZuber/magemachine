using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : AI
{
    // Start is called before the first frame update
    void Awake()
    {
        MeleeWeaponController melee = GetComponent<MeleeWeaponController>();
        wait = new AIWaitBehavior();
        chase = new AIChaseBehavior(1.0f);
        attack = new AIMeleeBehavior(melee, 1.0f);
        Initialize();
    }
}
