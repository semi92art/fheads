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
    
public enum OpponentsNumAndAge
{
    Main,
    Legend,
    Main_Main,
    Main_Legend,
    Legend_Main,
    Legend_Legend
}

public enum AgeType
{
    main, legend
}

[System.Serializable]
public class CareerOpponentMain
{
    public OpponentType oppType;
    public Names.PlayerName oppName;
}

[System.Serializable]
public class CareerOpponentLegend
{
    public OpponentType oppType;
    public Names.PlayerName_2 oppName_2;

}

[System.Serializable]
public class CareerGame
{
    public OpponentsNumAndAge oppsNumAndAge;
    public List<CareerOpponentMain> oppsMain;
    public List<CareerOpponentLegend> oppsLegend;
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

    public void RestartLevel()
    {
        if (scr.alPrScr.isRandGame == 0)
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
        //FIXME показ рекламы
        // FindObjectOfType<UnityAds_0>().ShowRewardedAd();
    }

    private void RestartForFree()
    {
        SceneManager.LoadScene(2);
    }
        
    public void ShowInterstitialAd()
    {
        // TODO
        // if (GoogleMobileAd.IsInterstitialReady)
        // //     GoogleMobileAd.ShowInterstitialAd();
    }

    public bool Int2Bool(int _int)
	{
        if (_int == 0)
			return false;
		else 
			return true;
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

	/// <summary>
	/// Convert money count in integer format to string. Example: 10000 = 10,000$
	/// </summary>
	/// <returns>The string.</returns>
	/// <param name="money">Money.</param>
	public string moneyString(int money)
	{
        return moneyString_0(money);
	}

    public string moneyString(float money)
    {
        int money_1 = (int)money;
        return moneyString_0(money_1);
    }

    private string moneyString_0(int money)
    {
        string strNum1 = "";
        string strNum2 = "";
        string strNum3 = "";

        string moneyStr = money.ToString("D");

        if (moneyStr.Length <= 3)
        {
            return moneyStr + "C";
        }
        else if (moneyStr.Length > 3 && moneyStr.Length <= 6)
        {
            int num1 = Mathf.FloorToInt(money / 1000);
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
            int num1 = Mathf.FloorToInt(money / 1000000);
            int num2 = money - num1 * 1000000;
            int num2_1 = Mathf.FloorToInt(num2 / 1000);

            if (num2_1 < 10)
                strNum2 = "00" + num2_1.ToString("D");
            else if (num2_1 >= 10 && num2_1 < 100)
                strNum2 = "0" + num2_1.ToString("D");
            else
                strNum2 = "" + num2_1.ToString("D");

            int num3 = money - num1 * 1000000 - num2_1 * 1000;

            if (num3 < 10)
                strNum3 = "00" + num3.ToString("D");
            else if (num3 >= 10 && num3 < 100)
                strNum3 = "0" + num3.ToString("D");
            else
                strNum3 = "" + num3.ToString("D");

            strNum1 = num1.ToString("D");
            return strNum1 + "," + strNum2 + "," + strNum3 + "C";
        }
        else
            return "";
    }


    public string string_IMPERVUM_Number(int _num)
	{
        switch (_num)
        {
            case 1:
                return "I";
            case 2:
                return "III";
            case 3:
                return "III";
            case 4:
                return "IV";
            case 5:
                return "V";
            case 6:
                return "VI";
            case 7:
                return "VII";
            case 8:
                return "VIII";
            case 9:
                return "IX";
            case 10:
                return "X";
            case 11:
                return "XI";
            case 12:
                return "XII";
            case 13:
                return "XIII";
            case 14:
                return "XIV";
            case 15:
                return "XV";
            case 16:
                return "XVI";
            case 17:
                return "XVII";
            case 18:
                return "XVIII";
            case 19:
                return "IXX";
            case 20:
                return "XX";
            case 21:
                return "XXI";
            case 22:
                return "XXII";
            case 23:
                return "XXIII";
            case 24:
                return "XXIV";
            case 25:
                return "XXV";
            case 26:
                return "XXVI";
            case 27:
                return "XXVII";
            case 28:
                return "XXVIII";
            case 29:
                return "XXIX";
            case 30:
                return "XXX";
            case 31:
                return "XLI";
            case 32:
                return "XLII";
            case 33:
                return "XLIII";
            case 34:
                return "XLIV";
            case 35:
                return "XLV";
            case 36:
                return "XLVI";
            case 37:
                return "XLVII";
            case 38:
                return "XLVIII";
            case 39:
                return "XLIX";
            case 40:
                return "XL";
            case 41:
                return "LI";
            case 42:
                return "LII";
            case 43:
                return "LIII";
            case 44:
                return "LIV";
            case 45:
                return "LV";
            case 46:
                return "LVI";
            case 47:
                return "LVII";
            case 48:
                return "LVIII";
            case 49:
                return "LIX";
            case 50:
                return "L";
            default:
                return "-";
        }
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
