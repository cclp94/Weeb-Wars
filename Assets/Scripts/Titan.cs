using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Titan : Enemy {

    [SerializeField]
    GameObject rightHand;
    [SerializeField]
    GameObject leftHand;
    [SerializeField]
    float mUpSpeed;
    [SerializeField]
    float mDownSpeed;
    [SerializeField]
    CameraShake camera;

    private Vector3 restPositionRight;
    private Vector3 restPositionLeft;
    private Vector3 upPositionRight;
    private Vector3 upPositionLeft;

    // Use this for initialization
    void Start () {
        mUpDirection = true;
        restPositionRight = rightHand.transform.position;
        restPositionLeft = leftHand.transform.position;
        upPositionRight.Set(restPositionRight.x, restPositionRight.y + 4.0f, restPositionRight.z);
        upPositionLeft.Set(restPositionLeft.x, restPositionLeft.y + 4.0f, restPositionLeft.z);
    }
    bool mUpDirection;
	// Update is called once per frame
	void Update () {
        Vector3 targetRight = Vector3.zero;
        Vector3 targetLeft = Vector3.zero;
        float acceleration = 0;
        if (mUpDirection)
        {
            targetRight = upPositionRight - rightHand.transform.position;
            targetLeft = upPositionLeft - leftHand.transform.position;
            acceleration = mUpSpeed;
        }
        else
        {
            targetRight = restPositionRight - rightHand.transform.position;
            targetLeft = restPositionLeft - leftHand.transform.position;
            acceleration = mDownSpeed;
        }
        if(Math.Abs(targetLeft.y) <= 0.095 || Math.Abs(targetRight.y) <= 0.095)
        {
            if (!mUpDirection)
            {
                camera.Shake(1);
            }
            mUpDirection = !mUpDirection;
        }

        print(targetLeft.y);
        print(targetRight.y);

        rightHand.transform.Translate(targetRight * Time.deltaTime * acceleration);
        leftHand.transform.Translate(targetLeft * Time.deltaTime * acceleration);
    }


    override public void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            col.GetComponent<WeebPlayer>().TakeDamage(5);
        }
    }
}
