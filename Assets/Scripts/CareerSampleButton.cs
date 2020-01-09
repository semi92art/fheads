using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CareerSampleButton : MonoBehaviour
{
    public Color col_0, col_1;

	public Image 
	showcase, 
	plIm,
	legIm,
	stIm;

    public Image[] starsIm;

	public Text 
	awardWord, 
	awardCount, 
	plText, 
	stadText;


	void Start()
	{
		transform.localScale = new Vector3 (1, 1, 1);
	}
}
