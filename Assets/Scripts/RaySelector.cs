using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySelector : MonoBehaviour
{
    public LineRenderer laser;

    public float lineMaxLength = 10f;

    public bool toggled;

    public GameObject target { get; set; }
    
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
        
        laser.SetPositions(new[] { targetPosition, endPosition });
    }
}
