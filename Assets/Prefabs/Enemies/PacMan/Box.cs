using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

    private int mChase;

    // Use this for initialization
    void Start()
    {
        mChase = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PacMan" || other.tag == "Placer" || other.tag == "Ghost")
        {
            other.transform.Rotate(mChase * 90 * Vector3.forward);
            if (other.tag == "Ghost")
            {
                other.transform.GetChild(0).Rotate(mChase * -90 * Vector3.forward);
            }
        }
    }

    public void setChase(int b)
    {
        mChase = b;
    }


}
