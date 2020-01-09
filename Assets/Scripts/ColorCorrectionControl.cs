using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class ColorCorrectionControl : MonoBehaviour 
{
    [SerializeField]
	private Scripts scr;

	public float[] gDiffPlusSat;
	public float[] gDiffMinusSat;

	private ColorCorrectionCurves colCurvs;

	void Awake()
	{
		colCurvs = GetComponent<ColorCorrectionCurves>();
	}

	public void SetNewSaturation()
	{
		int diff;
		diff = Score.score - Score.score1;

		if (diff < 0)
		{
			if (diff <= -5)
				colCurvs.saturation = gDiffMinusSat[5];
			else
				colCurvs.saturation = gDiffMinusSat[-diff];
		}
		else
		{
			if (diff > 5)
				colCurvs.saturation = gDiffPlusSat[5];
			else
				colCurvs.saturation = gDiffPlusSat[diff];
		}
			
	}
}
