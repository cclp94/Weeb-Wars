using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMan : MonoBehaviour {

    private float speed;
    [SerializeField]
    int damage = 10;

    Animator anim;
    int mChase;

    // Use this for initialization
    void Start()
    {
        mChase = 1;
        speed = transform.parent.GetComponent<Variables>().getSpeed();
        if (tag == "Ghost")
        {
            anim = transform.GetChild(0).GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(mChase * Vector2.right * Time.deltaTime * speed);
    }

    public void setChase(int b)
    {
        if (tag == "Ghost")
        {
            mChase = b;
            anim.SetInteger("mChase", mChase);
        }
        else
        {
            transform.Rotate(180 * Vector3.forward);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<WeebPlayer>().TakeDamage(damage);
            col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(-5.0f, 0.0f, 0.0f);
        }
    }

}
