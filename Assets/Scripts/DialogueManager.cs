using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    public TextMesh nameText;
    public TextMesh dialogueText;
    
    void Start()
    {
        sentences = new Queue<string>();
    }

    void Update()
    {

    }

    public void StartDialogue(Dialogue dialogue)
    {
        // Debug.Log("start conversation with " + dialogue.name);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (var sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        var sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        
        // for ()
    }

    private void EndDialogue()
    {
        Debug.Log("End of conversation");
    }
}
