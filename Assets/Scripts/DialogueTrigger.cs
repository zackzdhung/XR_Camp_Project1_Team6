using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void TriggerDialogue()
    {
        gameObject.GetComponent<Interactable>().SetUpDialoguePanel();
        dialogueManager.StartDialogue(dialogue[curIndex]);

        if (hasSoundEffect)
        {
            audioSource.PlayOneShot(audioClip);
            hasSoundEffect = false;
        }

        curIndex++;
        curIndex = Math.Min(dialogue.Length - 1, curIndex);     
    }
}