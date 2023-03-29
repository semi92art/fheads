using UnityEngine;
using UnityEngine.UI;

public class ChangableTextScript : MonoBehaviour 
{
	public Scripts scr;
	[HideInInspector]
	public Text text1;

	public string[] menuStr;
	public string[] menuStrRus;

	void Awake()
	{
		if ((Application.systemLanguage == SystemLanguage.Russian ||
			Application.systemLanguage == SystemLanguage.Ukrainian ||
			Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
			menuStr = menuStrRus;
	}

	void Update()
	{
		if (scr.gM.MainMenuBools.menuCareer) 
		{
			if (text1.text != menuStr[0])
				text1.text = menuStr[0];
			else 
			{
				if (!text1.enabled)
					text1.enabled = true;
			}
		}
		else if (scr.gM.MainMenuBools.menuFreePlay) 
		{
			if (text1.text != menuStr[1]) 
				text1.text = menuStr[1];
			else 
			{
				if (!text1.enabled)
					text1.enabled = true;
			}
		}
		else if (scr.gM.MainMenuBools.menuProfilePlayers) 
		{
			if (text1.text != menuStr[2])
				text1.text = menuStr[2];
			else 
			{
				if (!text1.enabled)
					text1.enabled = true;
			}	
		}
		else if (scr.gM.MainMenuBools.menuProfileStadiums) 
		{
			if (text1.text != menuStr[3]) 
				text1.text = menuStr[3];
			else 
			{
				if (!text1.enabled)
					text1.enabled = true;
			}
		}
	}
}
