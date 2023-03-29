using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlagSampleButton : MonoBehaviour 
{
	public Image flagIm;
	public Text countryName;

	void Start()
	{
		transform.localScale = new Vector3 (1, 1, 1);
	}
}

