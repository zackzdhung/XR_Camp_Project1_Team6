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

    private GameFlowController gameFlowController;
    void Start()
    {
        mCameraRig = FindObjectOfType<OVRCameraRig>();
        gameFlowController = FindObjectOfType<GameFlowController>();
    }

    // void Update()
    // {
    //     if (OVRInput.GetDown(OVRInput.Button.Two))
    //     {
    //         gameFlowController.curEvent -= 2;
    //         gameFlowController.StartNextEvent(0);
    //         
    //     }
    // }


}
