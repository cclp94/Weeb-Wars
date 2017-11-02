using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {
    [SerializeField]
    public bool isOneTimePress;

    bool isPressed;

    void Start(){
        isPressed = false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
		isPressed = true;

	}

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(!isOneTimePress)
            isPressed = false;
    }

    public bool IsPressed(){
        return isPressed;
    }

}
