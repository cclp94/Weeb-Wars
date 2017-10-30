using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    [SerializeField]
    public GameObject mPausePanel;

    private bool mIsMenuOpen;

    void Start()
    {
        mPausePanel.SetActive(false);
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
    }
   
}
