using System.Collections;
using System.Collections.Generic;
using Berrygotchi;
using Unity.VisualScripting;
using UnityEngine;

public class Eating : Berrygotchi.AIController.AIState
{
    AIController owner;
    GameObject sparkle;
    float eatingTime = 2f;


    public override void OnEnter(AIController o)
    {
        owner = o;
Debug.Log("Entered Eating");

        sparkle = new();
        SpriteRenderer sparkle_r = sparkle.AddComponent<SpriteRenderer>();
        sparkle.transform.position = new Vector3(100, 100, 0);
        sparkle_r.sprite = Resources.Load<Sprite>("sparkle");
Debug.Log(sparkle_r.sprite == null ? "couldn't load sparkle" : "sparkle loaded");

        
    }

    public override void OnLeave()
    {
        GameObject.Destroy(owner.goalPos.gameObject);
        GLOBAL.foods.Remove(owner.goalPos.gameObject);
        GLOBAL.berries.GetComponent<MonoBehaviour>().StartCoroutine(Sparkle());
        GameObject.Destroy(sparkle);

        if(owner.goalPos.gameObject.name == AssetLibrary.mush_wonk + "(Clone)")
            Actions.Add(
                Actions.WobbleScreen,
                1,
                6.0f,
                7,
                2,
                Actions.queue.Count);

        if(GLOBAL.statusEnergy < (GLOBAL.MAX_ENERGY - 5))
            GLOBAL.statusEnergy += 5;

    }

    public override void OnUpdate()
    {
        eatingTime -= Time.deltaTime;

        if(eatingTime <= 0)
            owner.ChangeState(new Resting());
    }

    private IEnumerator Sparkle()
    {
        GameObject p1 = GameObject.Instantiate(sparkle);
        GameObject p2 = GameObject.Instantiate(sparkle);
        GameObject p3 = GameObject.Instantiate(sparkle);

        Vector3 rootPos = GLOBAL.berries.transform.position;

        p1.transform.position = rootPos + new Vector3(1, 6, 0);
        p2.transform.position = rootPos + new Vector3(-2, 4, 0);
        p3.transform.position = rootPos + new Vector3(3, 3, 0);

        for(int frames = 4; frames > 0; frames--)
        {
            p1.transform.position += new Vector3(1, 6, 0);
            p2.transform.position += new Vector3(-2, 4, 0);
            p3.transform.position += new Vector3(3, 3, 0);
            yield return new WaitForSeconds(0.2f);
        }

        GameObject.Destroy(p1);
        GameObject.Destroy(p2);
        GameObject.Destroy(p3);
        
    }
}
