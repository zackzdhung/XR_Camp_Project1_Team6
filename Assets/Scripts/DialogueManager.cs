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
    public bool isTyping { get; private set; }

    public Animator dialogueAnim;
    public Animator optionAnim;
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");
    private PlayerInput playerInput;
    private GameObject dialoguePanel;
    public GameObject optionPanel;

    public TextMesh optionATitleText;
    public TextMesh optionBTitleText;
    public TextMesh optionAText;
    public TextMesh optionBText;

    private AudioManager audioManager;

    private bool isComputerSetSecondEvent;
    
    private bool isFourthEvent;
    private bool isFifthEvent;
    private bool isSixthEvent;
    private bool isSeventhEvent;
    private bool isEighthEvent;

    public bool waitForPlayerOptionInput;

    public TextMesh gameOverTitleText;
    public TextMesh gameOverText;
    public TextMesh gameOverHintText;
    
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
        isFourthEvent = false;
        isSixthEvent = false;
        waitForPlayerOptionInput = false;
        isTyping = false;
    }

    public void StartDialogue(Dialogue dialogue, int curIdx, bool isOption, bool hasOption, bool isGameOver = false, bool isEnd = false)
    {
        isInConversation = true;
        waitForPlayerOptionInput = false;
        if (isGameOver || isEnd)
        {
            Debug.Log("Start GameOver Dialogue");
            gameOverTitleText.gameObject.SetActive(true);
            gameOverTitleText.text = dialogue.name;
            gameOverText.gameObject.SetActive(true);
        }
        else if (isOption)
        {
            Debug.Log("Start Option Dialogue");
            optionPanel.SetActive(true);
            optionATitleText.gameObject.SetActive(true);
            optionAText.gameObject.SetActive(true);
            optionBTitleText.gameObject.SetActive(true);
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
                            isFourthEvent = true;
                            break;
                        case 1:
                            isFifthEvent = true;
                            break;
                        case 2:
                            isSixthEvent = true;
                            break;
                        case 3:
                            isSeventhEvent = true;
                            break;
                        case 4:
                            isEighthEvent = true;
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

        DisplayNextSentence(isOption, hasOption, isGameOver, isEnd);
    }

    public void DisplayNextSentence(bool isOption, bool hasOption, bool isGameOver = false, bool isEnd = false)
    {
        if (isGameOver || isEnd)
        {
            if (!waitForPlayerOptionInput) StartCoroutine(TypeGameOverMessage(sentences, isEnd));
            return;
        }
        if (isOption)
        {
            if (!waitForPlayerOptionInput) StartCoroutine(TypeOptions(sentences));
            return;
        }
        
        // choice
        if (sentences.Count == 0)
        {
            EndDialogue(false, hasOption, false);
            return;
        }

        var sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeGameOverMessage(Queue<string> queue, bool isEnd)
    {
        gameOverText.text = "";
        foreach (var letter in queue.Dequeue().ToCharArray())
        {
            gameOverText.text += letter;
            yield return null;
        }
        EndDialogue(false, false, true, isEnd);
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
        EndDialogue(true, false, false);
    }

    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (var letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            // yield return new WaitForSeconds(0.1f);
            yield return null;
        }
        
        if (isComputerSetSecondEvent && sentences.Count == 2)
        {
            isComputerSetSecondEvent = false;
            audioManager.PlayVocal(2);
            audioManager.PlaySoundEffect(1);
        } 
        else if (isFourthEvent && sentences.Count == 1)
        {
            isFourthEvent = false;
            // audioManager.PlayVocal(14);
            yield return StartCoroutine(audioManager.PlaySoundClipRoutine(audioManager.vocals[14]));
        }
        else if (isFifthEvent && sentences.Count == 0)
        {
            isFifthEvent = false;
            yield return StartCoroutine(audioManager.PlaySoundClipRoutine(audioManager.vocals[15]));
        }
        else if (isSixthEvent && sentences.Count == 3)
        {
            yield return StartCoroutine(audioManager.PlaySoundClipRoutine(audioManager.vocals[16]));
        }
        else if (isSixthEvent && sentences.Count == 0)
        {
            isSixthEvent = false;
            yield return StartCoroutine(audioManager.PlaySoundClipRoutine(audioManager.vocals[17]));
        }
        else if (isSeventhEvent && sentences.Count == 0)
        {
            isSeventhEvent = false;
            yield return StartCoroutine(audioManager.PlaySoundClipRoutine(audioManager.vocals[18]));
        }
        else if (isEighthEvent && sentences.Count == 1)
        {
            isEighthEvent = false;
            yield return StartCoroutine(audioManager.PlaySoundClipRoutine(audioManager.vocals[19]));
        }

        isTyping = false;
    }


    private void  EndDialogue(bool isOption, bool hasOption, bool isGameOver, bool isEnd = false)
    {
        if (isEnd)
        {
            gameOverHintText.text = "Press B to close the Game";
            isInConversation = false;
            StartCoroutine(playerInput.GameEnd());
        }
        else if (isGameOver)
        {
            gameOverHintText.gameObject.SetActive(true);
            isInConversation = false;
            StartCoroutine(playerInput.RollBack());
        }
        else if (isOption)
        {
            isInConversation = false;
            StartCoroutine(playerInput.MakeChoice(true, hasOption));
        }
        else
        {
            isInConversation = false;
            dialogueAnim.SetBool(IsOpen, false);
            dialoguePanel.SetActive(false);
            StartCoroutine(playerInput.MakeChoice(false, hasOption));
        }

    }
}
