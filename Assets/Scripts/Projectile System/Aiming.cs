using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public AimTarget target;

    // Update is called once per frame
    void Update()
    {
        Quaternion look = Quaternion.LookRotation(FindDirection(), Vector3.up);
        transform.rotation = look;
    }

    private Vector3 FindDirection()
    {
        if (target.validTarget)
        {
            return target.target - transform.position;
        }
        else
        {
            return target.target;
        }
    }
}
