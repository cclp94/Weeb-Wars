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

    float kFollowSpeed = 4.5f;
    float stepOverThreshold = 0.1f;

    void Update ()
    {
        if(mTarget != null)
        {
            Debug.Log("Camera x: " + transform.position.x);
            Debug.Log("left most x: " + mLeftMostLimit.transform.position.x);
            Vector3 targetPosition = new Vector3(mTarget.transform.position.x,transform.position.y, transform.position.z);
            Vector3 direction = targetPosition - transform.position;
            Debug.Log("Direction x: " + direction.x);
            if (direction.x < 0 && !(transform.position.x - 6.0f > mLeftMostLimit.position.x))
            {
                Debug.Log("Here");
                targetPosition.x = transform.position.x;
                direction = targetPosition - transform.position;
            }

            if(direction.magnitude > stepOverThreshold)
            {
                transform.Translate (direction * kFollowSpeed * Time.deltaTime);
            }
            else
            {
                // If close enough, just step over
                transform.position = targetPosition;
            }
        }
    }
}
