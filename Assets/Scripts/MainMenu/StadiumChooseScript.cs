using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StadiumChooseScript : MonoBehaviour 
{
	public GameObject[] sceneObjects;

	public GameObject blackBackground;
	public GameObject[] stadiums;
	public GameObject menuStadiums;
	public Transform environmentTr;

	private int stad = -1;

	void Awake()
	{
		for (int i = 0; i < stadiums.Length; i++)
		{
			if (i == 0)
				stadiums[i].SetActive(true);
			else 
				stadiums[i].SetActive(false);
		}
			
		DestroyImmediate(GameObject.Find("ChangeStadBut"));
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.S))
			SetStadiumHandle();
	}

	public void SetStadiumHandleByNumber(int number)
	{
		for (int i = 0; i < stadiums.Length; i++)
		{
			if (i == number)
				stadiums[i].SetActive(true);
			else 
				stadiums[i].SetActive(false);
		}
	}

	public void SetStadiumHandle()
	{
		stad++;

		if (stad > stadiums.Length - 1)
			stad = 0;
		
		for (int i = 0; i < stadiums.Length; i++)
		{
			if (i == stad)
				stadiums[i].SetActive(true);
			else 
				stadiums[i].SetActive(false);
		}
	}

	public void SetStadiumBackHandle()
	{
		stad--;

		if (stad <= 0)
			stad = stadiums.Length;
		
		for (int i = 0; i < stadiums.Length; i++)
		{
			if (i == stad)
				stadiums[i].SetActive(true);
			else
				stadiums[i].SetActive(false);
		}
	}

	public void SetBlackBackground()
	{
		for (int i = 0; i < sceneObjects.Length; i++)
			sceneObjects [i].SetActive (false);

		blackBackground.SetActive(true);
		GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
	}

	public void DisableBlackBackground()
	{
		for (int i = 0; i < sceneObjects.Length; i++)
			sceneObjects [i].SetActive (true);

		blackBackground.SetActive(false);
		GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
	}

	public void DestroyAllNotActiveStadiums()
	{
		#if UNITY_EDITOR
		for (int i = 0; i < stadiums.Length; i ++)
		{
			if (!stadiums[i].activeSelf)
			DestroyImmediate(stadiums[i]);
			else
			stadiums[i].transform.SetParent(environmentTr);
		}

		DestroyImmediate(blackBackground);
		DestroyImmediate(menuStadiums);
		#endif
	}
}


