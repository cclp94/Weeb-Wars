using UnityEngine;
using System.Collections;

public class LaserBullet : GunUpgrade
{

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "MovableObject")
            Destroy(this.gameObject);
    }

}
