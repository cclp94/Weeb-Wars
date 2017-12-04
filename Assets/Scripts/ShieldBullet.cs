using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBullet : GunUpgrade
{

    [SerializeField]
    GameObject lB;

    //float timer;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
    }

    private void Update()
    {
        //timer += Time.deltaTime;

        //if (timer > 0.8) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "EnemyBullet")
        {
            GameObject laser = Instantiate(lB, col.transform.position, Quaternion.identity) as GameObject;

            Vector2 newDir = col.GetComponent<Rigidbody2D>().velocity.normalized;
            print(newDir);
            newDir *= -1.1f;

            laser.GetComponent<LaserBullet>().SetDirection(newDir);

            Destroy(col.gameObject);

        }

    }
}
