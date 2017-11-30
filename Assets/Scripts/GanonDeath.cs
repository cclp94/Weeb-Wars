using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GanonDeath : MonoBehaviour
{

    Rigidbody2D mRigidbody2D;
    Animator mAnimator;

    bool grounded;
    float timer = 0.0f;

    // Use this for initialization
    void Start()
    {
        mRigidbody2D = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        updateAnimator();
        if (GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x) GetComponent<SpriteRenderer>().flipX = false;
        if (grounded) timer += Time.deltaTime;
        if (timer > 1) Destroy(this.gameObject);
    }

    void updateAnimator()
    {
        mAnimator.SetBool("Grounded", grounded);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Surface")
        {
            grounded = true;
        }
    }
}
