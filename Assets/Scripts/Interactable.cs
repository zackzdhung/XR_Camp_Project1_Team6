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
    public bool hasSoundEffect;

    private AudioSource audioSource;
    public AudioClip audioClip;

    void Start()
    {
        dialoguePanel = GameObject.FindWithTag("DialoguePanel");
        selected = false;
        if (hasSoundEffect)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.clip = audioClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!selected) return;
        if (hasSoundEffect) audioSource.Play();
        
        dialoguePanel.transform.position = dialoguePanelPosition;
        // var position = transform.position;
        // var dir = (position - centerEyeAnchor.transform.position).normalized;
        // dialoguePanel.transform.LookAt(position + dir);
        dialoguePanel.transform.rotation = Quaternion.Euler(dialoguePanelRotation);
    }
}
