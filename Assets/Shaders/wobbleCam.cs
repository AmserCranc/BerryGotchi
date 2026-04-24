using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class wobbleCam : MonoBehaviour
{
    public Material mat;

    [Range(0, 10)] public int amp = 0;    
    [Range(0, 10)] public int freq = 0;
    [Range(0, 10)] public int speed = 0;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if(mat is not null)
        {
            mat.SetFloat("_Amp", amp);
            mat.SetFloat("_Freq", freq);
            mat.SetFloat("_Speed", speed);

            Graphics.Blit(src, dest, mat);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }    
}
