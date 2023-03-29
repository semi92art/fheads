using UnityEngine;
using System.Collections;

public class SkyScript : MonoBehaviour 
{
	public Scripts scr;
	public float[] sunBeamScale;
	public SpriteRenderer stadiumSprR;
	public Sprite[] stadiumSpr;
	public bool isTraining;
	public int currentSky;
	public Color[] lineRColors;
	public Color[] cameraColors;
	public Color[] sunColors;
	public Color[] planeColors;
	public Color[] planeTraceColors;
	public Color[] cloudsColors;
	public Material[] sunMaterials;
	[HideInInspector]
	public Color currPlaneTraceColor;

	[HideInInspector]
	public bool isPlaneDestroyed;
	public int stad_ind;

	void Awake()
	{
		if (scr.alPrScr.freePlay == 0)
		{
			float rand1 = Random.value;
			int rand1_int = Mathf.FloorToInt(rand1 * 2.9f) + 1;
			stadiumSprR.sprite = stadiumSpr[rand1_int];
			Debug.Log("rand1_int = " + rand1_int);
			stad_ind = rand1_int;
		}
		else
			stadiumSprR.sprite = stadiumSpr [0];

		if (scr.sunEmit != null)
		{
			if (scr.sunEmit.xSun <= -10)
				currentSky = 2;
			else if (scr.sunEmit.xSun > -10 && scr.sunEmit.xSun <= 5)
				currentSky = 1;
			else
				currentSky = 0;
		}
			
		if (scr.alPrScr.freePlay == 0)
			isTraining = false;
		else
			isTraining = true;
	}

	void Update()
	{
		#if UNITY_EDITOR
		if (Input.GetKeyDown (KeyCode.Q))
			ChangeSkyAndStadiumColor ();

		if (Input.GetKeyDown (KeyCode.E))
			ChangeStadium ();
		#endif
	}

	private void ChangeSkyAndStadiumColor()
	{
		if (currentSky != sunColors.Length - 1)
			currentSky++;
		else
			currentSky = 0;

		// for (int i = 0; i < scr.sunEmit.sunLightTr.Length; i++) 
		// 	scr.sunEmit.sunLightTr [i].gameObject.GetComponent<DynamicLight> ().LightMaterial = sunMaterials [currentSky];
	}

	private void ChangeStadium()
	{
	}

	public void SetNextSky()
	{
		if (currentSky != sunColors.Length - 1)
			currentSky++;
		else
			currentSky = 0;

		currPlaneTraceColor = planeTraceColors [currentSky];

		// for (int i = 0; i < scr.sunEmit.sunLightTr.Length; i++) 
		// 	scr.sunEmit.sunLightTr [i].gameObject.GetComponent<DynamicLight> ().LightMaterial = sunMaterials [currentSky];
	}

	public void EnableDisableStadium()
	{

	}
}
