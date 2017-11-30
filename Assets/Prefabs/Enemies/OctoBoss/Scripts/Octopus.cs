using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : MonoBehaviour {

    [SerializeField]
    float hp = 3;
    [SerializeField]
    GameObject deathPrefab;
    [SerializeField]
    GameObject powerBall;
    [SerializeField]
    GameObject shieldLeft;
    [SerializeField]
    GameObject shieldRight;
    [SerializeField]
    GameObject shieldGun;

    float timer;
    float timerBall;
    float timerVuln;
    Vector3 start;
    Vector3 startBall;
    Vector3 startVelo;
    bool left;

	// Use this for initialization
	void Start () {
        timer = 0.0f;
        timerVuln = -1.0f;
        start = transform.parent.position;
        shieldRight.SetActive(false);
        left = true;
    }

    private void InstantBall()
    {
        GameObject newBall = Instantiate(powerBall, transform.position - transform.up, Quaternion.identity);
        Rigidbody2D body = newBall.GetComponent<Rigidbody2D>();
        body.velocity = -5.0f * transform.up;
    }

	// Update is called once per frame
	void Update () {
        timer = timer + 2 * Time.deltaTime;
        timerBall = timerBall + Time.deltaTime;
        transform.position = start + new Vector3(2 * Mathf.Cos(timer), Mathf.Sin(timer), 0.0f);
        transform.Rotate(new Vector3(0.0f, 0.0f, Mathf.Sin(timer)));
        if (timer > 2 * Mathf.PI)
        {
            timer = 0.0f;
        }
        if (timerBall > 0.7f)
        {
            InstantBall();
            timerBall = 0.0f;
        }
        if (timerVuln >= 0.0f)
        {
            timerVuln = timerVuln - Time.deltaTime;
        }
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "DamagingBullet" && timerVuln < 0.0f)
        {
            timerVuln = 1.0f;
            TakeHealth(1);
            if (left)
            {
                shieldRight.SetActive(true);
                shieldLeft.SetActive(false);
                left = false;
            }else if (!left)
            {
                shieldRight.SetActive(false);
                shieldLeft.SetActive(true);
                left = true;
            }
            Destroy(col.gameObject);
        }
    }

    public void TakeHealth(float dam)
    {
        hp -= dam;
        if (hp <= 0)
            Die();
    }

    virtual public void Die()
    {
        Instantiate(deathPrefab, transform.position, Quaternion.identity);
        Instantiate(shieldGun, transform.position, Quaternion.identity);
        Destroy(shieldRight);
        Destroy(shieldLeft);
        Destroy(gameObject);
    }

}
