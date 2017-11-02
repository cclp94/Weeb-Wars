using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCannonShot : MonoBehaviour {
    private Rigidbody2D mRigidBody2D;

    private float mSpeed = 5;

	// Use this for initialization
	void Start () {
		mRigidBody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetDirection(Vector2 direction)
	{
		GetComponent<Rigidbody2D>().velocity = direction * mSpeed;
		FaceDirection(direction);
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
	public void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			collision.GetComponent<WeebPlayer>().TakeDamage(1);
			Destroy(this.gameObject);
		}
		else if (collision.gameObject.layer == LayerMask.NameToLayer("Level"))
		{
			Destroy(gameObject);
		}
	}
}
