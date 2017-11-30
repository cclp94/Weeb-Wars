using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy {

    public GameObject bossHPPrefab;

    private float initialHp;
    private BossHP hpBar;
	// Use this for initialization
	void Awake () {
        initialHp = base.hp;
        print("Starting boss");
        GameObject go = Instantiate(bossHPPrefab);
        go.GetComponent<Canvas>().worldCamera = Camera.main;
        hpBar = go.GetComponentInChildren<BossHP>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    override public void TakeHealth(float damage)
    {
        hpBar.UpdateHealth(base.hp-damage, initialHp);
        base.TakeHealth(damage);
        
    } 
}
