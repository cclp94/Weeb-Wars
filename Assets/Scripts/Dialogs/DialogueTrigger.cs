using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType <DialogManager>().StartDialogue(dialogue);
    }

     void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            TriggerDialogue();
            Debug.Log("Entered empty Game Object");
            Destroy(this.gameObject);
        }
    }

}
