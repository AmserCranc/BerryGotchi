using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace Berrygotchi
{
    public class Idle : GameplayState
    {
        public override void OnEnter(Brain b)
        {
            GLOBAL.berries.GetComponent<AIController>()
                .ChangeState(
                    new Resting());

            GLOBAL.foods = new();
        }

        public override void OnLeave()
        {
            
        }

        public override void OnUpdate()
        {
            ProcessAIs();
        }



    }


}
