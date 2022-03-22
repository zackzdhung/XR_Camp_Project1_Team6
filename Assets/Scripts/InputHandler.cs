using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    private OVRCameraRig mCameraRig;
    private bool buttonOneInput = OVRInput.GetDown(OVRInput.Button.One);
    private bool buttonTwoInput = OVRInput.GetDown(OVRInput.Button.Two);

    public DialogueTrigger dialogueTrigger;
    public DialogueManager dialogueManager;
    void Start()
    {
        mCameraRig = FindObjectOfType<OVRCameraRig>();
    }

    void Update()
    {
        buttonOneInput = OVRInput.GetDown(OVRInput.Button.One);
        buttonTwoInput = OVRInput.GetDown(OVRInput.Button.Two);

        if (buttonOneInput)
        {
            dialogueTrigger.TriggerDialogue();
        }

        if (buttonTwoInput)
        {
            dialogueManager.DisplayNextSentence();
        }
        
    }


}
