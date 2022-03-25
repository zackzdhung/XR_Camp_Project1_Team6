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

    private AudioManager audioManager;

    // keyboard testing
    public GameObject computerSet;
    public GameObject closet;
    public GameObject phone;
    public GameObject clock;
    public GameObject couch;
    public GameObject door;


    void Start()
    {
        raySelector = FindObjectOfType<RaySelector>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        gameFlowController = FindObjectOfType<GameFlowController>();
        hasNewChoice = true;
        audioManager = FindObjectOfType<AudioManager>();
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

        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            gameFlowController.curEvent = 3;
            gameFlowController.StartNextEvent(0);
        }
    }

    private void GetButtonInput()
    {
        // VR input
        // if (OVRInput.GetDown(OVRInput.Button.One))
        // {
        //     audioSource.PlayOneShot(buttonSound);
        //     dialogueManager.DisplayNextSentence();
        // }
        
        // keyboard testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.PlayOneShot(buttonSound);
            dialogueManager.DisplayNextSentence();
        }
    }

    private void GetTriggerInput()
    {
        // VR input
        // if (raySelector.target == null|| !raySelector.target.CompareTag("InteractableObject")) return;
        // if (!hasNewChoice) return;
        // raySelector.target.GetComponent<Interactable>().selected = true;
        // raySelector.target.GetComponent<DialogueTrigger>().TriggerDialogue();

        // keyboard testing
        if (!Input.anyKeyDown) return;
        Debug.Log("Get Player Keyboard Input");
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            raySelector.target = computerSet;
            raySelector.target.GetComponent<Interactable>().selected = true;
            raySelector.target.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            raySelector.target = closet;
            raySelector.target.GetComponent<Interactable>().selected = true;
            raySelector.target.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            raySelector.target = phone;
            raySelector.target.GetComponent<Interactable>().selected = true;
            raySelector.target.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            raySelector.target = clock;
            raySelector.target.GetComponent<Interactable>().selected = true;
            raySelector.target.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            raySelector.target = couch;
            raySelector.target.GetComponent<Interactable>().selected = true;
            raySelector.target.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            raySelector.target = door;
            raySelector.target.GetComponent<Interactable>().selected = true;
            raySelector.target.GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }

    public void MakeChoice()
    {
        gameFlowController.MakeChoice(raySelector.target);
        hasNewChoice = true;
        raySelector.target.GetComponent<Interactable>().selected = false;
        raySelector.target = null;
    }
}
