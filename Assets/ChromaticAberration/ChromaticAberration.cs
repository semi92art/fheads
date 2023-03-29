using UnityEngine;

[ExecuteInEditMode]
public class ChromaticAberration : MonoBehaviour
{
    [Range(-10, 10)]
    public float RedX = 0;
    [Range(-10, 10)]
    public float RedY = 0;
    [Range(-10, 10)]
    public float GreenX = 0;
    [Range(-10, 10)]
    public float GreenY = 0;
    [Range(-10, 10)]
    public float BlueX = 0;
    [Range(-10, 10)]
    public float BlueY = 0;
    [Range(-1,1)]
    public float FishEyeDistortion;

    static readonly int redXString = Shader.PropertyToID("_RedX");
    static readonly int redYString = Shader.PropertyToID("_RedY");
    static readonly int greenXString = Shader.PropertyToID("_GreenX");
    static readonly int greenYString = Shader.PropertyToID("_GreenY");
    static readonly int blueXString = Shader.PropertyToID("_BlueX");
    static readonly int blueYString = Shader.PropertyToID("_BlueY");
    static readonly int distortionString = Shader.PropertyToID("_Distortion");


    public Material material;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat(redXString, RedX);
        material.SetFloat(redYString, RedY);
        material.SetFloat(greenXString, GreenX);
        material.SetFloat(greenYString, GreenY);
        material.SetFloat(blueXString, BlueX);
        material.SetFloat(blueYString, BlueY);
        material.SetFloat(distortionString, FishEyeDistortion);
        Graphics.Blit(source, destination, material, 0);
    }
}
