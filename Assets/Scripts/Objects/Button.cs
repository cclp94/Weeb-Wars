using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    [SerializeField]
    GameObject gate;

    //[SerializeField]
    //Animation down;

    private Gate gateScript;
    private string colTag;

    void Awake()
    {
        gateScript = gate.GetComponent<Gate>();
        //down = GetComponent<Animation>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        colTag = col.gameObject.tag;
        if ((colTag == "MovableObject" || colTag == "Player") && col.transform.position.y > transform.position.y)
        {
            gateScript.OpenGate();
            //down.Play();
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "MovableObject" || colTag == "Player")
        {
            gateScript.CloseGate();
            //down.Play();
        }
    }



}
