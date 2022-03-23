using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySelector : MonoBehaviour
{
    public LineRenderer laser;

    public float lineMaxLength = 10f;

    public bool toggled;

    public GameObject target { get; private set; }

    public bool isChanged { get; set; }
    // public bool endDialogue;
    //
    //
    // public GameObject InfoPanel;
    // public float cooldown = 5.0f;
    // private float curCooldown = 0.0f;
    // void Start()
    // {
    //     var position = transform.position;
    //     Vector3[] startLinePositions = {position, position};
    //     laser.SetPositions(startLinePositions);
    //     laser.enabled = false;
    //     InfoPanel.SetActive(false);
    //     isChanged = false;
    //     endDialogue = true;
    // }
    //
    // void Update()
    // {
    //     toggled = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.9f;
    //     laser.enabled = toggled;
    //     if (!toggled)
    //     {
    //         targetHit = false;
    //     }
    //     else
    //     {
    //         var transform1 = transform;
    //         StartRayCast(transform1.position, transform1.forward, lineMaxLength);
    //     }
    //
    //     curCooldown += Time.deltaTime;
    //     if (curCooldown >= cooldown) InfoPanel.SetActive(false);
    // }
    //
    // private void StartRayCast(Vector3 targetPosition, Vector3 direction, float length)
    // {
    //     targetHit = false;
    //     GameObject cur = target;
    //     var endPosition = targetPosition + direction * length;
    //     if (!Physics.Raycast(new Ray(targetPosition, direction), out var hit))
    //     {
    //         targetHit = false;
    //         laser.SetPosition(0, targetPosition);
    //         laser.SetPosition(1, endPosition);
    //         InfoPanel.SetActive(false);
    //         return;
    //     }
    //     
    //     target = hit.collider.gameObject;
    //     
    //     
    //     if (target.CompareTag("InteractableObject"))
    //     {
    //         if (target != cur && endDialogue)
    //         {
    //             endDialogue = false;
    //             isChanged = true;
    //             curCooldown = 0.0f;
    //             cur.GetComponent<Interactable>().selected = false;
    //         }
    //         InfoPanel.SetActive(true);
    //         targetHit = true;
    //         endPosition = hit.point;
    //         target.GetComponent<Interactable>().selected = true;
    //     }
    //     
    //     laser.SetPosition(0, targetPosition);
    //     laser.SetPosition(1, endPosition);
    //
    // }
    
    void Start()
    {
        var position = transform.position;
        Vector3[] startLinePositions = {position, position};
        laser.SetPositions(startLinePositions);
        laser.enabled = false;
    }

    void Update()
    {
        toggled = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.9f;
        laser.enabled = toggled;
        if (toggled)
        {
            var transform1 = transform;
            StartRayCast(transform1.position, transform1.forward, lineMaxLength);
        }
    }

    private void StartRayCast(Vector3 targetPosition, Vector3 direction, float length)
    {
        var backup = target;
        var endPosition = targetPosition + direction * length;
        if (!Physics.Raycast(new Ray(targetPosition, direction), out var hit))
        {
            laser.SetPositions(new[] { targetPosition, endPosition });
            return;
        }
        
        target = hit.collider.gameObject;

        if (target != backup && target != null) isChanged = true;
        
        laser.SetPositions(new[] { targetPosition, endPosition });
    }
}
