using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctoPower : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Surface")
        {
            Destroy(gameObject);
        }else if (col.transform.tag == "Player")
        {
            col.gameObject.GetComponent<WeebPlayer>().TakeDamage(10);
			Destroy (gameObject);
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
