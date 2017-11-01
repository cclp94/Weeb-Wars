using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    [SerializeField]
    GameObject gate;
    [SerializeField]
    Sprite on;
    [SerializeField]
    Sprite off;
    [SerializeField]
    float down;
    [SerializeField]
    float waitTime;

    private bool isPushed;
    private float timer;
    private Gate gateScript;
    private string colTag;

    void Awake()
    {
        gateScript = gate.GetComponent<Gate>();
    }

    private void Update()
    {
        timer = timer + Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "MovableObject" || col.tag == "Player") && !isPushed && timer > waitTime)
        {
            transform.position = transform.position - new Vector3(0.0f, down, 0.0f);
            GetComponent<SpriteRenderer>().sprite = on;
            isPushed = true;
            timer = 0.0f;
            gateScript.OpenGate();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if ((col.tag == "MovableObject" || col.tag == "Player") && isPushed && timer > waitTime)
        {
            Debug.Log(timer);
            transform.position = transform.position + new Vector3(0.0f, down, 0.0f);
            GetComponent<SpriteRenderer>().sprite = off;
            isPushed = false;
            timer = 0.0f;
            gateScript.CloseGate();
        }
    }



}
