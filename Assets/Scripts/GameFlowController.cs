using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class GameFlowController : MonoBehaviour
{
    public int curEvent { private set; get; }
    
    public Event[] events;
    private int eventCount;
    
    private DialogueManager dialogueManager;

    public Animator anim;
    private static readonly int IsGameOver = Animator.StringToHash("IsGameOver");

    private AudioManager audioManager;

    public GameObject gameOverText;
    private static readonly int IsGameOverTrigger = Animator.StringToHash("IsGameOverTrigger");
    private static readonly int PlayTrigger = Animator.StringToHash("PlayTrigger");

    public GameObject cameraRigGameObject;
    
    public Vector3 nextScenePosition;
    public Vector3 nextSceneRotation;

    public Image fadeImage;

    public GameObject foods;

    void Start()
    {
        curEvent = 0;
        curEvent = 0;
        dialogueManager = FindObjectOfType<DialogueManager>();
        anim.SetBool(IsGameOver, false);
        events[curEvent].StartEvent();
        eventCount = 9;
        audioManager = FindObjectOfType<AudioManager>();
        gameOverText.SetActive(false);
        audioManager.PlayBGM(0);
        foods.SetActive(false);
    }

    public void StartNextEvent(int choice)
    {
        Debug.Log("curEvent : " + curEvent);
        if (events[curEvent].isDead[choice] || events[curEvent].isEnd)
        {
            Debug.Log("GameOver! || isEnd!");
            StartCoroutine(GameOver(choice));
        }
        else
        {
            curEvent++;
            curEvent = Math.Min(eventCount - 1, curEvent);
            Debug.Log("Cur event after math min = " + curEvent);
            StartCoroutine(StartNextEventCoroutine());
        }
    }
    
    private IEnumerator StartNextEventCoroutine(bool skip = false)
    {
        switch (curEvent)
        {
            case 2:
                if (skip)
                {
                    audioManager.PlayBGM(1);
                    events[curEvent].StartEvent();
                }
                else
                {
                    yield return StartCoroutine(StartTransition(new []{2, 3}));
                }
                break;
            case 4:
                yield return StartCoroutine(StartTransition(new []{7}, true));
                break;
            case 8:
                yield return StartCoroutine(StartTransition(new[]{8}));
                break;
            default:
                events[curEvent].StartEvent();
                break;
        }
    }

    // private IEnumerator WaitForPlayerInput(int soundEffectIndex, bool)
    // {
    //     audioManager.PlaySoundEffect(soundEffectIndex);
    //     while (!OVRInput.GetDown(OVRInput.Button.One))
    //     {
    //         yield return null;
    //     }
    // }

    private IEnumerator StartTransition(IEnumerable<int> soundEffectIndices, bool needTeleport = false)
    {
        anim.SetTrigger(IsGameOverTrigger);
        // while (!anim.GetNextAnimatorStateInfo(0).IsName("GameOver"))
        // {
        //     // Debug.Log("IEnumerator StartTransition to state GameOver Yield return null");
        //     yield return null;
        // }

        anim.SetTrigger(PlayTrigger);

        while (!anim.GetNextAnimatorStateInfo(0).IsName("Play"))
        {
            // Debug.Log("IEnumerator StartTransition to state Play Yield return null");
            yield return null;
        }
        
        switch (curEvent)
        {
            case 2:
                audioManager.StopBGM();
                break;
            case 8:
                foods.SetActive(false);
                break;
        }


        if (needTeleport)
        {
            cameraRigGameObject.transform.position = nextScenePosition;
            cameraRigGameObject.transform.rotation = Quaternion.Euler(nextSceneRotation);
        }

        foreach (var soundEffectIndex in soundEffectIndices)
        {
            audioManager.PlaySoundEffect(soundEffectIndex);
            do
            {
                yield return null;
            } while (audioManager.IsPlaying());
        }

        while (fadeImage.color.a != 0)
        {
            yield return null;
        }
        
        switch (curEvent)
        {
            case 2:
                audioManager.PlayBGM(1);
                break;
            case 4:
                audioManager.PlayBGM(2);
                break;
        }

        events[curEvent].StartEvent();
    }

    private IEnumerator GameOver(int choice)
    {
        anim.SetTrigger(IsGameOverTrigger);
        audioManager.StopBGM();
        if (!events[curEvent].isEnd && choice != 1)
        {
            StartCoroutine(audioManager.PlaySoundClipRoutine(audioManager.audioClipGameOver));
        }
        while (!anim.GetNextAnimatorStateInfo(0).IsName("GameOver"))
        {
            // Debug.Log("IEnumerator StartTransition to state GameOver Yield return null");
            yield return null;
        }

        if (events[curEvent].isEnd)
        {
            Debug.Log("EndGame");
            EndGame(choice);
        }
        else
        {
            TriggerGameOver(choice);
        }
    }

    private void EndGame(int choice)
    {
        Debug.Log("Trigger EndGame");
        events[curEvent].gameOverObjects[choice].GetComponent<DialogueTrigger>().TriggerDialogue(isEnd: true);
    }

    public void Play()
    {
        anim.SetTrigger(PlayTrigger);
        dialogueManager.gameOverTitleText.gameObject.SetActive(false);
        dialogueManager.gameOverText.gameObject.SetActive(false);
        dialogueManager.gameOverHintText.gameObject.SetActive(false);
        // StartCoroutine(PlayCoroutine());
    }
    // private IEnumerator PlayCoroutine()
    // {
    //     anim.SetTrigger(PlayTrigger);
    //     while (anim.GetCurrentAnimatorStateInfo(0).IsName("GameOver"))
    //     {
    //         yield return null;
    //     }
    // }

    private void TriggerGameOver(int choice)
    {
        Debug.Log("Trigger GameOver");
        events[curEvent].gameOverObjects[choice].GetComponent<DialogueTrigger>().TriggerDialogue(isGameOver : true);
    }

    public void MakeChoice(GameObject g)
    {
        var choice = g == events[curEvent].choices[0] ? 0 : 1;
        events[curEvent].EndEvent(choice);
        Debug.Log("Make Choice " + choice);
    }

    public void TriggerOptions()
    {
        Debug.Log("Trigger Options");
        events[curEvent].optionObject.GetComponent<DialogueTrigger>().TriggerDialogue(true);
    }

    public void Choose(int optionIdx)
    {
        Debug.Log("Make Choice (option) " + optionIdx);
        events[curEvent].EndEvent(optionIdx);
    }

    public void RollBack()
    {
        switch (curEvent)
        {
            case 1:
                audioManager.PlayBGM(0);
                break;
            case 8:
                audioManager.PlayBGM(2);
                break;
        }
        StartCoroutine(StartNextEventCoroutine(curEvent == 2));
    }
}
