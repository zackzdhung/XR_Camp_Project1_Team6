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
    
    private bool isRjFirstEvent;
    private bool isRjThirdEvent;
    private bool isRjFifthEvent;
    
    void Start()
    {
        sentences = new Queue<string>();
        isInConversation = false;
        playerInput = FindObjectOfType<PlayerInput>();
        dialoguePanel = GameObject.FindWithTag("DialoguePanel");
        // dialoguePanel.SetActive(false);
        audioManager = FindObjectOfType<AudioManager>();
        isComputerSetSecondEvent = false;
        isRjFirstEvent = false;
        isRjThirdEvent = false;
    }

    public void StartDialogue(Dialogue dialogue, int curIdx)
    {
        isInConversation = true;
        dialoguePanel.SetActive(true);
        nameText.gameObject.SetActive(true);
        hintText.gameObject.SetActive(true);
        dialogueText.gameObject.SetActive(true);
        dialogueAnim.SetBool(IsOpen, true);
        nameText.text = dialogue.name;
        switch (dialogue.name)
        {
            case "電腦":
                isComputerSetSecondEvent = true;
                break;
            case "阿傑":
                switch (curIdx)
                {
                    case 0:
                        isRjFirstEvent = true;
                        break;
                    case 2:
                        isRjThirdEvent = true;
                        break;
                    case 4:
                        isRjFifthEvent = true;
                        break;
                }

                break;
        }

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

        if (isComputerSetSecondEvent && sentences.Count == 2)
        {
            isComputerSetSecondEvent = false;
            audioManager.PlayVocal(2);
            audioManager.PlaySoundEffect(1);
        }

        if (isRjFirstEvent && sentences.Count == 1)
        {
            isRjFirstEvent = false;
            audioManager.PlayVocal(14);
        }
        
        if (isRjThirdEvent && sentences.Count == 2)
        {
            isRjThirdEvent = false;
            audioManager.PlayVocal(16);
        }

        // if (isRjThirdEvent && sentences.Count == 0)
        // {
        //     isRjThirdEvent = false;
        //     audioManager.PlayVocal(17);
        // }
        
        if (isRjFifthEvent && sentences.Count == 1)
        {
            isRjFifthEvent = false;
            audioManager.PlayVocal(19);
        }
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
