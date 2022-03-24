﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // public DialogueTrigger dialogueTrigger;
    private DialogueManager dialogueManager;

    private RaySelector raySelector;

    private GameFlowController gameFlowController;

    private bool hasNewChoice;

    public AudioSource audioSource;
    public AudioClip buttonSound;
    
    void Start()
    {
        raySelector = FindObjectOfType<RaySelector>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        gameFlowController = FindObjectOfType<GameFlowController>();
        hasNewChoice = true;
    }

    void Update()
    {
        if (dialogueManager.isInConversation)
        {
            GetButtonInput();
        }
        // else if (gameFlowController.IsOptional())
        // {
        //     GetChoiceInput();
        // }
        else
        {
            GetTriggerInput();
        }
        
        // if (OVRInput.GetDown(OVRInput.Button.Two))
        // {
        //     gameFlowController.curEvent = 3;
        //     gameFlowController.StartNextEvent(0);
        // }
    }

    private void GetChoiceInput()
    {
        // if (OVRInput.GetDown(OVRInput.Button.One))
        // {
        //     audioSource.PlayOneShot(buttonSound);
        // }
        // if (OVRInput.GetDown(OVRInput.Button.One))
        // {
        //     audioSource.PlayOneShot(buttonSound);
        // }
    }

    private void GetButtonInput()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            audioSource.PlayOneShot(buttonSound);
            dialogueManager.DisplayNextSentence();
        }
    }

    private void GetTriggerInput()
    {
        if (raySelector.target == null|| !raySelector.target.CompareTag("InteractableObject")) return;
        if (!hasNewChoice) return;
        raySelector.target.GetComponent<Interactable>().selected = true;
        raySelector.target.GetComponent<Interactable>().SetUpPanel();
        raySelector.target.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    public void MakeChoice()
    {
        if (gameFlowController.IsOptional())
        {
            // TODO
        }
        else
        {
            gameFlowController.MakeChoice(raySelector.target);
            hasNewChoice = true;
            raySelector.target.GetComponent<Interactable>().selected = false;
            raySelector.target = null;
        }
    }
}
