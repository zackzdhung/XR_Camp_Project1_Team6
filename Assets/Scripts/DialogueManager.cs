using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    public TextMesh nameText;
    public TextMesh dialogueText;
    // private RaySelector raySelector;
    public bool isInConversation;

    public Animator dialogueAnim;
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");
    private PlayerInput playerInput;
    private GameObject dialoguePanel;
    
    void Start()
    {
        sentences = new Queue<string>();
        // raySelector = FindObjectOfType<RaySelector>();
        isInConversation = false;
        playerInput = FindObjectOfType<PlayerInput>();
        dialoguePanel = GameObject.FindWithTag("DialoguePanel");
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isInConversation = true;
        dialoguePanel.SetActive(true);
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

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(String sentence)
    {
        dialogueText.text = "";
        foreach (var letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    

    private void EndDialogue()
    {
        isInConversation = false;
        // StartCoroutine(CloseDialoguePanel());
        // TODO
        // set inactive after animation finished
        dialogueAnim.SetBool(IsOpen, false);
        dialoguePanel.SetActive(false);
        
        playerInput.MakeChoice();
    }

    // IEnumerator CloseDialoguePanel()
    // {
    //     dialogueAnim.SetBool(IsOpen, false);
    //     while (dialogueAnim.IsInTransition(0) || dialogueAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
    //     {
    //         yield return null;
    //     }
    //     dialoguePanel.SetActive(false);
    // }
    
}
