using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContinueTournScript : MonoBehaviour
{
	public Scripts scr;

	public ScrollRect carScrRect, carScrRectRetro;
	public Animator prevAnim, prevAnimRetro;
	public Text prevText, prevTextRetro;
	public GameObject backButton, noButton, yesButton;
	public GameObject backButtonRetro, noButtonRetro, yesButtonRetro;
	private int timer;
	private bool isLoading;

	void Update()
	{
		if (isLoading)
			timer++;

		if (timer == 1)
		{
			scr.objM.loadingPanelAnim.gameObject.SetActive(true);
			scr.objM.loadingPanelAnim.SetTrigger("call");
		}
		else if (timer == 2)
		{
			YesButtonFunction();
		}
	}

	public void ContinueTourn()
	{
		string finT = "";

		finT = scr.alPrScr.finishTourn;

		if (finT == "CupGoes" || finT == "PlayoffGoes" || finT == "ChampGoes")
		{
			carScrRect.horizontal = false;
			prevAnim.SetTrigger("call");
			backButton.SetActive(false);
			noButton.SetActive(true);
			yesButton.SetActive(true);

			if ((Application.systemLanguage == SystemLanguage.Russian ||
				Application.systemLanguage == SystemLanguage.Ukrainian ||
				Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
				prevText.text = "Продолжить турнир? Если нет, результаты будут потеряны.";
			else
				prevText.text = "Continue current tournament? If no, you will loose results.";
		}
		else
		{
			backButton.SetActive(true);
			noButton.SetActive(false);
			yesButton.SetActive(false);
		}
	}

	public void YesButton()
	{
		isLoading = true;
	}

	public void YesButtonFunction()
	{
		string finT = scr.alPrScr.finishTourn;

		if (finT == "CupGoes")
		{
			for (int i = 0; i < 10; i++)
			{
				if (scr.alScr.varList.Count < 10)
					scr.alScr.varList.Add(new AllLevelsScript.CupVariants());
			}

			scr.alPrScr.fromMenuToTourn = "Yes";
			SceneManager.LoadScene("Cup");
		} 
		else if (finT == "PlayoffGoes")
		{
			scr.alPrScr.fromMenuToTourn = "Yes";
			SceneManager.LoadScene("Playoff");
		} 
		else if (finT == "ChampGoes")
		{
			scr.alPrScr.fromMenuToTourn = "Yes";
			SceneManager.LoadScene("Championship");
		}

		scr.alPrScr.isChange0 = true;
	}

	public void NoButton()
	{
		carScrRect.horizontal = true;
		scr.gM.SetCupDefaults();
		scr.alPrScr.finishTourn = "Finished";
		noButton.SetActive(false);
		yesButton.SetActive(false);
		backButton.SetActive(true);

		if ((Application.systemLanguage == SystemLanguage.Russian ||
			Application.systemLanguage == SystemLanguage.Ukrainian ||
			Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
			prevText.text = "Выиграйте предыдущий турнир, чтобы открыть этот!";
		else
			prevText.text = "Win previous tournament to open this!";

		for (int i = 0; i < 10; i++)
		{
			if (scr.alScr.varList.Count < 10)
				scr.alScr.varList.Add(new AllLevelsScript.CupVariants());					
		}
	}
}
