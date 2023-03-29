using UnityEngine;

[ExecuteInEditMode]
public class ColorGrading : MonoBehaviour
{
    public Color Color = Color.white;
    [Range(-180, 180)]
    public int Hue = 0;
    [Range(0, 1)]
    public float Contrast = 0f;
    [Range(-1, 1)]
    public float Brightness = 0f;
    [Range(-1, 1)]
    public float Saturation = 0f;
    [Range(-1, 1)]
    public float Exposure = 0f;
    [Range(-1, 1)]
    public float Gamma = 0f;
    [Range(0, 1)]
    public float Sharpness = 0f;
    [Range(0, 1)]
    public float Blur = 0f;
    public Texture2D BlurMask;
    public Color VignetteColor = Color.black;
    [Range(0, 1)]
    public float VignetteAmount = 0f;
    [Range(0.001f, 1)]
    public float VignetteSoftness = 0.0001f;

    public Material material;
    static readonly int color = Shader.PropertyToID("_Color");
    static readonly int hueCos = Shader.PropertyToID("_HueCos");
    static readonly int hueSin = Shader.PropertyToID("_HueSin");
    static readonly int hueVector = Shader.PropertyToID("_HueVector");
    static readonly int contrast = Shader.PropertyToID("_Contrast");
    static readonly int brightness = Shader.PropertyToID("_Brightness");
    static readonly int saturation = Shader.PropertyToID("_Saturation");
    static readonly int centralFactor = Shader.PropertyToID("_CentralFactor");
    static readonly int sideFactor = Shader.PropertyToID("_SideFactor");
    static readonly int blur = Shader.PropertyToID("_Blur");
    static readonly int blurMask = Shader.PropertyToID("_MaskTex");
    static readonly int vignetteColorString = Shader.PropertyToID("_VignetteColor");
    static readonly int vignetteAmountString = Shader.PropertyToID("_VignetteAmount");
    static readonly int vignetteSoftnessString = Shader.PropertyToID("_VignetteSoftness");

    static readonly string blurKeyword = "BLUR";
    static readonly string shaprenKeyword = "SHARPEN";
    static readonly string vignetteKeyword = "VIGNETTE";

    float cos;

    private void Start()
    {
        if (BlurMask == null)
        {
            Shader.SetGlobalTexture(blurMask, Texture2D.whiteTexture);
        }
        else
        {
            Shader.SetGlobalTexture(blurMask, BlurMask);
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetColor(color, (Mathf.Pow(2, Exposure) - Gamma) * Color);
        cos = Mathf.Cos(Mathf.Deg2Rad * Hue);
        material.SetFloat(hueCos, cos);
        material.SetFloat(hueSin, Mathf.Sin(Mathf.Deg2Rad * Hue));
        cos = 0.57735f * (1 - cos);
        material.SetVector(hueVector, new Vector3(cos, cos, cos));
        material.SetFloat(contrast, Contrast + 1f);
        material.SetFloat(brightness, Brightness * 0.5f + 0.5f);
        material.SetFloat(saturation, Saturation + 1f);

        if (Blur > 0)
        {
            material.EnableKeyword(blurKeyword);
            material.SetFloat(blur, Blur);
        }
        else
        {
            material.DisableKeyword(blurKeyword);
        }

        if (Sharpness > 0)
        {
            material.EnableKeyword(shaprenKeyword);
            material.SetFloat(centralFactor, 1.0f + (3.2f * Sharpness));
            material.SetFloat(sideFactor, 0.8f * Sharpness);
        }
        else
        {
            material.DisableKeyword(shaprenKeyword);
        }

        if (VignetteAmount > 0)
        {
            material.EnableKeyword(vignetteKeyword);
            material.SetColor(vignetteColorString, VignetteColor);
            material.SetFloat(vignetteAmountString, 1 - VignetteAmount);
            material.SetFloat(vignetteSoftnessString, 1 - VignetteSoftness - VignetteAmount);
        }
        else
        {
            material.DisableKeyword(vignetteKeyword);
            material.SetFloat(vignetteAmountString, 1f);
            material.SetFloat(vignetteSoftnessString, 0.999f);
        }

        Graphics.Blit(source, destination, material, 0);
    }
}
