﻿/*
//  Copyright (c) 2015 José Guerreiro. All rights reserved.
//
//  MIT license, see http://www.opensource.org/licenses/mit-license.php
//  
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//  
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//  
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
*/

using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace cakeslice
{
	[RequireComponent(typeof(Renderer))]
	/* [ExecuteInEditMode] */
	public class Outline : MonoBehaviour
	{
		public Renderer Renderer { get; private set; }
		public SpriteRenderer SpriteRenderer { get; private set; }
		public SkinnedMeshRenderer SkinnedMeshRenderer { get; private set; }
		public MeshFilter MeshFilter { get; private set; }

		[Range(0, 2)]
		public int color;
		public bool eraseRenderer;
        public int stage;
		public bool anim;
        bool pingPong = false;

		private void Awake()
		{
			Renderer = GetComponent<Renderer>();
			SkinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
			SpriteRenderer = GetComponent<SpriteRenderer>();
			MeshFilter = GetComponent<MeshFilter>();
		}

		void OnEnable()
		{
			OutlineEffect.Instance?.AddOutline(this);
		}

		void OnDisable()
		{
			OutlineEffect.Instance?.RemoveOutline(this);
		}

        // Update is called once per frame
        void Update()
        {
            if(OutlineEffect.Instance == null) return;
			
			if(stage != 0) {
				OutlineEffect.Instance?.RemoveOutline(this);
			}else{
				OutlineEffect.Instance?.AddOutline(this);
			}


            Color c = OutlineEffect.Instance.GetColorFromID(color);
			if(!anim) 
			// else if(!anim) 
			{
				c.a = 1;	
			}
			else if(pingPong)
				// if(pingPong)
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

		private Material[] _SharedMaterials;
		public Material[] SharedMaterials
		{
			get
			{
				if (_SharedMaterials == null)
					_SharedMaterials = Renderer.sharedMaterials;

				return _SharedMaterials;
			}
		}
	}
}