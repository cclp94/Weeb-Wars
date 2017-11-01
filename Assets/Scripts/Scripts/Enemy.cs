using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    GameObject mExplosionPrefab;
    [SerializeField]
    float hp = 1;
	public AudioClip damage;
	private AudioSource source;


    void OnTriggerEnter2D(Collider2D col)
    {
		source = GetComponent<AudioSource> ();
        if (col.gameObject.tag == "DamagingBullet")
        {
            col.GetComponent<GunUpgrade>().Collide(this.gameObject);   
        }

		source.PlayOneShot (damage, 5F);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            col.GetComponent<WeebPlayer>().TakeDamage(3);
        }
    }

    public void TakeHealth(float dam)
    {
        hp -= dam;
        if (hp <= 0)
            Die();
    }

    virtual public void Die()
    {
        Destroy(gameObject);
        Instantiate(mExplosionPrefab, transform.position, Quaternion.identity);
    }

}
