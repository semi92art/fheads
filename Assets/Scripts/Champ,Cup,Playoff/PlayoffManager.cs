using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayoffManager : MonoBehaviour 
{
	public Scripts scr;

	public WinCupPanelScript winCupPanScr;
	public Image prizeIm;
	public Color color1;
	public Sprite orangeBox, blueBox, redBox;
	public Animator[] fillLine;
	public Image[] flag;
	public Text[] name1;
	public Text[] score;
	public Image[] finalists;
	public Text[] questSign;
	public Image[] showcases;
	public Image showcaseQ, showcaseS, showcaseF, showcaseW;
	public Animator[] questSignAnim;

	private int timer, timer1;
	
	void Awake()
	{
		Time.timeScale = 1;

		for (int i = 0; i < finalists.Length; i++)
			finalists[i].color = color1;
		
		if (scr.alPrScr.plfGames == 0)
			scr.plfL.RandomSelect ();
	}
		
	void Update()
	{
		UpdateMainLeague();
	}

	private void UpdateMainLeague()
	{
		if (Input.GetKeyDown(KeyCode.R))
			SceneManager.LoadScene("Playoff");

		timer++;

		if (timer == 1)
		{
			prizeIm.sprite = winCupPanScr.cupWinImage.sprite;

			for (int i = 0; i < 8; i++)
			{
				if (i == 0) 
				{
					int j = scr.alPrScr.playoffPlayers[i];
					finalists[i].sprite = scr.prScrL.itemList[j].icon;
					name1[i].text = scr.prScrL.itemList[j].name0;
					scr.objPlf.flags[i].sprite = scr.prScrL.itemList[j].flag;

					scr.alScr.playerName = scr.prScrL.itemList[scr.alPrScr.playoffPlayers[0]].name;
					scr.alScr.playerName0 = scr.prScrL.itemList[scr.alPrScr.playoffPlayers[0]].name0;
					scr.alScr.playerFlag = scr.prScrL.itemList[scr.alPrScr.playoffPlayers[0]].flag;
					scr.alScr.playerSprite = scr.prScrL.itemList[scr.alPrScr.playoffPlayers[0]].icon;
				}
				else 
				{
					int j = scr.alPrScr.playoffPlayers[i];
					finalists[i].sprite = scr.prScrL.itemList[j].icon;
					name1[i].text = scr.prScrL.itemList[j].name0;
					scr.objPlf.flags[i].sprite = scr.prScrL.itemList[j].flag;
				}
			}
		}

		if (scr.alPrScr.plfGames >= 1) 
		{
			if (scr.alPrScr.plfGoals[0] > scr.alPrScr.plfGoals[1])
				showcases[1].sprite = redBox;
			else
				showcases[0].sprite = redBox;

			if (scr.alPrScr.plfGoals[2] > scr.alPrScr.plfGoals[3])
				showcases[3].sprite = redBox;
			else
				showcases[2].sprite = redBox;

			if (scr.alPrScr.plfGoals[4] > scr.alPrScr.plfGoals[5])
				showcases[5].sprite = redBox;
			else
				showcases[4].sprite = redBox;

			if (scr.alPrScr.plfGoals[6] > scr.alPrScr.plfGoals[7])
				showcases[7].sprite = redBox;
			else
				showcases[6].sprite = redBox;

			if (scr.alPrScr.plfGames >= 2)
			{
				if (scr.alPrScr.plfGoals[8] > scr.alPrScr.plfGoals[9])
					showcases[9].sprite = redBox;
				else
					showcases[8].sprite = redBox;

				if (scr.alPrScr.plfGoals[10] > scr.alPrScr.plfGoals[11])
					showcases[11].sprite = redBox;
				else
					showcases[10].sprite = redBox;

				if (scr.alPrScr.plfGames >= 3)
				{
					if (scr.alPrScr.plfGoals[12] > scr.alPrScr.plfGoals[13])
						showcases[13].sprite = redBox;
					else
						showcases[12].sprite = redBox;
				}
			}
		}
			
		if (scr.alPrScr.plfGames == 0) 
		{
			if(timer == 1)
			{
				for (int i = 0; i < name1.Length; i++)
				{
					if (i < 8)
					{
						name1[i].color = color1;
						flag[i].color = new Color(1, 0.94f, 0.75f, 1);
						scr.objPlf.flags[i].color = Color.white;
					} 
					else 
					{
						name1[i].color = Color.clear;
						flag[i].color = Color.clear;
						scr.objPlf.flags[i].color = Color.clear;
					}
				}

				foreach (var item in score)
					item.color = Color.clear;

				int j = scr.alPrScr.playoffPlayers[1];

				scr.alScr.enemySprite = scr.prScrL.itemList[j].icon;
				scr.alScr.enemyName = scr.prScrL.itemList[j].name;
				scr.alScr.enemyName0 = scr.prScrL.itemList[j].name0;
				scr.alScr.enemyFlag = scr.prScrL.itemList[j].flag;
				scr.alPrScr.enemyIndex = j;
				showcaseQ.sprite = orangeBox;
			}
		} 
		else if (scr.alPrScr.plfGames == 1) 
		{
			if (scr.alPrScr.isW == 1)
			{
				if (Time.timeSinceLevelLoad > 0 && Time.timeSinceLevelLoad <= Time.maximumDeltaTime)
					fillLine[0].SetTrigger("call");
				else if (Time.timeSinceLevelLoad >= 0.5f && Time.timeSinceLevelLoad <= 0.5f + Time.maximumDeltaTime)
					fillLine[2].SetTrigger("call");
				else if (Time.timeSinceLevelLoad >= 1 && Time.timeSinceLevelLoad <= 1 + Time.maximumDeltaTime)
					fillLine[3].SetTrigger("call");
			}

			if (timer == 1)
			{
				for (int i = 0; i < name1.Length; i++)
				{
					if (i < 8)
					{
						name1[i].color = color1;
						flag[i].color = new Color(1, 0.94f, 0.75f, 1);
						scr.objPlf.flags[i].color = Color.white;
					} 
					else 
					{
						if (scr.alPrScr.isW == 1)
						{
							name1[i].color = Color.clear;
							flag[i].color = Color.clear;
							scr.objPlf.flags[i].color = Color.clear;
						}
						else
						{
							name1[i].color = color1;
							flag[i].color = new Color(1, 0.94f, 0.75f, 1);
							scr.objPlf.flags[i].color = Color.white;
						}
					}
				}
				for (int i = 0; i < score.Length; i++)
				{
					if (i < 4)
						score[i].color = color1;
					else
					{
						if (scr.alPrScr.isW == 0)
							score[i].color = color1;
						else
							score[i].color = Color.clear;
					}
				}

				for (int i = 0; i < questSignAnim.Length; i++)
				{
					if (i < 4)
						questSignAnim[i].SetTrigger("call");
					else 
					{
						if (scr.alPrScr.isW == 0)
							questSignAnim[i].SetTrigger("call");
					}

				}
				showcaseQ.sprite = orangeBox;
			}
			if (Time.timeSinceLevelLoad >= 0.5f && Time.timeSinceLevelLoad <= 1.5f)
			{
				for (int i = 0; i < questSign.Length; i++)
				{
					if (i < 4)
						questSign[i].color = Color.clear;
					else
					{
						if (scr.alPrScr.isW == 0)
							questSign[i].color = Color.clear;
					}
				}
				if (scr.alPrScr.isW == 1)
				{
					showcaseQ.sprite = blueBox;
					showcaseS.sprite = orangeBox;
				}
					
				for (int i = 8; i < scr.alPrScr.playoffPlayers.Length; i++)
				{
					int j = scr.alPrScr.playoffPlayers[i];

					if (i == 8)
						finalists [i].sprite = scr.prScrL.itemList [j].icon;
					else if (i > 8 && i < 12) 
						finalists [i].sprite = scr.prScrL.itemList [j].icon;
					else
					{
						if (scr.alPrScr.isW == 0)
							finalists[i].sprite = scr.prScrL.itemList[j].icon;
					}
				}
			}

			for (int i = 8; i < scr.alPrScr.playoffPlayers.Length; i++)
			{
				int j = scr.alPrScr.playoffPlayers[i];

				if (i == 8)
				{
					name1[i].text = scr.prScrL.itemList[j].name0;
					scr.objPlf.flags[i].sprite = scr.prScrL.itemList[j].flag;
				}
				else
				{
					name1[i].text = scr.prScrL.itemList[j].name0;
					scr.objPlf.flags[i].sprite = scr.prScrL.itemList[j].flag;
				}
			}

			if (timer == 1)
			{
				for (int i = 0; i < name1.Length; i++)
				{
					if (i < 12)
					{
						name1[i].color = color1;
						flag[i].color = new Color(1, 0.94f, 0.75f, 1);
						scr.objPlf.flags[i].color = Color.white;
					} 
					else 
					{
						if (scr.alPrScr.isW == 0)
						{
							name1[i].color = color1;
							flag[i].color = new Color(1, 0.94f, 0.75f, 1);
							scr.objPlf.flags[i].color = Color.white;
						}
						else
						{
							name1 [i].color = Color.clear;
							flag [i].color = Color.clear;
							scr.objPlf.flags [i].color = Color.clear;
						}
					}
				}
			}

			int j9 = scr.alPrScr.playoffPlayers[9];
			scr.alScr.enemySprite = scr.prScrL.itemList[j9].icon;
			scr.alScr.enemyName = scr.prScrL.itemList[j9].name;
			scr.alScr.enemyName0 = scr.prScrL.itemList[j9].name0;
			scr.alScr.enemyFlag = scr.prScrL.itemList[j9].flag;
			scr.alPrScr.enemyIndex = j9;
		} 
		else if (scr.alPrScr.plfGames >= 2) 
		{
			if (timer == 1)
			{
				for (int i = 0; i < 4; i++)
				{
					fillLine[0].SetTrigger("call1");
					fillLine[1].SetTrigger("call1");
					fillLine[2].SetTrigger("call1");
					fillLine[3].SetTrigger("call1");
					questSign[i].color = Color.clear;

					int j = scr.alPrScr.playoffPlayers[i + 8];

					if (i == 0)
					{
						finalists[i + 8].sprite = scr.prScrL.itemList[j].icon;
						name1[i + 8].text = scr.prScrL.itemList[j].name0;
						scr.objPlf.flags[i + 8].sprite = scr.prScrL.itemList[j].flag;
					}
					else 
					{
						finalists[i + 8].sprite = scr.prScrL.itemList[j].icon;
						name1[i + 8].text = scr.prScrL.itemList[j].name0;
						scr.objPlf.flags[i + 8].sprite = scr.prScrL.itemList[j].flag;
					}
				}
			}

			if (scr.alPrScr.plfGames == 2) 
			{
				if (scr.alPrScr.isW == 1)
				{
					if (Time.timeSinceLevelLoad >= 0 && Time.timeSinceLevelLoad <= Time.maximumDeltaTime)
						fillLine[4].SetTrigger("call");
					else if (Time.timeSinceLevelLoad >= 0.5f && Time.timeSinceLevelLoad <= 0.5f + Time.maximumDeltaTime)
						fillLine[6].SetTrigger("call");
					else if (Time.timeSinceLevelLoad >= 1 && Time.timeSinceLevelLoad <= 1 + Time.maximumDeltaTime)
						fillLine[7].SetTrigger("call");
				}

				if (timer == 1)
				{
					for (int i = 0; i < name1.Length; i++)
					{
						if (i < 12)
						{
							name1[i].color = color1;
							flag[i].color = new Color(1, 0.94f, 0.75f, 1);
							scr.objPlf.flags[i].color = Color.white;
						} 
						else 
						{
							if (scr.alPrScr.isW == 1)
							{
								name1[i].color = Color.clear;
								flag[i].color = Color.clear;
								scr.objPlf.flags[i].color = Color.clear;
							}
							else
							{
								name1[i].color = color1;
								flag[i].color = new Color(1, 0.94f, 0.75f, 1);
								scr.objPlf.flags[i].color = Color.white;
							}
						}
					}

					for (int i = 0; i < score.Length; i++)
					{
						if (i < 6)
							score[i].color = color1;
						else
						{
							if (scr.alPrScr.isW == 0)
								score[i].color = color1;
							else
								score[i].color = Color.clear;
						}
					}

					for (int i = 4; i < questSignAnim.Length; i++)
					{
						if (i < 6)
							questSignAnim[i].SetTrigger("call");
						else
						{
							if (scr.alPrScr.isW == 0)
								questSignAnim[i].SetTrigger("call");
						}
					}
					showcaseS.sprite = orangeBox;
				}

				if (Time.timeSinceLevelLoad >= 1 && Time.timeSinceLevelLoad <= 1.5f)
				{
					for (int i = 0; i < name1.Length; i++)
					{
						if (i < 14)
						{
							name1[i].color = color1;
							flag[i].color = new Color(1, 0.94f, 0.75f, 1);
							scr.objPlf.flags[i].color = Color.white;
						} 
						else 
						{
							if (scr.alPrScr.isW == 1)
							{
								name1[i].color = Color.clear;
								flag[i].color = Color.clear;
								scr.objPlf.flags[i].color = Color.clear;
							}
							else
							{
								name1[i].color = color1;
								flag[i].color = new Color(1, 0.94f, 0.75f, 1);
								scr.objPlf.flags[i].color = Color.white;
							}
						}
					}
				}

				int j12 = scr.alPrScr.playoffPlayers[12];
				name1[12].text = scr.prScrL.itemList[j12].name0;
				scr.objPlf.flags[12].sprite = scr.prScrL.itemList[j12].flag;

				int j13 = scr.alPrScr.playoffPlayers[13];
				name1[13].text = scr.prScrL.itemList[j13].name0;
				scr.objPlf.flags[13].sprite = scr.prScrL.itemList[j13].flag;

				int j14 = scr.alPrScr.playoffPlayers[14];

				if (scr.alPrScr.isW == 0)
				{
					name1[14].text = scr.prScrL.itemList[j14].name0;
					scr.objPlf.flags[14].sprite = scr.prScrL.itemList[j14].flag;
				}

				if (Time.timeSinceLevelLoad >= 0.5f && Time.timeSinceLevelLoad <= 1.5f)
				{
					if (scr.alPrScr.isW == 0)
						finalists[14].sprite = scr.prScrL.itemList[j14].icon;


					for (int i = 4; i < questSign.Length; i++)
					{
						if (i < 6)
							questSign[i].color = Color.clear;
						else
						{
							if (scr.alPrScr.isW == 0)
								questSign[i].color = Color.clear;
						}
					}
					if (scr.alPrScr.isW == 1)
					{
						if (Time.timeSinceLevelLoad < 1)
							showcaseS.sprite = orangeBox;
						else if (Time.timeSinceLevelLoad >= 1)
						{
							showcaseF.sprite = orangeBox;
							showcaseS.sprite = blueBox;
						}
					} 
					else 
						showcaseS.sprite = orangeBox;

					finalists[12].sprite = scr.prScrL.itemList[j12].icon;
					finalists[13].sprite = scr.prScrL.itemList[j13].icon;

					scr.alScr.enemySprite = scr.prScrL.itemList[j13].icon;
					scr.alScr.enemyName = scr.prScrL.itemList[j13].name;
					scr.alScr.enemyName0 = scr.prScrL.itemList[j13].name0;
					scr.alScr.enemyFlag = scr.prScrL.itemList[j13].flag;
					scr.alPrScr.enemyIndex = j13;
				}
			} 
			else if (scr.alPrScr.plfGames == 3) 
			{
				if (timer == 1)
				{
					prizeIm.color = Color.white;
					fillLine[4].SetTrigger("call1");
					fillLine[5].SetTrigger("call1");
					fillLine[6].SetTrigger("call1");
					fillLine[7].SetTrigger("call1");

					for (int i = 0; i < name1.Length; i++)
					{
						if (i < 14)
						{
							name1[i].color = color1;
							flag[i].color = new Color(1, 0.94f, 0.75f, 1);
							scr.objPlf.flags[i].color = Color.white;
						} 
						else 
						{
							if (scr.alPrScr.isW == 1)
							{
								name1[i].color = Color.clear;
								flag[i].color = Color.clear;
								scr.objPlf.flags[i].color = Color.clear;
							}
							else
							{
								name1[i].color = Color.clear;
								flag[i].color = Color.clear;
								scr.objPlf.flags[i].color = Color.clear;
							}
						}
					}

					foreach (var item in score)
						item.color = color1;

					questSignAnim[6].SetTrigger("call");
					questSign[4].color = Color.clear;
					questSign[5].color = Color.clear;
				}

				if (scr.alPrScr.isW == 1)
				{
					if (Time.timeSinceLevelLoad >= 0 && Time.timeSinceLevelLoad <= Time.maximumDeltaTime)
						fillLine[8].SetTrigger("call");
					else if (Time.timeSinceLevelLoad >= 0.5f && Time.timeSinceLevelLoad <= 0.5f + Time.maximumDeltaTime)
						fillLine[10].SetTrigger("call");
				}

				int j14 = scr.alPrScr.playoffPlayers[14];
				name1[14].text = scr.prScrL.itemList[j14].name0;
				scr.objPlf.flags[14].sprite = scr.prScrL.itemList[j14].flag;


				scr.objPlf.flags[14].color = Color.white;

				foreach (var item in name1)
					item.color = color1;

				foreach (var item in flag)
					item.color = color1;

				if (Time.timeSinceLevelLoad < 0.5f)
				{
					showcaseF.sprite = orangeBox;
					int j12 = scr.alPrScr.playoffPlayers[12];

					finalists[12].sprite = scr.prScrL.itemList[j12].icon;
					name1[12].text = scr.prScrL.itemList[j12].name0;
					scr.objPlf.flags[12].sprite = scr.prScrL.itemList[j12].flag;

						
					int j13 = scr.alPrScr.playoffPlayers[13];
					finalists[13].sprite = scr.prScrL.itemList[j13].icon;
					name1[13].text = scr.prScrL.itemList[j13].name0;
					scr.objPlf.flags[13].sprite = scr.prScrL.itemList[j13].flag;
				} 
				else if (Time.timeSinceLevelLoad >= 0.5f  && Time.timeSinceLevelLoad <= 1.5f)
				{
					questSign[6].color = Color.clear;
					finalists[14].sprite = scr.prScrL.itemList[j14].icon;

					if (scr.alPrScr.isW == 1)
					{
						if (Time.timeSinceLevelLoad < 1) 
							showcaseF.sprite = orangeBox;
						else if (Time.timeSinceLevelLoad >= 1)
						{
							showcaseW.sprite = orangeBox;
							showcaseF.sprite = blueBox;
						}
					} 
					else 
						showcaseF.sprite = orangeBox;
				}
			}
		}

		if (timer == 1)
		{
			score[0].text = scr.alPrScr.plfGoals[0] + ":" + scr.alPrScr.plfGoals[1];
			score[1].text = scr.alPrScr.plfGoals[2] + ":" + scr.alPrScr.plfGoals[3];
			score[2].text = scr.alPrScr.plfGoals[4] + ":" + scr.alPrScr.plfGoals[5];
			score[3].text = scr.alPrScr.plfGoals[6] + ":" + scr.alPrScr.plfGoals[7];

			score[4].text = scr.alPrScr.plfGoals[8] + ":" + scr.alPrScr.plfGoals[9];
			score[5].text = scr.alPrScr.plfGoals[10] + ":" + scr.alPrScr.plfGoals[11];
			score[6].text = scr.alPrScr.plfGoals[12] + ":" + scr.alPrScr.plfGoals[13];
		}

		scr.alPrScr.isChange0 = true;
	}
} 
