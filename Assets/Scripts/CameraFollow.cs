using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform mTarget;
    [SerializeField]
    Transform mDeathZone;

    float kFollowSpeed = 4.5f;
    float stepOverThreshold = 0.1f;

    void Update ()
    {
        if(mTarget != null)
        {
            Vector3 targetPosition = new Vector3(mTarget.transform.position.x,(mTarget.transform.position.y > mDeathZone.position.y)? mTarget.transform.position.y : transform.position.y, transform.position.z);
            Vector3 direction = targetPosition - transform.position;

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
