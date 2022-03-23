using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

namespace cakeslice
{
    public class OutlineAnimation : MonoBehaviour
    {
        [Range(0, 2)]
        public int color;
        bool pingPong = false;

        public int stage;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if(OutlineEffect.Instance == null) return;

            Color c = OutlineEffect.Instance.GetColorFromID(color);

            if(pingPong)
            {
                c.a += Time.deltaTime;

                if(c.a >= 1)
                    pingPong = false;
            }
            else
            {
                c.a -= Time.deltaTime;

                if(c.a <= 0)
                    pingPong = true;
            }

            c.a = Mathf.Clamp01(c.a);
            OutlineEffect.Instance.SetColorFromID(c, color);
            OutlineEffect.Instance.UpdateMaterialsPublicProperties();
        }
    }
}