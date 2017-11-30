using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour {

    [SerializeField]
    float speed = 4;

    private bool chase;

    public float getSpeed()
    {
        return speed;
    }

    public void setChase(bool b)
    {
        chase = b;
    }
    public bool getChase()
    {
        return chase;
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
