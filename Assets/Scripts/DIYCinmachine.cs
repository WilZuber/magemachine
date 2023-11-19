using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIYCinmachine : MonoBehaviour
{
    private Transform pivot;
    private Ray ray;
    private RaycastHit hit;
    private float distance = 1.5f;
    private Vector3 direction;

    void Start()
    {
        pivot = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        direction = -transform.forward;
        ray = new Ray(pivot.position, direction);
        Debug.DrawRay(ray.origin, ray.direction*distance, Color.red);

        if (Physics.Raycast(ray, out hit, distance, LayerMask.GetMask("Room")) && (hit.distance < distance))
        {
            transform.position = hit.point;
        }
        else
        {
            transform.position = distance * direction + pivot.position;
        }
    }
}
