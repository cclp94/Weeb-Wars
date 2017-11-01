using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanBody : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            transform.parent.SendMessage("SetIsHitable", true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            transform.parent.SendMessage("SetIsHitable", false);
        }
    }
}
