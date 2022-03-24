using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] vocals;
    public AudioClip[] soundEffects;
    // private int curVocal;
    // private int curSoundEffect;
    private AudioSource audioSource;
    public AudioClip audioClipGameOver;
    
    // private GameFlowController GameFlowControllerController;

    void Start()
    {
        // curVocal = 0;
        // curSoundEffect = 1;
        audioSource = gameObject.GetComponent<AudioSource>();
        // gameFlowController = FindObjectOfType<GameFlowController>();
    }

    void Update()
    {
        
    }

    public void PlayVocal(int idx)
    {
        PlaySoundClip(vocals[idx]);
        // curVocal++;
    }

    public void PlaySoundEffect(int idx)
    {
        PlaySoundClip(soundEffects[idx]);
        // curSoundEffect++;
    }

    private void PlaySoundClip(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        // StartCoroutine(PlaySoundClipRoutine(audioClip));
    }
    public void PlayGameOver()
    {
        audioSource.clip = audioClipGameOver;
        audioSource.Play();
    }

    IEnumerator PlaySoundClipRoutine(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        yield return new WaitForSeconds(audioClip.length);
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }
    
}
