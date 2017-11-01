using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUpgrade : MonoBehaviour {
    [SerializeField]
    float mSpeed= 5;
    [SerializeField]
    Sprite weaponIcon;
    [SerializeField]
    Color mGunColor;
    [SerializeField]
    float mDamage = 1;
	[SerializeField]
	float mMaxBullets = 1;
    Rigidbody2D mRigidBody2D;

    void Awake()
    {
        // Must be done in Awake() because SetDirection() will be called early. Start() won't work.
        mRigidBody2D = GetComponent<Rigidbody2D>();

        // Set a default direction
        mRigidBody2D.velocity = Vector2.right * mSpeed;
    }

    public void SetDirection(Vector2 direction)
    {
        mRigidBody2D.velocity = direction * mSpeed;
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

     virtual public void Collide(GameObject enemy){
		enemy.GetComponent<Enemy>().TakeHealth(GetDamage());
        Destroy(this.gameObject);
	}

    public float GetDamage()
    {
        return mDamage;
    }

    public bool canInstatiateNewBullet(){
        if(GameObject.FindGameObjectsWithTag("DamagingBullet").Length < mMaxBullets){
            return true;
        }
        return false;
    }

    public Sprite GetWeaponIcon()
    {
        return weaponIcon;
    }

    public Color GetGunColor()
    {
        return mGunColor;
    }
}
