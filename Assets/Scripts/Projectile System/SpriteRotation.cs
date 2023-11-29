using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Camera.main != null)
        {
            Transform camera = Camera.main.transform;
            transform.rotation = Quaternion.LookRotation(camera.forward, camera.up);
        }
    }
}
