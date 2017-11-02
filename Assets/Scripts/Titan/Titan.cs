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
    CameraShake mCamera;
    [SerializeField]
    GameObject fallingObjectPrefab;
    [SerializeField]
    AudioSource roarSound;

    private Vector3 restPositionRight;
    private Vector3 restPositionLeft;
    private Vector3 upPositionRight;
    private Vector3 upPositionLeft;

    // Use this for initialization
    void Start () {
        gameObject.SetActive(false);
        mUpDirection = true;
        restPositionRight = rightHand.transform.position;
        restPositionLeft = leftHand.transform.position;
        upPositionRight.Set(restPositionRight.x, restPositionRight.y + 4.0f, restPositionRight.z);
        upPositionLeft.Set(restPositionLeft.x, restPositionLeft.y + 4.0f, restPositionLeft.z);
        lasStump = Time.time;
        isStumpping = true;
        mIsHittable = false;
        mIsScreaming = false;
        boulders = new List<GameObject>();
    }

    bool mUpDirection;
    float lasStump;
    bool isStumpping;
    bool mIsScreaming;
    // Update is called once per frame
    void Update () {
        GetComponent<Animator>().SetBool("isHeadTurned", mIsHittable);
        GetComponent<Animator>().SetBool("isScreaming", mIsScreaming);
        if (Time.time - lasStump >= 10 && !isStumpping)
        {
            isStumpping = true;
            lasStump = Time.time;
        }
        if(isStumpping)
            StumpHands();
    }
    List<GameObject> boulders;
    public void fallBoulders(){
        int randomPick = UnityEngine.Random.Range(0, 8);
        for (int i = 0; i < 8; i++){
            GameObject go = Instantiate(fallingObjectPrefab, new Vector3(transform.position.x + (6.0f) - (2.0f * i) , transform.position.y + 10.0f, transform.position.z), Quaternion.identity);
            if(i == randomPick)
            {
                print(go);
                boulders.Add(go);
                go.GetComponent<ExpirableFallingObject>().SetIsTrigger(false);
            }else
            {
                Destroy(go, 5);
            }
        }

    }

    
    override public void OnTriggerStay2D(Collider2D col)
    {
        /*if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            col.GetComponent<WeebPlayer>().TakeDamage(5);
        }*/
    }
    private bool mIsHittable;
    private int hitCount;
    public override void OnTriggerEnter2D(Collider2D col)
    {
        if (mIsHittable)
        {
            if (mIsHittable && col.gameObject.tag == "DamagingBullet")
            {
                col.GetComponent<GunUpgrade>().Collide(this.gameObject);
                hitCount++;
            }
            if(hitCount % 10 == 0)
            {
                ScreamToDestroyAllBoulders();
            }
        }
    }

    private void ScreamToDestroyAllBoulders()
    {
        mIsScreaming = true;
        isStumpping = true;
        lasStump = Time.time;
        roarSound.Play();
    }

    private void DestroyAllBoulders()
    {
        foreach (GameObject go in boulders)
        {
            if (go != null)
            {
                Destroy(go);
            }
        }
    }

    public void SetIsHitable(bool isHittable)
    {
        this.mIsHittable = isHittable;
    }

    public bool IsFalling(){
        return !mUpDirection;
    }

    private void StumpHands()
    {
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
        if (Math.Abs(targetLeft.y) <= 0.095 || Math.Abs(targetRight.y) <= 0.095)
        {
            if (!mUpDirection)
            {
                mCamera.Shake(0.7f);
                if (mIsScreaming)
                {
                    DestroyAllBoulders();
                    mIsScreaming = false;
                }else
                {
                    fallBoulders();
                }
                
                isStumpping = false;
            }
            mUpDirection = !mUpDirection;
        }

        rightHand.transform.Translate(targetRight * Time.deltaTime * acceleration *(mIsScreaming ? 5.0f : 1.0f));
        leftHand.transform.Translate(targetLeft * Time.deltaTime * acceleration * (mIsScreaming ? 5.0f : 1.0f));
    }
}
