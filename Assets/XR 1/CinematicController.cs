using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicController : MonoBehaviour
{

    public float noiseStrength;
    public int stage;
    public int threshold;
    List<Material> cinematicMaterials;
    List<Material> originMaterials;
    Renderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<Renderer>();
        originMaterials = new List<Material>();
        cinematicMaterials = new List<Material>();
        if(meshRenderer != null)
        {
            for (int i = 0; i < meshRenderer.materials.Length; ++i)
            {
                
                originMaterials.Add(meshRenderer.materials[i]);
                cinematicMaterials.Add(new Material(originMaterials[i]));
                cinematicMaterials[i].shader = Shader.Find("UltraEffects/Cinematic");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(originMaterials.Count != 0)
        {
            if(stage != 0 || threshold >= 20)
            {
                meshRenderer.materials = originMaterials.ToArray();
            }
            else
            {
                noiseStrength += Time.deltaTime;
                meshRenderer.materials = cinematicMaterials.ToArray();
                for(int i = 0; i < cinematicMaterials.Count; ++i)
                {
                    cinematicMaterials[i].SetFloat("_Strength", Mathf.Sin(noiseStrength * 2f) * 3f + 1.5f);
                }
            }
        }
    }
}
