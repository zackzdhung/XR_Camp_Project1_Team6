using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    public TextMesh nameText;
    public TextMesh dialogueText;
    private RaySelector raySelector;

    public Animator dialogueAnim;
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");

    void Start()
    {
        sentences = new Queue<string>();
        raySelector = FindObjectOfType<RaySelector>();
    }

    void Update()
    {

    }

    public void StartDialogue(Dialogue dialogue)
    {
        // Debug.Log("start conversation with " + dialogue.name);
        dialogueAnim.SetBool(IsOpen, true);
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
        dialogueAnim.SetBool(IsOpen, false);
        Debug.Log("End of conversation");
        raySelector.endDialogue = true;
    }
}
