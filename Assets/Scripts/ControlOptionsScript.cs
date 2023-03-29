using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlOptionsScript : MonoBehaviour 
{
	public Scripts scr;

	private int[] targetFrameRate = new int[4];
	public Toggle[] graphicToggles;

	[Header("Scene 'MainMenu' objects:")]
	public Color colPushed; 
	public Color colReleased;

	public GameObject hintButton;
	public Image button1, button2;

	[Header("Scene 'Level' objects:")]
	public GameObject[] standartControl;
	public GameObject[] advancedControl;
		
	private void SetFrameRateAsScreen()
	{
		targetFrameRate [0] = Screen.currentResolution.refreshRate;	
		targetFrameRate [1] = 2 * Screen.currentResolution.refreshRate;	
		targetFrameRate [2] = -1;		
	}

	void Awake() 
	{
		Application.targetFrameRate = -1;

		if (PlayerPrefs.GetInt("Control") == 0) 
			SetControl_0(); 
		else if (PlayerPrefs.GetInt("Control") == 1) 
			SetControl_1();
	}	


	public void SetControl_0()
	{
		PlayerPrefs.SetInt("Control", 0);
		button1.color = colPushed;
		button2.color = colReleased;

		if (SceneManager.GetActiveScene().name == "Level")
		{
			for (int i = 0; i < standartControl.Length; i++)
				standartControl[i].SetActive(true);

			for (int i = 0; i < advancedControl.Length; i++)
				advancedControl[i].SetActive(false);
			
			hintButton.SetActive(false);
		}
	}

	public void SetControl_1()
	{
		PlayerPrefs.SetInt("Control", 1);

		button1.color = colReleased;
		button2.color = colPushed;

		if (SceneManager.GetActiveScene().name == "Level")
		{
			for (int i = 0; i < standartControl.Length; i++)
				standartControl[i].SetActive(false);

			for (int i = 0; i < advancedControl.Length; i++)
				advancedControl[i].SetActive(true);
			
			hintButton.SetActive(true);
		}
	}

	public void SetLowQuality()
	{
		Debug.Log("Set Low Quality");
		Application.targetFrameRate = targetFrameRate[0];
		scr.alPrScr.graphicQuality = 0;
		scr.alPrScr.isChange0 = true;

		if (SceneManager.GetActiveScene().name == "Level")
		{
			if (scr.sunEmit != null)
			{
				for (int i = 0; i < scr.sunEmit.sunLightTr.Length; i++)
				{
					if (scr.sunEmit.sunLightTr [i] != null)
						scr.sunEmit.sunLightTr [i].gameObject.SetActive (false);
				}	
			}
		}
	}

	public void SetMediumQuality()
	{
		Debug.Log("Set Medium Quality");
		Application.targetFrameRate = targetFrameRate[1];
		scr.alPrScr.graphicQuality = 1;
		scr.alPrScr.isChange0 = true;

		if (SceneManager.GetActiveScene().name == "Level")
		{
			if (scr.sunEmit != null)
			{
				for (int i = 0; i < scr.sunEmit.sunLightTr.Length; i++)
				{
					if (scr.sunEmit.sunLightTr [i] != null)
						scr.sunEmit.sunLightTr [i].gameObject.SetActive (false);
				}
			}
		}

		#if UNITY_EDITOR
		//Application.targetFrameRate = 10000;
		#endif
	}

	public void SetHighQuality()
	{
		Debug.Log("Set High Quality");
		Application.targetFrameRate = targetFrameRate[2];
		scr.alPrScr.graphicQuality = 2;
		scr.alPrScr.isChange0 = true;

		if (SceneManager.GetActiveScene().name == "Level")
		{
			if (scr.sunEmit != null)
			{
				for (int i = 0; i < scr.sunEmit.sunLightTr.Length; i++)
				{
					if (scr.sunEmit.sunLightTr [i] != null)
						scr.sunEmit.sunLightTr [i].gameObject.SetActive (true);
				}
			}
		}

		#if UNITY_EDITOR
		//Application.targetFrameRate = 10000;
		#endif
	}
}
