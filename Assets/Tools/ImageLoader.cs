using System.IO;
using UnityEngine;

public static class ImageLoader
{
    public static Texture2D LoadPNG(string path)
    {
        byte[] data = File.ReadAllBytes(path);

        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(data); 

        return tex;
    }
}