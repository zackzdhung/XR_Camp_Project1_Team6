using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    public TextMesh nameText;
    public TextMesh dialogueText;
    public TextMesh hintText;
    public bool isInConversation;

    public Animator dialogueAnim;
    public Animator optionAnim;
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");
    private PlayerInput playerInput;
    private GameObject dialoguePanel;
    public GameObject optionPanel;

    // public TextMesh choiceAText;
    // public TextMesh choiceBText;
    public TextMesh optionAText;
    public TextMesh optionBText;

    private AudioManager audioManager;

    private bool isComputerSetSecondEvent;
    
    private bool isRjFirstEvent;
    private bool isRjThirdEvent;
    private bool isRjFifthEvent;

    public bool waitForPlayerOptionInput;
    
    void Start()
    {
        sentences = new Queue<string>();
        isInConversation = false;
        playerInput = FindObjectOfType<PlayerInput>();
        dialoguePanel = GameObject.FindWithTag("DialoguePanel");
        // dialoguePanel.SetActive(false);
        optionPanel = GameObject.FindWithTag("OptionPanel");
        audioManager = FindObjectOfType<AudioManager>();
        isComputerSetSecondEvent = false;
        isRjFirstEvent = false;
        isRjThirdEvent = false;
        waitForPlayerOptionInput = false;
    }

    public void StartDialogue(Dialogue dialogue, int curIdx, bool isOption, bool hasOption)
    {
        isInConversation = true;
        waitForPlayerOptionInput = false;
        if (isOption)
        {
            Debug.Log("Start Option Dialogue");
            optionPanel.SetActive(true);
            optionAText.gameObject.SetActive(true);
            optionBText.gameObject.SetActive(true);
            optionAnim.SetBool(IsOpen, true);
        }
        else
        {
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
        }
        sentences.Clear();

        foreach (var sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence(isOption, hasOption);
    }
    public void DisplayNextSentence(bool isOption, bool hasOption)
    {
        if (isOption)
        {
            // StopAllCoroutines();
            if (!waitForPlayerOptionInput) StartCoroutine(TypeOptions(sentences));
            return;
        }
        
        // choice
        if (sentences.Count == 0)
        {
            EndDialogue(false, hasOption);
            return;
        }

        var sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        // if (isComputerSetSecondEvent && sentences.Count == 2)
        // {
        //     isComputerSetSecondEvent = false;
        //     audioManager.PlayVocal(2);
        //     audioManager.PlaySoundEffect(1);
        // }
        //
        // if (isRjFirstEvent && sentences.Count == 1)
        // {
        //     isRjFirstEvent = false;
        //     audioManager.PlayVocal(14);
        // }
        //
        // if (isRjThirdEvent && sentences.Count == 2)
        // {
        //     isRjThirdEvent = false;
        //     audioManager.PlayVocal(16);
        // }
        //
        // // if (isRjThirdEvent && sentences.Count == 0)
        // // {
        // //     isRjThirdEvent = false;
        // //     audioManager.PlayVocal(17);
        // // }
        //
        // if (isRjFifthEvent && sentences.Count == 1)
        // {
        //     isRjFifthEvent = false;
        //     audioManager.PlayVocal(19);
        // }
    }

    private IEnumerator TypeOptions(Queue<string> options)
    {
        optionAText.text = "";
        foreach (var letter in options.Dequeue().ToCharArray())
        {
            optionAText.text += letter;
            yield return null;
        }
        optionBText.text = "";
        foreach (var letter in options.Dequeue().ToCharArray())
        {
            optionBText.text += letter;
            yield return null;
        }
        waitForPlayerOptionInput = true;
        if (options.Count != 0) yield break;
        Debug.Log("TypeOptions end dialogue");
        EndDialogue(true, false);
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (var letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
        
        if (isComputerSetSecondEvent && sentences.Count == 2)
        {
            isComputerSetSecondEvent = false;
            audioManager.PlayVocal(2);
            audioManager.PlaySoundEffect(1);
        }

        if (isRjFirstEvent && sentences.Count == 1)
        {
            isRjFirstEvent = false;
            // audioManager.PlayVocal(14);
            yield return StartCoroutine(audioManager.PlaySoundClipRoutine(audioManager.vocals[14]));
        }
        
        if (isRjThirdEvent && sentences.Count == 2)
        {
            isRjThirdEvent = false;
            // audioManager.PlayVocal(16);
            yield return StartCoroutine(audioManager.PlaySoundClipRoutine(audioManager.vocals[16]));
        }

        if (isRjThirdEvent && sentences.Count == 0)
        {
            isRjThirdEvent = false;
            // audioManager.PlayVocal(17);
            yield return StartCoroutine(audioManager.PlaySoundClipRoutine(audioManager.vocals[17]));
        }
        
        if (isRjFifthEvent && sentences.Count == 1)
        {
            isRjFifthEvent = false;
            // audioManager.PlayVocal(19);
            yield return StartCoroutine(audioManager.PlaySoundClipRoutine(audioManager.vocals[16]));
        }
    }
    

    private void EndDialogue(bool isOption, bool hasOption)
    {
        if (isOption)
        {
            isInConversation = false;
            StartCoroutine(playerInput.MakeChoice(true, hasOption));
        }
        else
        {
            isInConversation = false;
            // StartCoroutine(CloseDialoguePanel());
            // TODO
            // set inactive after animation finished
            dialogueAnim.SetBool(IsOpen, false);
            // hintText.gameObject.SetActive(false);
            dialoguePanel.SetActive(false);
            StartCoroutine(playerInput.MakeChoice(false, hasOption));
        }
        
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
