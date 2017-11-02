using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public Queue<string> sentences;
    public Text nameText;
    public Text dialogueText;
    public Animator animator;

    private bool onPause = false;

    // Use this for initialization
    void Start () {
        sentences = new Queue<string>();
	}

    public void StartDialogue(Dialogue dialogue)
    {

        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
        StartCoroutine(Wait(0.5f));


    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {

        animator.SetBool("IsOpen", false);
        togglePause();
    }

    public void togglePause()
    {
        onPause = !onPause;
        if (onPause)
        {
            Time.timeScale = 0;
        }
        else { Time.timeScale = 1; }
    }

    IEnumerator Wait(float duration)
    {
        //Coroutine to wait
        yield return new WaitForSeconds(duration);
        togglePause();
    }

}
