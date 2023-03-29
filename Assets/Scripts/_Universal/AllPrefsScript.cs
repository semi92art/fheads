using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AllPrefsScript : MonoBehaviour 
{
	[Header("Delete all PlayerPrefs:")]
	public bool deletePrefs;
	[Header("Money Count:")]
	public int moneyCount;
	[Header("Launches:")]
	public int launches;
	[Space(10)]
	public int moneyWin;

	public int lotteryCount;
	public int facebookShared;
	public string[] bestResultInTourns;
	public int graphicQuality;

	public string googlePlayAccountName;
	public int isLoadedSaveOnReinstall;
	public float totalPlayingTime;
	public int stadiumOrTraining, timeOfDay;
	public int winsInARow, bestResult;
	public int isLeaderboardGame;
	public float soundVolume;
	public float musicVolume;

	public int[] cupWins, cupTies, cupLoses, cupGoalDiff, cupPoints;
	public int[] chWins, chTies, chLoses, chGoalDiff, chPoints, chCurrG;
	public int[] chWins1, chTies1, chLoses1, chGoalDiff1, chPoints1, chCurrG1;


	public int cupGames, plfGames, champGames, champGames1;
	public int isW;
	public int unlock;
	public int trn, trn1, opndTrns;
	public int stadium;
	public int ballN;
	public int isLoadedFromMenu;
	public int freePlay;
	public string rndT1, rndT2;
	public string finishTourn;
	public int[] cupPlayers;
	public int[] playoffPlayers;
	public int[] plfGoals;
	public int[] champPlayers;
	public int[] pl2InChamp;
	public int[] openedPlayers, openedStadiums, openedBalls;

	public int playerIndex, enemyIndex, enemyIndexFP;
	public int bluetooth;

	public int secondCupWinner;
	public int isSecTour;
	public string fromMenuToTourn;

	[HideInInspector]
	public bool langChange;
	[HideInInspector]
	public bool isChange0;
	[HideInInspector]
	public bool setMoney;
	[HideInInspector]
	public bool is1stMenuLaunch;
	[HideInInspector]
	public int videoAdvCount = 3;
	
	void Awake()
	{
		if (deletePrefs)
			PlayerPrefs.DeleteAll();

		PlayerPrefs.SetInt("MainMenuLaunch", 0);
		launches = PlayerPrefs.GetInt("Launches");
		Debug.Log("Launches = " + launches);

		if (launches == 0) 
		{
			PlayerPrefs.SetString ("PlayerLeague", "main");

			PlayerPrefs.SetInt("IsNoAdvertising", 0);
			PlayerPrefs.SetFloat("SoundVolume", 0.5f);
			PlayerPrefs.SetFloat("MusicVolume", 0.5f);

			PlayerPrefs.SetInt("OpenedPlayer0", 1);
			PlayerPrefs.SetInt("OpenedPlayer1", 1);
			PlayerPrefs.SetInt("OpenedPlayer2", 1);
			PlayerPrefs.SetInt("OpenedPlayer3", 1);

			PlayerPrefs.SetInt("OpenedStadium0", 1);
			PlayerPrefs.SetInt("OpenedStadium1", 1);
			PlayerPrefs.SetInt("OpenedStadium2", 1);

			PlayerPrefs.SetInt("OpenedBall0", 1);

			PlayerPrefs.SetInt ("EnemyIndexFP", 2);

			PlayerPrefs.SetInt("GraphicQuality", 1);
			PlayerPrefs.SetInt ("LotteryCount", 2);
		}

		#if UNITY_EDITOR
		langChange = true;
		#else
		langChange = true;
		#endif
			
		isChange0 = true;
		GetValues();
	}

	private float currentPlayingTime;
	private bool setTotalPlayingTime;

	void Update()
	{
		InputFcn();

		if (moneyCount < 0)
		{
			moneyCount = 0;
			setMoney = true;
		}

		SetValues();
		GetValues();
	}

	private void InputFcn()
	{
		#if UNITY_EDITOR
		// If Scene MainMenu.
		if (SceneManager.GetActiveScene ().buildIndex == 1) 
		{
			if (Input.GetKeyDown(KeyCode.D))
			{
				PlayerPrefs.DeleteAll();
				moneyCount = 0;
				setMoney = true;
			}

			if (Input.GetKeyDown(KeyCode.M))
			{
				moneyCount += 5000;
				setMoney = true;
			}

			if (Input.GetKeyDown(KeyCode.N))
			{
				moneyCount -= 5000;
				setMoney = true;
			}

			if (Input.GetKeyDown(KeyCode.K))
			{
				moneyCount += 50000;
				setMoney = true;
			}

			if (Input.GetKeyDown(KeyCode.L))
				FindObjectOfType<LotteryScript> ().LotteryPlusOne ();
		}
		#endif
	}

	private void SetValues()
	{
		if(isChange0)
		{
			PlayerPrefs.SetInt("Launches", launches);
			PlayerPrefs.SetInt ("LotteryCount", lotteryCount);
			PlayerPrefs.SetInt("FacebookShared", facebookShared);

			for (int i = 0; i < bestResultInTourns.Length; i++) 
			{
				string str = "BestResultInTourns" + i;
				PlayerPrefs.SetString (str, bestResultInTourns [i]);
			}
				
			PlayerPrefs.SetInt("GraphicQuality", graphicQuality);
			PlayerPrefs.SetString("GooglePlayAccountName", googlePlayAccountName);
			PlayerPrefs.SetInt("IsLoadedSaveOnReinstall", isLoadedSaveOnReinstall);
			PlayerPrefs.SetInt ("EnemyIndexFP", enemyIndexFP);
			PlayerPrefs.SetInt("StadiumOrTraining", stadiumOrTraining);
			PlayerPrefs.SetInt("TimeOfDay", timeOfDay);
			PlayerPrefs.SetInt("BestResult", bestResult);
			PlayerPrefs.SetInt("WinsInARow", winsInARow);
			PlayerPrefs.SetInt("IsLeaderboardGame", isLeaderboardGame);

			PlayerPrefs.SetFloat("SoundVolume", soundVolume);
			PlayerPrefs.SetFloat("MusicVolume", musicVolume);
			PlayerPrefs.SetInt ("CupGames", cupGames);

			for (int i = 0; i < 4; i++)
			{
				string w = "W" + i.ToString();
				string t = "T" + i.ToString();
				string l = "L" + i.ToString();
				string d = "GD" + i.ToString();
				string p = "P" + i.ToString();
				PlayerPrefs.SetInt(w, cupWins[i]);
				PlayerPrefs.SetInt(t, cupTies[i]);
				PlayerPrefs.SetInt(l, cupLoses[i]);
				PlayerPrefs.SetInt(d, cupGoalDiff[i]);
				PlayerPrefs.SetInt(p, cupPoints[i]);
				string pl = "CupPlayer" + i.ToString();
				PlayerPrefs.SetInt(pl, cupPlayers[i]);
			}

			for (int i = 0; i < 10; i++)
			{
				string pl2ch = "pl2inchamp" + i.ToString();
				PlayerPrefs.SetInt(pl2ch, pl2InChamp[i]);

				string pl1 = "ChampPlayer" + i.ToString();
				PlayerPrefs.SetInt(pl1, champPlayers[i]);

				string w = "ChW" + i.ToString();
				string t = "ChT" + i.ToString();
				string l = "ChL" + i.ToString();
				string d = "ChGD" + i.ToString();
				string p = "ChP" + i.ToString();
				string cur = "ChCurrG" + i.ToString();
				PlayerPrefs.SetInt (w, chWins[i]);
				PlayerPrefs.SetInt (t, chTies[i]);
				PlayerPrefs.SetInt (l, chLoses[i]);
				PlayerPrefs.SetInt (d, chGoalDiff[i]);
				PlayerPrefs.SetInt (p, chPoints[i]);
				PlayerPrefs.SetInt (cur, chCurrG[i]);
				string w1 = "ChW1" + i.ToString();
				string t1 = "ChT1" + i.ToString();
				string l1 = "ChL1" + i.ToString();
				string d1 = "ChGD1" + i.ToString();
				string p1 = "ChP1" + i.ToString();
				string cur1 = "ChCur1" + i.ToString();
				PlayerPrefs.SetInt (w1, chWins1[i]);
				PlayerPrefs.SetInt (t1, chTies1[i]);
				PlayerPrefs.SetInt (l1, chLoses1[i]);
				PlayerPrefs.SetInt (d1, chGoalDiff1[i]);
				PlayerPrefs.SetInt (p1, chPoints1[i]);
				PlayerPrefs.SetInt (cur1, chCurrG1[i]);
			}

			for (int i = 0; i < 14; i++)
			{
				string pl = "PlayoffPlayer"+i.ToString();
				PlayerPrefs.SetInt(pl, playoffPlayers[i]);
				string plfG = "PlayoffGoals" + i.ToString();
				PlayerPrefs.SetInt(plfG, plfGoals[i]);
			}

			for (int i = 0; i < 60; i++)
			{
				string pl = "OpenedPlayer" + i;
				PlayerPrefs.SetInt(pl, openedPlayers[i]); 
			}
				
			for (int i = 0; i < 18; i++)
			{
				string st = "OpenedStadium" + i;
				PlayerPrefs.SetInt(st, openedStadiums[i]); 
			}

			for (int i = 0; i < openedBalls.Length; i++)
			{
				string ball = "OpenedBall" + i;
				PlayerPrefs.SetInt(ball, openedBalls[i]); 
			}

			PlayerPrefs.SetInt("IsWin", isW);
			PlayerPrefs.SetInt("Unlock", unlock);
			PlayerPrefs.SetInt("PlayoffGames", plfGames);
			PlayerPrefs.SetInt("Tourn", trn);
			PlayerPrefs.SetInt("Tourn1", trn1);
			PlayerPrefs.SetInt("OpenedTourns", opndTrns);
			PlayerPrefs.SetInt("Stadium", stadium);
			PlayerPrefs.SetString("RoundTable1", rndT1);
			PlayerPrefs.SetString("RoundTable2", rndT2);
			PlayerPrefs.SetInt("BallNumber", ballN);
			PlayerPrefs.SetInt("IsLoadedFromMenu", isLoadedFromMenu);
			PlayerPrefs.SetInt("FreePlay", freePlay);
			PlayerPrefs.SetString("FinishTourn", finishTourn);
			PlayerPrefs.SetInt("SecondCupWinner", secondCupWinner);
			PlayerPrefs.SetInt("ChampGames", champGames);
			PlayerPrefs.SetInt("IsSecondTour", isSecTour);
			PlayerPrefs.SetInt("ChampGames1", champGames1);
			PlayerPrefs.SetString("FromMenuToTourn", fromMenuToTourn);
			PlayerPrefs.SetInt("PlayerIndex", playerIndex);
			PlayerPrefs.SetInt("EnemyIndex", enemyIndex);
			PlayerPrefs.SetInt("MoneyWin", moneyWin);
			PlayerPrefs.SetInt("Bluetooth", bluetooth);

			isChange0 = false;
		}

		if (SceneManager.GetActiveScene ().buildIndex == 1)
		{
			if (PlayerPrefs.GetInt("MoneyPurchase") != 0)
			{
				moneyCount += PlayerPrefs.GetInt("MoneyPurchase");
				PlayerPrefs.SetInt("MoneyPurchase", 0);
			}

			PlayerPrefs.SetInt("MoneyCount", moneyCount);
		}

		if(setMoney)
		{
			PlayerPrefs.SetInt("MoneyCount", moneyCount);
			setMoney = false;
		}
	}

	private void GetValues()
	{
		if(isChange0)
		{
			launches = PlayerPrefs.GetInt("Launches");
			lotteryCount = PlayerPrefs.GetInt ("LotteryCount");
			facebookShared = PlayerPrefs.GetInt("FacebookShared");

			for (int i = 0; i < bestResultInTourns.Length; i++) 
			{
				string str = "BestResultInTourns" + i;
				bestResultInTourns [i] = PlayerPrefs.GetString (str);
			}
				
			graphicQuality = PlayerPrefs.GetInt("GraphicQuality");
			googlePlayAccountName = PlayerPrefs.GetString("GooglePlayAccountName");
			isLoadedSaveOnReinstall = PlayerPrefs.GetInt("IsLoadedSaveOnReinstall");
			enemyIndexFP = PlayerPrefs.GetInt ("EnemyIndexFP");
			stadiumOrTraining = PlayerPrefs.GetInt("StadiumOrTraining");
			timeOfDay = PlayerPrefs.GetInt("TimeOfDay");
			bestResult = PlayerPrefs.GetInt("BestResult");
			winsInARow = PlayerPrefs.GetInt("WinsInARow");
			isLeaderboardGame = PlayerPrefs.GetInt("IsLeaderboardGame");
			soundVolume = PlayerPrefs.GetFloat("SoundVolume");
			musicVolume = PlayerPrefs.GetFloat("MusicVolume");
			cupGames = PlayerPrefs.GetInt("CupGames");

			for (int i = 0; i < 4; i++)
			{
				string w = "W" + i.ToString();
				string t = "T" + i.ToString();
				string l = "L" + i.ToString();
				string d = "GD" + i.ToString();
				string p = "P" + i.ToString();
				cupWins[i] = 		PlayerPrefs.GetInt(w);
				cupTies[i] = 		PlayerPrefs.GetInt(t);
				cupLoses[i] = 		PlayerPrefs.GetInt(l);
				cupGoalDiff[i] = 	PlayerPrefs.GetInt(d);
				cupPoints[i] = 		PlayerPrefs.GetInt(p);
				string pl = "CupPlayer" + i.ToString();
				cupPlayers[i] = 	PlayerPrefs.GetInt(pl);
			}

			for (int i = 0; i < 10; i++)
			{
				string pl2ch = "pl2inchamp" + i.ToString();
				pl2InChamp[i] = PlayerPrefs.GetInt(pl2ch);

				string pl1 = "ChampPlayer" + i.ToString();
				champPlayers[i] = PlayerPrefs.GetInt(pl1);
				
				string w = "ChW" + i.ToString();
				string t = "ChT" + i.ToString();
				string l = "ChL" + i.ToString();
				string d = "ChGD" + i.ToString();
				string p = "ChP" + i.ToString();
				string cur = "ChCurrG" + i.ToString();
				chWins[i] = 		PlayerPrefs.GetInt(w);
				chTies[i] = 		PlayerPrefs.GetInt(t);
				chLoses[i] = 		PlayerPrefs.GetInt(l);
				chGoalDiff[i] = 	PlayerPrefs.GetInt(d);
				chPoints[i] = 		PlayerPrefs.GetInt(p);
				chCurrG[i] = 		PlayerPrefs.GetInt (cur);
				string w1 = "ChW1" + i.ToString();
				string t1 = "ChT1" + i.ToString();
				string l1 = "ChL1" + i.ToString();
				string d1 = "ChGD1" + i.ToString();
				string p1 = "ChP1" + i.ToString();
				string cur1 = "ChCur1" + i.ToString();
				chWins1[i] = 		PlayerPrefs.GetInt(w1);
				chTies1[i] = 		PlayerPrefs.GetInt(t1);
				chLoses1[i] = 		PlayerPrefs.GetInt(l1);
				chGoalDiff1[i] = 	PlayerPrefs.GetInt(d1);
				chPoints1[i] = 		PlayerPrefs.GetInt(p1);
				chCurrG1[i] = 		PlayerPrefs.GetInt(cur1);
			}

			for (int i = 0; i < 14; i++)
			{
				string pl = "PlayoffPlayer"+i.ToString();
				playoffPlayers[i] = PlayerPrefs.GetInt(pl);
				string plfG = "PlayoffGoals" + i.ToString();
				plfGoals[i] = PlayerPrefs.GetInt(plfG);
			}

			for (int i = 0; i < openedPlayers.Length; i++)
			{
				string pl = "OpenedPlayer" + i;
				openedPlayers[i] = PlayerPrefs.GetInt(pl);
			}

			for (int i = 0; i < 18; i++)
			{
				string st = "OpenedStadium" + i;
				openedStadiums[i] = PlayerPrefs.GetInt(st);
			}

			for (int i = 0; i < openedBalls.Length; i++)
			{
				string ball = "OpenedBall" + i;
				openedBalls[i] = PlayerPrefs.GetInt(ball);
			}

			isW = PlayerPrefs.GetInt("IsWin");
			unlock = PlayerPrefs.GetInt("Unlock");
			plfGames = PlayerPrefs.GetInt("PlayoffGames");
			trn = PlayerPrefs.GetInt("Tourn");
			trn1 = PlayerPrefs.GetInt("Tourn1");
			opndTrns = PlayerPrefs.GetInt("OpenedTourns");
			stadium = PlayerPrefs.GetInt("Stadium");
			rndT1 = PlayerPrefs.GetString("RoundTable1");
			rndT2 = PlayerPrefs.GetString("RoundTable2");
			ballN = PlayerPrefs.GetInt("BallNumber");
			isLoadedFromMenu = PlayerPrefs.GetInt("IsLoadedFromMenu");
			freePlay = PlayerPrefs.GetInt("FreePlay");
			finishTourn = PlayerPrefs.GetString("FinishTourn");
			secondCupWinner = PlayerPrefs.GetInt("SecondCupWinner");
			champGames = PlayerPrefs.GetInt ("ChampGames");
			isSecTour = PlayerPrefs.GetInt("IsSecondTour");
			champGames1 = PlayerPrefs.GetInt("ChampGames1");
			fromMenuToTourn = PlayerPrefs.GetString("FromMenuToTourn");
			playerIndex = PlayerPrefs.GetInt("PlayerIndex");
			enemyIndex = PlayerPrefs.GetInt("EnemyIndex");
			moneyCount = PlayerPrefs.GetInt ("MoneyCount");
			moneyWin = PlayerPrefs.GetInt("MoneyWin");
			bluetooth = PlayerPrefs.GetInt("Bluetooth");
		}
	}
}
