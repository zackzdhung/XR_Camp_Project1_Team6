using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class InfoPanel : MonoBehaviour
{
    [TextArea]
    public String title;
    [TextArea]
    public String info;

    public String[] hints;

    public TextMesh titleText;

    private RaySelector raySelector;
    void Start()
    {
        raySelector = FindObjectOfType<RaySelector>();
    }

    void Update()
    {
        if (raySelector.target != null)
        {
            titleText.text = raySelector.target.name;
        }
    }

    public void SetEnable(bool enable)
    {
        transform.gameObject.SetActive(enable);
    }
    
    
}
