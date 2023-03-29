using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Objects_Championship : MonoBehaviour 
{
	public Scripts scr;

	public Text leagueText;
	public WinCupPanelScript winCupPanScr;
	public AudioSource buttonsSource, mainThemeSource;
	public AudioSource moneyWin1Source, moneyWin2Source;
	public Animator loadingPanelAnim, quitPanelMenu;
	public Animator secondPanel;
	public GameObject noButton, yesButton, closeButton;
	public Image cupImage;
	public Sprite silverMedal, bronzeMedal, goldMedal;
	public Color color1;
	[HideInInspector]
	public Sprite currCupSprite;

	void Awake()
	{
		buttonsSource.volume = scr.alPrScr.musicVolume;
		mainThemeSource.volume = scr.alPrScr.musicVolume;
		moneyWin1Source.volume = scr.alPrScr.musicVolume;
		moneyWin2Source.volume = scr.alPrScr.musicVolume;
		currCupSprite = winCupPanScr.cupSprites [scr.alPrScr.trn];

		int trn = scr.alPrScr.trn1;
		int lN = scr.gM.leagueN(trn);


		if ((Application.systemLanguage == SystemLanguage.Russian ||
			Application.systemLanguage == SystemLanguage.Ukrainian ||
			Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
			leagueText.text = "основа: чемпионат лиги " + scr.gM.leagueNumberString(lN);
		else
			leagueText.text = "base: league " + scr.gM.leagueNumberString(lN) + " championship";
	}
}
