using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Objects_Cup : MonoBehaviour
{
	public Scripts scr;

	public Text leagueText;
	public Animator infoPanelAnim;
	public AudioSource buttonsSource, mainThemeSource;
	public AudioSource moneyWin1Source, moneyWin2Source;
	public Animator loadingPanelAnim;
	public Animator quitPanelMenu;
	public GameObject noButton, yesButton, closeButton;

	void Awake()
	{
		buttonsSource.volume = scr.alPrScr.musicVolume;
		mainThemeSource.volume = scr.alPrScr.musicVolume;
		moneyWin1Source.volume = scr.alPrScr.musicVolume;
		moneyWin2Source.volume = scr.alPrScr.musicVolume;

		int trn = scr.alPrScr.trn1;
		int lN = scr.gM.leagueN(trn);

		if ((Application.systemLanguage == SystemLanguage.Russian ||
			Application.systemLanguage == SystemLanguage.Ukrainian ||
			Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
			leagueText.text = "основа: кубок лиги " + scr.gM.leagueNumberString(lN);
		else
			leagueText.text = "base: league " + scr.gM.leagueNumberString(lN) + " cup";
	}
}
