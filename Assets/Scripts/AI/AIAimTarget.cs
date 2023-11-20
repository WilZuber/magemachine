using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAimTarget : AimTarget
{
    AI ai;

    void Start()
    {
        ai = GetComponent<AI>();
    }

    void Update()
    {
        
    }
    
    void FixedUpdate()
    {
        if (ai.CanSeePlayer())
        {
            target = ai.player.transform.position + 0.5f*Vector3.up;
            validTarget = true;
        }
        else
        {
            target = transform.forward;
            validTarget = false;
        }
    }
}
