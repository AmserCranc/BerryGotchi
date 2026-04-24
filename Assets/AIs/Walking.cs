using System.Collections;
using System.Collections.Generic;
using Berrygotchi;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Walking : Berrygotchi.AIController.AIState
{
    AIController owner;

    //Wandering
    float wanderSpeed = 5f;

    public override void OnEnter(AIController o)
    {
        owner = o;

        if(o.goalPos == null)
        {
            GameObject randomTarget = new();
            randomTarget.transform.position = new Vector3(
                                                    Random.Range(GLOBAL.wallMIN, GLOBAL.wallMax),
                                                    Random.Range(GLOBAL.floorMIN, GLOBAL.floorMax),
                                                    0);
            o.goalPos = randomTarget.transform;
        }
Debug.Log("Entered Walking");
    }

    public override void OnLeave()
    {
        
    }

    public override void OnUpdate()
    {
        Vector3 current = owner.transform.position;
        Vector2 next = Vector2.MoveTowards(current, owner.goalPos.position, wanderSpeed * Time.deltaTime);

        owner.GetComponentInChildren<SpriteRenderer>().flipX = owner.facingRight;
        owner.transform.position = next;

//pixel snapping
        Vector3 sPos = owner.transform.GetChild(0).transform.localPosition;
        owner.transform.GetChild(0).transform.localPosition = new Vector3(
                                                            Mathf.Round(sPos.x),
                                                            Mathf.Round(sPos.y),
                                                            sPos.z);
//Set facing direction of sprite
        owner.facingRight = owner.goalPos.position.x >= owner.transform.position.x
            ? true
            : false;

//If goal is reached
        if(Vector3.Distance(owner.transform.position, owner.goalPos.position ) < 3)
        {
            switch(owner.goalPos.tag)
            {
                case "food":
                    owner.ChangeState(new Eating());
                    return;

                default:
                    GameObject.Destroy(owner.goalPos.gameObject);
                    owner.goalPos = null;
                    owner.ChangeState(new Resting());
                    return;
            }
            
        }    
            
    }
}
