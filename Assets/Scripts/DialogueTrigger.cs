using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] audioClips;
    public bool hasSoundEffect;
    
    public Dialogue[] dialogue;
    private DialogueManager dialogueManager;
    private int curIndex;


    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        curIndex = 0;
        if (hasSoundEffect)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.clip = audioClips[curIndex];
        }
    }

    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogue[curIndex]);
        audioSource.Play();
        curIndex++;
        curIndex = Math.Min(dialogue.Length-1, curIndex);
    }
}
