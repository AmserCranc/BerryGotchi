using System.Collections.Generic;
using UnityEngine;

static public class AssetLibrary
{
    static public Dictionary<
                    string, Dictionary<
                        string, GameObject>> dictionaries = new();

    static public GameObject LoadSpriteResource(string _dictionary, string _name)
    {
        if(!dictionaries.ContainsKey(_dictionary))
            dictionaries.Add(_dictionary, new Dictionary<string, GameObject>());

        if(dictionaries[_dictionary].ContainsKey(_name))
            return dictionaries[_dictionary][_name];
        
        GameObject asset = new(_name);
        SpriteRenderer asset_r = asset.AddComponent<SpriteRenderer>();
        asset.transform.position = new Vector3(100, 100, 0);
        asset_r.sprite = Resources.Load<Sprite>(_name);
Debug.Log(asset_r.sprite == null ? $"couldn't load {_name}" : $"{_name} loaded");
        dictionaries[_dictionary].Add(_name, asset);

        return asset;
    }

//Backgrounds
    static public string BG_1 = "back";
    static public string MG_1 = "mid";
    static public string FG_1 = "front";

//Foods
    static public string berry = "berry";
    static public string carrot = "carrot";
    static public string mush_spooky = "mush_spooky";
    static public string mush_wonk = "mush_wonk";
    static public string mush1 = "mush1";
    static public string mush2 = "mush2";

//Particle
    static public string heart = "heart";
    static public string hand = "hand";
    static public string sparkle = "sparkle";
}