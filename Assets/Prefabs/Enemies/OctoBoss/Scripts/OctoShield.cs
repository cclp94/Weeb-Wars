using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctoShield : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "DamagingBullet")
        {
            Destroy(col.gameObject);
        }
    }

}
