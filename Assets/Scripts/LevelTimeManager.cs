using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#if UNITY_EDITOR
using UnityEngine.Profiling;
#endif

public class LevelTimeManager : MonoBehaviour 
{
    public Scripts scr;

    [HideInInspector]
    public Rigidbody2D[] rigBodies;
    public Rigidbody2D[] contrRidBodies;

	public bool timeFreeze;
	public Button secondPerButton;
	public Text timeText;
	public Text periodsText;
	public GameObject congradPanel;
    
	[Header("Number of times:")]
	//[HideInInspector]
	public int matchPeriods;
	private int periodsOnBegin;
	[Header("Begin time in seconds:")]
	public float beginTime;

	public Color color1, color2, shadowColor1, shadowColor2;
	public float time0;
	public static int resOfGame;
	public static bool isEndOfTime;

	public bool isBetweenTimes;

	private bool isNextTime;
	private int tim, tim1;
    [HideInInspector]
    public int time1;
    [HideInInspector]
	public int time1Check;
	private string secondsOnDisp;
	private int minutes, seconds;
	private Shadow timeShadow;
	[HideInInspector]
	public bool isGoldenGoal;
    private int whistleTim = 0;
    private float tim3;
    private bool isTim3End;


	void Awake ()
	{
        scr.congrPan.congrPanel.SetActive(false);
		time0 = beginTime + 1;
        timeShadow = timeText.GetComponent<Shadow>();
		secondPerButton.interactable = false;

		periodsOnBegin = matchPeriods;  
        periodsText.text = "PERIOD " + "1/" + periodsOnBegin;
	}

	public void CallBackBetweenTimesPanel()
	{
        if (scr.bonObjMan.isWatchVideoInPause)
        {
//#if !UNITY_EDITOR
            scr.bonObjMan.WatchVideo(1);
//#else
            //CallBackBetweenTimesPanel_0();
//#endif
        }
        else
            CallBackBetweenTimesPanel_0();
	}

    private void CallBackBetweenTimesPanel_0()
    {
        System.GC.Collect();
        scr.camSize.tim = 0;
        isBetweenTimes = false;
        Time.timeScale = GameManager.Instance.currTimeScale;
        time0 = beginTime + 1;
        MatchManager.Instance.Restart = true;
        tim = 0;
        tim1 = 0;
        int currentPeriod = periodsOnBegin - matchPeriods + 1;
        periodsText.text = "PERIOD " + currentPeriod + "/" + periodsOnBegin;
        scr.objLev.mainCanvas.enabled = false;
        scr.bonObjMan.WatchVideo(2);
    }
 
	void Update ()
	{
        if (!timeFreeze && MatchManager.Instance.GameStarted)
            TimeUpdate();
    }	

	private void TimeUpdate()
	{
		if (isBetweenTimes)
		{
            if (tim3 < 1f)
                tim3 += Time.unscaledDeltaTime;
            else if (tim3 > 1f && !isTim3End)
			{
                if (!isTim3End)
                {
                    isTim3End = true;
                    secondPerButton.interactable = true;
                }
			}
		}

        time0 -= Time.deltaTime;
        time1 = (int)time0;

		if (time1 != time1Check) 
		{
            if (time1 >= 60)
            {
                if (time1 == 90)
                {
                    timeText.color = color1;
                    isEndOfTime = false;
                    minutes = 1; 
                }

                seconds = time1 - (int)60;
            }
            else
            {
                switch (time1)
                {
                    case 59:
                        minutes = 0;
                        break;
                    case 3:
                        isEndOfTime = true;
                        break;
                }

                seconds = time1 > 0 ? time1 : 0;
            }

            secondsOnDisp = seconds < 10 ? "0" : "";

			if (time0 > 1) 
			{
                timeText.text = minutes.ToString() + ":" + secondsOnDisp.ToString() + seconds.ToString();

				if (time0 <= 15) 
				{
					if (time1 % 2 == 0) 
					{
                        timeText.color = color2;
						timeShadow.effectColor = shadowColor2;
					}
					else
					{
                        timeText.color = color1;
						timeShadow.effectColor = shadowColor1;
					}

                    if (time1 != time1Check)
                    {
                        if (scr.levAudScr.ticTocSource.enabled)
                            scr.levAudScr.ticTocSource.Play();
                    }
				}
			} 
			else
			{
				tim++;

				if (tim == 1) 
					matchPeriods--;

				if (matchPeriods == 0)
				{
					if (Score.score == Score.score1)
						GoldenGoal();
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
            periodsText.text = "GOLDEN GOAL";
            timeText.text = "0:00";
			periodsText.color = new Color(1, 0.7f, 0);
			periodsText.lineSpacing = 1.1f;
			isGoldenGoal = true;
		}
	}

	private void NoGoldenGoal()
	{
		whistleTim ++;

		if (whistleTim <= 2)
		{
		    scr.congrPan.congrPanel.SetActive(true);
			scr.objLev.mainCanvas.enabled = true;

            if (scr.levAudScr.SoundOn)
			    scr.levAudScr.longWhistle.Play();

            timeText.text = "0:00";

            if (tim1 == 0)
            {
                if (matchPeriods == 0)
                {
	                GameManager.Instance.MenuResult();
                    resOfGame = Score.score > Score.score1 ? 1 : 3;
                    GameManager.Instance.Rigidbodies_TimeScale_0();
                    scr.objLev.controlsCanvas.enabled = false;

                    if (!PrefsManager.Instance.IsRandomOpponent)
                    {
                        if (resOfGame == 1)
                        {
	                        PrefsManager.Instance.Game++;
                            GameManager.Instance.SetStadium();
                            scr.objLev.text_Result.gameObject.SetActive(false);
                            scr.objLev._anim_VictText.winState = 1;
                            scr.objLev.text_Victory.text = "VICTORY";
                            scr.fwScr.SetActiveWinFirework();
                        }
                        else
                        {
                            scr.objLev._anim_VictText.winState = 0;
                            scr.objLev.text_Victory.text = "DEFEAT";
                            scr.objLev.text_Result.gameObject.SetActive(true);

                            int _canRestart = PlayerPrefs.GetInt("CanRestart");
                            _canRestart--;
                            PlayerPrefs.SetInt("CanRestart", _canRestart);

                            if (_canRestart > 0)
                                scr.objLev.obj_RestartButon.SetActive(true);  
                        }
                    }
                    else
                    {
                        scr.objLev._anim_VictText.winState = 1;
                        scr.objLev.text_Result.gameObject.SetActive(false);
                        scr.objLev.obj_RestartButon.SetActive(false);

                        if (resOfGame == 1)
                            scr.objLev.text_Victory.text = "VICTORY";
                        else
                            scr.objLev.text_Victory.text = "DEFEAT";
                    }


                }
                else
                {
                    isNextTime = true;
                }

                tim1++;
            }
		}
	}
}