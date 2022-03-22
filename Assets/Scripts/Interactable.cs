using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public bool selected;
    public string title;
    public string description;
    public TextMesh textTitle;
    public TextMesh textDescription;

    void Start()
    {
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            // infoPanel.SetActive(true);
            textTitle.text = title;
            textDescription.text = description;
        }
    }
}
