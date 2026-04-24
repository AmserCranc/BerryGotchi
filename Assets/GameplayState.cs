using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace Berrygotchi
{
    public abstract class GameplayState
    {
        public abstract void OnEnter(Brain b);
        public abstract void OnUpdate();
        public abstract void OnLeave();



        public string action0, action1, action2;

        public Texture2D flipbook;
        public Material mat;
        public RenderMode renderMode;

        public enum RenderMode
        {
            FLIPBOOK, THREE_D, PHYSICS_SIM
        }


        protected void ProcessAIs()
        {
            foreach(AIController ai in GLOBAL.processableAIs)
                ai.state.OnUpdate();
        }

    }

}