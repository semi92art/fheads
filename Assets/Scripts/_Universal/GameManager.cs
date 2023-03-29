using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR_WIN || UNITY_EDITOR_64 || UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour 
{
	public Scripts scr;
	
	public bool exitBool;
	[HideInInspector]
	public bool isPlayoff;
	public bool quitPanel, pauseInLevel, pauseOptions, gamePaused;


	private int loadPanTimer = -2;
	private int timer;
	private int[,] genRoundRobin;
	private bool isSame2;
	private GameObject loadingPanel;

	[System.Serializable]
	public class Bools
	{
		public bool mainMenu, menuSinglePlayer, menuCareer, 
		menuFreePlay, menuOptions, menuProfile,
		menuProfilePlayers, menuProfileStadiums,
		menuLottery;
	}

	[Header("Loaded menues in main scene MainMenu")]
	public Bools MainMenuBools;

	void Awake()
	{
		ChooseLastObject("AllLevelScript");
		AddItemsToVarList();
		ChooseLastBallsMaterials ();
		ChooseLastObject("BluetoothScr");
		ChooseLastObject("MultiplayerScr");

		switch (SceneManager.GetActiveScene().name) 
		{
		case "MainMenu":
			scr.alPrScr.isLeaderboardGame = 0;
			// scr.alPrScr.multiplayer = 0;
			scr.alPrScr.bluetooth = 0;

			if(scr.alPrScr.finishTourn == "")
				scr.alPrScr.finishTourn = "Finished";
			
			Time.timeScale = 1;

			if (scr.alPrScr.finishTourn != "CupGoes" &&
				scr.alPrScr.finishTourn != "PlayoffGoes" &&
				scr.alPrScr.finishTourn != "ChampGoes")
			{
				SetCupDefaultsMainLeague ();
			}

			scr.alPrScr.freePlay = 0;
			MainMenuBools.mainMenu = true;
			DestroyImmediate (GameObject.Find ("ChampList"));
			DestroyImmediate (GameObject.Find ("ChampListImage"));

			break;
		case "Playoff":
			loadingPanel = scr.objPlf.loadingPanelAnim.gameObject;

			break;
		case "Cup":
			loadingPanel = scr.objCup.loadingPanelAnim.gameObject;

			break;
		case "Level":
			loadingPanel = scr.objLev.loadingPanel;

			break;
		case "Championship":
			loadingPanel = scr.objCh.loadingPanelAnim.gameObject;

			break;
		}
	}

	void Update()
	{
		switch (SceneManager.GetActiveScene().name) 
		{
		case "MainMenu":
			if(Input.GetKey(KeyCode.R))
				SceneManager.LoadScene("MainMenu");
			
			if (Input.GetKeyUp (KeyCode.Escape))
			{
				if (MainMenuBools.mainMenu)
					ExitMenu ();
				else if (MainMenuBools.menuSinglePlayer)
				{
					MenuSinglePlayerBack ();
					MainMenuOn();
					// scr.leaderB.gameObject.SetActive(true);
				} 
				else if (MainMenuBools.menuProfile ||
					MainMenuBools.menuFreePlay ||
					MainMenuBools.menuCareer) 
				{
					CallBackTrophyPanel("back");
					DependentMenuBack ();
					TopPanelBack ();
					LogoOn ();

					if (MainMenuBools.menuProfile)
					{
						// scr.leaderB.gameObject.SetActive(true);
					}
				} 
				else if (MainMenuBools.menuOptions)
				{
					OptionsBack ();
					LogoOn();
					// scr.leaderB.gameObject.SetActive(true);
				}
			}
			
			break;
		case "Playoff":
			if (Input.GetKeyDown (KeyCode.Escape))
			{
				if (!quitPanel) QuitPanelCall();
				else QuitPanelBack();
			}

			break;
		case "Cup":
			if (Input.GetKeyDown (KeyCode.Escape))
			{
				if (!quitPanel) QuitPanelCall();
				else QuitPanelBack();
			}

			break;
		case "Level":
			if(loadPanTimer >= 0) 
				loadPanTimer++;

			switch (loadPanTimer) 
			{
			case 1:
				scr.objLev.loadingPanel.SetActive(true);
				scr.objLev.loadPanelText.text = "loading...";
				scr.objLev.loadingPanel.GetComponent<Animator>().SetTrigger("call");
				scr.congrPan.mainCanvas.enabled = true;
				break;
			case 2:
				if (scr.alPrScr.freePlay == 1 || scr.alPrScr.isLeaderboardGame == 1)
				{
					SceneManager.LoadScene("MainMenu");
					scr.alPrScr.isLeaderboardGame = 0;
					scr.alPrScr.isChange0 = true;
				} 
				else 
				{
					string finT = "";
					finT = scr.alPrScr.finishTourn;

					switch (finT)
					{
					case "CupGoes":
						SceneManager.LoadScene("Cup"); 
						break;
					case "ChampGoes":
						SceneManager.LoadScene("Championship"); 
						break;
					case "PlayoffGoes":
						SceneManager.LoadScene("Playoff"); 
						break;
					case "Finshed":
						SceneManager.LoadScene("MainMenu"); 
						break;
					case "":
						SceneManager.LoadScene("MainMenu"); 
						break;
					}
				}
				break;
			case -2:
				loadPanTimer ++;
				if (scr.objLev.loadingPanel.gameObject.activeSelf)
					scr.objLev.loadingPanel.GetComponent<Animator>().SetTrigger("call");
				
				break;
			}

			if (Time.timeSinceLevelLoad <= .1f && Time.timeScale != 1)
				Time.timeScale = 1;
			
			if (Input.GetKeyUp (KeyCode.Escape)) 
			{
				if (scr.tM.isBetweenTimes && Time.timeScale == 0)
					scr.tM.CallBackBetweenTimesPanel();
				else
				{
					if (!gamePaused && scr.pMov.startGame)
					{
						if (!pauseInLevel && !pauseOptions)
						{
							Pause();
							scr.objLev.mainCanvas.enabled = true;
						}	
						else if (pauseInLevel && !pauseOptions) 
						{
							switch (scr.objLev.quitPanel.activeSelf)
							{
							case true:
								PauseQuitBack();
								scr.objLev.resumeButton.SetActive(true);
								scr.objLev.optionsButton.SetActive(true);
								scr.objLev.exitButton.SetActive(true);
								if (scr.alPrScr.freePlay == 1) 
									scr.objLev.restartButton.SetActive(true);
								
								break;
							case false:
								PauseBack();
								scr.objLev.resumeButton.SetActive(false);
								scr.objLev.optionsButton.SetActive(false);
								scr.objLev.exitButton.SetActive(false);
								if (scr.alPrScr.freePlay == 1) 
									scr.objLev.restartButton.SetActive(false);

								scr.objLev.mainCanvas.enabled = false;
								break;
							}
						} 
						else if (!pauseInLevel && pauseOptions)
							PauseOptionsBack();
					}
				}
			}

			break;
		case "Championship":
			if(Input.GetKey(KeyCode.R)) SceneManager.LoadScene("Championship");

			if (Input.GetKeyDown (KeyCode.Escape)) 
			{
				if (!quitPanel) QuitPanelCall();
				else QuitPanelBack();
			}

			break;
		}
	}

	public void SetActiveRestartButton()
	{
		if (scr.alPrScr.freePlay == 1) 
			scr.objLev.restartButton.SetActive(true);
	}

	public void CallBackTrophyPanel(string trigger)
	{
		if (!MainMenuBools.menuCareer &&
			!MainMenuBools.menuProfile &&
			!MainMenuBools.menuFreePlay)
				GameObject.Find("TrophyPanel").GetComponent<Animator>().SetTrigger(trigger);
	}

	public void DependentMenuBack() 
	{
		if (MainMenuBools.menuCareer) 
		{
			scr.objM.menuCareerAnim.SetTrigger ("back");
			MenuSinglePlayerOn ();
			MainMenuBools.menuCareer = false;
			scr.objM.menuCareerAnim.gameObject.SetActive (false);
		}
		else if (MainMenuBools.menuProfile && MainMenuBools.menuFreePlay)
		{
			scr.dblScr.SetPlayerNotFP ();
			scr.objM.freePlayAnim.SetTrigger ("fpback");
			MenuSinglePlayerOn ();
			scr.objM.currProfileAnim.SetTrigger ("back");
			MainMenuBools.menuFreePlay = false;
			scr.objM.freePlayAnim.gameObject.SetActive (false);

			scr.objM.profileLeagueText.gameObject.SetActive (true);
			scr.objM.menuProfAnim.gameObject.SetActive (true);
			scr.objM.menuProfAnim.SetTrigger ("back");
			MainMenuBools.menuProfile = false;

			scr.objM.profilePlayersAnim.gameObject.SetActive (false);
			scr.objM.profileStadiumButton.SetActive (true);
			scr.objM.profilePlayersButton.SetActive (true);
			scr.currPrPan.enemy.SetActive (false);
		} 
		else if (MainMenuBools.menuProfile && !MainMenuBools.menuFreePlay)
		{
			// scr.leaderB.gameObject.SetActive(true);
			scr.objM.profileLeagueText.gameObject.SetActive (true);
			scr.objM.menuProfAnim.gameObject.SetActive (true);
			scr.objM.menuProfAnim.SetTrigger ("back");
			MainMenuOn ();
			MainMenuBools.menuProfile = false;
		} 

		if (MainMenuBools.menuProfilePlayers)
			MainMenuBools.menuProfilePlayers = false;	 
		else if (MainMenuBools.menuProfileStadiums)
		{
			scr.objM.profStadiumsAnim.SetBool ("call", false);
			MainMenuBools.menuProfileStadiums = false;
		}
	}

	public void GoToMainMenu()
	{
		for (int i = 0; i <= 1; i++)
		{
			switch (i) 
			{
			case 0:
				loadingPanel.SetActive(true);
				loadingPanel.GetComponent<Animator>().SetTrigger("call");
				break;
			case 1:
				SceneManager.LoadScene("MainMenu");
				break;
			}
		}
	}
	
	public void GoToMenuNewGame (RectTransform transformQuestionPanel)
	{
		transformQuestionPanel.anchoredPosition = new Vector3 (transformQuestionPanel.anchoredPosition.x, -300, 0);
	}

	public void IsInPause()
	{
		exitBool = true;
		Time.timeScale = 1;
	}

	public void GoToMenu()
	{
		// If loadPanTimer = 0, loadPaneTimer++ and then loads needed menu.
		loadPanTimer = 0;
	}
	
	public void ContinueCup()
	{
		Time.timeScale = 1;

		if (scr.alPrScr.freePlay == 1)
		{
			
		}
		else
		{
			if (scr.alPrScr.isLeaderboardGame == 1)
			{
				if (Score.score > Score.score1)
					scr.alPrScr.winsInARow ++;
			}
			else
			{
				switch (scr.alPrScr.finishTourn)
				{
				case "CupGoes":
					scr.alPrScr.cupGames++;
					if (TimeManager.resultOfGame == 1)
					{
						scr.alPrScr.cupWins[0]++;
						scr.alPrScr.cupGoalDiff[0]+=(Score.score - Score.score1);
						scr.alPrScr.cupPoints[0]+=3;
						scr.alPrScr.cupLoses[scr.alPrScr.cupGames]++;
						scr.alPrScr.cupGoalDiff[scr.alPrScr.cupGames]-=(Score.score - Score.score1);
					} 
					else if (TimeManager.resultOfGame == 2)
					{
						scr.alPrScr.cupTies[0]++;
						scr.alPrScr.cupPoints[0]++;
						scr.alPrScr.cupTies[scr.alPrScr.cupGames]++;
						scr.alPrScr.cupPoints[scr.alPrScr.cupGames]++;
					}
					else if (TimeManager.resultOfGame == 3)
					{
						scr.alPrScr.cupLoses[0]++;
						scr.alPrScr.cupGoalDiff[0]+=(Score.score - Score.score1);
						scr.alPrScr.cupWins[scr.alPrScr.cupGames]++;
						scr.alPrScr.cupGoalDiff[scr.alPrScr.cupGames]-=(Score.score - Score.score1);
						scr.alPrScr.cupPoints[scr.alPrScr.cupGames]+=3;
					}

					RandomResultsInCup ();

					if (scr.alPrScr.cupGames == 3) 
					{
						if (Score.score > Score.score1) 
							scr.alPrScr.isW = 1;
						else 
							scr.alPrScr.isW = 0;
					}

					break;
				case "PlayoffGoes":
					scr.alPrScr.plfGames ++;

					if (Score.score > Score.score1) scr.alPrScr.isW = 1;
					else scr.alPrScr.isW = 0;

					switch (scr.alPrScr.plfGames)
					{
					case 1:
						scr.alPrScr.plfGoals[0] = Score.score;
						scr.alPrScr.plfGoals[1] = Score.score1;

						for (int i = 0; i < 3; i++)
						{
							int rW = scr.plfLS.randWin[i];
							int rG = scr.plfLS.randGoals[i];
							int rD = scr.plfLS.randDiff[i];
							scr.alPrScr.plfGoals[2*(i + 1)] = rG + rD * (1 - rW);
							scr.alPrScr.plfGoals[2*(i + 1) + 1] = rG + rD * rW;
							scr.alPrScr.playoffPlayers[i + 9] = scr.alPrScr.playoffPlayers[2*(i + 1) + rW];
						}

						switch (scr.alPrScr.isW)
						{
						case 0:
							scr.alPrScr.playoffPlayers[8] = scr.alPrScr.playoffPlayers[1];

							int rand01 = Mathf.CeilToInt(2 * UnityEngine.Random.value);
							int rand02 = rand01 + Mathf.CeilToInt(1 + 4 * UnityEngine.Random.value);

							int rand11 = Mathf.CeilToInt(2 * UnityEngine.Random.value);
							int rand12 = rand11 + Mathf.CeilToInt(1 + 4 * UnityEngine.Random.value);

							int rand21 = Mathf.CeilToInt(2 * UnityEngine.Random.value);
							int rand22 = rand21 + Mathf.CeilToInt(1 + 4 * UnityEngine.Random.value);

							float rand1 = UnityEngine.Random.value;

							if (rand1 < 0.5f)
							{
								scr.alPrScr.playoffPlayers[12] = scr.alPrScr.playoffPlayers[8];
								scr.alPrScr.plfGoals[8] = rand02;
								scr.alPrScr.plfGoals[9] = rand01;
							}	
							else
							{
								scr.alPrScr.playoffPlayers[12] = scr.alPrScr.playoffPlayers[9];
								scr.alPrScr.plfGoals[8] = rand01;
								scr.alPrScr.plfGoals[9] = rand02;
							}

							float rand2 = UnityEngine.Random.value;

							if (rand2 < 0.5f)
							{
								scr.alPrScr.playoffPlayers[13] = scr.alPrScr.playoffPlayers[10];
								scr.alPrScr.plfGoals[10] = rand12;
								scr.alPrScr.plfGoals[11] = rand11;
							}
							else
							{
								scr.alPrScr.playoffPlayers[13] = scr.alPrScr.playoffPlayers[11];
								scr.alPrScr.plfGoals[10] = rand11;
								scr.alPrScr.plfGoals[11] = rand12;
							}

							float rand3 = UnityEngine.Random.value;

							if (rand3 < 0.5f)
							{
								scr.alPrScr.playoffPlayers[14] = scr.alPrScr.playoffPlayers[12];
								scr.alPrScr.plfGoals[12] = rand22;
								scr.alPrScr.plfGoals[13] = rand21;
							}
							else
							{
								scr.alPrScr.playoffPlayers[14] = scr.alPrScr.playoffPlayers[13];
								scr.alPrScr.plfGoals[12] = rand21;
								scr.alPrScr.plfGoals[13] = rand22;
							}

							break;
						case 1:
							scr.alPrScr.playoffPlayers[8] = scr.alPrScr.playoffPlayers[0];
							break;
						}
						break;
					case 2:
						scr.alPrScr.plfGoals[8] = Score.score;
						scr.alPrScr.plfGoals[9] = Score.score1;
						int rW1 = scr.plfLS.randWin[3];
						int rG1 = scr.plfLS.randGoals[3];
						int rD1 = scr.plfLS.randDiff[3];
						scr.alPrScr.plfGoals[10] = rG1 + rD1 * (1 - rW1);
						scr.alPrScr.plfGoals[11] = rG1 + rD1 * rW1;
						scr.alPrScr.playoffPlayers[13] = scr.alPrScr.playoffPlayers[rW1 + 10];

						float rand4 = UnityEngine.Random.value;
						int rand41 = Mathf.CeilToInt(2 * UnityEngine.Random.value);
						int rand42 = rand41 + Mathf.CeilToInt(1 + 4 * UnityEngine.Random.value);

						switch (scr.alPrScr.isW)
						{
						case 0:
							scr.alPrScr.playoffPlayers[12] = scr.alPrScr.playoffPlayers[9];

							if (rand4 < 0.5f)
							{
								scr.alPrScr.playoffPlayers[14] = scr.alPrScr.playoffPlayers[12];
								scr.alPrScr.plfGoals[12] = rand42;
								scr.alPrScr.plfGoals[13] = rand41;
							}	
							else
							{
								scr.alPrScr.playoffPlayers[14] = scr.alPrScr.playoffPlayers[13];
								scr.alPrScr.plfGoals[12] = rand41;
								scr.alPrScr.plfGoals[13] = rand42;
							}
							break;
						case 1:
							scr.alPrScr.playoffPlayers[12] = scr.alPrScr.playoffPlayers[8];
							break;
						}
						break;
					case 3:
						scr.alPrScr.plfGoals[12] = Score.score;
						scr.alPrScr.plfGoals[13] = Score.score1;

						switch (scr.alPrScr.isW){
						case 0:
							scr.alPrScr.playoffPlayers[14] = scr.alPrScr.playoffPlayers[13];
							break;
						case 1:
							scr.alPrScr.playoffPlayers[14] = scr.alPrScr.playoffPlayers[12];
							break;
						}
						break;
					}
					break;
				case "ChampGoes":
					RandomResultsInChamp();
					scr.alPrScr.champGames++;

					if (scr.alPrScr.champGames == 9)
					{
						scr.alPrScr.champGames = 0;
						scr.alPrScr.isSecTour ++;
					}

					SetEnemy();
					break;
				}
			}
		}
			
		scr.alPrScr.isChange0 = true;
	}

	public void SetEnemy()
	{
		int k = scr.alPrScr.champGames;
		scr.alPrScr.playerIndex = scr.alPrScr.champPlayers[0];
		scr.alPrScr.enemyIndex = scr.alPrScr.champPlayers[genRoundRobin[0, k]];

		scr.alPrScr.isChange0 = true;
	}

	public void RandomResultsInChamp()
	{

			int k = 0;
			int j = 0;

			genRoundRobin = GenerateRoundRobin (scr.alPrScr.champPlayers.Length);

			for (int i = 0; i < 10; i++) 
			{
				if (scr.alPrScr.champGames == 0 && scr.alPrScr.isSecTour == 1)
				{
					if (SceneManager.GetActiveScene().name == "Championship")
						k = 8;
					else if (SceneManager.GetActiveScene().name == "Level")
						k = scr.alPrScr.champGames;
				}
				else
					k = scr.alPrScr.champGames;

				j = genRoundRobin[i, k];

				for (int i1 = 0; i1 < i; i1++) 
				{
					if (genRoundRobin [i1, k] == i) 
						isSame2 = true;
				}

				if (!isSame2) 
					scr.alPrScr.pl2InChamp[i] = j;
				else 
					scr.alPrScr.pl2InChamp[i] = 0;

				if (SceneManager.GetActiveScene().name == "Level")
				{
					float skillDiff = scr.chLIm.itemList[j].summSkill - scr.chLIm.itemList[i].summSkill;
					float randWin0 = UnityEngine. Random.value + skillDiff;
					int randScore = Mathf.RoundToInt (UnityEngine.Random.value * 2) + 1;
					int loose = Mathf.RoundToInt(UnityEngine.Random.value);
					int win = loose + randScore;
					int tie = loose;

					if (scr.chLIm.itemList[i].imageSprite!=scr.alScr.playerSprite)
					{
						if (!isSame2)
						{
							if (randWin0 <= .3f)
							{
								scr.alPrScr.chWins [i] ++;
								scr.alPrScr.chPoints [i] += 3;
								scr.alPrScr.chGoalDiff [i] += randScore;
								scr.alPrScr.chCurrG [i] = win;
								scr.alPrScr.chLoses [j] ++;
								scr.alPrScr.chGoalDiff [j] -= randScore;
								scr.alPrScr.chCurrG [j] = loose;
							} 
							else if (randWin0 > .3f && randWin0 <= .7f) 
							{
								scr.alPrScr.chTies [i] ++;
								scr.alPrScr.chPoints [i] ++;
								scr.alPrScr.chCurrG [i] = tie;
								scr.alPrScr.chTies [j] ++;
								scr.alPrScr.chPoints [j] ++;
								scr.alPrScr.chCurrG [j] = tie;
							}
							else if (randWin0 > .7f) 
							{
								scr.alPrScr.chLoses [i] ++;
								scr.alPrScr.chGoalDiff [i] -= randScore;
								scr.alPrScr.chCurrG [i] = loose;
								scr.alPrScr.chWins [j] ++;
								scr.alPrScr.chGoalDiff [j] += randScore;
								scr.alPrScr.chPoints [j] += 3;
								scr.alPrScr.chCurrG [j] = win;
							}
						}
					}
				}
				isSame2 = false;
			}
		

		if (SceneManager.GetActiveScene().name == "Level")
			SetMyPoints();
		
		scr.alPrScr.isChange0 = true;
	}

	private void SetMyPoints()
	{
		int myG = Score.score;
		int enG = Score.score1;

		int j0 = genRoundRobin [0, scr.alPrScr.champGames];
		scr.alPrScr.chGoalDiff [0] += (myG - enG);
		scr.alPrScr.chGoalDiff [j0] -= (myG - enG);
		scr.alPrScr.chCurrG [0] = myG;
		scr.alPrScr.chCurrG [j0] = enG;

		if (myG > enG) 
		{
			scr.alPrScr.chWins [0] ++;
			scr.alPrScr.chPoints [0] += 3;
			scr.alPrScr.chLoses [j0] ++;
		} 
		else if (myG==enG)
		{
			scr.alPrScr.chTies [0] ++;
			scr.alPrScr.chPoints [0] ++;
			scr.alPrScr.chTies [j0] ++;
			scr.alPrScr.chPoints [j0] ++;
		} 
		else if (myG < enG) 
		{
			scr.alPrScr.chLoses [0] ++;
			scr.alPrScr.chWins [j0] ++;
			scr.alPrScr.chPoints [j0] += 3;
		}
	}

	void RandomResultsInCup()
	{
		int whoWins = Mathf.RoundToInt (UnityEngine.Random.value * 2.9f + .55f);
		int randSc = Mathf.RoundToInt (UnityEngine.Random.value * 6 + 1);

		if (scr.alPrScr.cupGames == 1)
		{
			if (whoWins == 1) 
			{
				scr.alPrScr.cupWins [2] ++;
				scr.alPrScr.cupLoses [3] ++;
				scr.alPrScr.cupGoalDiff [2] += randSc;
				scr.alPrScr.cupGoalDiff [3] -= randSc;
				scr.alPrScr.cupPoints [2] += 3;
			} 
			else if (whoWins == 2)
			{
				scr.alPrScr.cupTies [2] ++;
				scr.alPrScr.cupTies [3] ++;
				scr.alPrScr.cupPoints [2] ++;
				scr.alPrScr.cupPoints [3] ++;
			} 
			else if (whoWins == 3)
			{
				scr.alPrScr.cupLoses [2] ++;
				scr.alPrScr.cupWins [3] ++;
				scr.alPrScr.cupGoalDiff [2] -= randSc;
				scr.alPrScr.cupGoalDiff [3] += randSc;
				scr.alPrScr.cupPoints [3] += 3;
			}
		}
		else if (scr.alPrScr.cupGames == 2)
		{
			if (whoWins == 1)
			{
				scr.alPrScr.cupWins [1] ++;
				scr.alPrScr.cupLoses [3] ++;
				scr.alPrScr.cupGoalDiff [1] += randSc;
				scr.alPrScr.cupGoalDiff [3] -= randSc;
				scr.alPrScr.cupPoints [1] +=3;
			} 
			else if (whoWins == 2)
			{
				scr.alPrScr.cupTies [1] ++;
				scr.alPrScr.cupTies [3] ++;
				scr.alPrScr.cupPoints [1] ++;
				scr.alPrScr.cupPoints [3] ++;
			} 
			else if (whoWins == 3)
			{
				scr.alPrScr.cupLoses [1] ++;
				scr.alPrScr.cupWins [3] ++;
				scr.alPrScr.cupGoalDiff [1] -= randSc;
				scr.alPrScr.cupGoalDiff [3] += randSc;
				scr.alPrScr.cupPoints [3] += 3;
			}
		} 
		else if (scr.alPrScr.cupGames == 3)
		{
			if (whoWins == 1) 
			{
				scr.alPrScr.cupWins [1] ++;
				scr.alPrScr.cupLoses [2] ++;
				scr.alPrScr.cupGoalDiff [1] += randSc;
				scr.alPrScr.cupGoalDiff [2] -= randSc;
				scr.alPrScr.cupPoints [1] += 3;
			} 
			else if (whoWins == 2) 
			{
				scr.alPrScr.cupTies [1] ++;
				scr.alPrScr.cupTies [2] ++;
				scr.alPrScr.cupPoints [1] ++;
				scr.alPrScr.cupPoints [2] ++;
			} 
			else if (whoWins == 3) 
			{
				scr.alPrScr.cupLoses [1] ++;
				scr.alPrScr.cupWins [2] ++;
				scr.alPrScr.cupGoalDiff [1] -= randSc;
				scr.alPrScr.cupGoalDiff [2] += randSc;
				scr.alPrScr.cupPoints [2] += 3;
			}
		}

		scr.alPrScr.isChange0 = true;
	}

	public void CupNextGameStart()
	{
		for (int i = 0; i <= 1; i++) 
		{
			switch (i) 
			{
			case 0:
				scr.objCup.loadingPanelAnim.gameObject.SetActive(true);
				scr.objCup.loadingPanelAnim.SetBool ("call", true);

				break;
			case 1:
				if (scr.alPrScr.cupGames == 3 && isPlayoff) 
				{
					if (scr.alPrScr.finishTourn == "CupGoes")
					{
						scr.alPrScr.plfGames = 0;
						scr.alPrScr.isChange0 = true;
					}

					SceneManager.LoadScene("Playoff");
				} 
				else if(scr.alPrScr.cupGames == 3 && !isPlayoff)
					SceneManager.LoadScene("MainMenu");
				else 
					SceneManager.LoadScene("Level");
				
				break;
			}
		}
	}
	
	public void LevelRestartInLevel()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("Level");
	}

	public void LevelStartInPlayoff()
	{
		int plfGames = 0;
		plfGames = scr.alPrScr.plfGames;

		for (int i = 0; i <= 1; i++)
		{
			if (i == 0)
			{
				scr.objPlf.loadingPanelAnim.gameObject.SetActive(true);
				scr.objPlf.loadingPanelAnim.SetBool ("call", true);
			}
			else if (i == 1)
			{
				if (plfGames == 3)
					SceneManager.LoadScene("MainMenu");
				else
				{
					if (scr.lostC.lostPlayoff)
						SceneManager.LoadScene("MainMenu");
					else
						SceneManager.LoadScene("Level");
				}
			}
		}
	}
	
	public void LevelStartInChamp()
	{
		for (int i = 0; i <= 1; i++) 
		{
			switch (i) 
			{
			case 0:
				scr.objCh.loadingPanelAnim.gameObject.SetActive(true);
				scr.objCh.loadingPanelAnim.SetBool ("call", true);

				break;
			case 1:
				if (!scr.chLIm.endOfChamp) 
					SceneManager.LoadScene("Level");
				else
					SceneManager.LoadScene("MainMenu");
				
				break;
			}
		}
	}

	
	public void WinGame1()
	{
		if (SceneManager.GetActiveScene().name == "Level")
		{
			Score.score = 6;
			Score.score1 = 0;
			scr.scoreScr.SetScore();
			scr.tM.time0 = 2;
		}
	}

	public void LooseGame()
	{
		if (SceneManager.GetActiveScene().name == "Level")
		{
			Score.score = 0;
			Score.score1 = 3;
			scr.scoreScr.SetScore();
			scr.tM.time0 = 2;
		}
	}

	public void TieGame()
	{
		if (SceneManager.GetActiveScene().name == "Level")
		{
			Score.score = 1;
			Score.score1 = 1;
			scr.scoreScr.SetScore();
			scr.tM.time0 = 2;
		}
	}
	
	public void SetCupDefaults()
	{
		SetCupDefaultsMainLeague();
	}

	public void SetCupDefaultsMainLeague()
	{
		scr.alPrScr.cupGames = 0;
		scr.alPrScr.plfGames = 0;
		scr.alPrScr.champGames = 0;
		scr.alPrScr.isSecTour = 0;

		for (int i = 0; i < 4; i++)
		{
			scr.alPrScr.cupWins[i] = 0;
			scr.alPrScr.cupTies[i] = 0;
			scr.alPrScr.cupLoses[i] = 0;
			scr.alPrScr.cupGoalDiff[i] = 0;
			scr.alPrScr.cupPoints[i] = 0;
		}

		for (int i = 0; i < 10; i++)
		{
			scr.alPrScr.chWins[i] = 0;
			scr.alPrScr.chTies[i] = 0;
			scr.alPrScr.chLoses[i] = 0;
			scr.alPrScr.chGoalDiff[i] = 0;
			scr.alPrScr.chPoints[i] = 0;
			scr.alPrScr.chCurrG[i] = 0;
		}

		scr.alPrScr.isChange0 = true;
	}



	public void LoadFreePlay()
	{
		scr.alPrScr.unlock = 0;
		scr.alPrScr.freePlay = 1;
		scr.alPrScr.isChange0 = true;
	}

	public void LoadMultiplayer()
	{
		// scr.alPrScr.multiplayer = 1;
		scr.alPrScr.isChange0 = true;
	}

	public void LoadMultiplayer2()
	{
		// if (scr.alPrScr.multiplayer == 0)
		// 	scr.alPrScr.multiplayer = 1;
		// else
		// 	scr.alPrScr.multiplayer = 0;
		// 	
		scr.alPrScr.isChange0 = true;
	}

	public void LoadMultiplayer3()
	{
		scr.alPrScr.isChange0 = true;
	}

	public void LoadBluetoothGame()
	{
		scr.alPrScr.bluetooth = 1;
	}

	private bool isEnemyMain;

	public void ChooseRandomEnemy()
	{
		if (PlayerPrefs.GetInt("OnlineUnlocked") == 0)
		{
			
		}
		else
		{
			int k;

			if (Random.value > 0.5f)
				isEnemyMain = true;

			k = Mathf.RoundToInt(Random.value *  ((float)scr.prScrL.itemList.Count - 1.0f));

			scr.alPrScr.enemyIndex = k;
			scr.alScr.enemyFlag = scr.prScrL.itemList[k].flag;
			scr.alScr.enemySprite = scr.prScrL.itemList[k].icon;
			scr.alScr.enemyName = scr.prScrL.itemList[k].name;
			scr.alScr.enemyName0 = scr.prScrL.itemList[k].name0;

			scr.alPrScr.isChange0 = true;
		}
	}

	public void ChooseRandomPlayer()
	{
		int k = 0;
		k = Mathf.RoundToInt(Random.value * scr.prScrL.itemList.Count);

		if (isEnemyMain && k == scr.alPrScr.enemyIndex)
		{
			if (k > 0)
				k --;
			else
				k ++;
		}

		scr.alScr.playerFlag = scr.prScrL.itemList[k].flag;
		scr.alScr.playerSprite = scr.prScrL.itemList[k].icon;
		scr.alScr.playerName = scr.prScrL.itemList[k].name;
		scr.alScr.playerName0 = scr.prScrL.itemList[k].name0;

		scr.alPrScr.isChange0 = true;
	}
		
	public void LoadOnlineLevel()
	{
		if (PlayerPrefs.GetInt("OnlineUnlocked") == 0)
		{
			// scr.facebookScr.isMessage = true;
		}
		else if (PlayerPrefs.GetInt("OnlineUnlocked") == 1)
		{
			for (int i = 0; i < 2; i++) 
			{
				if (i == 0)
				{
					scr.objM.loadingPanelAnim.gameObject.SetActive(true);
					scr.objM.loadingPanelAnim.SetTrigger("call");
					scr.alPrScr.isLeaderboardGame = 1;
					scr.alPrScr.isChange0 = true;
				} 
				else if (i == 1)
				{
					SceneManager.LoadScene("Level");
				}
			}
		}
	}

	public void LoadSimpleLevel()
	{
		for (int i = 0; i < 2; i++) 
		{
			if (i == 0)
			{
				scr.objM.loadingPanelAnim.gameObject.SetActive(true);
				scr.objM.loadingPanelAnim.SetTrigger("call");
			} 
			else if (i == 1)
			{
				SceneManager.LoadScene("Level");
			}
		}
	}

	public void Quit()
	{
		for (int i = 0; i < 2; i++) 
		{
			if (i == 0)
			{
				if (SceneManager.GetActiveScene().name=="Cup") 
				{
					scr.objCup.loadingPanelAnim.gameObject.SetActive (true);
					scr.objCup.loadingPanelAnim.SetBool ("call", true);
				} 
				else if (SceneManager.GetActiveScene().name=="Playoff") 
				{
					scr.objPlf.loadingPanelAnim.gameObject.SetActive (true);
					scr.objPlf.loadingPanelAnim.SetBool ("call", true);
				} 
				else if (SceneManager.GetActiveScene().name=="Championship") 
				{
					scr.objCh.loadingPanelAnim.gameObject.SetActive (true);
					scr.objCh.loadingPanelAnim.SetBool ("call", true);
				}
			} 
			else if (i == 1)
			{
				SceneManager.LoadScene("MainMenu");
			}
		}
	}

	public void QuitPanelCall()
	{
		if (SceneManager.GetActiveScene().name == "Cup") 
			scr.objCup.quitPanelMenu.SetTrigger("call");
		else if (SceneManager.GetActiveScene().name == "Playoff")
			scr.objPlf.quitPanelMenu.SetTrigger("call");
		else if (SceneManager.GetActiveScene().name == "Championship")
			scr.objCh.quitPanelMenu.SetTrigger("call");

		quitPanel = true;
	}

	public void QuitPanelBack()
	{
		if (SceneManager.GetActiveScene().name == "Cup") 
			scr.objCup.quitPanelMenu.SetTrigger("back");
		else if (SceneManager.GetActiveScene().name == "Playoff") 
			scr.objPlf.quitPanelMenu.SetTrigger("back");
		else if (SceneManager.GetActiveScene().name == "Championship")
			scr.objCh.quitPanelMenu.SetTrigger("back");

		quitPanel = false;
	}

	public void ExitMenu()
	{
		scr.objM.menuExitAnim.gameObject.SetActive (true);

		if (!scr.objM.menuExitAnim.GetBool("call")) 
		{
			MainMenuOff ();
			scr.objM.menuExitAnim.SetBool("call", true);
		} 
		else if (scr.objM.menuExitAnim.GetBool("call"))
		{
			Debug.Log("Game quited!");
			Application.Quit();
		}
	}

	public void ExitMenuBack()
	{
		MainMenuOn ();
		scr.objM.menuExitAnim.SetBool("call", false);
		scr.objM.menuExitAnim.gameObject.SetActive (false);
	}
	
	public void BackFromScrollPanel1(RectTransform transformBackFromScrollPanel)
	{
		transformBackFromScrollPanel.anchoredPosition = new Vector3 (800, -100, 0);
	}
	
	public void BackFromScrollPanel2(RectTransform transformQuestionPanel)
	{
		transformQuestionPanel.anchoredPosition = new Vector3 (1280, 500, 0);
	}
	
	public void ExitQuestionNo(RectTransform transformExitQuestion)
	{
		transformExitQuestion.anchoredPosition = new Vector3 (transformExitQuestion.anchoredPosition.x, 750, 0);
	}
	
	public void MenuCareerCall ()
	{
		MainMenuBools.menuCareer = true;
		scr.objM.menuCareerAnim.SetTrigger ("call");
	}
	
	public void MenuFreePlay (Animator AnimMenu)
	{
		MainMenuBools.menuFreePlay = true;
		AnimMenu.SetTrigger ("fpcall");
	}

	public void MenuBluetoothCall(Animator anim)
	{
		anim.SetTrigger ("call");
	}
	
	public void MenuProfile ()
	{
		if (!MainMenuBools.menuProfile)
		{
			MainMenuBools.menuProfile = true;

			if (!scr.objM.menuProfAnim.gameObject.activeSelf)
				scr.objM.menuProfAnim.gameObject.SetActive (true);
			
			scr.objM.menuProfAnim.SetTrigger ("call");
		}
	}

	public void MenuProfileInFreePlay ()
	{
		if (!MainMenuBools.menuProfile)
		{
			MainMenuBools.menuProfile = true;

			if (!scr.objM.menuProfAnim.gameObject.activeSelf)
				scr.objM.menuProfAnim.gameObject.SetActive (true);

			scr.objM.menuProfAnim.SetTrigger ("call1");
		}
	}
	
	public void BotVisibleOff (SpriteRenderer botRenderer){
		Color bot1 = botRenderer.color;
		bot1.a = 0;
		botRenderer.color = bot1;
	}
	
	public void LegVisibleOff (SpriteRenderer legRenderer)
	{
		Color leg1 = legRenderer.color;
		leg1.a = 0;
		legRenderer.color = leg1;
	}
	
	public void BallVisibleOff (SpriteRenderer ballRenderer)
	{
		Color ball1 = ballRenderer.color;
		ball1.a = 0;
		ballRenderer.color = ball1;
	}

	public void MenuSinglePlayerOn ()
	{
		scr.objM.singlePlayerRect.gameObject.SetActive (true);
		scr.objM.singlePlayerRect.GetComponent<Animator>().SetTrigger ("call");
		MainMenuBools.menuSinglePlayer = true;
	}
	
	public void BotVisibleOn (SpriteRenderer botRenderer)
	{
		Color bot1 = botRenderer.color;
		bot1.a = 1;
		botRenderer.color = bot1;
	}
	
	public void LegVisibleOn (SpriteRenderer legRenderer)
	{
		Color leg1 = legRenderer.color;
		leg1.a = 1;
		legRenderer.color = leg1;
	}
	
	public void BallVisibleOn (SpriteRenderer ballRenderer)
	{
		Color ball1 = ballRenderer.color;
		ball1.a = 1;
		ballRenderer.color = ball1;
	}
	
	public void LogoOn ()
	{
		scr.objM.logoAnim.gameObject.SetActive(true);
	}

	public void LogoOff()
	{
		scr.objM.logoAnim.gameObject.SetActive(false);
	}

	public void ExitGame()
	{
		Debug.Log("Game quited!");
		Application.Quit();
	}

	public void DeleteAllPrefs()
	{
		PlayerPrefs.DeleteAll ();
	}

	public void Pause () 
	{
		scr.objLev.pauseControlsScreen.SetActive(true);
		pauseInLevel = true;
		scr.objLev.pauseMenuAnim.gameObject.SetActive(true);
		scr.objLev.pauseMenuAnim.ResetTrigger("back");

		if (scr.objLev.pauseOptionsAnim.gameObject.activeSelf)
			scr.objLev.pauseOptionsAnim.ResetTrigger("back");
		
		scr.objLev.pauseMenuAnim.SetTrigger ("call");

		scr.objLev.resumeButton.SetActive(true);
		scr.objLev.optionsButton.SetActive(true);
		scr.objLev.exitButton.SetActive(true);

		if (scr.alPrScr.freePlay == 1) 
			scr.objLev.restartButton.SetActive(true);

		Time.timeScale = 0;
	}
	
	public void PauseBack()
	{
		scr.objLev.pauseControlsScreen.SetActive(false);
		pauseInLevel = false;
		scr.objLev.pauseMenuAnim.SetTrigger("back");
		Time.timeScale = 1;
		scr.objLev.pauseMenuAnim.gameObject.SetActive(false);
	}
	
	public void PauseOptions ()
	{
		pauseOptions = true;
		pauseInLevel = false;
		scr.objLev.pauseOptionsAnim.gameObject.SetActive (true);
		scr.objLev.pauseMenuAnim.SetTrigger("back");
		scr.objLev.pauseOptionsAnim.SetTrigger("call");
	}

	public void PauseQuitBack ()
	{
		scr.hints.isQuitPanel = false;
		scr.objLev.quitPanel.SetActive(false);
	}

	public void PauseOptionsBack ()
	{
		pauseOptions = false;
		pauseInLevel = true;
		scr.objLev.pauseMenuAnim.SetTrigger("call");
		scr.objLev.pauseOptionsAnim.SetTrigger("back");
		scr.objLev.pauseOptionsAnim.gameObject.SetActive (false);
	}
	
	public void Options ()
	{
		scr.objM.menuOptionsAnim.gameObject.SetActive (true);
		MainMenuBools.menuOptions = true;
		scr.objM.menuOptionsAnim.SetTrigger ("call");
	}
	
	public void OptionsBack ()
	{
		MainMenuBools.menuOptions = false;
		scr.objM.menuOptionsAnim.SetTrigger ("back");
		scr.objM.menuOptionsAnim.gameObject.SetActive (false);
		MainMenuOn();
	}
	
	public void MenuSinglePlayer1 ()
	{
		MainMenuBools.menuSinglePlayer = true;
		scr.objM.singlePlayerRect.gameObject.SetActive (true);
		scr.objM.singlePlayerRect.GetComponent<Animator> ().SetBool("call", true);

		if (scr.alPrScr.finishTourn == "Finished" || scr.alPrScr.finishTourn == "FinishedCup")
			scr.objM.nowPlayingMainText.gameObject.SetActive (false);
	}

	public void MenuSinglePlayerBack ()
	{
		MainMenuBools.menuSinglePlayer = false;
		scr.objM.singlePlayerRect.anchoredPosition = 
			new Vector3 (270, scr.objM.singlePlayerRect.anchoredPosition.y, 0);
		scr.objM.singlePlayerRect.GetComponent<Animator> ().SetBool("call", false);
		scr.objM.singlePlayerRect.gameObject.SetActive (false);
	}

	public void TopPanelCall()
	{
		scr.objM.topPanelAnim.gameObject.SetActive (true);
		scr.objM.topPanelAnim.SetBool ("call", true);
	}

	public void TopPanelBack()
	{
		scr.objM.topPanelAnim.SetBool ("call", false);
		scr.changTextScr.text1.enabled = false;
	}

	public void MainMenuOff()
	{
		scr.objM.mainMenuAnim.SetBool ("call", true);
		MainMenuBools.mainMenu = false;
	}
	
	public void MainMenuOn()
	{
		scr.objM.mainMenuAnim.gameObject.SetActive (true);
		scr.objM.mainMenuAnim.SetBool ("call", false);
		MainMenuBools.mainMenu = true;
	}
	
	public void MenuResult()
	{
		gamePaused = true;
		scr.objLev.resultMenuAnim.gameObject.SetActive(true);
		scr.objLev.resultMenuAnim.SetTrigger("call");
		scr.objLev.gates1Coll.enabled = true;
		scr.objLev.gates2Coll.enabled = true;
		Time.timeScale = 0;
	}

	public void MenuResultBack()
	{
		scr.objLev.resultMenuAnim.SetBool ("call", false);
		Time.timeScale = 1;
		scr.objLev.resultMenuAnim.gameObject.SetActive(false);
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
	
	public void MenuProfilePlayersStart ()
	{
		if (!MainMenuBools.menuProfilePlayers)
		{
			scr.objM.profilePlayersAnim.SetBool("call", true);
			MainMenuBools.menuProfilePlayers = true;
		}
	}

	public void MenuProfilePlayersStartFP ()
	{
		if (!MainMenuBools.menuProfilePlayers)
		{
			scr.objM.profilePlayersAnim.SetBool("call", true);
			MainMenuBools.menuProfilePlayers = true;
		}
	}
	
	public void MenuProfilePlayers (Animator AnimMenu)
	{
		if (!MainMenuBools.menuProfilePlayers) 
		{
			AnimMenu.SetBool("call", true);
			MainMenuBools.menuProfilePlayers = true;
		}
	}

	public void MenuProfilePlayersBack (Animator AnimMenu)
	{
		if (MainMenuBools.menuProfilePlayers)
		{
			AnimMenu.SetBool("call", false);
			MainMenuBools.menuProfilePlayers = false;
		}
	}

	public void PlayerLeagueOn (Animator AnimMenu)
	{
		AnimMenu.SetBool("call", true);
	}

	public void PlayerLeagueOff (Animator AnimMenu)
	{
		AnimMenu.SetBool("call", false);
	}

	public void MenuProfileStadiums (Animator AnimMenu)
	{
		if (!MainMenuBools.menuProfileStadiums) 
		{
			AnimMenu.SetBool("call", true);
			MainMenuBools.menuProfileStadiums = true;
		}
	}
	
	public void MenuProfileStadiumsBack (Animator AnimMenu)
	{
		if (MainMenuBools.menuProfileStadiums)
		{
			AnimMenu.SetBool("call", false);
			MainMenuBools.menuProfileStadiums = false;
		}
	}
		
	void ChooseLastObject(string tag)
	{
		GameObject[] obj = GameObject.FindGameObjectsWithTag(tag);

		if (obj.Length >= 2)
		{
			for (int i = 1; i < obj.Length; i++) 
				DestroyImmediate(obj[i]);	
		}
	}

	public void ChooseLastMenuProfileMaterials()
	{
		GameObject[] menuProfMat = GameObject.FindGameObjectsWithTag ("MenuProfileMaterials");

		if (menuProfMat.Length == 2)
		{
			if (SceneManager.GetActiveScene().name == "MainMenu")
				DestroyImmediate (menuProfMat [0]); 
			 else 
				DestroyImmediate (menuProfMat [1]); 
		}
	}

	private const int BYE = -1;

	private int [,] GenerateRoundRobinOdd(int num_teams)
	{
		int n2 = (int)((num_teams - 1) / 2);
		int[,] results = new int[num_teams, num_teams];
		int[] teams = new int[num_teams];
		
		for (int i = 0; i < num_teams; i++) 
		{
			teams[i] = i;
		}
		
		for (int round = 0; round < num_teams; round++)
		{
			for (int i = 0; i < n2; i++)
			{
				int team1 = teams[n2 - i];
				int team2 = teams[n2 + i + 1];
				results[team1, round] = team2;
				results[team2, round] = team1;
			}

			results[teams[0], round] = BYE;
			RotateArray(teams);
		}
		return results;
	}
	
	private void RotateArray(int[] teams)
	{
		int tmp = teams[teams.Length - 1];
		System.Array.Copy(teams, 0, teams, 1, teams.Length - 1);
		teams[0] = tmp;
	}
	
	private int[,] GenerateRoundRobinEven(int num_teams)
	{
		int[,] results = GenerateRoundRobinOdd(num_teams - 1);
		int[,] results2 = new int[num_teams, num_teams - 1];
		
		for (int team = 0; team < num_teams - 1; team++)
		{
			for (int round = 0; round < num_teams - 1; round++)
			{
				if (results[team, round] == BYE)
				{
					results2[team, round] = num_teams - 1;
					results2[num_teams - 1, round] = team;
				} 
				else 
				{
					results2[team, round] = results[team, round];
				}
			}
		}
		return results2;
	}
	
	public int[,] GenerateRoundRobin(int num_teams)
	{
		if (num_teams % 2 == 0) 
			return GenerateRoundRobinEven (num_teams);
		 else 
			return GenerateRoundRobinOdd (num_teams);
	}

	public void ChooseLastBallsMaterials()
	{
		GameObject[] ballsMat = GameObject.FindGameObjectsWithTag ("BallsMaterials");

		if (ballsMat.Length==2)
		{
			DestroyImmediate (ballsMat [0]);
		}
	}

	public void ChooseLastPlayoffListSave()
	{
		if (SceneManager.GetActiveScene().name != "MainMenu")
		{
			GameObject[] playoffListSave = GameObject.FindGameObjectsWithTag ("PlayoffListSave");

			for (int i = 1; i < playoffListSave.Length; i++)
				DestroyImmediate(playoffListSave[i]);
		}
	}

	void AddItemsToVarList()
	{
		if(SceneManager.GetActiveScene().name == "MainMenu")
		{
			if (scr.alPrScr.finishTourn != "CupGoes")
			{
				for (int i = 0; i < 10; i++)
				{
					if(scr.alScr.varList.Count < 10)
						scr.alScr.varList.Add(new AllLevelsScript.CupVariants());
				}
			}
		}
	}



	public void SetTrophyIdle()
	{
		GameObject trP = GameObject.Find ("TrophyPanel");
		Animator anim = trP.GetComponent<Animator> ();

		if (!anim.GetCurrentAnimatorStateInfo(0).IsName("TrophyPanelAnim"))
			anim.SetTrigger("call");
	}

	public int isNetWorkGameStarted;

	public void NetworkAction()
	{
		isNetWorkGameStarted = 1;
	}

	public void NetworkAction2()
	{
		isNetWorkGameStarted = 2;
	}

	public void NetworkAction3()
	{
		isNetWorkGameStarted = 3;
	}

	public string moneyString(int money)
	{
		string strNum1 = "";
		string strNum2 = "";
		string strNum3 = "";

		string moneyStr = money.ToString("D");

		if (moneyStr.Length <= 3) 
		{
			return moneyStr + "$";
		} 
		else if (moneyStr.Length > 3 && moneyStr.Length <= 6) 
		{
			int num1 = Mathf.CeilToInt (money / 1000);
			int num2 = money - num1 * 1000;

			if (num2 < 10)
				strNum2 = "00" + num2.ToString ("D");
			else if (num2 >= 10 && num2 < 100)
				strNum2 = "0" + num2.ToString ("D");
			else
				strNum2 = "" + num2.ToString ("D");
			
			strNum1 = num1.ToString ("D");
			return strNum1 + "," + strNum2 + "$";
		} 
		else if (moneyStr.Length > 6 && moneyStr.Length <= 9) 
		{
			int num1 = Mathf.CeilToInt (money / 1000000);
			int num2 = money - num1 * 1000000;
			int num2_1 = Mathf.CeilToInt (num2 / 1000);

			if (num2_1 < 10)
				strNum2 = "00" + num2_1.ToString ("D");
			else if (num2 >= 10 && num2 < 100)
				strNum2 = "0" + num2_1.ToString ("D");
			else
				strNum2 = "" + num2_1.ToString ("D");

			int num3 = money - num1 * 1000000 - num2_1 * 1000;

			if (num3 < 10)
				strNum3 = "00" + num3.ToString ("D");
			else if (num3 >= 10 && num3 < 100)
				strNum3 = "0" + num3.ToString ("D");
			else
				strNum3 = "" + num3.ToString ("D");

			strNum1 = num1.ToString ("D");
			return strNum1 + "," + strNum2 + "," + strNum3 + "$";
		} else
			return "";
	}

	public int leagueN(int trn)
	{
		if (trn > 0 && trn <= 2)
			return 6;
		else if (trn > 2 && trn <= 3)
			return 5;
		else if (trn > 4 && trn <= 5)
			return 4;
		else if (trn > 6 && trn <= 7)
			return 3;
		else if (trn > 8 && trn <= 10)
			return 2;
		else if (trn > 10 && trn <= 12)
			return 1;
		else
			return 0;
	}

	public string leagueNumberString(int leagueN)
	{
		if (leagueN == 0)
			return "xui";
		else if (leagueN == 1)
			return "i";
		else if (leagueN == 2)
			return "ii";
		else if (leagueN == 3)
			return "iii";
		else if (leagueN == 4)
			return "iv";
		else if (leagueN == 5)
			return "v";
		else if (leagueN == 6)
			return "vi";
		else if (leagueN == 7)
			return "vii";
		else if (leagueN == 8)
			return "viii";
		else if (leagueN == 9)
			return "ix";
		else if (leagueN == 10)
			return "x";
		else 
			return "xui2";
	}
}



