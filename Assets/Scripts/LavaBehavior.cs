using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBehavior : MonoBehaviour {

    Vector3 startPosition;
    Vector3 endPosition;
    public float speed;
    bool trapEnclenched = false;

    [SerializeField] GameObject player;

    private void Start()
    {
        endPosition = new Vector3(99.53f, 9.56f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < 10f) {
            trapEnclenched = true;
        }

        if (!(transform.position == endPosition) && trapEnclenched)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
        }
    }
}
