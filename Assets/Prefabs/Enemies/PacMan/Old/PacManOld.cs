using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManOld : MonoBehaviour {

    [SerializeField]
    float timeRight = 4.0f;
    [SerializeField]
    float timeUp = 2.0f;
    [SerializeField]
    float speed = 4.0f;
    [SerializeField]
    int damage = 10;

    int mRight = 1;
    int mUp = 0;
    int mChase;
    float timer = 0.0f;

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        timer = -(timeRight + timeUp);
        mChase = 1;
	}

    // Update is called once per frame
    void Update()
    {
        timer = timer + mChase * Time.deltaTime;
        transform.Translate(mRight * speed * Time.deltaTime, mUp * speed * Time.deltaTime, 0.0f);
        if (timer < - timeRight - timeUp || timer > timeRight + timeUp)
        {
            timer = -mChase * (timeRight + timeUp);
            transform.Translate(-2 * mChase * mRight * speed * Time.deltaTime, -2 * mChase * mUp * speed * Time.deltaTime, 0.0f);
        }
        else if (timer > timeRight && timer < timeRight + timeUp)
        {
            mRight = 0 * mChase;
            mUp = -1 * mChase;
        }
        else if (timer > 0.0f && timer < timeRight)
        {
            mRight = -1 * mChase;
            mUp = 0 * mChase;
        }
        else if (timer > - timeUp && timer < 0.0f)
        {
            mRight = 0 * mChase;
            mUp = 1 * mChase;
        }
        else if (timer > - timeRight - timeUp && timer < -timeUp)
        {
            mRight = 1 * mChase;
            mUp = 0 * mChase;
        }
        anim.SetInteger("mChase", mChase);
        anim.SetInteger("mRight", mRight);
        anim.SetInteger("mUp", mUp);
    }

    public void setChase(int b)
    {
        mChase = b;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<WeebPlayer>().TakeDamage(damage);
        }
    }
}
