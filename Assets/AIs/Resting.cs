using System;
using System.Collections;
using System.Collections.Generic;
using Berrygotchi;
using Unity.VisualScripting;
using UnityEngine;

public class Resting : Berrygotchi.AIController.AIState
{
    AIController owner;
    uint lifeTime;
    uint ticks;
    uint wanderChance = 1; //Denominator
    uint wanderFreq = 128;
    uint sleepFreq = 1024;

    Vector2 goalPos;


    uint callsPerTick = 4;

//Bobbing
    uint bobFreq = 16;
    int bobHeight = 1;
    int stepToggle = 0;


//Sleeping
    uint sleepChance = 256;


    public override void OnEnter(AIController o)
    {
        owner = o;
        lifeTime = 0;
Debug.Log("Entered Resting");
    }

    public override void OnLeave()
    {
        
    }

    public override void OnUpdate()
    {
        lifeTime++;
        Debug.Log("Berries tick");

//Bobbing animation
        stepToggle ^= ((lifeTime % bobFreq) == 0) ? 1 : 0;
        owner.transform.GetChild(0).localPosition = new Vector3(0, stepToggle, 0);

//Tick brain
        if(lifeTime % callsPerTick != 0)
            return;
        
//If can see food
        if(GLOBAL.foods.Count > 0)
        {
            float shortestDistance = Int32.MaxValue;
            int nearestFoodIDX = 0;

            for (int i = 0; i < GLOBAL.foods.Count; i++)
            {
                float distance = (GLOBAL.foods[i].transform.position - owner.transform.position).sqrMagnitude;

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestFoodIDX = i;
                }
            }

            owner.GetComponent<AIController>().goalPos = GLOBAL.foods[nearestFoodIDX].transform;
            owner.GetComponent<AIController>().ChangeState(new Walking());
            return;
        }
            
//Wandering behaviour
        if(ticks % wanderFreq == 0)
        {
            owner.GetComponent<AIController>().ChangeState(new Walking());
            return;
        }
//Sleeping behaviour
        if(ticks % sleepFreq == 0)
        {
            owner.GetComponent<AIController>().ChangeState(new Sleeping());
            return;
        }
        ticks++;
    }

}
