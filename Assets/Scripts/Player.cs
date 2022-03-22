using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool buttonOneInput;
    private bool buttonTwoInput;

    public DialogueTrigger dialogueTrigger;
    public DialogueManager dialogueManager;

    private RaySelector raySelector;
    void Start()
    {
        raySelector = FindObjectOfType<RaySelector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (raySelector.isChanged)
        {
            dialogueManager.StartDialogue(raySelector.target.GetComponent<DialogueTrigger>().dialogue);
            raySelector.isChanged = false;
        }
        // buttonOneInput = OVRInput.GetDown(OVRInput.Button.One);
        // buttonTwoInput = OVRInput.GetDown(OVRInput.Button.Two);
        
    }
}
