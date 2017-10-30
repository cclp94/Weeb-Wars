using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    [SerializeField]
    int playerDamage;

    [SerializeField]
    int throwPower;

    Rigidbody2D body;

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.tag);
        if (col.tag == "MovableObject")
        {
            body = col.gameObject.GetComponent<Rigidbody2D>();
            body.velocity = new Vector2(body.velocity.x, throwPower);
        }else if(col.tag == "Player")
        {
            body = col.gameObject.GetComponent<Rigidbody2D>();
            col.GetComponent<WeebPlayer>().TakeDamage(playerDamage);
            body.velocity = new Vector2(body.velocity.x, throwPower);
        }



    }

}
