using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class TimeManager : MonoBehaviour 
{
	public Scripts scr;

	public Button secondPerButton;
	public Text periodsText;
	public GameObject congradPanel;

	[Header("Number of times:")]
	//[HideInInspector]
	public int matchPeriods = 1;
	private int periodsOnBegin;
	[Header("Begin time in seconds:")]
	public float beginTime;
	public Color color1, color2, shadowColor1, shadowColor2;
	public float time0;
	public static Text text;
	public static int resultOfGame;
	public static bool isEndOfTime;

	public bool isBetweenTimes;

	private bool isNextTime;
	private int timer, timer1;
	private int time1, time1Check;
	private string secondsOnDisp;
	private int minutes, seconds;
	private Shadow timeShadow;
	[HideInInspector]
	public bool isGoldenGoal;

	void Awake ()
	{
		time0 = beginTime + 1;
		text = GetComponent <Text> ();
		timeShadow = GetComponent<Shadow> ();
		secondPerButton.interactable = false;
		scr.objLev.secontTimePanelText.color = new Vector4(
			scr.objLev.secontTimePanelText.color.r,
			scr.objLev.secontTimePanelText.color.g,
			scr.objLev.secontTimePanelText.color.b,
			1);

		periodsOnBegin = matchPeriods;

		if ((Application.systemLanguage == SystemLanguage.Russian ||
			Application.systemLanguage == SystemLanguage.Ukrainian ||
			Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
			periodsText.text = "тайм " + "1/" + periodsOnBegin;
		else
			periodsText.text = "period " + "1/" + periodsOnBegin;


		if ((Application.systemLanguage == SystemLanguage.Russian ||
			Application.systemLanguage == SystemLanguage.Ukrainian ||
			Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
			scr.objLev.secontTimePanelText.text = "Конец 1 тайма";
		else
			scr.objLev.secontTimePanelText.text = "End of 1st period";
	}

	private void CallBetweenTimesPanel()
	{
		if (isNextTime)
		{
			scr.objLev.mainCanvas.enabled = true;
			isBetweenTimes = true;
			scr.objLev.secondTimePanelAnim.gameObject.SetActive(true);
			scr.objLev.secondTimePanelAnim.SetTrigger("call");
			isNextTime = false;
			Time.timeScale = 0;
		}
	}

	public void CallBackBetweenTimesPanel()
	{
		isBetweenTimes = false;
		scr.objLev.secondTimePanelAnim.SetTrigger("back");
		scr.objLev.secondTimePanelAnim.gameObject.SetActive(false);
		Time.timeScale = 1;
		time0 = beginTime + 1;
		scr.pMov.restart = true;
		timer = 0;
		timer1 = 0;
		int currentPeriod = periodsOnBegin - matchPeriods + 1;

		if ((Application.systemLanguage == SystemLanguage.Russian ||
			Application.systemLanguage == SystemLanguage.Ukrainian ||
			Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
			periodsText.text = "тайм " + currentPeriod + "/" + periodsOnBegin;
		else
			periodsText.text = "period " + currentPeriod + "/" + periodsOnBegin;

		scr.objLev.mainCanvas.enabled = false;
	}

	private void SetNameOfCurrentPeriod()
	{
		string ending = "";
		int currentPeriod = periodsOnBegin - matchPeriods + 1;
		if (currentPeriod == 1) 
			ending = "st";
		else if (currentPeriod == 2)
			ending = "nd";
		else if (currentPeriod == 3) 
			ending = "rd";
		else 
			ending = "th";

		if ((Application.systemLanguage == SystemLanguage.Russian ||
			Application.systemLanguage == SystemLanguage.Ukrainian ||
			Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
			scr.objLev.secontTimePanelText.text = "Начать " + currentPeriod + " тайм";
		else
			scr.objLev.secontTimePanelText.text = "Start " + currentPeriod + ending + " period";
	}

	private int timer2;

	void Update ()
	{
		if (isBetweenTimes)
		{
			timer2 ++;

			if (timer2 == 50)
			{
				secondPerButton.interactable = true;

				scr.objLev.secontTimePanelText.color = new Vector4(
					scr.objLev.secontTimePanelText.color.r,
					scr.objLev.secontTimePanelText.color.g,
					scr.objLev.secontTimePanelText.color.b,
					1);

				SetNameOfCurrentPeriod();
			}
		}
		


		time0 -= Time.deltaTime;
		time1 = (int)time0;

		if (time1 != time1Check) 
		{
			switch (time1) 
			{
			case 90:
				text.color = color1;
				isEndOfTime = false;
				minutes = 1;
				break;
			case 59:
				minutes = 0;
				break;
			case 3: isEndOfTime = true;
				break;
			}
				
			if (time1 >= 60) 
				seconds = time1 - 60;
			else
			{
				if (time1 > 0) seconds = time1;
				else seconds = 0;
			} 

			if (seconds < 10) secondsOnDisp = "0";
			else secondsOnDisp = "";

			if (time0 > 1) 
			{
				text.text = minutes + ":" + secondsOnDisp + seconds;

				if (time0 <= 15) 
				{
					if (time1 % 2 == 0) 
					{
						text.color = color2;
						timeShadow.effectColor = shadowColor2;
					}
					else
					{
						text.color = color1;
						timeShadow.effectColor = shadowColor1;
					}

					if (time1 != time1Check)
						scr.levAudScr.ticTocSource.Play ();
				}
			} 
			else
			{
				timer++;

				if (timer == 1) 
					matchPeriods--;

				if (matchPeriods == 0)
				{
					if (Score.score == Score.score1)
					{
						if (scr.alPrScr.isLeaderboardGame == 1 || scr.alPrScr.freePlay == 1)
							GoldenGoal();
						else
						{
							if (scr.alPrScr.finishTourn == "PlayoffGoes")
								GoldenGoal();
							else
								NoGoldenGoal();
						}
					}
					else
						NoGoldenGoal();
				}
				else
					NoGoldenGoal();
			}
		}
			
		time1Check = time1;
	}

	private void GoldenGoal()
	{
		if (!isGoldenGoal)
		{
			if ((Application.systemLanguage == SystemLanguage.Russian ||
				Application.systemLanguage == SystemLanguage.Ukrainian ||
				Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
				periodsText.text = "золотой гол";
			else
				periodsText.text = "golden goal";

			text.text = "0:00";
			periodsText.color = new Color(1, 0.7f, 0);
			periodsText.fontSize = 40;
			periodsText.lineSpacing = 1.1f;

			isGoldenGoal = true;
		}
	}

	private void NoGoldenGoal()
	{
		if (!scr.congrPan.congrPanel.activeSelf)
			scr.congrPan.congrPanel.SetActive(true);

		scr.objLev.mainCanvas.enabled = true;
		scr.levAudScr.refereeWhistleSource.Play ();
		text.text = "0:00";	

		switch (timer1)
		{
		case 0:
			if (matchPeriods == 0)
			{
				scr.gM.MenuResult ();

				if (Score.score > Score.score1)
					resultOfGame = 1;
				else if (Score.score == Score.score1) 
					resultOfGame = 2;
				else
					resultOfGame = 3;
			} 
			else
			{
				isNextTime = true;
				CallBetweenTimesPanel();
			}

			timer1++;
			break;
		}
	}
}