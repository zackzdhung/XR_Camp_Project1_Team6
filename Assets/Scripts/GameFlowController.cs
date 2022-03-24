using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    public int curEvent { set; get; }
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
        if (events[curEvent].isDead[choice])
        {
            Debug.Log("GameOver!");
            GameOver();
        }
        else
        {
            StopAllCoroutines();
            curEvent++;
            curEvent = Math.Min(eventCount - 1, curEvent);
            // TODO : transition condition
            switch (curEvent)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    StartCoroutine(StartTransition(new []{2, 3}));
                    break;
                case 3:
                    break;
                case 4:
                    StartCoroutine(StartTransition(new []{7}, true));
                    break;
                case 5:
                    audioManager.PlayVocal(15);
                    break;
                case 6:
                    audioManager.PlayVocal(17);
                    break;
                case 7:
                    audioManager.PlayVocal(18);
                    break;
                case 8:
                    StartCoroutine(StartTransition(new[] {8}));
                    break;
            }
            StopAllCoroutines();
            events[curEvent].StartEvent();
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
        // anim.SetBool(IsGameOver, true);
        anim.SetTrigger(IsGameOverTrigger);
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("GameOver"))
        // while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        // while (anim.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime)
        {
            yield return null;
        }
        // yield return new WaitForSeconds(3);

        anim.SetTrigger(PlayTrigger);
        
        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("Play"))
        // while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        // while (anim.GetCurrentAnimatorStateInfo(0).length > anim.GetCurrentAnimatorStateInfo(0).normalizedTime)

        {
            yield return null;
        }
        // yield return new WaitForSeconds(2);

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
    }

    private void GameOver()
    {
        anim.SetTrigger(IsGameOverTrigger);
        // TODO
    }

    public void MakeChoice(GameObject g)
    {
        var choice = g == events[curEvent].choices[0] ? 0 : 1;
        events[curEvent].EndEvent(choice);
        Debug.Log("choice idx = " + choice);
    }

    public bool IsOptional()
    {
        return events[curEvent].isOption;
    }
}
