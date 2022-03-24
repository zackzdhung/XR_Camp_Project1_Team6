using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public bool selected;
    public GameObject dialoguePanel;
    public Vector3 dialoguePanelPosition;
    public Vector3 dialoguePanelRotation;

    void Start()
    {
        // dialoguePanel = GameObject.FindWithTag("DialoguePanel");
        selected = false;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (!selected) return;
    //
    // }

    public void SetUpPanel()
    {
        dialoguePanel.SetActive(true);
        dialoguePanel.transform.position = dialoguePanelPosition;
        dialoguePanel.transform.rotation = Quaternion.Euler(dialoguePanelRotation);
    }
}
