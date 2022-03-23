using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] vocals;
    private int curVocal;
    private AudioSource audioSource;
    
    void Start()
    {
        curVocal = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Speak()
    {
        
    }
}
