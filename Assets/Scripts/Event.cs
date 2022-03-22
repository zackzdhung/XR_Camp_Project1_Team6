using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Event : MonoBehaviour
{
    public GameObject[] choices;

    public void StartEvent()
    {
        foreach (var choice in choices)
        {
            choice.tag = "InteractableObject";
        }
    }

    public void EndEvent()
    {
        foreach (var choice in choices)
        {
            choice.tag = "Untagged";
        }
    }
    
}
