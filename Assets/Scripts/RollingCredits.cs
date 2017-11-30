using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollingCredits : MonoBehaviour {

    public GameObject credits;
    public LevelManager level;
	// Use this for initialization
	void Start () {
        level = GetComponent<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
        credits.transform.position = credits.transform.position + Vector3.up * Time.deltaTime;
        if (credits.transform.position.y > transform.position.y+20){
            level.LoadLevel("Choose a Level");
        }
	}
}
