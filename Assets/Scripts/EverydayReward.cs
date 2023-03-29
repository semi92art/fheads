using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EverydayReward : MonoBehaviour 
{
	public Scripts scr;

	public int reward;
	public Animator evrdRewardAnim;
	public Button evrdRewardButton;
	public Text timeToGettingAward;
	public Text timeToGettingAward1;
	public Text rewardText;

	private int day;
	private int second, minute, hour;
	private bool ready, readyCheck;
	private string getStr;

	void Awake()
	{
		rewardText.text = scr.gM.moneyString (reward);
		day = PlayerPrefs.GetInt ("Day");
		evrdRewardButton.interactable = false;
		evrdRewardAnim.enabled = false;

		if (PlayerPrefs.GetInt ("Ready") == 0)
			ready = false;
		else if (PlayerPrefs.GetInt ("Ready") == 1)
			ready = true;

		if ((Application.systemLanguage == SystemLanguage.Russian ||
		    Application.systemLanguage == SystemLanguage.Ukrainian ||
		    Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
			getStr = "получить!";
		else
			getStr = "get prize!";
	}

	void Update()
	{
		if (Time.frameCount % 10 == 0) 
		{
			second = System.DateTime.Now.Second;
			minute = System.DateTime.Now.Minute;
			hour = System.DateTime.Now.Hour;
		}

		if (day != System.DateTime.Today.Day)
		{
			day = System.DateTime.Today.Day;
			PlayerPrefs.SetInt ("Day", day);
			PlayerPrefs.SetInt ("Ready", 1);
			ready = true;
		}

		if (ready && !readyCheck) 
		{
			timeToGettingAward1.enabled = false;

			evrdRewardButton.interactable = true;
			evrdRewardAnim.enabled = true;
			timeToGettingAward.text = getStr;
		}
		else if (!ready) 
		{
			if (!timeToGettingAward1)
				timeToGettingAward1.enabled = true;

			evrdRewardButton.interactable = false;
			evrdRewardAnim.SetTrigger("call");
			timeToGettingAward.text = TimeToGettingReward (hour, minute, second);
		}

		if (awardGet)
		{
			timer += Time.deltaTime;

			if (timer > 0.5f)
			{
				if (!awardGet_1)
				{
					scr.alPrScr.moneyCount += reward;
					scr.alPrScr.setMoney = true;

					awardGet_1 = true;
				}
				else
				{
					if (timer > 1.5f)
					{
						TopPanelBack_1 ();
						timer = 0.0f;
						awardGet = false;
						awardGet_1 = false;
					}
				}
			}
		}
			
		if (isTopPanelBack_1)
		{
			timer1 += Time.deltaTime;

			if (timer1 > 1.0f)
			{
				scr.objM.topPanelAnim.gameObject.SetActive (false);
				Debug.Log ("Top Panel Set Active False");
				isTopPanelBack_1 = false;
				timer1 = 0.0f;
			}
		}

		readyCheck = ready;
	}

	private float timer, timer1;
	private bool awardGet_1;
	[HideInInspector]
	public bool awardGet;
	private bool isTopPanelBack_1;

	public void GetEverydayReward()
	{
		TopPanelCall_1 ();
		awardGet = true;

		timeToGettingAward1.enabled = true;
		PlayerPrefs.SetInt ("Ready", 0);
		ready = false;
		evrdRewardButton.GetComponent<Image> ().color = Color.white;
	}

	private void TopPanelCall_1()
	{
		scr.objM.topPanelAnim.gameObject.SetActive (true);
		scr.objM.topPanelAnim.SetTrigger ("call1");
	}

	private void TopPanelBack_1()
	{
		scr.objM.topPanelAnim.SetTrigger ("back1");
		isTopPanelBack_1 = true;
	}

	private string CurrentTime(int hour, int minute, int second)
	{
		string _hour = null;
		string _minute = null;
		string _second = null;
		string _time = null;

		if (hour == 24)
			hour = 0;

		if (minute == 60)
			minute = 0;

		if (second == 60)
			second = 0;

		if (hour < 10) 
			_hour = " " + hour.ToString ();
		else
			_hour = hour.ToString ();

		if (minute < 10) 
			_minute = "0" + minute.ToString ();
		else
			_minute = minute.ToString ();

		if (second < 10) 
			_second = "0" + second.ToString ();
		else
			_second = second.ToString ();

		_time = _hour + ":" + _minute + ":" + _second;

		return _time;
	}

	private string TimeToGettingReward(int hour, int minute, int second)
	{
		string _hour = null;
		string _minute = null;
		string _second = null;
		string _time = null;

		hour = 24 - hour;
		minute = 60 - minute;
		second = 60 - second;

		if (hour == 24)
			hour = 0;
		
		if (minute == 60)
			minute = 0;
		
		if (second == 60)
			second = 0;

		if (hour < 10) 
			_hour = " " + hour.ToString ();
		else
			_hour = hour.ToString ();

		if (minute < 10) 
			_minute = "0" + minute.ToString ();
		else
			_minute = minute.ToString ();

		if (second < 10) 
			_second = "0" + second.ToString ();
		else
			_second = second.ToString ();

		_time = _hour + ":" + _minute + ":" + _second;

		return _time;
	}
}
