using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour {

    [SerializeField]
    public int breakDamage;

    private float currentDamage;

    void Start()
    {
        currentDamage = 0;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "DamagingBullet" && breakDamage != -1)
        {
            currentDamage += col.gameObject.GetComponent<GunUpgrade>().GetDamage();
            if (currentDamage >= breakDamage)
                Destroy(this.gameObject);
            Destroy(col.gameObject);
        }

    }
}
