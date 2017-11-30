using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy {

    public GameObject bossHPPrefab;
    public PauseMenu PauseMenu;

    private float initialHp;
    private GameObject hpCanvas;
    private BossHP hpBar;
	// Use this for initialization
	void Awake () {
        initialHp = base.hp;
        print("Starting boss");
        hpCanvas = Instantiate(bossHPPrefab);
        hpCanvas.SetActive(false);
        hpCanvas.GetComponent<Canvas>().worldCamera = Camera.main;
        hpBar = hpCanvas.GetComponentInChildren<BossHP>();
    }
	
	// Update is called once per frame
	void Update () {
        /* Delay for explosion animation or anything
		if(beatBossTime >= 0 && Time.time - beatBossTime >= 5)
        {
            print("Complete");
            PauseMenu.CompleteLevel();
        }*/
	}

    override public void TakeHealth(float damage)
    {
        hpBar.UpdateHealth(base.hp-damage, initialHp);
        base.TakeHealth(damage);
        
    } 

    protected void ShowHP()
    {
        hpCanvas.SetActive(true);
    }

    virtual public void Activate()
    {
        gameObject.SetActive(true);
        ShowHP();
    }
    float beatBossTime = -1f;
    override public void Die()
    {
        print("Died");
        base.Die();
        PauseMenu.CompleteLevel();
        beatBossTime = Time.time;
    }
}
