using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class EverydayReward : MonoBehaviour 
{
    public Scripts scr;

    public int reward;
	private int day;
    private bool isAward;

	void Awake()
	{
        IsNewDay();
	}

    private void IsNewDay()
    {
        day = PlayerPrefs.GetInt ("Day");

        if (day != System.DateTime.Today.Day && PrefsManager.Instance.LaunchesCount != 1)
        {
            day = System.DateTime.Today.Day;
            PlayerPrefs.SetInt ("Day", day);
            scr.allAw.CallAwardPanel_1();
            isAward = true;
        }
    }
        
    void Start()
    {
        if (isAward)
            scr.allAw.CallAwardPanel_1();
    }

	public void GetEverydayReward()
	{
        PrefsManager.Instance.MoneyCount += reward;
        PlayerPrefs.SetInt ("Ready", 0);
	}
}
