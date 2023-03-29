using UnityEngine;
using System.Collections;

public class LandObjectsScript : MonoBehaviour 
{
	public int varA, varB;

	public Sprite treeNoWindSpr, treeWind1Spr;
	public SpriteRenderer[] trees, trees1;
	public Color color1;
	private float newH1;
	public int timeMultiply100;

	void Awake()
	{
		float _float1 = 0.0f;

		Color.RGBToHSV (color1, out newH1, out _float1, out _float1);

		for (int i = 0; i < trees.Length; i++) 
		{
			float rand = Random.value;

			trees [i].color = Color.HSVToRGB (
				newH1,
				0.3f + 0.3f * Random.value,
				0.3f + 0.5f * Random.value);

			trees [i].color = new Vector4 (
				trees [i].color.r,
				trees [i].color.g,
				trees [i].color.b,
				1);

			trees [i].transform.localScale = new Vector3 (0.5f + rand * 0.7f, 0.5f + rand * 0.7f, 1);
		}

		for (int i = 0; i < trees1.Length; i++) 
		{
			float rand = Random.value;

			trees1 [i].color = Color.HSVToRGB (
				newH1,
				0.3f + 0.3f * Random.value,
				0.3f + 0.5f * Random.value);

			trees1 [i].color = new Vector4 (
				trees1 [i].color.r,
				trees1 [i].color.g,
				trees1 [i].color.b,
				1);

			trees1 [i].transform.localScale = new Vector3 (0.5f + rand * 0.7f, 0.5f + rand * 0.7f, 1);
		}
	}
}
