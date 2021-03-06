﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour { 
    [SerializeField]
    public float hp = 1;
    [SerializeField]
    public GameObject deathPrefab;

    virtual public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "DamagingBullet")
        {
            col.GetComponent<GunUpgrade>().Collide(this.gameObject);   
        }
    }

    virtual public void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            col.GetComponent<WeebPlayer>().TakeDamage(3);
        }
    }

    virtual public void TakeHealth(float dam)
    {
        hp -= dam;
        if (hp <= 0)
            Die();
    }

    virtual public void Die()
    {
        Destroy(gameObject);
        Instantiate(deathPrefab, transform.position, Quaternion.identity);
    }

}
