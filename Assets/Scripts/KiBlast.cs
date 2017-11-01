using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiBlast : MonoBehaviour {

    [SerializeField]
    float bSpeed;

    Rigidbody2D mRigidBody2D;

    Animator bAnimator;

	// Use this for initialization
	void Awake () {
        bAnimator = GetComponent<Animator>();
        mRigidBody2D = GetComponent<Rigidbody2D>();
	}
	
	public void setDirection(Vector2 dir)
    {
        mRigidBody2D.velocity = bSpeed * dir;
        FaceDirection(dir);
    }

    private void FaceDirection(Vector2 direction)
    {
        float angle = 0;
        if (direction.x >= 0)
        {
            angle = Vector2.Angle(Vector2.right, direction);
            Vector3 newScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            if (direction.y >= 0)
                transform.Rotate(Vector3.forward, angle);
            else
                transform.Rotate(Vector3.forward, -angle);
        }
        else
        {
            angle = Vector2.Angle(Vector2.left, direction);
            Vector3 newScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.localScale = newScale;
            if (direction.y >= 0)
                transform.Rotate(Vector3.forward, -angle);
            else
                transform.Rotate(Vector3.forward, angle);
        }


    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            col.GetComponent<WeebPlayer>().TakeDamage(3);
        }
    }
}
