using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Event : MonoBehaviour
{
    public GameObject[] choices;
    public bool[] isDead;

    public void StartEvent()
    {
        foreach (var choice in choices)
        {
            // choice.tag = "InteractableObject";
            choice.GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void EndEvent()
    {
        foreach (var choice in choices)
        {
            // choice.tag = "Untagged";
            choice.GetComponent<BoxCollider>().enabled = false;
        }
    }
    
}
