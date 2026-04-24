using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Berrygotchi
{
    public class Intro : GameplayState
    {
        float frametime = 0.083f;
        int frameCount = 0;
        int texSize;

        public override void OnEnter(Brain b)
        {
            flipbook = Resources.Load<Texture2D>("FLIPBOOK_intro");
            mat = new(Shader.Find("Flipbook"));
            mat.SetTexture("_Flipbook", flipbook);
            flipbook.filterMode = FilterMode.Point;
            texSize = flipbook.height;

            Debug.Log(flipbook == null ? "Flipbook missing" : "Flipbook loaded");
            Debug.Log(mat.shader == null ? "Shader missing" : mat.shader.name);
        }

        public override void OnLeave()
        {
            
        }

        public override void OnUpdate()
        {
            if(Time.deltaTime < frametime)
                return;

            UpdateAnimations();
        }

        private void UpdateAnimations()
        {
            mat.mainTextureOffset = new Vector2(texSize * frameCount, 0);


            frameCount++;
        }
    }

}
