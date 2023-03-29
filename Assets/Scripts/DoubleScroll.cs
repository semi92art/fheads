using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;



[System.Serializable]
public class posItemList
{
	public float anchPos;
	public int numOfPlayers;
}

[System.Serializable]
public class Countries
{
	public string name;
	public Sprite flag;
}


public class DoubleScroll : MonoBehaviour 
{
	public Scripts scr;

	public Sprite butSpr, butSprPushed;
	public Sprite[] stadiumSpr;
	public Image stadiumIm;

	public Image 
	stadiumButtonIm,  
	morningButIm, 
	afternoonButIm, 
	eveningButIm;

	public List <Countries> countriesList;
	public List<posItemList>plItemList;

	private int currStad;

	void Awake()
	{
		SetStadiumAndWeatherButtons();
	}
	
	void Update()
	{
		
	}

	public void SetPlayerNotFP()
	{
		int i = scr.alPrScr.playerIndex;

		scr.alScr.playerFlag = scr.prScrL.itemList[i].flag;
		scr.alScr.playerName = scr.prScrL.itemList[i].name;
		scr.alScr.playerName0 = scr.prScrL.itemList[i].name0;
		scr.alScr.playerSprite = scr.prScrL.itemList[i].icon;
	}

	public void SetStadium(int set)
	{
		if (set == 1)
			currStad ++;	

		int currStad1 = 0;

		if (currStad == 4)
			currStad = 0;

		if (scr.alPrScr.timeOfDay == 0)
			currStad1 = currStad;
		else if (scr.alPrScr.timeOfDay == 1)
			currStad1 = currStad + 4;
		else if (scr.alPrScr.timeOfDay == 2)
			currStad1 = currStad + 8;

		stadiumIm.sprite = stadiumSpr[currStad1];
		Debug.Log("CurrStad1 = " + currStad1);

		scr.alPrScr.stadiumOrTraining = currStad;
		scr.alPrScr.isChange0 = true;
	}

	public void SetTimeOfDay(int tod)
	{
		if (tod == 0)
		{
			morningButIm.sprite = butSprPushed;
			afternoonButIm.sprite = butSpr;
			eveningButIm.sprite = butSpr;
			scr.alPrScr.timeOfDay = 0;
		}
		else if (tod == 1)
		{
			morningButIm.sprite = butSpr;
			afternoonButIm.sprite = butSprPushed;
			eveningButIm.sprite = butSpr;
			scr.alPrScr.timeOfDay = 1;
		}
		else if (tod == 2)
		{
			morningButIm.sprite = butSpr;
			afternoonButIm.sprite = butSpr;
			eveningButIm.sprite = butSprPushed;
			scr.alPrScr.timeOfDay = 2;
		}
			
		SetStadium(0);
		scr.alPrScr.isChange0 = true;
	}

	private void SetStadiumAndWeatherButtons()
	{
		SetStadium(0);
		SetTimeOfDay(scr.alPrScr.timeOfDay);
	}
}

