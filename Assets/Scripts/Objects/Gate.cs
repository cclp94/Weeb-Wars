using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

    [SerializeField]
    Sprite open;
    [SerializeField]
    Sprite close;

    public void OpenGate()
    {
        GetComponent<SpriteRenderer>().sprite = open;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void CloseGate()
    {
        GetComponent<SpriteRenderer>().sprite = close;
        GetComponent<BoxCollider2D>().enabled = true;
    }


}
