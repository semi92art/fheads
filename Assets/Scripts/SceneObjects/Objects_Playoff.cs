using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Objects_Playoff : MonoBehaviour {

	public Scripts scr;

	public Text leagueText;
	public AudioSource buttonsSource, mainThemeSource;
	public AudioSource moneyWin1Source, moneyWin2Source;
	public Animator loadingPanelAnim, quitPanelMenu;
	public GameObject noButton, yesButton, closeButton;
	public Color textColor1;

	//[HideInInspector]
	public Image[] flags;


	void Awake()
	{
		buttonsSource.volume = scr.alPrScr.musicVolume;
		mainThemeSource.volume = scr.alPrScr.musicVolume;
		moneyWin1Source.volume = scr.alPrScr.musicVolume;
		moneyWin2Source.volume = scr.alPrScr.musicVolume;
		closeButton.SetActive(false);

		int trn = scr.alPrScr.trn1;
		int lN = scr.gM.leagueN(trn);

		if ((Application.systemLanguage == SystemLanguage.Russian ||
			Application.systemLanguage == SystemLanguage.Ukrainian ||
			Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
			leagueText.text = "основа: кубок лиги " + scr.gM.leagueNumberString(lN);
		else
			leagueText.text = "base league " + scr.gM.leagueNumberString(lN) + " cup";
	}

	public void SetFlagsInvisible(){
		scr.plfMan = gameObject.GetComponent<PlayoffManager>();
		for (int i = 0; i < scr.plfMan.flag.Length; i++){
			flags[i].color = Color.clear;
			scr.plfMan.flag[i].color = Color.clear;
		}
	}

	public void SetFlagsVisible(){
		scr.plfMan = gameObject.GetComponent<PlayoffManager>();
		for (int i = 0; i < scr.plfMan.flag.Length; i++){
			flags[i].color = Color.white;
			scr.plfMan.flag[i].color = new Color(1, 0.94f, 0.75f, 1);
		}
	}

	public void SetNamesInvisible(){
		scr.plfMan = gameObject.GetComponent<PlayoffManager>();
		for (int i = 0; i < scr.plfMan.name1.Length; i++){
			scr.plfMan.name1[i].color = Color.clear;
		}
	}
	
	public void SetNamesVisible(){
		scr.plfMan = gameObject.GetComponent<PlayoffManager>();
		for (int i = 0; i < scr.plfMan.name1.Length; i++){
			scr.plfMan.name1[i].color = textColor1;
		}
	}

	public void SetScoresInvisible(){
		scr.plfMan = gameObject.GetComponent<PlayoffManager>();
		for (int i = 0; i < scr.plfMan.score.Length; i++){
			scr.plfMan.score[i].color = Color.clear;
		}
	}
	
	public void SetScoresVisible(){
		scr.plfMan = gameObject.GetComponent<PlayoffManager>();
		for (int i = 0; i < scr.plfMan.score.Length; i++){
			scr.plfMan.score[i].color = textColor1;
		}
	}
}
