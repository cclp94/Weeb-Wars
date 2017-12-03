using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ganon : BossEnemy
{

    [SerializeField]
    Transform mTarget;
    [SerializeField]
    GameObject kiBlast;
    [SerializeField]
    float mFollowSpeed;

    bool flying;
    bool attacking;
    bool hit;

    Animator gAnimator;

    float distance;
    Vector2 facingDirection;

    int flyDir = 0;
    float comboTimer;
    float idleTimer;

    int cAttacks;
    bool combo;

    [SerializeField]
    bool engaging = false;
    bool wounded = false;

    Vector2 blastDir = new Vector2(2.0f, -1.0f);

    public AudioClip damage;
    private AudioSource source;

    // Use this for initialization
    void Start()
    {
        gAnimator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        engaging = true;
    }

    // Update is called once per frame
    void Update()
    {
        comboTimer += Time.deltaTime;
        idleTimer += Time.deltaTime;
        distance = Vector3.Distance(mTarget.position, transform.position);

        // Sprite faces player
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

        hit = false;
        attacking = false;

        if (!engaging && distance < 14) engaging = true;

        // At half health
        //if (hp == 20)
        //{
        //    wounded = true;
        //}
        if (wounded)
        {
            if (blastDir.x < -2.0)
            {
                wounded = false;
            }
            else if (Mathf.Abs(transform.position.x - mTarget.position.x) > 0.5f && !combo)
            {
                Vector3 direction = new Vector3(mTarget.position.x, mTarget.position.y + 6.0f, 0.0f);
                transform.Translate(direction.normalized * mFollowSpeed * Time.deltaTime, Space.World);
            }
            else combo = true;

            if (comboTimer > 0.15 && combo)
            {
                attacking = true;
                flying = false;
                UpdateAnimator();

                GameObject kB = Instantiate(kiBlast, transform.position, Quaternion.identity) as GameObject;
                KiBlast blast = kB.GetComponent<KiBlast>();
                blastDir.x -= 0.3f;

                blast.setDirection(blastDir.normalized);
                comboTimer = 0;
            }
        }
        else if (engaging)
        {
            if (distance > 8 && distance < 14) follow();
            else if (combo && comboTimer > 0.4)
            {
                flying = false;
                attack();
                cAttacks++;
                comboTimer = 0;
                if (cAttacks > 2)
                {
                    cAttacks = 0;
                    combo = false;
                    flyDir = (flyDir + 1) % 4;
                }
            }
            else if (flyDir == 0)
            {
                if (transform.position.y - mTarget.position.y > 5)
                    combo = true;
                else
                    transform.Translate(Vector3.up * mFollowSpeed * Time.deltaTime, Space.World);
            }
            else if (flyDir == 1)
            {
                if (transform.position.y - mTarget.position.y < 2.5)
                    combo = true;
                else
                {
                    Vector3 direction = new Vector3(-2.0f, -1.0f, 0.0f);
                    transform.Translate(direction.normalized * mFollowSpeed * Time.deltaTime, Space.World);
                }
            }
            else if (flyDir == 2)
            {
                if (transform.position.y - mTarget.position.y > 5)
                    combo = true;
                else
                    transform.Translate(Vector3.up * mFollowSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                if (transform.position.y - mTarget.position.y < 2.5)
                    combo = true;
                else
                {
                    Vector3 direction = new Vector3(2.0f, -1.0f, 0.0f);
                    transform.Translate(direction.normalized * mFollowSpeed * Time.deltaTime, Space.World);
                }
            }

            if (distance > 7.0) engaging = false;
        }

    }

    private void UpdateAnimator()
    {
        gAnimator.SetBool("Flying", flying);
        gAnimator.SetBool("Attack", attacking);
        gAnimator.SetBool("Hit", hit);
    }

    void follow()
    {
        if (mTarget != null)
        {
            flying = true;
            attacking = false;
            UpdateAnimator();

            Vector2 direction = mTarget.transform.position - transform.position;
            direction.y += 2.5f;
            transform.Translate(direction.normalized * mFollowSpeed * Time.deltaTime, Space.World);
        }
    }

    void attack()
    {
        attacking = true;
        flying = false;
        UpdateAnimator();

        GameObject kB = Instantiate(kiBlast, transform.position, Quaternion.identity) as GameObject;
        KiBlast blast = kB.GetComponent<KiBlast>();
        blast.bSpeed = 7;
        kB.GetComponent<TimedExpiration>().mExpirationTime = 6;
        Vector2 bDirection = mTarget.position - transform.position;
        bDirection.Normalize();

        blast.setDirection(bDirection);

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "DamagingBullet")
        {
            flying = false;
            attacking = false;
            hit = true;
            UpdateAnimator();

            TakeHealth(4);
            if (hp == 20) wounded = true;
            Destroy(col.gameObject);
            source.PlayOneShot(damage, 1F);
        }
    }
}
