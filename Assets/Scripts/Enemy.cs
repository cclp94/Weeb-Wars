using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    GameObject mExplosionPrefab;
    [SerializeField]
    float hp = 1;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "DamagingBullet")
        {
            TakeHealth(col.GetComponent<GunUpgrade>().GetDamage());
            Destroy(col.gameObject);         
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            col.GetComponent<WeebPlayer>().TakeDamage(3);
        }
    }

    void TakeHealth(float dam)
    {
        hp -= dam;
        if(hp <= 0)
        {
            Destroy(gameObject);
            Instantiate(mExplosionPrefab, transform.position, Quaternion.identity);
        }
    }
}
