using UnityEngine;

public class FastGlitch : MonoBehaviour
{
    public Material material;
    [Range(0,1)]
    public float ChromaticGlitch;
    [Range(0, 1)]
    public float FrameGlitch;
    [Range(0, 2)]
    public float PixelGlitch;

    private Vector2 rand1 = new Vector2(5.0f, 1.0f);
    private Vector2 rand2 = new Vector2(31.0f, 1.0f);
    private Vector2 randMul2 = new Vector2(127.1f, 311.7f);
    static readonly int amountString = Shader.PropertyToID("_Amount");
    static readonly int frameString = Shader.PropertyToID("_Frame");
    static readonly int pixelString = Shader.PropertyToID("_Pixel");
    static readonly string frameKeyword = "FRAME";
    static readonly string pixelKeyword = "PIXEL";
    private float t, a, b, c;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        t = Time.realtimeSinceStartup;
        if (ChromaticGlitch != 0)
        {
            a = (1.0f + Mathf.Sin(t * 6.0f)) * ((0.5f + Mathf.Sin(t * 16.0f) * 0.25f)) * (0.5f + Mathf.Sin(t * 19.0f) * 0.25f) * (0.5f + Mathf.Sin(t * 27.0f) * 0.25f);
            material.SetFloat(amountString, ChromaticGlitch * Mathf.Pow(a, 3.0f) * 0.5f);
        }
        else
            material.SetFloat(amountString, 0f);

        if (FrameGlitch != 0)
        {
            material.SetFloat(frameString, (1.0f + Mathf.Cos(t * 80.0f)) * 0.02f * FrameGlitch);
            material.EnableKeyword(frameKeyword);
        }
        else
            material.DisableKeyword(frameKeyword);

        if (PixelGlitch != 0)
        {
            b = Mathf.Sin(Vector2.Dot(rand1 * Mathf.Floor(t * 12.0f), randMul2)) * 43758.5453123f;
            c = Mathf.Sin(Vector2.Dot(rand2 * Mathf.Floor(t * 12.0f), randMul2)) * 43758.5453123f;
            material.SetVector(pixelString, new Vector2((b - Mathf.Floor(b)) * PixelGlitch * 0.1f, (c - Mathf.Floor(c)) * PixelGlitch * 0.1f));
            material.EnableKeyword(pixelKeyword);
        }
        else
            material.DisableKeyword(pixelKeyword);

        Graphics.Blit(source, destination, material);
    }
}
