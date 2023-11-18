using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform camera = Camera.main.transform;
        transform.rotation = Quaternion.LookRotation(camera.forward, camera.up);
    }
}
