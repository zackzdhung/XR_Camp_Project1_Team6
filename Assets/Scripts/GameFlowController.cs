using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    private enum Level
    {
        Room,
        Street,
        Restaurant
    }

    private Level currentLevel;
    private bool isLevelChanged;

    void Start()
    {
        currentLevel = Level.Room;
        isLevelChanged = false;
    }

    void Update()
    {
        
    }
}
