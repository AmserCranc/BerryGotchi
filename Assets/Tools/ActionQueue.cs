using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Berrygotchi;
using Unity.VisualScripting;
using UnityEngine;

public delegate IEnumerator Action(int w, float x, byte y, byte z, int index);

static public class Actions
{
    public struct ActionData 
    {
        public int w; 
        public float x; 
        public byte y; 
        public byte z;
    }
    static public List<Action> queue = new();
    static public List<ActionData> data = new();

    static public void Execute()
    {
        for(int action = 0; action < queue.Count; action++)
        {
            var method = queue[action](
                data[action].w, 
                data[action].x,
                data[action].y,
                data[action].z,
                action);
            GLOBAL.berries.GetComponent<MonoBehaviour>().StartCoroutine(method);

            queue.RemoveAt(action);
            data.RemoveAt(action);
        }
    }

    static public void Add(Action _a, int _w, float _x, byte _y, byte _z, int index)
    {
        ActionData d = new();
        d.w = _w;
        d.x = _x;
        d.y = _y;
        d.z = _z;

        queue.Add(_a);
        data.Add(d);

        Debug.Log($"{_a} added to Action Queue");
    }

    static public IEnumerator ChangeScreenColour(int w, float x, byte y, byte z, int index)
    {
        Berrygotchi.GameplayState brain = GameObject.Find("Brain").GetComponent<GameplayState>();

        brain.mat.SetColor("_OBJ0_Col1", Palettes.library[w][0]);
        brain.mat.SetColor("_OBJ0_Col2", Palettes.library[w][1]);

        brain.mat.SetColor("_OBJ1_Col1", Palettes.library[w][2]);
        brain.mat.SetColor("_OBJ1_Col2", Palettes.library[w][3]);

        brain.mat.SetColor("_BG0_Col1", Palettes.library[w][4]);
        brain.mat.SetColor("_BG0_Col2", Palettes.library[w][5]);
        brain.mat.SetColor("_BG0_Col3", Palettes.library[w][6]);

        yield return null;
       
    }

    static public IEnumerator FlipScreen(int w, float x, byte y, byte z, int index)
    {

        yield return null;
    }

    static public IEnumerator WobbleScreen(int w, float x, byte y, byte z, int index)
    {
        wobbleCam wobble = Camera.main.GetComponent<wobbleCam>();
        wobble.amp = w;
        wobble.freq = y;
        wobble.speed = z;

        for(float time = x; time > 0;)
        {
            time -= Time.deltaTime;
            yield return null;
        }

        wobble.amp = 0;
        wobble.freq = 0;
        wobble.speed = 0;
    }

    static public IEnumerator PetBerries(int w, float x, byte y, byte z, int index)
    {
        Debug.Log("petting Berries");

        GLOBAL.statusEnergy -= 1;

        GameObject hand = new();
        GameObject heart = new();
        SpriteRenderer hand_r = hand.AddComponent<SpriteRenderer>();
        SpriteRenderer heart_r = heart.AddComponent<SpriteRenderer>();
        hand.transform.position = GLOBAL.berries.transform.position + new Vector3(-100, -100, 0);
        heart.transform.position = GLOBAL.berries.transform.position + new Vector3(-100, -100, 0);
        hand_r.sprite = Resources.Load<Sprite>("hand");
Debug.Log(hand_r.sprite == null ? "couldn't load hand" : "hand loaded");
        heart_r.sprite = Resources.Load<Sprite>("heart");
Debug.Log(hand_r.sprite == null ? "couldn't load heart" : "heart loaded");

//Animate
        hand.transform.position = GLOBAL.berries.transform.position + new Vector3(0, 8, 0);
        yield return new WaitForSeconds(x);

        hand.transform.position = GLOBAL.berries.transform.position + new Vector3(0, 8, 0);
        hand.transform.position += new Vector3(y, 0, 0);
        yield return new WaitForSeconds(x);

        hand.transform.position = GLOBAL.berries.transform.position + new Vector3(0, 8, 0);
        hand.transform.position += new Vector3(-y, 0, 0);
        yield return new WaitForSeconds(x);

        hand.transform.position = GLOBAL.berries.transform.position + new Vector3(0, 8, 0);
        hand.transform.position += new Vector3(-y, 0, 0);
        heart.transform.position = GLOBAL.berries.transform.position + new Vector3(0, 16, 0);
        yield return new WaitForSeconds(x);

        hand.transform.position = GLOBAL.berries.transform.position + new Vector3(0, 8, 0);
        hand.transform.position += new Vector3(y, 0, 0);
        heart.transform.position += new Vector3(y, y, 0);
        yield return new WaitForSeconds(x);

        hand.transform.position = GLOBAL.berries.transform.position + new Vector3(0, 8, 0);
        hand.transform.position += new Vector3(y, 0, 0);
        heart.transform.position += new Vector3(-y, y, 0);
        yield return new WaitForSeconds(x);

        GameObject.Destroy(hand);
        heart.transform.position += new Vector3(y, y, 0);
        yield return new WaitForSeconds(x);

        heart.transform.position += new Vector3(-y, y, 0);
        yield return new WaitForSeconds(x);

        GameObject.Destroy(heart); 
    }

    static public IEnumerator ThrowBerry(int w, float x, byte y, byte z, int index)
    {
        Debug.Log("throwing berry");

        AssetLibrary.LoadSpriteResource("foods", AssetLibrary.berry);
        AssetLibrary.LoadSpriteResource("foods", AssetLibrary.carrot);
        AssetLibrary.LoadSpriteResource("foods", AssetLibrary.mush1);
        AssetLibrary.LoadSpriteResource("foods", AssetLibrary.mush2);
        AssetLibrary.LoadSpriteResource("foods", AssetLibrary.mush_spooky);
        AssetLibrary.LoadSpriteResource("foods", AssetLibrary.mush_wonk);

        int random = Random.Range(0, 6);
        GameObject selected = AssetLibrary.dictionaries["foods"].ElementAt(random).Value;
        GameObject instance = GameObject.Instantiate(selected);
        instance.tag = "food";

        GLOBAL.foods.Add(instance);

        instance.transform.position = new Vector3(
                                        Random.Range(-50, 50), 
                                        75, 
                                        0);

        int landY = Random.Range(GLOBAL.floorMIN, GLOBAL.floorMax);

        for(int i = 75; i > landY; i -= 5)
        {
            instance.transform.position -= new Vector3(0, 5, 0);
            instance.GetComponent<SpriteRenderer>().flipX = (i % 3 == 0);
            instance.GetComponent<SpriteRenderer>().flipY = (i % 2 == 0);
            yield return new WaitForSeconds(x);
        }
    }    
}

static public class Palettes
{
    public static readonly Dictionary<int, Color[]> library = new()
    {
        {
            0x12, new Color[]
            {
                new Color32(255, 175, 99, 255),
                new Color32(132, 45, 0, 255),

                new Color32(255, 175, 99, 255),
                new Color32(132, 45, 0, 255),

                new Color32(255, 175, 99, 255),
                new Color32(132, 45, 0, 255),
                new Color32(0, 0, 0, 255)
            }
        },

        {
            0xB0, new Color[]
            {
                new Color32(124, 255, 44, 255),
                new Color32(0, 132, 0, 255),

                new Color32(101, 166, 156, 255),
                new Color32(0, 0, 254, 255),

                new Color32(255, 134, 133, 255),
                new Color32(149, 55, 55, 255),
                new Color32(0, 0, 0, 255)
            }
        },

        {
            0x05, new Color[]
            {
                new Color32(80, 255, 0, 255),
                new Color32(255, 64, 0, 255),

                new Color32(80, 255, 0, 255),
                new Color32(255, 64, 0, 255),

                new Color32(80, 255, 0, 255),
                new Color32(255, 64, 0, 255),
                new Color32(0, 0, 0, 255)
            }
        },
    };
}

