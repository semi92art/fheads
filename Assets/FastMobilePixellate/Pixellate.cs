using UnityEngine;

[ExecuteInEditMode]
public class Pixellate : MonoBehaviour
{
    [Range(0, 1)]
    [Tooltip("Pixel Dencity")]
    public float Dencity = 0.0f;

    public Material material;

    static readonly int dencityString = Shader.PropertyToID("_Dencity");

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(Dencity>0f)
        {
            Vector2 data = (Screen.width > Screen.height ? new Vector2((float)Screen.width / Screen.height, 1) : new Vector2(1, (float)Screen.height / Screen.width)) * (150 - Dencity * 140);
            material.SetVector(dencityString, data);
            Graphics.Blit(source, destination, material, 0);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}