using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCannon : Enemy {

    [SerializeField]
    public bool isDirectionRight;
    [SerializeField]
    public GameObject shootPrefab;
    [SerializeField]
    public float shotInterval;

    private float lastShot;


	// Use this for initialization
	void Start () {
        lastShot = Time.time;

	}

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastShot >= shotInterval)
        {
            GameObject go = Instantiate(shootPrefab, transform.position, Quaternion.identity);
            Vector2 dir = (isDirectionRight) ? Vector2.right : Vector2.left;
            go.GetComponent<WallCannonShot>().SetDirection(dir);
            lastShot = Time.time;
        }
    }

}
