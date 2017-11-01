using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TeleportState{
    NONE,
    HIT_ENEMY,
    TELEPORT_PLAYER,
    SECOND_SHOT_PLAYER,
    SECOND_SHOT_ENEMY
}

public class TeleportationBullet : GunUpgrade {

    static TeleportState currentState;
    static GameObject teletransportTarget;

    void Update(){
        if(Input.GetButtonDown("Aux Fire")){
            Debug.Log("Fire");
			if (currentState == TeleportState.HIT_ENEMY)
			{
				teletransportTarget.transform.position = transform.position;
				currentState = TeleportState.NONE;
				Destroy(this.gameObject);
			}else if (currentState == TeleportState.SECOND_SHOT_ENEMY)
			{
                Debug.Log("enemy teleporting");

            }else if(currentState == TeleportState.NONE){
				GameObject player = GameObject.FindGameObjectWithTag("Player");
				player.transform.position = transform.position;
                player.GetComponent<WeebPlayer>().ResetGravity();
				Destroy(this.gameObject);
            }
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Surface" && currentState == TeleportState.NONE)
        {
            //GameObject player = GameObject.FindGameObjectWithTag("Player");
            //player.GetComponent<WeebPlayer>().ResetGravity();
            //player.transform.position = transform.position;
            Destroy(this.gameObject);
        }else if(col.gameObject.tag == "MovableObject") {
            currentState = TeleportState.HIT_ENEMY;
            teletransportTarget = col.gameObject;
            Destroy(this.gameObject);
        }

    }

    override public void Collide(GameObject enemy){
        Debug.Log("Hit enemy");
        currentState = TeleportState.HIT_ENEMY;
        teletransportTarget = enemy;
        Destroy(this.gameObject);
    }
   
}
