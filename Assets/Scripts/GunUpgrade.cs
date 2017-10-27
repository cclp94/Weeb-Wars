using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUpgrade : MonoBehaviour {
    [SerializeField]
    float mSpeed;
    [SerializeField]
    Sprite weaponIcon;
    [SerializeField]
    Color mGunColor;
    [SerializeField]
    float mDamage = 1;
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
        }
        else
        {
            angle = Vector2.Angle(Vector2.left, direction);
            Vector3 newScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.localScale = newScale;
        }
        if (direction.y >= 0)
        {
            transform.Rotate(Vector3.forward, angle);
        }
        else
        {
            transform.Rotate(Vector3.forward, -angle);
        }
        
    }

    public float GetDamage()
    {
        return mDamage;
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
