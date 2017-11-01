using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpirableFallingObject : MovableObject {
    bool mFalling;
    private BoxCollider2D mCollider;
	// Use this for initialization
	void Awake () {
        mFalling = true;
        mCollider = this.GetComponent<BoxCollider2D>();
        mCollider.isTrigger = true;
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

    override public void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
        if (col.gameObject.layer == LayerMask.NameToLayer("Player") && mFalling)
        {
            col.gameObject.GetComponent<WeebPlayer>().TakeDamage(2);
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer(("Level")))
        {
            mFalling = false;
        }
    }

    public void SetIsTrigger(bool isTrigger)
    {
        mCollider.isTrigger = isTrigger;
    }
}
