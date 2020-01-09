using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour
{
	void Awake()
	{
		ChooseLastObject("AllLevelScript");
		ChooseLastObject("MenuProfileMaterials");
	}

	void Start () 
	{
	
	}

	private void ChooseLastObject(string tag)
	{
		GameObject[] obj = GameObject.FindGameObjectsWithTag(tag);

		if (obj.Length >= 2)
		{
			for (int i = 1; i < obj.Length; i++) 
				DestroyImmediate(obj[i]);	
		}
	}
}
