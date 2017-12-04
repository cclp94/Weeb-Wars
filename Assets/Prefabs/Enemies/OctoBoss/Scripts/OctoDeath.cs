using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctoDeath : MonoBehaviour {

    Animator anim;
    float timer;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        timer = 0.0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.5f) Destroy(this.gameObject);
    }

}
