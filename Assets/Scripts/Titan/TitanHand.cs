using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanHand : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
            if(transform.parent.GetComponent<Titan>().IsFalling()){
                col.GetComponent<WeebPlayer>().TakeDamage(5);
            }
			
		}
	}
}
