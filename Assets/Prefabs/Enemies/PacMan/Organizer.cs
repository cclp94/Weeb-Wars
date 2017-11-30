using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organizer : MonoBehaviour {

    [SerializeField]
    GameObject[] ghosts = new GameObject[4];
    [SerializeField]
    GameObject[] boxes = new GameObject[4];
    [SerializeField]
    GameObject placer;
    [SerializeField]
    Sprite[] numbers;

    PacMan[] scripts = new PacMan[4];
    Box[] scriptsB = new Box[4];
    int count;
    bool turned;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            scripts[i] = ghosts[i].GetComponent<PacMan>();
            scriptsB[i] = boxes[i].GetComponent<Box>();
        }
        count = 4;
        turned = false;
    }

    public void countDown()
    {
        count = count - 1;
    }

    public Sprite getCount()
    {
        return numbers[count];
    }

    // Update is called once per frame
    void Update()
    {
        if (count == 0 && !turned)
        {
            Destroy(placer);
            turned = true;
            for (int i = 0; i < ghosts.Length; i++)
            {
                scripts[i].setChase(-1);
                scriptsB[i].setChase(-1);
            }
        }
    }

}
