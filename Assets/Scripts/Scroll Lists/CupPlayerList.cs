using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CupItem
{
	public Sprite imageSprite;
	public string Name, Name0;
	public Sprite flag;
	[HideInInspector]
	public int Games, Wins, Ties, Loses, GoalsDiff, Points, indexOf;
	[HideInInspector]
	public Animator anim;
	public int index;
}

public class CupPlayerList : MonoBehaviour 
{
	private Scripts scr;

	public Sprite orangeBoxPoints, orangeBox, orangeBox1, redBox, redBox1;
	public GameObject[] player0;
	public Animator[] anim0;
	public List<CupItem>itemList;
	public Transform contentPanel;
	

	void Start () 
	{
		scr = FindObjectOfType<Scripts> ();

		GameObject[] listImage = GameObject.FindGameObjectsWithTag("CupListImage");

		if (listImage.Length == 2)
			DestroyImmediate(listImage[1]);
		
		Time.timeScale = 1;

		if (scr.alPrScr.cupGames == 3) 
			scr.objCup.infoPanelAnim.SetTrigger("call");

		LoadStats ();
		MyPlayer ();
		ChoosePlayers();
		SetImages();
		Sort ();
		SetAnimators();
		CheckForPlayoff();
		PopulateList ();
	}

	void PopulateList ()
	{
		for (int item = 0; item < 4; item++)
		{
			CupPlayer player = player0[item].GetComponent<CupPlayer>();
			player.playerImage.sprite = itemList[item].imageSprite;
			player.playerName.text = itemList[item].Name0;
			player.flag.sprite = itemList[item].flag;
			player.playerGames.text = "" + itemList[item].Games;
			player.playerWins.text = "" + itemList[item].Wins;
			player.playerTies.text = "" + itemList[item].Ties;
			player.playerLoses.text = "" + itemList[item].Loses;
			player.playerGoalsDiff.text = "" + itemList[item].GoalsDiff;
			player.playerPoints.text = "" + itemList[item].Points;
			player.playerAnim = itemList[item].anim;

			int currCupGames = 0;
			currCupGames = scr.alPrScr.cupGames;

			if (currCupGames == 3) 
			{
				if (item == 2 || item == 3)
				{
					player.showcase.sprite = redBox1;
					player.pGamesImage.sprite = redBox;
					player.pWinsImage.sprite = redBox;
					player.pTiesImage.sprite = redBox;
					player.pLosesImage.sprite = redBox;
					player.pGoalsDiffImage.sprite = redBox;
					player.pPointsImage.sprite = redBox;
				}
			}
			if (itemList[item].imageSprite == scr.alScr.playerSprite)
			{
				if (currCupGames != 3)
				{
					player.showcase.sprite = orangeBox1;
					player.pGamesImage.sprite = orangeBox;
					player.pWinsImage.sprite = orangeBox;
					player.pTiesImage.sprite = orangeBox;
					player.pLosesImage.sprite = orangeBox;
					player.pGoalsDiffImage.sprite = orangeBox;
					player.pPointsImage.sprite = orangeBoxPoints;
				}
				else 
				{
					if (item != 2 && item != 3)
					{
						player.showcase.sprite = orangeBox1;
						player.pGamesImage.sprite = orangeBox;
						player.pWinsImage.sprite = orangeBox;
						player.pTiesImage.sprite = orangeBox;
						player.pLosesImage.sprite = orangeBox;
						player.pGoalsDiffImage.sprite = orangeBox;
						player.pPointsImage.sprite = orangeBoxPoints;
					}
				}
			}
		}
	}
	
	void Sort()
	{
		itemList.Sort(delegate(CupItem x, CupItem y)
			{
				if (y.Points != x.Points)
					return y.Points.CompareTo(x.Points);
				else if (y.GoalsDiff != x.GoalsDiff)
					return y.GoalsDiff.CompareTo(x.GoalsDiff);
				else
					return x.indexOf.CompareTo(y.indexOf);
			});
	}

	void SetAnimators()
	{
		itemList[0].anim = anim0[0];
		itemList[1].anim = anim0[1];
		itemList[2].anim = anim0[2];
		itemList[3].anim = anim0[3];
	}
	
	void MyPlayer()
	{
		if (scr.alPrScr.finishTourn == "CupGoes")
		{
			int k = scr.alPrScr.cupPlayers[0];
			scr.alScr.playerSprite = scr.prScrL.itemList [k].icon;
			scr.alScr.playerName = scr.prScrL.itemList [k].name;
			scr.alScr.playerName0 = scr.prScrL.itemList [k].name0;
			scr.alScr.playerFlag = scr.prScrL.itemList [k].flag;
		}

		itemList [0].imageSprite = scr.alScr.playerSprite;
		itemList [0].Name = scr.alScr.playerName;
		itemList [0].Name0 = scr.alScr.playerName0;
		itemList [0].flag = scr.alScr.playerFlag;
	}
	
	void LoadStats()
	{
		for (int i = 0; i < 4; i++)
		{
			itemList [i].Games = scr.alPrScr.cupGames;
			itemList [i].Wins = scr.alPrScr.cupWins[i];
			itemList [i].Ties = scr.alPrScr.cupTies[i];
			itemList [i].Loses = scr.alPrScr.cupLoses[i];
			itemList [i].GoalsDiff = scr.alPrScr.cupGoalDiff[i];
			itemList [i].Points = scr.alPrScr.cupPoints[i];
			itemList [i].indexOf = i;
		} 
	}

	void CheckForPlayoff()
	{
		bool cupGamesEquals3 = false;

		if (scr.alPrScr.cupGames == 3) 
			cupGamesEquals3 = true;

		if (cupGamesEquals3) 
		{
			if (itemList [0].imageSprite == scr.alScr.playerSprite)
			{
				scr.gM.isPlayoff = true;
				scr.alScr.secondWinnerSprite = itemList [1].imageSprite;
				scr.alScr.secondWinnerName = itemList [1].Name;
				scr.alScr.secondWinnerName0 = itemList [1].Name0;
				scr.alScr.secondWinnerFlag = itemList [1].flag;
			} 
			else if (itemList [1].imageSprite == scr.alScr.playerSprite) 
			{
				scr.gM.isPlayoff = true;
				scr.alScr.secondWinnerSprite = itemList [0].imageSprite;
				scr.alScr.secondWinnerName = itemList [0].Name;
				scr.alScr.secondWinnerName0 = itemList [0].Name0;
				scr.alScr.secondWinnerFlag = itemList [0].flag;
			}
			else
				scr.gM.isPlayoff = false;

			if(scr.gM.isPlayoff)
			{
				for (int i = 0; i < scr.prScrL.itemList.Count; i++)
				{
					if(scr.prScrL.itemList[i].icon == scr.alScr.secondWinnerSprite)
						scr.alPrScr.secondCupWinner = i;
				}
			}
		}
	}

	private void ChoosePlayersInMainLeague()
	{
		int[] random = new int[4];

		if (scr.alPrScr.finishTourn == "CupGoes")
		{
			for (int i = 0; i < 4; i++)
			{
				if (i == 0)
				{
					itemList[i].imageSprite = scr.prScrL.itemList[scr.alPrScr.cupPlayers[i]].icon;
					itemList[i].Name = scr.prScrL.itemList[scr.alPrScr.cupPlayers[i]].name;
					itemList[i].Name0 = scr.prScrL.itemList[scr.alPrScr.cupPlayers[i]].name0;
					itemList[i].flag = scr.prScrL.itemList[scr.alPrScr.cupPlayers[i]].flag;
				}
				else
				{
					itemList[i].imageSprite = scr.prScrL.itemList[scr.alPrScr.cupPlayers[i]].icon;
					itemList[i].Name = scr.prScrL.itemList[scr.alPrScr.cupPlayers[i]].name;
					itemList[i].Name0 = scr.prScrL.itemList[scr.alPrScr.cupPlayers[i]].name0;
					itemList[i].flag = scr.prScrL.itemList[scr.alPrScr.cupPlayers[i]].flag;
				}
			}

			if (scr.alPrScr.fromMenuToTourn == "Yes")
			{
				for (int i = 0; i < scr.alScr.varList.Count; i++)
					scr.alScr.varList[i].index = Mathf.RoundToInt(scr.prScrL.itemList.Count * (scr.alPrScr.trn - 1) / 6 + i);

				for (int k = 0; k < 10; k++)
				{
					for (int j = 0; j < scr.alPrScr.cupPlayers.Length; j++)
					{
						for (int i = 0; i < scr.alScr.varList.Count; i++)
						{
							//Debug.Log("i = " + i + ", j = " + j);
							if (scr.alScr.varList.Count > 6)
							{
								if (scr.alScr.varList[i].index == scr.alPrScr.cupPlayers[j])
									scr.alScr.varList.RemoveAt(i);
							}
						}
					}
					if (scr.alScr.varList.Count == 10)
						break;
				}
				scr.alPrScr.fromMenuToTourn = "No";
			}
		}
		else
		{
			if (scr.alPrScr.cupGames == 0) 
			{
				for (int i = 0; i < scr.alScr.varList.Count; i++) 
					scr.alScr.varList [i].index = Mathf.RoundToInt(scr.prScrL.itemList.Count * (scr.alPrScr.trn - 1) / 6 + i);

				for (int i = 0; i < scr.prScrL.itemList.Count; i++)
				{
					if (scr.prScrL.itemList[i].icon == scr.alScr.playerSprite)
					{
						random [0] = i;
						scr.alPrScr.cupPlayers [0] = random [0];
						for (int j = 0; j < scr.alScr.varList.Count; j++) 
						{
							if (scr.alScr.varList [j].index == random [0])
							{
								scr.alScr.varList.RemoveAt (j);
								break;
							}
							if (scr.prScrL.itemList [scr.alScr.varList [j].index].flag == scr.alScr.playerFlag) 
							{
								scr.alScr.varList.RemoveAt (j);
								break;
							}
						}
						break;
					}
				}

				for (int i = 1; i < 4; i++) 
				{
					int m = (int)(Random.value * (scr.alScr.varList.Count - 1));
					random [i] = scr.alScr.varList [m].index;
					scr.alPrScr.cupPlayers [i] = random [i];
					itemList [i].imageSprite = scr.prScrL.itemList [random [i]].icon;
					itemList [i].Name = scr.prScrL.itemList [random [i]].name;
					itemList [i].Name0 = scr.prScrL.itemList [random [i]].name0;
					itemList [i].flag = scr.prScrL.itemList [random [i]].flag;
					itemList [i].index = random [i];
					scr.alScr.varList.RemoveAt (m);
				}
			}
			scr.alPrScr.finishTourn = "CupGoes";
		}

		scr.alPrScr.isChange0 = true;
	}



	void ChoosePlayers()
	{
		ChoosePlayersInMainLeague();
		scr.alPrScr.isChange0 = true;
	}

	void SetImages()
	{
		for (int i = 0; i < itemList.Count; i++)
		{
			scr.cupLIm.itemList[i].imageSprite = itemList[i].imageSprite;
			scr.cupLIm.itemList[i].Name = itemList[i].Name;
			scr.cupLIm.itemList[i].Name0 = itemList[i].Name0;
			scr.cupLIm.itemList[i].flag = itemList[i].flag;
			scr.cupLIm.itemList[i].index = itemList[i].index;
		}

		if (scr.alPrScr.cupGames < 3)
		{
			scr.alScr.enemySprite = scr.cupLIm.itemList [scr.alPrScr.cupGames + 1].imageSprite;
			scr.alScr.enemyName = scr.cupLIm.itemList [scr.alPrScr.cupGames + 1].Name;
			scr.alScr.enemyName0 = scr.cupLIm.itemList [scr.alPrScr.cupGames + 1].Name0;
			scr.alScr.enemyFlag = scr.cupLIm.itemList [scr.alPrScr.cupGames + 1].flag;
			scr.alPrScr.enemyIndex = scr.cupLIm.itemList [scr.alPrScr.cupGames + 1].index;
		}

		scr.alPrScr.isChange0 = true;
	}
}
