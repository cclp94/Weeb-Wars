using UnityEngine;
using System.Collections;
using System;

public class SailorMoon : Enemy, FollowTarget
{
    [SerializeField]
    Transform mTarget;
    [SerializeField]
    float mFollowSpeed;
    [SerializeField]
    float mFollowRange;
    [SerializeField]
    GameObject projectile;

    float mArriveThreshold = 5.0f;
    Vector2 mFacingDirection;

    // Animator booleans
    bool mRunning;
    bool mAttacking;
    bool hurt;

    // References to other components and game objects
    Animator mAnimator;
    Rigidbody2D mRigidBody2D;

    float attackTimer;
    AudioSource hurtSound;

    void Start()
    {
        // Get references to other components and game objects
        mAnimator = GetComponent<Animator>();
        mRigidBody2D = GetComponent<Rigidbody2D>();
        hurtSound = GetComponent<AudioSource>();

        mFacingDirection = Vector2.right;

    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        Follow(mTarget);
        UpdateAnimator();
        rescaleCollider();

        hurt = false;
    }

    private void rescaleCollider()
    {
        Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        gameObject.GetComponent<BoxCollider2D>().size = S;
        //gameObject.GetComponent<BoxCollider2D>().offset = new Vector2((S.x / 2),0);
    }

    private void UpdateAnimator()
    {
        mAnimator.SetBool("isRunning", mRunning);
        mAnimator.SetBool("isAttacking", mAttacking);
        mAnimator.SetBool("isHurt", hurt);
    }

    public void SetTarget(Transform target)
    {
        mTarget = target;
    }

    public void Follow(Transform target)
    {
        if (target != null)
        {
            Vector2 targetPos = target.transform.position;
            Vector2 pos = transform.position;
            Vector2 direction = targetPos - pos;
            if (direction.magnitude <= mFollowRange)
            {
                if (direction.magnitude > mArriveThreshold)
                {
                    mRunning = true;
                    mAttacking = false;

                    Vector2 newPos = (Vector2)(new Vector2(transform.position.x, 0)) + (new Vector2(direction.normalized.x * Time.deltaTime * mFollowSpeed, 0));
                    //mRigidBody2D.MovePosition(newPos);
                    transform.Translate(new Vector2(direction.normalized.x, 0) * mFollowSpeed * Time.deltaTime, Space.World);
                }
                else
                {
                    mRunning = false;
                    if (attackTimer > 2)
                    {
                        mAttacking = true;
                        UpdateAnimator();

                        attackTimer = 0;
                        GameObject kB = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
                        KiBlast blast = kB.GetComponent<KiBlast>();
                        Vector2 bDirection = mTarget.position - transform.position;
                        bDirection.Normalize();

                        blast.setDirection(bDirection);
                    }
                    
                    //transform.position = target.transform.position;
                }

                if (mTarget.position.x > transform.position.x)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            else
            {
                mAttacking = false;
                mRunning = false;
            }
        }
    }

    public override void Die()
    {
        base.Die();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "DamagingBullet")
        {
            mAttacking = false;
            mRunning = false;
            hurt = true;
            hurtSound.Play();
            UpdateAnimator();
            col.gameObject.GetComponent<GunUpgrade>().Collide(this.gameObject);
            attackTimer = 0;

        }
    }

    public void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            col.gameObject.GetComponent<WeebPlayer>().TakeDamage(3);
        }
    }
}
