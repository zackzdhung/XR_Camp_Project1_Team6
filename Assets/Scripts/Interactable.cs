using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public bool selected;

    // public string title;
    // public string description;
    private GameObject dialoguePanel;
    private GameObject optionPanel;
    public Vector3 dialoguePanelPosition;
    public Vector3 dialoguePanelRotation;

    void Start()
    {
        dialoguePanel = GameObject.FindWithTag("DialoguePanel");
        optionPanel = GameObject.FindWithTag("OptionPanel");
        selected = false;
    }

    public void SetUpDialoguePanel()
    {
        Debug.Log("SetUpDialoguePanel");
        dialoguePanel.SetActive(true);
        dialoguePanel.transform.position = dialoguePanelPosition;
        dialoguePanel.transform.rotation = Quaternion.Euler(dialoguePanelRotation);
    }

    public void SetUpOptionPanel()
    {
        Debug.Log("SetUpOptionPanel");
        optionPanel.SetActive(true);
        // optionPanel.transform.position = optionPanelPosition;
        // optionPanel.transform.rotation = Quaternion.Euler(optionPanelRotation);
    }
}
