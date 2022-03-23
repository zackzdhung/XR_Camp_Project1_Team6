using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Event : MonoBehaviour
{
    public GameObject[] choices;
    public bool[] isDead;
    [TextArea]
    public String[] options;
    public bool isOption;
    

    public void StartEvent()
    {
        if (isOption)
        {
            
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

    public void EndEvent()
    {
        if (isOption)
        {
            
        }
        else
        {
            foreach (var choice in choices)
            {
                // choice.tag = "Untagged";
                choice.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
