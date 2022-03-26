using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

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
    private static readonly int IsOpen = Animator.StringToHash("isOpen");


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
        if (dialogueManager.isInConversation && !dialogueManager.waitForPlayerOptionInput && !audioManager.IsPlaying())
        {
            GetButtonInput();
        }
        else if (gameFlowController.curEvent < 4)
        {
            GetTriggerInput();
        }
    }

    private void GetButtonInput()
    {
        if (dialogueManager.isTyping) return;
        // VR input
        if (!OVRInput.GetDown(OVRInput.Button.One)) return;
        audioSource.PlayOneShot(buttonSound);
        dialogueManager.DisplayNextSentence(false, gameFlowController.curEvent >= 4);

        // keyboard testing
        // if (!Input.GetKeyDown(KeyCode.Space)) return;
        // Debug.Log("Get keycode.space input");
        // audioSource.PlayOneShot(buttonSound);
        // dialogueManager.DisplayNextSentence(false,gameFlowController.curEvent >= 4);
    }

    private void GetTriggerInput()
    {
        // VR input
        if (raySelector.target == null|| !raySelector.target.CompareTag("InteractableObject")) return;
        if (!hasNewChoice) return;
        hasNewChoice = false;
        raySelector.target.GetComponent<Interactable>().selected = true;
        raySelector.target.GetComponent<DialogueTrigger>().TriggerDialogue();

        // keyboard testing
        // if (!Input.anyKeyDown) return;
        // Debug.Log("Get Player Keyboard Input");
        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     gameFlowController.curEvent = 3;
        //     gameFlowController.StartNextEvent(0);
        // }
        // else if (Input.GetKeyDown(KeyCode.Keypad0))
        // {
        //     raySelector.target = computerSet;
        //     raySelector.target.GetComponent<Interactable>().selected = true;
        //     raySelector.target.GetComponent<DialogueTrigger>().TriggerDialogue();
        // }
        // else if (Input.GetKeyDown(KeyCode.Keypad1))
        // {
        //     raySelector.target = closet;
        //     raySelector.target.GetComponent<Interactable>().selected = true;
        //     raySelector.target.GetComponent<DialogueTrigger>().TriggerDialogue();
        // }
        // else if (Input.GetKeyDown(KeyCode.Keypad2))
        // {
        //     raySelector.target = phone;
        //     raySelector.target.GetComponent<Interactable>().selected = true;
        //     raySelector.target.GetComponent<DialogueTrigger>().TriggerDialogue();
        // }
        // else if (Input.GetKeyDown(KeyCode.Keypad3))
        // {
        //     raySelector.target = clock;
        //     raySelector.target.GetComponent<Interactable>().selected = true;
        //     raySelector.target.GetComponent<DialogueTrigger>().TriggerDialogue();
        // }
        // else if (Input.GetKeyDown(KeyCode.Keypad4))
        // {
        //     raySelector.target = couch;
        //     raySelector.target.GetComponent<Interactable>().selected = true;
        //     raySelector.target.GetComponent<DialogueTrigger>().TriggerDialogue();
        // }
        // else if (Input.GetKeyDown(KeyCode.Keypad5))
        // {
        //     raySelector.target = door;
        //     raySelector.target.GetComponent<Interactable>().selected = true;
        //     raySelector.target.GetComponent<DialogueTrigger>().TriggerDialogue();
        // }
    }

    public IEnumerator MakeChoice(bool isOption, bool hasOption)
    {
        if (hasOption)
        {
            gameFlowController.TriggerOptions();
        }
        else if (isOption)
        {
            yield return StartCoroutine(GetPlayerOptionInput());
        }
        else
        {
            gameFlowController.MakeChoice(raySelector.target);
            hasNewChoice = true;
            raySelector.target.GetComponent<Interactable>().selected = false;
            raySelector.target = null;
        }
    }

    private IEnumerator GetPlayerOptionInput()
    {
        Debug.Log("GetPlayerOptionInput Start");
        var done = false;
        while (!done)
        {
            // VR input
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                done = true;
                gameFlowController.Choose(0);
            } 
            else if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                done = true;                
                gameFlowController.Choose(1);
            }
            // Keyboard testing
            // if (Input.GetKeyDown(KeyCode.A))
            // {
            //     done = true;
            //     gameFlowController.Choose(0);
            // } 
            // else if (Input.GetKeyDown(KeyCode.B))
            // {
            //     done = true;                
            //     gameFlowController.Choose(1);
            // }
            // Debug.Log("GetPlayerOptionInput yield return");
            yield return null;
        }
        dialogueManager.optionAnim.SetBool(IsOpen, false);
        dialogueManager.optionPanel.SetActive(false);
    }

    public IEnumerator RollBack()
    {
        Debug.Log("RollBack Start");
        var done = false;
        while (!done)
        {
            // VR input
            if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                done = true;
                gameFlowController.RollBack();
            }
            // Keyboard testing
            // if (Input.GetKeyDown(KeyCode.B))
            // {
            //     done = true;
            //     gameFlowController.Choose(0);
            // } 
            // Debug.Log("GetPlayerOptionInput yield return");
            yield return null;
        }

        gameFlowController.Play();
    }

    public IEnumerator GameEnd()
    {
        Debug.Log("RollBack Start");
        var done = false;
        while (!done)
        {
            // VR input
            if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                done = true;
                Application.Quit();
            }

            // Keyboard testing
            // if (Input.GetKeyDown(KeyCode.B))
            // {
            //     done = true;
            //     gameFlowController.Choose(0);
            // } 
            // Debug.Log("GetPlayerOptionInput yield return");
            yield return null;
        }
    }
}
