using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBossBullet : MonoBehaviour {

    public float speed;

    private Vector2 direction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 v = (direction * speed * Time.deltaTime);
        transform.position = transform.position + v;
	}

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        print("Bullet Collided");
        string collisionTag = col.gameObject.tag;
        if (collisionTag == "Surface")
        {
            print("Here");
            direction = Vector2.Reflect(direction, col.contacts[0].normal);
        }else if(collisionTag == "Player")
        {
            col.gameObject.SendMessage("TakeDamage", 3);
            Destroy(this.gameObject);
        }
    }
}
