using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGun : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
           // col.gameObject.GetComponent<WeebPlayer>().unlockWeaponUpgrade("Shield");
           Destroy(gameObject);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
    //    {
    //        collision.gameObject.GetComponent<WeebPlayer>().unlockWeaponUpgrade("TeleportationBullet");
    //    }
    //}
}
