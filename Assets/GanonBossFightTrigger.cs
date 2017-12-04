using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GanonBossFightTrigger : MonoBehaviour
{
    [SerializeField]
    public GameObject ganonBoss;
    [SerializeField]
    public GameObject mCamera;
    [SerializeField]
    AudioClip fightSong;
    [SerializeField]
    SpriteRenderer bossMask;

    private bool wasTriggered;
    private bool ganonActive;

    // Use this for initialization
    void Start()
    {
        wasTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (wasTriggered && bossMask.color.a > 0)
        {
            bossMask.color = new Color(0, 0, 0, bossMask.color.a - Time.fixedDeltaTime);
        }
        if (bossMask.color.a <= 0.9 && !ganonActive)
        {
            print("Here");
            mCamera.GetComponent<AudioSource>().clip = fightSong;
            mCamera.GetComponent<AudioSource>().Play();
            ganonBoss.GetComponent<Ganon>().Activate();
            ganonActive = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            EnterStage();
            wasTriggered = true;
        }
    }

    private void EnterStage()
    {
        
    }
}
