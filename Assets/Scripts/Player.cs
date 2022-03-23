using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public DialogueTrigger dialogueTrigger;
    private DialogueManager dialogueManager;

    private RaySelector raySelector;

    private GameFlowController gameFlowController;
    void Start()
    {
        raySelector = FindObjectOfType<RaySelector>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        gameFlowController = FindObjectOfType<GameFlowController>();
    }

    void Update()
    {
        GetPlayerInput();

        // buttonTwoInput = OVRInput.GetDown(OVRInput.Button.Two);  
    }

    private void GetPlayerInput()
    {
        if (raySelector.target == null|| !raySelector.target.CompareTag("InteractableObject")) return;
        if (!raySelector.isChanged) return;
        raySelector.isChanged = false;
        raySelector.target.GetComponent<Interactable>().selected = true;
        raySelector.target.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    public void MakeChoice()
    {
        gameFlowController.MakeChoice(raySelector.target);
    }
}
