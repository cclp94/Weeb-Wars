using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toMainMenu : MonoBehaviour {

    [SerializeField]
    GameObject pause;
    PauseMenu script;

    void Start()
    {
        script = pause.GetComponent<PauseMenu>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            script.CompleteLevel();
        }
    }
}
