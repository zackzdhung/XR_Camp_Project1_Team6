using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Outline = XR_1.OutlineEffect.OutlineEffect.Outline;

public class Event : MonoBehaviour
{
    public GameObject[] choices;
    public bool[] isDead;
    
    public bool isOption;
    [TextArea]
    public String[] options;

    public bool[] isSoundEffect0;
    public bool[] isSoundEffect1;
    
    public int[] audioClipIndices0;
    public int[] audioClipIndices1;
    
    public AudioManager audioManager;

    public GameFlowController gameFlowController;

    public GameObject rj;

    public GameObject optionPanel;
    public TextMesh optionA;
    public TextMesh optionB;

    public void StartEvent()
    {
        if (isOption)
        {
            // TODO
            rj.GetComponent<DialogueTrigger>().TriggerDialogue();
            optionA.text = options[0];
            optionB.text = options[1];
        }
        else
        {
            foreach (var choice in choices)
            {
                // choice.tag = "InteractableObject";
                choice.GetComponent<BoxCollider>().enabled = true;

                foreach (var effect in choice.GetComponentsInChildren<Outline>())
                {
                    effect.stage = 0;
                }
               
            }
        }
       
    }

    public void EndEvent(int c)
    {
        if (isOption)
        {
            // TODO
            optionPanel.SetActive(false);
        }
        else
        {
            foreach (var choice in choices)
            {
                // choice.tag = "Untagged";
                choice.GetComponent<BoxCollider>().enabled = false;
                foreach (var effect in choice.GetComponentsInChildren<Outline>())
                {
                    effect.stage = 1;
                }
            }
        }
        
        var curIsSoundEffect = c == 0 ? isSoundEffect0 : isSoundEffect1;
        var curAudioClipsIndices = c == 0 ? audioClipIndices0 : audioClipIndices1;

        StartCoroutine(PlayAudioSequentially(curIsSoundEffect, curAudioClipsIndices, c));
    }

    private IEnumerator PlayAudioSequentially(IEnumerable<bool> curIsSoundEffect, IEnumerable<int> curAudioClipsIndices, int choice)
    {
        foreach (var info in curIsSoundEffect.Zip(curAudioClipsIndices, (b, i) => new {IsSoundeffect = b, Idx = i}))
        {
            // Debug.Log("isSoundEffect Length = " + cur.Length + " cur = " + isSoundEffect);
            if (info.IsSoundeffect)
            {
                Debug.Log("Play Sound Effect idx: " + info.Idx);
                audioManager.PlaySoundEffect(info.Idx);
            }
            else
            {
                Debug.Log("Play Vocal idx: " + info.Idx);
                audioManager.PlayVocal(info.Idx);
            }

            while (audioManager.IsPlaying())
            {
                yield return null;
            }
        }
        gameFlowController.StartNextEvent(choice);
    }
}
