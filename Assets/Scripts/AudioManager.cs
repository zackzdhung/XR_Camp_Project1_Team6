using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] vocals;
    private int curIndex;
    private AudioSource audioSource;
    public AudioClip audioClipGameOver;

    // private GameFlowController GameFlowControllerController;

    void Start()
    {
        curIndex = 0;
        // gameFlowController = FindObjectOfType<GameFlowController>();
    }

    void Update()
    {
        
    }

    void Speak()
    {
        
    }

    public void PlayGameOver()
    {
        audioSource.clip = audioClipGameOver;
        audioSource.Play();
    }

    IEnumerator PlaySoundClip()
    {
        
        yield return new WaitUntil(() => audioSource.isPlaying == false);
    }
}
