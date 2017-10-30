using UnityEngine;
using System.Collections;
using System;

public class FloorEnemy : Enemy, FollowTarget
{
    [SerializeField]
    Transform mTarget;
    [SerializeField]
    float mFollowSpeed;
    [SerializeField]
    float mFollowRange;

    float mArriveThreshold = 1.0f;
    Vector2 mFacingDirection;

    // Animator booleans
    bool mRunning;
    bool mAttacking;

    // References to other components and game objects
    Animator mAnimator;
    Rigidbody2D mRigidBody2D;

    void Start()
    {
        // Get references to other components and game objects
        mAnimator = GetComponent<Animator>();
        mRigidBody2D = GetComponent<Rigidbody2D>();

        mFacingDirection = Vector2.right;

    }

    void Update()
    {
        Follow(mTarget);
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        mAnimator.SetBool("isRunning", mRunning);
        mAnimator.SetBool("isAttacking", mAttacking);
    }

    public void SetTarget(Transform target)
    {
        mTarget = target;
    }

    public void Follow(Transform target)
    {
        if (target != null)
        {
            Vector2 direction = target.transform.position - transform.position;
            if (direction.magnitude <= mFollowRange)
            {
                if (direction.magnitude > mArriveThreshold)
                {
                    mRunning = true;
                    mAttacking = false;
                    transform.Translate(direction.normalized * mFollowSpeed * Time.deltaTime, Space.World);
                }
                else
                {
                    mAttacking = true;
                    mRunning = false;
                    //transform.position = target.transform.position;
                }
            }
        }
    }

    public override void Die()
    {
        base.Die();
        // Temp: give player TeleportationBullet Upgrade
        mTarget.gameObject.GetComponent<WeebPlayer>().unlockWeaponUpgrade("TeleportationBullet");
    }
}
