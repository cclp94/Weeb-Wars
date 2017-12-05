using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Megaman : Enemy {

    [SerializeField]
    float followSpeed;
    [SerializeField]
    Transform mTarget;
    [SerializeField]
    GameObject mBullet;

    float distance;

    Rigidbody2D mRigidbody;
    Animator mAnimator;

    Vector2 facingDirection;

    float attackTimer;
    float jumpTimer;
    float damageTimer;

    bool running;
    bool shooting;
    bool jumping;
    bool hit;
    bool grounded;

	// Use this for initialization
	void Start ()
    {
        mRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        mAnimator = this.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Mathf.Abs(transform.rotation.z) > 40)
        {
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            transform.position += new Vector3(0.0f, 1.0f, 0.0f);
        }

        running = false;
        grounded = true;

        attackTimer += Time.deltaTime;
        jumpTimer += Time.deltaTime;
        damageTimer += Time.deltaTime;
        if (mTarget != null)
        {
            if (mTarget.position.x > transform.position.x)
            {
                facingDirection = Vector2.right;
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                facingDirection = Vector2.left;
                GetComponent<SpriteRenderer>().flipX = true;
            }

            if (damageTimer > 1) hit = false;
            if (attackTimer > 0.3) shooting = false;

            distance = Vector2.Distance(mTarget.position, this.transform.position);

            if (distance < 9 && distance > 6 && !hit)
            {
                transform.Translate(facingDirection * followSpeed * Time.deltaTime, Space.World);
                running = true;
            }
            if (distance < 8)
            {
                if (attackTimer > 2 && !hit)
                {
                    shooting = true;
                    attackTimer = 0;

                    GameObject mb = Instantiate(mBullet, transform.position, Quaternion.identity) as GameObject;
                    KiBlast blast = mb.GetComponent<KiBlast>();
                    mb.GetComponent<TimedExpiration>().mExpirationTime = 4;
                    blast.setDirection(facingDirection);
                }
            }
        }

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        mAnimator.SetBool("isRunning", running);
        mAnimator.SetBool("isShooting", shooting);
        mAnimator.SetBool("isRising", jumping);
        mAnimator.SetBool("isHurt", hit);
        mAnimator.SetBool("isGrounded", grounded);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "DamagingBullet")
        {
            hit = true;
            TakeHealth(4);
            Destroy(col.gameObject);
            damageTimer = 0;
        }
    }
}
