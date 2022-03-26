using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] vocals;
    public AudioClip[] soundEffects;
    private AudioSource audioSource;
    public AudioClip audioClipGameOver;
    
    public AudioSource bgmAudioSource;
    public AudioClip[] bgms;
    
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlayVocal(int idx)
    {
        PlaySoundClip(vocals[idx]);
    }

    public void PlaySoundEffect(int idx)
    {
        PlaySoundClip(soundEffects[idx]);
    }

    private void PlaySoundClip(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void PlayBGM(int idx)
    {
        bgmAudioSource.clip = bgms[idx];
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }
    
    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }

    public IEnumerator PlaySoundClipRoutine(AudioClip audioClip)
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
