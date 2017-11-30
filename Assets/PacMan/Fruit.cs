using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour {

    [SerializeField]
    GameObject[] ghosts = new GameObject[4];
    [SerializeField]
    GameObject[] boxes = new GameObject[4];
    [SerializeField]
    GameObject placer;
    [SerializeField]
    float waitTime = 6.0f;

    PacMan[] scripts = new PacMan[4];
    Box[] scriptsB = new Box[4];
    Placer scriptP;
    float timer = -1.0f;
    Renderer rend;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            scripts[i] = ghosts[i].GetComponent<PacMan>();
            scriptsB[i] = boxes[i].GetComponent<Box>();
        }
        scriptP = placer.GetComponent<Placer>();
        rend = GetComponent<Renderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && timer == -1.0f)
        {
            rend.enabled = false;
            for (int i = 0; i < ghosts.Length; i++)
            {
                scripts[i].setChase(-1);
                scriptsB[i].setChase(-1);
                scriptP.setChase(-1);
            }
            timer = 0.0f;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (timer > waitTime)
        {
            rend.enabled = true;
            for (int i = 0; i < ghosts.Length; i++)
            {
                scripts[i].setChase(1);
                scriptsB[i].setChase(1);
                scriptP.setChase(1);
            }
            timer = -1.0f;
        }
        else if (timer >= 0.0f)
        {
            timer = timer + Time.deltaTime;
        }
    }


}
