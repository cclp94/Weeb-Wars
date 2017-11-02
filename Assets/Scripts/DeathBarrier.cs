using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour {

    [SerializeField]
    public GameObject connectedButtonGameObject;
    [SerializeField]
    public bool isOpen;

    private Animator mAnimator;
    List<Button> connectedButtons;

	// Use this for initialization
	void Start () {
        connectedButtons = new List<Button>();
        if (connectedButtonGameObject.transform.childCount == 0)
            connectedButtons.Add(connectedButtonGameObject.GetComponent<Button>());
        else{
            for (int i = 0; i < connectedButtonGameObject.transform.childCount; i++){
                connectedButtons.Add(connectedButtonGameObject.transform.GetChild(i).GetComponent<Button>());
            }
        }
        mAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		bool isPressed = true;
        foreach(Button b in connectedButtons){
            if(!b.IsPressed()){
                isPressed = false;
                break;
            }
        }
        isOpen = isPressed;
        mAnimator.SetBool("isOpen", isOpen);
        rescaleCollider();
	}

    public void OpenBarrier(){
        isOpen = true;
		
    }
	public void CloseBarrier()
	{
        isOpen = false;
	}

    private void rescaleCollider(){
		Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
		gameObject.GetComponent<BoxCollider2D>().size = S;
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0, -(S.y / 2));
    }

}
