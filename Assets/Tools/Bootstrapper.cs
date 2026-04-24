using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Brain : MonoBehaviour
{
    public Berrygotchi.GameplayState currentState;

    void Start()
    {
        GLOBAL.screenOut = GameObject.Find("Screen").GetComponent<SpriteRenderer>().material;
        GLOBAL.processableAIs = new();
        GLOBAL.berries = GameObject.Find("Berries");
        GLOBAL.berries.AddComponent<Berrygotchi.AIController>();
        GLOBAL.processableAIs.Add(GLOBAL.berries.GetComponent<Berrygotchi.AIController>());
        EnterState(new Berrygotchi.Idle());
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Actions.Add(
                Actions.PetBerries,
                0,
                0.2f,
                2,
                1,
                Actions.queue.Count);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Actions.Add(
                Actions.ThrowBerry,
                0,
                0.2f,
                0,
                0,
                Actions.queue.Count);
        }
    }

    void FixedUpdate()
    {
        currentState.OnUpdate();
        Actions.Execute();
        GLOBAL.screenOut.SetFloat("_FoodLevel", GLOBAL.statusHunger);
        GLOBAL.screenOut.SetFloat("_EnergyLevel", GLOBAL.statusEnergy);



    }

    public void EnterState(Berrygotchi.GameplayState s)
    {
        if(currentState is not null)
            currentState.OnLeave();
        currentState = s;
        currentState.OnEnter(this);
    }

}

public static class GLOBAL
{
    public const int resX = 160;
    public const int resY = 144;

    public const int floorMax = -20;
    public const int floorMIN = -60;
    public const int wallMax = 80;
    public const int wallMIN = -60;

    public static GameObject berries;
    public static List<Berrygotchi.AIController> processableAIs;
    public static List<GameObject> foods;
    public static Material screenOut;

    public const int MAX_ENERGY = 32;
    public const int MAX_HUNGER = 32;
    public static int statusEnergy = MAX_ENERGY;
    public static int statusHunger = MAX_HUNGER;


}
