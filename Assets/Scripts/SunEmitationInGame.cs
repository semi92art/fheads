using UnityEngine;
using System.Collections;

public class SunEmitationInGame : MonoBehaviour 
{
	public Scripts scr;

	public GameObject sampleLight;
	public Transform[] sunLightTr;
	public bool useLights;
	private float step;

	public float xSun0, ySun0;
	public float rSun;
	public float xSun, ySun;

	/*
	void Awake()
	{
		if (scr.alPrScr.freePlay == 0)
			xSun = -20 + 35 * Random.value;
		else
		{
			if (scr.alPrScr.timeOfDay == 0)
				xSun = -15;
			else if (scr.alPrScr.timeOfDay == 1)
				xSun = -9;
			else if (scr.alPrScr.timeOfDay == 2)
				xSun = 6;
		}
			
		ySun = Mathf.Sqrt (rSun * rSun - xSun * xSun);
		transform.position = new Vector3 (xSun + xSun0, ySun + ySun0, transform.position.z);
	}

	
	void Start()
	{
		if (useLights)
		{
			for (int i = 0; i < sunLightTr.Length; i++) 
			{
				sunLightTr [i].position = new Vector3 (
					xSun + xSun0 - i * 0,
					sunLightTr [i].position.y, 
					sunLightTr [i].position.z);
			}
		}
		else
		{
			for (int i = 0; i < sunLightTr.Length; i++) 
				sunLightTr [i].gameObject.SetActive(false);
		}
	}

	void Update()
	{
		step = (float)step0 / 10000;

		xSun += step;
		float ySunPow2 = rSun * rSun - xSun * xSun;

		//if (ySunPow2 >= 0)
		ySun = Mathf.Sqrt(rSun * rSun - xSun * xSun);

		newSunTr.position = new Vector3 (xSun + xSun0, ySun + ySun0, newSunTr.position.z);

		if (useLights)
		{
			for (int i = 0; i < sunLightTr.Length; i++)
				sunLightTr[i].position = new Vector3 (
					xSun + xSun0 - i, 
					sunLightTr[i].position.y, 
					sunLightTr[i].position.z);
		}
	}
	*/

	public void InstantiateLightObject(int count)
	{
		// if (useLights)
		// {
		// 	for (int j = 0; j < count; j++)
		// 	{
		// 		if (j < sunLightTr.Length)
		// 		{
		// 			GameObject sampleLight_1 = Instantiate (sampleLight) as GameObject;
		//
		// 			for (int i = 0; i < sunLightTr.Length; i++) 
		// 			{
		// 				if (sunLightTr [i] == null)
		// 				{
		// 					sunLightTr [i] = sampleLight_1.transform;
		// 					sunLightTr [i].position = new Vector3 (
		// 						transform.position.x,
		// 						transform.position.y + 40.0f,
		// 						0);
		// 					sunLightTr [i].gameObject.GetComponent<DynamicLight> ().LightMaterial =
		// 						scr.skyScr.sunMaterials [scr.skyScr.currentSky];
		// 					break;
		// 				}
		// 			}
		// 		}
		// 	}
		//
		// 	if (scr.alPrScr.graphicQuality == 0)
		// 		scr.controlOptScr.SetLowQuality ();
		// 	else if (scr.alPrScr.graphicQuality == 1)
		// 		scr.controlOptScr.SetMediumQuality ();
		// 	else if (scr.alPrScr.graphicQuality == 2)
		// 		scr.controlOptScr.SetHighQuality ();
		// }
	}
}
