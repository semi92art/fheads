using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class League_UI_Elements
{
    public Image im_CirclePercent;
    public Image im_Cup;
    public Image[] im_Wins;
    public Sprite spr_CupGold;
    public Sprite spr_CupGray;
    public Text text_cupName_1;
    public Text text_cupName_2;
    public Button _button;
}

[System.Serializable]
public class CareerOpponentMain
{
    public OpponentType oppType;
    public Names.PlayerName oppName;
}

[System.Serializable]
public class CareerGame
{
    public List<CareerOpponentMain> oppsMain;
}
    
public enum OpponentType
{
    Classic,
    Bycicle,
    Goalkeeper
}

public class UniversalFunctions : MonoBehaviour
{
    public Scripts scr;

    public Vector3 HSV_from_RGB(Color col)
    {
        float h_val, s_val, v_val;
        Color.RGBToHSV(col, out h_val, out s_val, out v_val);
        return new Vector3(h_val, s_val, v_val);
    }

    public bool IsMyDevice()
    {
        Objects_Level objL = FindObjectOfType<Objects_Level>();
        bool show = false;

        for (int i = 0; i < objL.idForTesting.Length; i++)
        {
            if (Android_Id() == objL.idForTesting[i])
            {
                show = true;
                break;
            }
        }

        return show;
    }

    public void RestartLevel()
    {
        if (!PrefsManager.Instance.IsRandomOpponent)
            RestartForVideo();
        else
        {
            if (scr.tM.matchPeriods == 0)
                RestartForFree();
            else
            {
                if (!scr.tM.isBetweenTimes)
                    RestartForFree();
            }
                
        }  
    }

    private void RestartForVideo()
    {
        FindObjectOfType<UnityAds_0>().ShowRewardedAd();
    }

    private void RestartForFree()
    {
        SceneManager.LoadScene(2);
    }
        
    public void ShowInterstitialAd()
    {
        if (!IsMyDevice() && !GameManager.Instance.isNoAds)
        {
            if (GoogleMobileAd.IsInterstitialReady)
                GoogleMobileAd.ShowInterstitialAd();
        }
    }

    public string Android_Id()
    {
        string android_id = "editor";

        #if !UNITY_EDITOR
        AndroidJavaClass up = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject> ("currentActivity");
        AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject> ("getContentResolver");  
        AndroidJavaClass secure = new AndroidJavaClass ("android.provider.Settings$Secure");
        android_id = secure.CallStatic<string> ("getString", contentResolver, "android_id");
        #endif

        return android_id;
    }

    public void ShowDeviceIdentifier()
    {
        Debug.Log(Android_Id());
    }

	public bool Int2Bool(int _Value)
    {
        return _Value != 0;
    }

	public string CurrentTime(int hour, int minute, int second)
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

	public string TimeToGettingReward(int hour, int minute, int second)
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

	public void ExitGame()
	{
		Application.Quit();
	}

	public void PictureOff(Image image1)
	{
		Color color2 = image1.color;
		color2.a = 0;
		image1.color = color2;
	}

	public void PictureOn(Image image1)
	{
		Color color2 = image1.color;
		color2.a = 1;
		image1.color = color2;
	}

	public void PlayerLeagueOn (Animator AnimMenu)
	{
		AnimMenu.SetBool("call", true);
	}

	public void PlayerLeagueOff (Animator AnimMenu)
	{
		AnimMenu.SetBool("call", false);
	}

	public string moneyString(int money)
	{
        return moneyString_0(money);
	}

    private string moneyString_0(int money)
    {
        string strNum1 = string.Empty;
        string strNum2 = string.Empty;
        string strNum3 = string.Empty;

        string moneyStr = money.ToString("D");

        if (moneyStr.Length <= 3)
        {
            return moneyStr + "C";
        }
        else if (moneyStr.Length > 3 && moneyStr.Length <= 6)
        {
            int num1 = Mathf.FloorToInt(money * 0.0001f);
            int num2 = money - num1 * 1000;

            if (num2 < 10)
                strNum2 = "00" + num2.ToString("D");
            else if (num2 >= 10 && num2 < 100)
                strNum2 = "0" + num2.ToString("D");
            else
                strNum2 = "" + num2.ToString("D");

            strNum1 = num1.ToString("D");
            return strNum1 + "," + strNum2 + "C";
        }
        else if (moneyStr.Length > 6 && moneyStr.Length <= 9)
        {
            int num1 = Mathf.FloorToInt(money * 0.0000001f);
            int num2 = Mathf.FloorToInt(money - num1 * 100);

            if (num2 < 10)
                strNum2 = "00" + num2.ToString("D");
            else if (num2 >= 10 && num2 < 100)
                strNum2 = "0" + num2.ToString("D");
            else
                strNum2 = "" + num2.ToString("D");

            int num3 = money - num1 * 1000000 - num2 * 1000;

            if (num3 < 10)
                strNum3 = "00" + num3.ToString("D");
            else if (num3 >= 10 && num3 < 100)
                strNum3 = "0" + num3.ToString("D");
            else
                strNum3 = "" + num3.ToString("D");

            strNum1 = num1.ToString("D");
            return strNum1 + "," + strNum2 + "," + strNum3 + "C";
        }
        return "";
    }

    public int Stadium(int _game)
    {
        switch (_game)
        {
            case 0:
                return 0;
            case 1:
                return 1;
            case 2:
                return 2;
            case 3:
                return 3;
            case 4:
                return 6;
            case 5:
                return 7;
            case 6:
                return 8;
            case 7:
                return 9;
            case 8:
                return 13;
            case 9:
                return 16;
            default:
                return 0;
        }
    }
}
