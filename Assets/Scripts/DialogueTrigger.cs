using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class DialogueTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audioClip;
    public bool hasSoundEffect;

    public Dialogue[] dialogue;
    private DialogueManager dialogueManager;
    private int curIndex;


    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        curIndex = 0;
        
        if (!hasSoundEffect) return;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void TriggerDialogue(bool isOption = false, bool hasOption = false, bool isGameOver = false, bool isEnd = false)
    {
        if (isOption)
        {
            Debug.Log("TriggerDialogue isOption");
            gameObject.GetComponent<Interactable>().SetUpOptionPanel();
        }
        else if (isEnd)
        {
            Debug.Log("TriggerDialogue isEnd");
        }
        else
        {
            gameObject.GetComponent<Interactable>().SetUpDialoguePanel();
        }
        dialogueManager.StartDialogue(dialogue[curIndex], curIndex, isOption, hasOption, isGameOver, isEnd);

        if (hasSoundEffect)
        {
            audioSource.PlayOneShot(audioClip);
            hasSoundEffect = false;
        }

        if (isGameOver || isEnd) return;
        curIndex++;
        curIndex = Math.Min(dialogue.Length - 1, curIndex);     
    }
}