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
    public TextMesh hintText;
    public bool isInConversation;

    public Animator dialogueAnim;
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");
    private PlayerInput playerInput;
    private GameObject dialoguePanel;
    
    private AudioManager audioManager;

    private bool isComputerSetSecondEvent;
    
    void Start()
    {
        sentences = new Queue<string>();
        isInConversation = false;
        playerInput = FindObjectOfType<PlayerInput>();
        dialoguePanel = GameObject.FindWithTag("DialoguePanel");
        // dialoguePanel.SetActive(false);
        audioManager = FindObjectOfType<AudioManager>();
        isComputerSetSecondEvent = false;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isInConversation = true;
        dialoguePanel.SetActive(true);
        nameText.gameObject.SetActive(true);
        hintText.gameObject.SetActive(true);
        dialogueText.gameObject.SetActive(true);
        dialogueAnim.SetBool(IsOpen, true);
        nameText.text = dialogue.name;
        if (dialogue.name == "電腦") isComputerSetSecondEvent = true;

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
        
        if (!isComputerSetSecondEvent || sentences.Count != 2) return;
        isComputerSetSecondEvent = false;
        audioManager.PlayVocal(2);
        audioManager.PlaySoundEffect(1);
    }

    private IEnumerator TypeSentence(string sentence)
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
        // hintText.gameObject.SetActive(false);
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
