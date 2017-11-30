using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBehavior : MonoBehaviour {

    Vector3 startPosition;
    Vector3 endPosition;
    public float speed;

    private void Start()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(99.36f, 9.56f, 0f);

    }

    // Update is called once per frame
    void Update()
    {
        if (!(transform.position == endPosition))
        {
           transform.position = Vector3.Lerp(new Vector3(99.36f, -4.21f, 0f), endPosition, speed * Time.deltaTime);
        }
    }
}
