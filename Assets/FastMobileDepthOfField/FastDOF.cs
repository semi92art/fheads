using UnityEngine;
using UnityEngine.XR;

[ExecuteInEditMode]
public class FastDOF : MonoBehaviour {
    public enum DepthMethod
    {
        CustomMaterials,
        Depth
    }
    public DepthMethod DepthCalculationMetod = DepthMethod.Depth;
    [Range(0, 1)]
	public float BlurAmount = 1f;
	public float Focus = 0f;
	public float Aperture = 1f;
    static readonly int blurAmountString = Shader.PropertyToID("_BlurAmount");
    static readonly int blurTexString = Shader.PropertyToID("_BlurTex");
    static readonly int focusAmountString = Shader.PropertyToID("_Focus");
	static readonly int apertureAmountString = Shader.PropertyToID("_Aperture");
    static readonly string isDepthKeyword = "ISDEPTH";
	public Material material=null;
    private int numberOfPasses = 3;
    RenderTextureDescriptor half, quarter, eighths, sixths;

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void OnDisable()
    {
        cam.depthTextureMode = DepthTextureMode.None;
    }

    void  OnRenderImage (RenderTexture source ,   RenderTexture destination){
        if (BlurAmount == 0)
        {
            Graphics.Blit(source, destination);
            return;
        }

        if (DepthCalculationMetod == DepthMethod.Depth && cam.depthTextureMode != DepthTextureMode.Depth)
        {
            cam.depthTextureMode = DepthTextureMode.Depth;
            material.EnableKeyword(isDepthKeyword);
        } 
        else if(DepthCalculationMetod == DepthMethod.CustomMaterials && cam.depthTextureMode == DepthTextureMode.Depth)
        {
            cam.depthTextureMode = DepthTextureMode.None;
            material.DisableKeyword(isDepthKeyword);
        }

        if (XRSettings.enabled)
        {
            half = XRSettings.eyeTextureDesc;
            half.height /= 2; half.width /= 2;
            quarter = XRSettings.eyeTextureDesc;
            quarter.height /= 4; quarter.width /= 4;
            eighths = XRSettings.eyeTextureDesc;
            eighths.height /= 8; eighths.width /= 8;
            sixths = XRSettings.eyeTextureDesc;
            sixths.height /= XRSettings.stereoRenderingMode == XRSettings.StereoRenderingMode.SinglePass ? 8 : 16; sixths.width /= XRSettings.stereoRenderingMode == XRSettings.StereoRenderingMode.SinglePass ? 8 : 16;
        }
        else
        {
            half = new RenderTextureDescriptor(Screen.width / 2, Screen.height / 2);
            quarter = new RenderTextureDescriptor(Screen.width / 4, Screen.height / 4);
            eighths = new RenderTextureDescriptor(Screen.width / 8, Screen.height / 8);
            sixths = new RenderTextureDescriptor(Screen.width / 16, Screen.height / 16);
            half.depthBufferBits = 0;
            quarter.depthBufferBits = 0;
            eighths.depthBufferBits = 0;
            sixths.depthBufferBits = 0;
        }

        Shader.SetGlobalFloat(focusAmountString, Focus);
		Shader.SetGlobalFloat(apertureAmountString, Aperture);
        numberOfPasses = Mathf.Max(Mathf.CeilToInt(BlurAmount * 4), 1);
        material.SetFloat(blurAmountString, numberOfPasses > 1 ? (BlurAmount * 4 - Mathf.FloorToInt(BlurAmount * 4 - 0.001f)) * 0.5f + 0.5f : BlurAmount * 4);

        RenderTexture blurTex = null;

        if (numberOfPasses == 1)
        {
            blurTex = RenderTexture.GetTemporary(half);
            blurTex.filterMode = FilterMode.Bilinear;
            Graphics.Blit(source, blurTex, material, 0);
        }
        else if (numberOfPasses == 2)
        {
            blurTex = RenderTexture.GetTemporary(half);
            var temp1 = RenderTexture.GetTemporary(quarter);
            blurTex.filterMode = FilterMode.Bilinear;
            temp1.filterMode = FilterMode.Bilinear;
            Graphics.Blit(source, temp1, material, 0);
            Graphics.Blit(temp1, blurTex, material, 0);
            RenderTexture.ReleaseTemporary(temp1);
        }
        else if (numberOfPasses == 3)
        {
            blurTex = RenderTexture.GetTemporary(quarter);
            var temp1 = RenderTexture.GetTemporary(eighths);
            blurTex.filterMode = FilterMode.Bilinear;
            temp1.filterMode = FilterMode.Bilinear;
            Graphics.Blit(source, blurTex, material, 0);
            Graphics.Blit(blurTex, temp1, material, 0);
            Graphics.Blit(temp1, blurTex, material, 0);
            RenderTexture.ReleaseTemporary(temp1);
        }
        else if (numberOfPasses == 4)
        {
            blurTex = RenderTexture.GetTemporary(quarter);
            var temp1 = RenderTexture.GetTemporary(eighths);
            var temp2 = RenderTexture.GetTemporary(sixths);
            blurTex.filterMode = FilterMode.Bilinear;
            temp1.filterMode = FilterMode.Bilinear;
            temp2.filterMode = FilterMode.Bilinear;
            Graphics.Blit(source, blurTex, material, 0);
            Graphics.Blit(blurTex, temp1, material, 0);
            Graphics.Blit(temp1, temp2, material, 0);
            Graphics.Blit(temp2, temp1, material, 0);
            Graphics.Blit(temp1, blurTex, material, 0);
            RenderTexture.ReleaseTemporary(temp1);
            RenderTexture.ReleaseTemporary(temp2);
        }

        material.SetTexture(blurTexString, blurTex);
        RenderTexture.ReleaseTemporary(blurTex);
        Graphics.Blit(source, destination, material, 1);
    }
}
