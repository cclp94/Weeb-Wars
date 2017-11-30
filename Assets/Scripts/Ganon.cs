using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ganon : Enemy
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

    [SerializeField]
    bool flyDir = true;

    float attackTimer;

    public AudioClip damage;
    private AudioSource source;

    // Use this for initialization
    void Start()
    {
        gAnimator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        //UpdateAnimator();
        distance = Vector3.Distance(mTarget.position, transform.position);

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

        if (distance < 6.0f)
        {
            if (attackTimer > 2)
            {
                flying = false;
                attackTimer = 0;
                attack();
            }
            else if (flyDir)
            {
                flying = true;
                if (this.transform.position.x - mTarget.position.x > 0)
                {
                    Vector2 direction = new Vector2(-1.0f, 0.5f);
                    transform.Translate(direction.normalized * mFollowSpeed * Time.deltaTime, Space.World);
                }
                else
                {
                    Vector2 direction = new Vector2(-1.0f, -0.5f);
                    transform.Translate(direction.normalized * mFollowSpeed * Time.deltaTime, Space.World);
                }
                if (transform.position.x - mTarget.position.x < -5.5f) flyDir = false;
            }
            else
            {
                flying = true;
                if (this.transform.position.x - mTarget.position.x < 0)
                {
                    Vector2 direction = new Vector2(1.0f, 0.5f);
                    transform.Translate(direction.normalized * mFollowSpeed * Time.deltaTime, Space.World);
                }
                else
                {
                    Vector2 direction = new Vector2(1.0f, -0.5f);
                    transform.Translate(direction.normalized * mFollowSpeed * Time.deltaTime, Space.World);
                }
                if (this.transform.position.x - mTarget.position.x > 5.5f) flyDir = true;
            }
        }
        else //*/ 
        if ((distance < 12.0f) && (distance > 6.0f))
        {
            follow();
        }
        else
        {
            attacking = false;
            flying = false;
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
            Destroy(col.gameObject);
            source.PlayOneShot(damage, 1F);
        }
    }
}
