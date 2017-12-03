using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovingPlatform : MonoBehaviour {

    [SerializeField]
    public float leftMove;
    [SerializeField]
    public float rightMove;
    [SerializeField]
    public float speed;

    private float originalX;

    private bool isMovingRight;

	// Use this for initialization
	void Start () {
        isMovingRight = true;
        originalX = transform.position.x;
        //print(originalX);
	}

	// Update is called once per frame
	void Update () {
        if (isMovingRight)
        {
            if (transform.position.x >= originalX + rightMove)
                isMovingRight = false;
            transform.position = new Vector3(transform.position.x+((originalX + rightMove) * Time.deltaTime * speed), transform.position.y, transform.position.z);
        }
        else
        {
            if (transform.position.x <= originalX - leftMove)
                isMovingRight = true;
            transform.position = new Vector3(transform.position.x-((originalX - leftMove) * Time.deltaTime * speed),transform.position.y, transform.position.z);
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
