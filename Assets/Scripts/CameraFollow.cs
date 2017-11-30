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
    [SerializeField]
    float maxCameraDistanceX;
    [SerializeField]
    float maxCameraDistanceY;

    bool facing = true;

    float smoothness = 2.0f;
    float stepOverThreshold = 0.1f;

    public bool outOfRange = false;
    float timer = 0;

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
            // Camera that follows mouse
            if (Input.GetKey(KeyCode.LeftControl))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - mTarget.transform.position;
                Vector3 targetPosition;
                if (outOfRange)
                {
                    timer += Time.deltaTime;
                    if (Vector3.Distance(transform.position, mTarget.position) < 10.5f && timer > 1)
                    {
                        outOfRange = false;
                        timer = 0;
                    }
                }
                if (Mathf.Abs(mousePos.x) > maxCameraDistanceX || Mathf.Abs(mousePos.y) > maxCameraDistanceY || outOfRange)
                {
                    outOfRange = true;
                    targetPosition = new Vector3(mTarget.position.x, mTarget.position.y, transform.position.z);
                }
                else
                    targetPosition = new Vector3(mTarget.transform.position.x + mousePos.x / 1.2f, mTarget.transform.position.y + mousePos.y / 1.2f, transform.position.z);

                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
            }
            else
            {
                Vector3 targetPosition = new Vector3(mTarget.position.x, mTarget.position.y + 1.0f, transform.position.z);

                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
            }
        }

    }
}
