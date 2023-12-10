using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTarget : MonoBehaviour
{
    public Vector3 target; //use as a point or direction depending on whether there is a valid target
    public bool validTarget;

    // Update is called once per frame
    void Update()
    {
        Ray ray = new(transform.position + 2 * transform.forward, transform.forward);
        validTarget = Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, -1, QueryTriggerInteraction.Ignore);
        target = validTarget ? hit.point : transform.forward;
    }
}
