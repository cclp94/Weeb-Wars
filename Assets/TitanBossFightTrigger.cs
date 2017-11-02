using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanBossFightTrigger : MonoBehaviour {
    [SerializeField]
    public GameObject titanBoss;
    [SerializeField]
    public GameObject mCamera;
    [SerializeField]
    AudioClip fightSong;
	[SerializeField]
    AudioSource titanRoar;
    [SerializeField]
    SpriteRenderer bossMask;

    private bool wasTriggered;
    private bool titanActive;

	// Use this for initialization
	void Start () {
        wasTriggered = false;
        titanActive = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(wasTriggered && !titanRoar.isPlaying && bossMask.color.a > 0 ){
            bossMask.color = new Color(0, 0, 0, bossMask.color.a - Time.fixedDeltaTime);
        }
        if(bossMask.color.a <= 0.9 &&  !titanActive){
            print("Here");
			mCamera.GetComponent<AudioSource>().clip = fightSong;
			mCamera.GetComponent<AudioSource>().Play();
            titanBoss.SetActive(true);
            titanActive = true;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player")){
            EnterStage();
            wasTriggered = true;
        }
    }

    private void EnterStage(){
        titanRoar.Play();
    }
}
