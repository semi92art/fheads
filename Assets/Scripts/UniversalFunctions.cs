using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Common.Managers.Advertising;
using Common.Utils;
using mazing.common.Runtime.Utils;
using Zenject;

[System.Serializable]
public class CareerOpponentMain
{
    public EOpponentType oppType;
    public Names.PlayerName oppName;
}

[System.Serializable]
public class CareerOpponentLegend
{
    public EOpponentType oppType;
    public Names.PlayerName_2 oppName_2;

}

[System.Serializable]
public class CareerGame
{
    public EOpponentsNumAndAge oppsNumAndAge;
    public List<CareerOpponentMain> oppsMain;
    public List<CareerOpponentLegend> oppsLegend;
}

public class UniversalFunctions : MonoBehaviour
{
    #region inject
    
    private IAdsManager AdsManager { get; set; }

    [Inject]
    private void Inject(IAdsManager _AdsManager)
    {
        AdsManager = _AdsManager;
    }

    #endregion

    public Scripts scr;

    public void RestartLevel()
    {
        if (scr.alPrScr.isRandGame == 0)
            RestartLevelForAds();
        else
        {
            if (scr.tM.matchPeriods == 0)
                RestartLevelCore();
            else
            {
                if (!scr.tM.isBetweenTimes)
                    RestartLevelCore();
            }
                
        }  
    }

    private void RestartLevelForAds()
    {
        if (AdsManager.RewardedAdReady)
            AdsManager.ShowRewardedAd(_OnReward: RestartLevelCore);
        else MazorCommonUtils.ShowAlertDialog("OOPS", "the ad didn't load. Try later...");
    }

    private static void RestartLevelCore()
    {
        SceneManager.LoadScene(2);
    }
        
    public void ShowInterstitialAd()
    {
        AdsManager.ShowInterstitialAd();
    }

    public string CurrentTime(int _Hour, int _Minute, int _Second)
	{
        if (_Hour == 24)   _Hour   = 0;
        if (_Minute == 60) _Minute = 0;
        if (_Second == 60) _Second = 0;
        string hour   = GetTimeValueStringNormalized(_Hour);
        string minute = GetTimeValueStringNormalized(_Minute);
        string second = GetTimeValueStringNormalized(_Second);
        return $"{hour}:{minute}:{second}";
	}
    
    public string TimeToGettingReward(int _Hour, int _Minute, int _Second)
    {
        _Hour   = _Hour == 0   ? 0 : 24 - _Hour;
        _Minute = _Minute == 0 ? 0 : 60 - _Minute;
        _Second = _Second == 0 ? 0 : 60 - _Second;
        string hour   = GetTimeValueStringNormalized(_Hour);
        string minute = GetTimeValueStringNormalized(_Minute);
        string second = GetTimeValueStringNormalized(_Second);
        return $"{hour}:{minute}:{second}";
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void PictureOff(Image _Image1)
	{
		Color color2 = _Image1.color;
		color2.a = 0;
		_Image1.color = color2;
	}

	public void PictureOn(Image image1)
	{
		Color color2 = image1.color;
		color2.a = 1;
		image1.color = color2;
	}

	public void PlayerLeagueOn (Animator _AnimMenu)
	{
		_AnimMenu.SetBool("call", true);
	}

	public void PlayerLeagueOff (Animator _AnimMenu)
	{
		_AnimMenu.SetBool("call", false);
	}

	/// <summary>
	/// Convert money count in integer format to string. Example: 10000 = 10,000$
	/// </summary>
	/// <returns>The string.</returns>
	/// <param name="_Money">Money.</param>
	public string MoneyString(int _Money)
	{
        return MoneyStringCore(_Money);
	}

    public string MoneyString(float _Money)
    {
        return MoneyStringCore((int)_Money);
    }

    private static string MoneyStringCore(int _Money)
    {
        string strNum1, strNum2;

        string moneyStr = _Money.ToString("D");
        static string Normalize(int _V) => _V.ToString("D");

        if (moneyStr.Length <= 3)
        {
            return moneyStr + "C";
        }
        if (moneyStr.Length > 3 && moneyStr.Length <= 6)
        {
            int num1 = Mathf.FloorToInt(_Money * 0.001f);
            int num2 = _Money - num1 * 1000;

            if (num2 < 10)
                strNum2 = "00" + num2.ToString("D");
            else if (num2 < 100)
                strNum2 = "0" + num2.ToString("D");
            else
                strNum2 = "" + num2.ToString("D");

            strNum1 = num1.ToString("D");
            return strNum1 + "," + strNum2 + "C";
        }
        if (moneyStr.Length > 6 && moneyStr.Length <= 9)
        {
            int num1 = Mathf.FloorToInt(_Money * 0.000001f );
            int num2 = _Money - num1 * 1000000;
            int num2_1 = Mathf.FloorToInt(num2 * 0.001f);

            if (num2_1 < 10)
                strNum2 = "00" + num2_1.ToString("D");
            else if (num2_1 < 100)
                strNum2 = "0" + num2_1.ToString("D");
            else
                strNum2 = "" + num2_1.ToString("D");

            int num3 = _Money - num1 * 1000000 - num2_1 * 1000;

            string strNum3;
            if (num3 < 10)
                strNum3 = "00" + num3.ToString("D");
            else if (num3 < 100)
                strNum3 = "0" + num3.ToString("D");
            else
                strNum3 = "" + num3.ToString("D");

            strNum1 = num1.ToString("D");
            return strNum1 + "," + strNum2 + "," + strNum3 + "C";
        }
        return "";
    }


    public string ImpervumNumberString(int _Num)
    {
        return _Num switch
        {
            1  => "I",
            2  => "III",
            3  => "III",
            4  => "IV",
            5  => "V",
            6  => "VI",
            7  => "VII",
            8  => "VIII",
            9  => "IX",
            10 => "X",
            11 => "XI",
            12 => "XII",
            13 => "XIII",
            14 => "XIV",
            15 => "XV",
            16 => "XVI",
            17 => "XVII",
            18 => "XVIII",
            19 => "IXX",
            20 => "XX",
            21 => "XXI",
            22 => "XXII",
            23 => "XXIII",
            24 => "XXIV",
            25 => "XXV",
            26 => "XXVI",
            27 => "XXVII",
            28 => "XXVIII",
            29 => "XXIX",
            30 => "XXX",
            31 => "XLI",
            32 => "XLII",
            33 => "XLIII",
            34 => "XLIV",
            35 => "XLV",
            36 => "XLVI",
            37 => "XLVII",
            38 => "XLVIII",
            39 => "XLIX",
            40 => "XL",
            41 => "LI",
            42 => "LII",
            43 => "LIII",
            44 => "LIV",
            45 => "LV",
            46 => "LVI",
            47 => "LVII",
            48 => "LVIII",
            49 => "LIX",
            50 => "L",
            _  => "-"
        };
    }

    private static string GetTimeValueStringNormalized(int _V)
    {
        return _V < 10 ? " " + _V : _V.ToString();
    }
}
