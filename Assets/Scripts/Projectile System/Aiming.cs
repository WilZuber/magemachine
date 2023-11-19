using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public AimTarget target;

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = FindDirection();
        if (!forward.Equals(Vector3.zero))
        {
            Vector3 up = transform.parent.up;
            Quaternion look = Quaternion.LookRotation(forward, up);
            transform.rotation = look;
        }
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
