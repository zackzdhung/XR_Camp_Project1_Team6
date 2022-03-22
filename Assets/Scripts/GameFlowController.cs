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

    private int curEvent;

    public Event[] events;

    private Level currentLevel;
    private bool isEvenChanged;
    

    void Start()
    {
        currentLevel = Level.Room;
        isEvenChanged = true;
        curEvent = 0;
    }

    void Update()
    {
        if (isEvenChanged)
        {
            events[curEvent].StartEvent();
            isEvenChanged = false;
        }
    }

    public void StartNextEvent()
    {
        curEvent++;
        isEvenChanged = true;
    }
}
