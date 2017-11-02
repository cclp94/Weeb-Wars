using UnityEngine;
using System.Collections;
using System;

public class Jamminger : Enemy, FollowTarget
{
    [SerializeField]
    Transform mTarget;
    [SerializeField]
    float mFollowSpeed;
    [SerializeField]
    float mFollowRange;

    float mArriveThreshold = 0.05f;

    void Update()
    {
        Follow(mTarget);
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
                    transform.Translate(direction.normalized * mFollowSpeed * Time.deltaTime, Space.World);
                }
                else
                {
                    transform.position = target.transform.position;
                }
            }
        }
    }

    public override void Die()
    {
        base.Die();
    }
}
