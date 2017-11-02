using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform mTarget;
    [SerializeField]
    Transform mDeathZone;
    [SerializeField]
    Transform mLeftMostLimit;

    bool facing = true;

    float smoothness = 2.0f;
    float stepOverThreshold = 0.1f;

    Vector3 originalPos;

    void Update()
    {
        if (mTarget != null)
        {/*
<<<<<<< HEAD
            Vector3 targetPosition = new Vector3(mTarget.transform.position.x, transform.position.y, transform.position.z);
            Vector3 direction = targetPosition - transform.position;
            if (direction.x < 0 && !(transform.position.x - 1.0f > mLeftMostLimit.position.x))
            {
                targetPosition.x = transform.position.x;
                direction = targetPosition - transform.position;
            }

            if (direction.magnitude > stepOverThreshold)
            {
                transform.Translate(direction * kFollowSpeed * Time.deltaTime);
=======*/
            if (mTarget.GetComponent<WeebPlayer>().GetFacingDirection() == Vector2.right)
            {
                Vector3 targetPosition = new Vector3(mTarget.transform.position.x + 5.0f, mTarget.transform.position.y+1.5f, transform.position.z);
                Vector3 direction = targetPosition - transform.position;
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
            }
            else
            {
                if (mTarget.transform.position.x - 1.0f < mLeftMostLimit.position.x)
                {
                    Vector3 targetPosition = new Vector3(mTarget.transform.position.x, mTarget.transform.position.y + 1.5f, transform.position.z);
                    Vector3 direction = targetPosition - transform.position;
                    transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
                }
                else
                {
                    Vector3 targetPosition = new Vector3(mTarget.transform.position.x - 5.0f, mTarget.transform.position.y + 1.5f, transform.position.z);
                    Vector3 direction = targetPosition - transform.position;
                    transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
                }
                
                   
            }
        }

    }
}
