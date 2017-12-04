﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctoShieldGun : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
			col.gameObject.GetComponent<WeebPlayer>().unlockWeaponUpgrade("Shield");
            Destroy(gameObject);
        }
    }

}
