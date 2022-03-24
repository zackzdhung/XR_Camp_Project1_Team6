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

    // Update is called once per frame
    void Update()
    {
        if (!selected) return;

        if (dialoguePanel == null) return;
        dialoguePanel.transform.position = dialoguePanelPosition;
        dialoguePanel.transform.rotation = Quaternion.Euler(dialoguePanelRotation);
    }
}
