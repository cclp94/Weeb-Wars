using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpirableFallingObject : MovableObject {
    bool mFalling;
	// Use this for initialization
	void Start () {
        mFalling = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.layer == LayerMask.NameToLayer("Player") && mFalling)
		{
			col.gameObject.GetComponent<WeebPlayer>().TakeDamage(2);
        }else if(col.gameObject.layer == LayerMask.NameToLayer(("Level"))){
            mFalling = false;
        }
	}
}
