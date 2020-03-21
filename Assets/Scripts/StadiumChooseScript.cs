using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StadiumChooseScript : MonoBehaviour 
{
    [SerializeField]
	private Scripts scr;

	public GameObject backgr;
	public Transform envTr;

	public GameObject[] sceneObjects;
	public GameObject[] stadiums;

	private int stad = -1;

	void Awake()
	{
		for (int i = 0; i < stadiums.Length; i++)
			stadiums[i].SetActive(i == PrefsManager.Instance.Stadium);
	}
  
	public void SetStadiumHandleByNumber(int number)
	{
		for (int i = 0; i < stadiums.Length; i++)
			stadiums[i].SetActive(i == number);
	}

	public void SetStadiumHandle()
	{
		stad++;

		if (stad > stadiums.Length - 1)
			stad = 0;
		
        for (int i = 0; i < stadiums.Length; i++)
        {           
            bool isActive = i == stad ? true : false;
            stadiums[i].SetActive(isActive);
        }

		Debug.Log ("Stadium = " + stad.ToString());
	}

	public void SetStadiumBackHandle()
	{
		stad--;

		if (stad <= 0)
			stad = stadiums.Length;
		
		for (int i = 0; i < stadiums.Length; i++)
		{			
            bool isActive = i == stad ? true : false;
            stadiums[i].SetActive(isActive);
		}

		Debug.Log ("Stadium = " + stad.ToString());
	}

	public void SetBlackBackground()
	{
		for (int i = 0; i < sceneObjects.Length; i++)
			sceneObjects [i].SetActive (false);

		backgr.SetActive(true);
	}

	public void DisableBlackBackground()
	{
		for (int i = 0; i < sceneObjects.Length; i++)
			sceneObjects [i].SetActive (true);

		backgr.SetActive(false);
	}

	public void DestroyAllNotActiveStadiums()
	{
		#if UNITY_EDITOR
		for (int i = 0; i < stadiums.Length; i ++)
		{
			if (!stadiums[i].activeSelf)
			DestroyImmediate(stadiums[i]);
			else
			stadiums[i].transform.SetParent(envTr);
		}

		DestroyImmediate(backgr);
		#endif
	}
}


