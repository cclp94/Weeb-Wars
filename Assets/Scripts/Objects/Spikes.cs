using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{

    [SerializeField]
    int playerDamage;

    [SerializeField]
    int throwPower;

    Rigidbody2D body;

    void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log(col.tag);
        if (col.gameObject.tag == "MovableObject")
        {
            body = col.gameObject.GetComponent<Rigidbody2D>();
            body.velocity = new Vector2(body.velocity.x, throwPower);
        }
        else if (col.gameObject.tag == "Player")
        {
            body = col.gameObject.GetComponent<Rigidbody2D>();
            col.gameObject.GetComponent<WeebPlayer>().TakeDamage(playerDamage);
            body.velocity = new Vector2(body.velocity.x, throwPower);
        }



    }

}