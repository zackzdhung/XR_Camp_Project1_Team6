﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GameFlowController : MonoBehaviour
{
    public int curEvent { set; get; }
    // private int curEvent;
    
    public Event[] events;
    private int eventCount;
    
    private DialogueManager dialogueManager;

    public Animator anim;
    private static readonly int IsGameOver = Animator.StringToHash("IsGameOver");

    private AudioManager audioManager;

    public GameObject gameOverText;
    private static readonly int IsGameOverTrigger = Animator.StringToHash("IsGameOverTrigger");
    private static readonly int PlayTrigger = Animator.StringToHash("PlayTrigger");

    public GameObject cameraRigGameObject;
    
    public Vector3 nextScenePosition;
    public Vector3 nextSceneRotation;

    void Start()
    {
        curEvent = 0;
        dialogueManager = FindObjectOfType<DialogueManager>();
        anim.SetBool(IsGameOver, false);
        events[curEvent].StartEvent();
        eventCount = 9;
        audioManager = FindObjectOfType<AudioManager>();
        gameOverText.SetActive(false);
    }

    public void StartNextEvent(int choice)
    {
        Debug.Log("curEvent : " + curEvent);
        if (events[curEvent].isDead[choice])
        {
            Debug.Log("GameOver!");
            GameOver();
        }
        else
        {
            curEvent++;
            // TODO check event bound?
            curEvent = Math.Min(eventCount - 1, curEvent);
            Debug.Log("Cur event after math min = " + curEvent);
            StartCoroutine(StartNextEventCoroutine());
            // switch (curEvent)
            // {
            //     case 0:
            //         break;
            //     case 1:
            //         break;
            //     case 2:
            //         StartCoroutine(StartTransition(new []{2, 3}));
            //         break;
            //     case 3:
            //         break;
            //     case 4:
            //         StartCoroutine(StartTransition(new []{7}, true));
            //         break;
            //     case 5:
            //         audioManager.PlayVocal(15);
            //         break;
            //     case 6:
            //         audioManager.PlayVocal(17);
            //         break;
            //     case 7:
            //         audioManager.PlayVocal(18);
            //         break;
            //     case 8:
            //         StartCoroutine(StartTransition(new[] {8}));
            //         break;
            // }
            // Debug.Log("Start event" + curEvent);
            // events[curEvent].StartEvent();
        }
    }

    private IEnumerator StartNextEventCoroutine()
    {
        switch (curEvent)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                yield return StartCoroutine(StartTransition(new []{2, 3}));
                break;
            case 3:
                break;
            case 4:
                yield return StartCoroutine(StartTransition(new []{7}, true));
                break;
            case 5:
                audioManager.PlayVocal(15);
                break;
            case 6:
                audioManager.PlayVocal(17);
                break;
            case 7:
                audioManager.PlayVocal(18);
                break;
            case 8:
                yield return StartCoroutine(StartTransition(new[] {8}));
                break;
        }
        Debug.Log("Start event" + curEvent);
        events[curEvent].StartEvent();    }

    // private IEnumerator WaitForPlayerInput(int soundEffectIndex, bool)
    // {
    //     audioManager.PlaySoundEffect(soundEffectIndex);
    //     while (!OVRInput.GetDown(OVRInput.Button.One))
    //     {
    //         yield return null;
    //     }
    // }

    private IEnumerator StartTransition(IEnumerable<int> soundEffectIndices, bool needTeleport = false)
    {
        anim.SetTrigger(IsGameOverTrigger);
        while (!anim.GetNextAnimatorStateInfo(0).IsName("GameOver"))
        {
            // Debug.Log("IEnumerator StartTransition to state GameOver Yield return null");
            yield return null;
        }

        anim.SetTrigger(PlayTrigger);
        
        while (!anim.GetNextAnimatorStateInfo(0).IsName("Play"))
        {
            // Debug.Log("IEnumerator StartTransition to state Play Yield return null");
            yield return null;
        }
        
        if (needTeleport)
        {
            cameraRigGameObject.transform.position = nextScenePosition;
            cameraRigGameObject.transform.rotation = Quaternion.Euler(nextSceneRotation);
        }

        foreach (var soundEffectIndex in soundEffectIndices)
        {
            audioManager.PlaySoundEffect(soundEffectIndex);
            do
            {
                yield return null;
            } while (audioManager.IsPlaying());
        }
    }

    private void GameOver()
    {
        anim.SetTrigger(IsGameOverTrigger);
        // TODO
    }

    public void MakeChoice(GameObject g)
    {
        var choice = g == events[curEvent].choices[0] ? 0 : 1;
        events[curEvent].EndEvent(choice);
        Debug.Log("Make Choice " + choice);
    }

    public void TriggerOptions()
    {
        Debug.Log("Trigger Options");
        events[curEvent].optionObject.GetComponent<DialogueTrigger>().TriggerDialogue(true);
    }

    public void Choose(int optionIdx)
    {
        events[curEvent].EndEvent(optionIdx);
        Debug.Log("Make Choice (option) " + optionIdx);
    }
}
