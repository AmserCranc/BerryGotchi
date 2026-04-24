using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Berrygotchi
{
    public class AIController : MonoBehaviour
    {
        public AIState state;
        public Transform goalPos;
        public bool facingRight;


        public void ChangeState(AIState s)
        {
            if(state is not null)
                state.OnLeave();
            state = s;
            state.OnEnter(this);
        }
        
        public abstract class AIState
        {
            public abstract void OnEnter(AIController o);
            public abstract void OnUpdate();
            public abstract void OnLeave();
        }
    }
}

