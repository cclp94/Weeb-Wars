using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    [SerializeField]
    public GameObject mPausePanel;
    public GameObject mGameOver;
    public GameObject mComplete;

    private bool mIsMenuOpen;

    void Start()
    {
        mPausePanel.SetActive(false);
        mGameOver.SetActive(false);
        mComplete.SetActive(false);
    }
	void Update()
    {
        
        if (Input.GetButtonDown("Pause"))
        {
            if (mIsMenuOpen)
            {
                Time.timeScale = 1;
                mPausePanel.SetActive(false);
                mIsMenuOpen = false;
            }
            else
            {
                Time.timeScale = 0;
                mPausePanel.SetActive(true);
                mIsMenuOpen = true;
            }
        }

        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            //Time.timeScale = 0;
            mGameOver.SetActive(true);
            mIsMenuOpen = true;
        }

        if (GameObject.FindGameObjectWithTag("TitanHand") == null)
        {
            //Time.timeScale = 0;
            mComplete.SetActive(true);
            mIsMenuOpen = true;
        }
    }
   
}
