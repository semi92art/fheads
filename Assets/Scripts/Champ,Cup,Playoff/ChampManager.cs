using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ChampManager : MonoBehaviour
{
	public Scripts scr;

	public float animSpeed;
	public int var1;
	public Animator quitPanelAnim;
	public Transform contentPanel;
	public GameObject[] player0;
	public Sprite orangeBox1, orangeBox;

	[HideInInspector]
	public bool isSetImages;
	
	private float timer;
	private int j;
	
	void Start()
	{
		if (scr.alPrScr.champGames == 0)
			scr.gM.RandomResultsInChamp();
			
		FirstFunction();
		ChoosePlayers ();
		SetAllPoints ();
		SetImages();
		isEndOfChamp ();
		scr.infM.SetInfoPanel();
		Sort ();
		PopulateList ();
		scr.alPrScr.isChange0 = true;
	}

	void Update()
	{
		if (Time.timeSinceLevelLoad > 0.3f && Time.timeSinceLevelLoad < 4)
		{
			for (int i = 1; i < player0.Length; i++)
			{
				RectTransform recTr = player0[i].GetComponent<RectTransform>();
				float posZ = recTr.localPosition.z;
				player0[i].GetComponent<RectTransform>().anchoredPosition = 
					Vector3.Lerp(recTr.anchoredPosition,
	            	new Vector3 (0, -10 - 44 * i, posZ), 
					Time.deltaTime * (animSpeed - Mathf.Pow(i, 1.8f) * 0.04f));
			}
		}
	}

	void FirstFunction()
	{
		Time.timeScale = 1;

		if (scr.alPrScr.isSecTour == 0)
			scr.alPrScr.champGames1 = scr.alPrScr.champGames;
		else if (scr.alPrScr.isSecTour == 1) 
			scr.alPrScr.champGames1 = scr.alPrScr.champGames + scr.alPrScr.champPlayers.Length - 1;
		else 
			scr.alPrScr.champGames1 = 20;
	}

	void ChoosePlayers()
	{
		if (scr.alPrScr.finishTourn != "ChampGoes")
		{
			scr.alPrScr.champGames = 0;
			SetChampListIm();
			scr.alPrScr.finishTourn = "ChampGoes";
		} else 
			SetChampListIm();

		scr.alPrScr.isChange0 = true;
	}

	private void SetChampListIm()
	{
		int first = 10 * var1;


		int res = scr.alScr.reservePlayers[var1];

		if (scr.alPrScr.finishTourn != "ChampGoes")
			scr.alPrScr.champPlayers[0] = scr.alPrScr.playerIndex;

		for (int i = 0; i < 10; i++)
		{
			if (i == 0)
			{
				int j0 = scr.alPrScr.champPlayers[0];

				scr.alScr.playerSprite = scr.prScrL.itemList[j0].icon;
				scr.alScr.playerName = scr.prScrL.itemList[j0].name;
				scr.alScr.playerName0 = scr.prScrL.itemList[j0].name0;
				scr.alScr.playerFlag = scr.prScrL.itemList[j0].flag;

				scr.chLIm.itemList [0].imageSprite = scr.alScr.playerSprite;
				scr.chLIm.itemList [0].Name = scr.alScr.playerName;
				scr.chLIm.itemList [0].Name0 = scr.alScr.playerName0;
				scr.chLIm.itemList [0].flag = scr.alScr.playerFlag;

				scr.alPrScr.playerIndex = j0;
				scr.alPrScr.fromMenuToTourn = "No";
			} 
			else 
			{
				if (scr.alPrScr.finishTourn != "ChampGoes")
				{
					scr.alPrScr.champPlayers[i] = first + i;

					if (scr.prScrL.itemList [scr.alPrScr.champPlayers[i]].icon == scr.alScr.playerSprite ||
						scr.prScrL.itemList [scr.alPrScr.champPlayers[i]].flag == scr.alScr.playerFlag)
					{
						scr.alPrScr.champPlayers[i] = res;
					}
				}

				j = scr.alPrScr.champPlayers[i];

				if (scr.prScrL.itemList [j].icon != scr.alScr.playerSprite)
				{
					scr.chLIm.itemList [i].imageSprite = scr.prScrL.itemList [j].icon;
					scr.chLIm.itemList [i].Name = scr.prScrL.itemList [j].name;
					scr.chLIm.itemList [i].Name0 = scr.prScrL.itemList [j].name0;
					scr.chLIm.itemList [i].flag = scr.prScrL.itemList [j].flag;
					scr.chLIm.itemList [i].summSkill = scr.prScrL.itemList [j].summSkill;
				} 
				else 
				{
					scr.chLIm.itemList [i].imageSprite = scr.prScrL.itemList [res].icon;
					scr.chLIm.itemList [i].Name = scr.prScrL.itemList [res].name;
					scr.chLIm.itemList [i].Name0 = scr.prScrL.itemList [res].name0;
					scr.chLIm.itemList [i].flag = scr.prScrL.itemList [res].flag;
					scr.chLIm.itemList [i].summSkill = scr.prScrL.itemList [res].summSkill;
				}
			}
		}

		if (scr.alPrScr.champGames == 0)
			scr.gM.SetEnemy();
			
		scr.alPrScr.isChange0 = true;
	}
	
	void SetAllPoints()
	{
		for (int i = 0; i < 10; i++)
		{
			scr.chLIm.itemList[i].Wins = scr.alPrScr.chWins[i];
			scr.chLIm.itemList[i].Ties = scr.alPrScr.chTies[i];
			scr.chLIm.itemList[i].Loses = scr.alPrScr.chLoses[i];
			scr.chLIm.itemList[i].GoalsDiff = scr.alPrScr.chGoalDiff[i];
			scr.chLIm.itemList[i].Points = scr.alPrScr.chPoints[i];
		}
	}

	void SetImages()
	{
		for (int i = 0; i < scr.chLIm.itemList.Count; i++)
		{
			scr.chL.itemList[i].currentGoals = scr.chLIm.itemList[i].currentGoals;
			scr.chL.itemList[i].flag 		 = scr.chLIm.itemList[i].flag;
			scr.chL.itemList[i].GoalsDiff 	 = scr.chLIm.itemList[i].GoalsDiff;
			scr.chL.itemList[i].imageSprite  = scr.chLIm.itemList[i].imageSprite;
			scr.chL.itemList[i].Loses 		 = scr.chLIm.itemList[i].Loses;
			scr.chL.itemList[i].Name 		 = scr.chLIm.itemList[i].Name;
			scr.chL.itemList[i].Name0 		 = scr.chLIm.itemList[i].Name0;
			scr.chL.itemList[i].Points 		 = scr.chLIm.itemList[i].Points;
			scr.chL.itemList[i].rand 		 = scr.chLIm.itemList[i].rand;
			scr.chL.itemList[i].summSkill 	 = scr.chLIm.itemList[i].summSkill;
			scr.chL.itemList[i].Ties 		 = scr.chLIm.itemList[i].Ties;
			scr.chL.itemList[i].Wins 		 = scr.chLIm.itemList[i].Wins;
		}
		isSetImages = true;
	}
	
	void isEndOfChamp()
	{
		if (scr.alPrScr.champGames == 0 && scr.alPrScr.isSecTour == 2)
			scr.chLIm.endOfChamp = true;

		for (int i = 0; i < 10; i++)
			scr.infM.player2[i] = scr.alPrScr.pl2InChamp[i];
	}
	
	void Sort()
	{
		scr.chL.itemList.Sort(delegate(ChampItem x, ChampItem y) {
			if (y.Points  !=  x.Points) {
				return y.Points.CompareTo(x.Points);
			} else if (y.GoalsDiff  !=  x.GoalsDiff){
				return y.GoalsDiff.CompareTo(x.GoalsDiff);
			} else{
				return y.indexOf.CompareTo(x.indexOf);
			}
		});
	}
	
	void PopulateList () 
	{
		for (int i = 0; i < player0.Length; i++)
		{
			ChampPlayer player = player0[i].GetComponent<ChampPlayer>();
			player.playerImage.sprite = scr.chL.itemList[i].imageSprite;
			player.playerName.text = scr.chL.itemList[i].Name0;
			player.playerWins.text = "" + scr.chL.itemList[i].Wins;
			player.playerTies.text = "" + scr.chL.itemList[i].Ties;
			player.playerLoses.text = "" + scr.chL.itemList[i].Loses;
			player.playerGoalsDiff.text = "" + scr.chL.itemList[i].GoalsDiff;
			player.playerPoints.text = "" + scr.chL.itemList[i].Points;
			player.flag.sprite = scr.chL.itemList[i].flag;
			player.playerGames.text = "" + scr.alPrScr.champGames1;

			
			if (scr.chL.itemList[i].imageSprite == scr.alScr.playerSprite)
			{
				player.showcase.sprite = orangeBox1;
				player.pGamesImage.sprite = orangeBox;
				player.pWinsImage.sprite = orangeBox;
				player.pTiesImage.sprite = orangeBox;
				player.pLosesImage.sprite = orangeBox;
				player.pGoalsDiffImage.sprite = orangeBox;
				player.pPointsImage.sprite = orangeBox;
			}
		}
	}
	
	public void ChooseLastChampListImage()
	{
		GameObject[] chLIm1 = GameObject.FindGameObjectsWithTag ("ChampListImage");

		for (int i = 1; i <= chLIm1.Length - 1; i++) 
			DestroyImmediate(chLIm1[i]);
	}
	
	public void ChooseLastChampList()
	{
		GameObject[] chL1 = GameObject.FindGameObjectsWithTag ("ChampList");

		for (int i = 1; i <= chL1.Length - 1; i++) 
			DestroyImmediate(chL1[i]);
	}
}
