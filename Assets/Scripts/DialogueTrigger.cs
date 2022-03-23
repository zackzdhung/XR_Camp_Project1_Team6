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
    private int currentDialogue;


    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        currentDialogue = 0;
        if (hasSoundEffect)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.clip = audioClip;
        }
    }

    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogue[currentDialogue]);
        audioSource.Play();
        currentDialogue++;
        currentDialogue = Math.Min(dialogue.Length-1, currentDialogue);
    }
}
