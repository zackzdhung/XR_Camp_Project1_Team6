using System.Collections;
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
        audioSource.clip = buttonSound;
    }

    void Update()
    {
        if (dialogueManager.isInConversation)
        {
            GetButtonInput();
            raySelector.laser.enabled = false;
        }
        else
        {
            GetTriggerInput();
        }
        // buttonTwoInput = OVRInput.GetDown(OVRInput.Button.Two);  
    }

    private void GetButtonInput()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            audioSource.Play();
            dialogueManager.DisplayNextSentence();
        }
    }

    private void GetTriggerInput()
    {
        if (raySelector.target == null|| !raySelector.target.CompareTag("InteractableObject")) return;
        // if (!raySelector.isChanged) return;
        // raySelector.isChanged = false;
        if (!hasNewChoice) return;
        raySelector.target.GetComponent<Interactable>().selected = true;
        raySelector.target.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    public void MakeChoice()
    {
        gameFlowController.MakeChoice(raySelector.target);
        hasNewChoice = true;
        raySelector.target.GetComponent<Interactable>().selected = false;
        raySelector.target = null;
    }
}
