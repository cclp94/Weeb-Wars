using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGun : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && gameObject.name == "AlienGun")
        {
            GameObject emptyWeaponType = GameObject.Find("EmptyWeaponType");
            Destroy(emptyWeaponType);
            
            GameObject weebGun = GameObject.FindGameObjectWithTag("Gun");
            weebGun.GetComponent<AllienGun>().enabled = true;

            Destroy(gameObject);
        }
    }
}
