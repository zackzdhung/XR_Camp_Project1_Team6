using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameFlowController : MonoBehaviour
{
    private enum Level
    {
        Room,
        Restaurant
    }

    private int curEvent;

    public Event[] events;

    // private Level currentLevel;
    private bool isEventChanged;

    private DialogueManager dialogueManager;
    

    void Start()
    {
        // currentLevel = Level.Room;
        isEventChanged = true;
        curEvent = 0;
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        if (dialogueManager.isInConversation)
        {
            GetPlayerInput();
        }
        if (!isEventChanged) return;
        // next event
        events[curEvent].StartEvent();
        isEventChanged = false;
        
    }

    private void GetPlayerInput()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            dialogueManager.DisplayNextSentence();
        }
        // TODO   
        // if (OVRInput.GetDown(OVRInput.Button.Two))
        // {
        //    
        // }
    }

    private void StartNextEvent(int choice)
    {
        if (events[curEvent].isDead[choice])
        {
            GameOver();
        }
        else
        {
            curEvent++;
            isEventChanged = true;    
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
