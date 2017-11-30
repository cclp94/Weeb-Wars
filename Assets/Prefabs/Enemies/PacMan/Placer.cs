using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour {

    [SerializeField]
    GameObject smallDot;
    [SerializeField]
    GameObject bigDot;

    [SerializeField]
    float frequency;

    private float speed;
    private int counter;
    private float timer;
    private int mChase;

    // Use this for initialization
    void Start () {
        speed = transform.parent.GetComponent<Variables>().getSpeed();
        counter = 0;
        mChase = 1;
    }

    public void setChase(int b)
    {
        mChase = b;

    }

    // Update is called once per frame
    void Update () {
        if (mChase == 1)
        {
            timer = timer + Time.deltaTime * frequency / speed;
        }
        transform.Translate(mChase * Vector2.right * Time.deltaTime * speed);
        if (timer > 1.0f)
        {
            timer = 0.0f;
            if (counter < 5)
            {
                GameObject newDot = Instantiate(smallDot, transform);
                newDot.transform.parent = transform.parent;
                counter++;
            }else
            {
                GameObject newDot = Instantiate(bigDot, transform);
                newDot.transform.parent = transform.parent;
                counter = 0;
            }
        }

    }
}
