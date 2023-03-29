using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hints : MonoBehaviour 
{
	public Scripts scr;

	public Color pushedCol, releasedCol;
	public Text hintButtonText;

	public GameObject startHelpText;
	public GameObject hintButton;

	public GameObject pauseOptPanel;
	public GameObject quitPanel;

	public bool isQuitPanel;

	
	void Awake()
	{
		Debug.Log("Control = " + PlayerPrefs.GetInt("Control"));

		//if (PlayerPrefs.GetInt("Control") == 0)
		//{
			//startHelpText.SetActive(false);
			//hintButton.SetActive(false);
		//}
		//else if (PlayerPrefs.GetInt("Control") == 1)
		//{
			startHelpText.SetActive(true);
			hintButton.SetActive(true);
		//}
	}


	public void TimeScale_0()
	{
		hintButtonText.color = pushedCol;

		if (!scr.gM.pauseInLevel)
		{
			if (!scr.gM.pauseOptions)
				Time.timeScale = 0;
			else
				pauseOptPanel.SetActive(false);
		}
		else
		{
			if (!scr.gM.pauseOptions)
			{
				if (!isQuitPanel)
				{
					scr.objLev.resumeButton.SetActive(false);
					scr.objLev.optionsButton.SetActive(false);
					scr.objLev.exitButton.SetActive(false);
					scr.objLev.restartButton.SetActive(false);
				}
				else
					quitPanel.SetActive(false);
			}
		}
	}

	public void TimeScale_1()
	{
		hintButtonText.color = releasedCol;
	
		if (!scr.gM.pauseInLevel)
		{
			if (!scr.gM.pauseOptions)
				Time.timeScale = 1;
			else
				pauseOptPanel.SetActive(true);
		}
		else
		{
			if (!scr.gM.pauseOptions)
			{
				if (!isQuitPanel)
				{
					scr.objLev.resumeButton.SetActive(true);
					scr.objLev.optionsButton.SetActive(true);
					scr.objLev.exitButton.SetActive(true);

					if (scr.alPrScr.freePlay == 1) 
						scr.objLev.restartButton.SetActive(true);
				}
				else
					quitPanel.SetActive(true);
			}
		}
	}

	public void QuitPanelCall()
	{
		isQuitPanel = true;
	}

	public void QuitPanelBack()
	{
		isQuitPanel = false;
	}
}
