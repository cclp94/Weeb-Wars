using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBulletPlus : GunUpgrade {

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collision");
        if (col.gameObject.tag == "Surface")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = transform.position;

            Destroy(this.gameObject);
        }//*/
        /*if (col.gameObject.tag == "Surface")
        {
            GameObject grav = GameObject.Instantiate(gravityWell);
            grav.transform.position = this.transform.position;
        }//*/
    }
   
}
