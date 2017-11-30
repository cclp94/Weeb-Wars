using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManObject : MonoBehaviour {

    private void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("Hoi");
        if (col.tag == "PacMan")
            Debug.Log("Hi");
        {
            col.transform.Rotate(90 * Vector3.forward);
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
