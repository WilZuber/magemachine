using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public AimTarget target;

    // Update is called once per frame
    void Update()
    {
        Vector3 up = transform.parent.up;
        Vector3 forward = FindDirection();
        if (Vector3.Dot(forward, up) == 1)
        {
            up = transform.parent.right;
        }
        Quaternion look = Quaternion.LookRotation(forward, up);
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
