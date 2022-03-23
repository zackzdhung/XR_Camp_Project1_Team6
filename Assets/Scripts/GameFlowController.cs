using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    private enum Level
    {
        Room,
        Restaurant
    }

    public int curEvent { private set; get; }
    // private int curEvent;
    
    public Event[] events;

    // private Level currentLevel;
    // private bool isEventChanged;

    private DialogueManager dialogueManager;
    

    void Start()
    {
        // currentLevel = Level.Room;
        // isEventChanged = true;
        curEvent = 0;
        dialogueManager = FindObjectOfType<DialogueManager>();
        events[curEvent].StartEvent();
    }

    // void Update()
    // {
    //     // if (dialogueManager.isInConversation)
    //     // {
    //     //     GetPlayerInput();
    //     // }
    //     // if (!isEventChanged) return;
    //     // // next event
    //     //
    //     // Debug.Log("start event idx = " + curEvent);
    //     // events[curEvent].StartEvent();
    //     // isEventChanged = false;
    //     
    // }

    // private void GetPlayerInput()
    // {
    //     if (OVRInput.GetDown(OVRInput.Button.One))
    //     {
    //         dialogueManager.DisplayNextSentence();
    //     }
    // }

    
    // private void StartNextEvent(int choice)
    private void StartNextEvent(int choice)
    {
        if (events[curEvent].isDead[choice])
        {
            Debug.Log("GameOver!");
            // GameOver();
        }
        else
        {
            curEvent++;
            // isEventChanged = true;
            events[curEvent].StartEvent();
        }
    }

    private void GameOver()
    {
        // TODO
    }

    public void MakeChoice(GameObject g)
    {
        var choice = g == events[curEvent].choices[0] ? 0 : 1;
        events[curEvent].EndEvent();
        StartNextEvent(choice);
    }
}
