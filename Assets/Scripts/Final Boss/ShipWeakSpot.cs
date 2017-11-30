using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipWeakSpot : MonoBehaviour {

    public int hits;

    private bool isDestroyed;
    private AlienShipBoss boss;
    private bool isVisible;
	// Use this for initialization
	void Start () {
        isDestroyed = false;
        boss = GetComponentInParent<AlienShipBoss>();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (!isDestroyed && col.gameObject.tag == "DamagingBullet")
        {
            hits--;
            Destroy(col.gameObject);

            if(hits <= 0)
            {
                isDestroyed = true;
                boss.TakeHealth(1);
                gameObject.SetActive(false);
            }
        }
    }

    public bool IsVisible()
    {
        return isVisible;
    }

    public void SetIsVisible(bool isVisible)
    {
        if (!isDestroyed)
        {
            this.isVisible = isVisible;
            gameObject.SetActive(this.isVisible);
        }
        
    }

    public bool IsDestroyed()
    {
        return isDestroyed;

    }
}
