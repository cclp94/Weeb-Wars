using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHorizontal : MonoBehaviour {

    [SerializeField]
    float speedVer;
    [SerializeField]
    float speedHor;
    [SerializeField]
    float time;

    private float timer;
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.position + new Vector3(speedHor * Time.deltaTime, speedVer * Time.deltaTime, 0.0f);
        timer = timer + Time.deltaTime;
        if (timer > time)
        {
            speedHor = - speedHor;
            speedVer = - speedVer;
            timer = 0.0f;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
