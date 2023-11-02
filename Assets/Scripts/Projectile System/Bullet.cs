using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 30);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Hit(other.gameObject);
        Destroy(this.gameObject);
    }
}
