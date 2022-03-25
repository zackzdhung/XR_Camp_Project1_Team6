using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    public void StartEvent()
    {
        if (isOption)
        {
            // TODO
        }
        else
        {
            foreach (var choice in choices)
            {
                // choice.tag = "InteractableObject";
                choice.GetComponent<BoxCollider>().enabled = true;
            }
        }
       
    }

    public void EndEvent(int c)
    {
        if (isOption)
        {
            // TODO
        }
        else
        {
            foreach (var choice in choices)
            {
                // choice.tag = "Untagged";
                choice.GetComponent<BoxCollider>().enabled = false;
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
                audioManager.PlaySoundEffect(info.Idx);
            }
            else
            {
                audioManager.PlayVocal(info.Idx);
            }

            while (audioManager.IsPlaying())
            {
                // Debug.Log("IEnumerator PlayAudioSequentially Yield return null");
                yield return null;
            }
        }
        gameFlowController.StartNextEvent(choice);
    }
}
