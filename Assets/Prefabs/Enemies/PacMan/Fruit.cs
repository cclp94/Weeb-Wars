using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour {

    Renderer rend;
    Organizer org;
    bool taken;

    // Use this for initialization
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        org = transform.parent.GetComponent<Organizer>();
        taken = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !taken)
        {
            org.countDown();
            Sprite number = org.getCount();
            GetComponent<SpriteRenderer>().sprite = number;
            taken = true;
        }
    }

}
