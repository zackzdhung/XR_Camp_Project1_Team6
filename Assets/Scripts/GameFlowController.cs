using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class GameFlowController : MonoBehaviour
{
    public int curEvent { private set; get; }
    // private int curEvent;
    
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

    void Start()
    {
        curEvent = 0;
        dialogueManager = FindObjectOfType<DialogueManager>();
        anim.SetBool(IsGameOver, false);
        events[curEvent].StartEvent();
        eventCount = 9;
        audioManager = FindObjectOfType<AudioManager>();
        gameOverText.SetActive(false);
    }

    public void StartNextEvent(int choice)
    {
        Debug.Log("curEvent : " + curEvent);
        if (events[curEvent].isDead[choice])
        {
            Debug.Log("GameOver!");
            StartCoroutine(GameOver(choice));
        }
        else if (curEvent == eventCount - 1)
        {
            // TODO
            // good end
            // StartCoroutine(GameEnd());
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
        while (!anim.GetNextAnimatorStateInfo(0).IsName("GameOver"))
        {
            // Debug.Log("IEnumerator StartTransition to state GameOver Yield return null");
            yield return null;
        }

        anim.SetTrigger(PlayTrigger);
        
        while (!anim.GetNextAnimatorStateInfo(0).IsName("Play"))
        {
            Debug.Log("IEnumerator StartTransition to state Play Yield return null");
            yield return null;
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
        events[curEvent].StartEvent();
    }

    private IEnumerator GameOver(int choice)
    {
        anim.SetTrigger(IsGameOverTrigger);
        while (!anim.GetNextAnimatorStateInfo(0).IsName("GameOver"))
        {
            // Debug.Log("IEnumerator StartTransition to state GameOver Yield return null");
            yield return null;
        }
        TriggerGameOver(choice);
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
        if (choice == 0)
        {
            events[curEvent].gameOverObjects[choice].GetComponent<DialogueTrigger>().TriggerDialogue(isGameOver: true);
        }
        else
        {
            events[curEvent].gameOverObjects[choice].GetComponent<DialogueTrigger>().TriggerDialogue(isEnd: true);
        }
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
        events[curEvent].EndEvent(optionIdx);
        Debug.Log("Make Choice (option) " + optionIdx);
    }

    public void RollBack()
    {
        StartCoroutine(StartNextEventCoroutine(curEvent == 2));
    }
}
