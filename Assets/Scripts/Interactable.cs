using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public bool selected;
    public string title;
    public string description;
    private GameObject dialoguePanel;
    public Vector3 dialoguePanelPosition;
    public Vector3 dialoguePanelRotation;

    void Start()
    {
        dialoguePanel = GameObject.FindWithTag("DialoguePanel");
        selected = false;
    }

    // void Update()
    // {
    //     if (!selected) return;
    //
    //     // if (dialoguePanel == null) return;
    //     Debug.Log("try to move dialogue panel position!");
    //     dialoguePanel.transform.position = dialoguePanelPosition;
    //     dialoguePanel.transform.rotation = Quaternion.Euler(dialoguePanelRotation);
    // }

    public void SetUpDialoguePanel()
    {
        Debug.Log("SetUpDialoguePanel");
        dialoguePanel.SetActive(true);
        dialoguePanel.transform.position = dialoguePanelPosition;
        dialoguePanel.transform.rotation = Quaternion.Euler(dialoguePanelRotation);
    }
}
