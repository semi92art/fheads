using Lean.Common;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AllPrefsScript : MonoBehaviour 
{
    public Scripts scr;

    public int pldG;

    [Header("Players Indexes:")]
    public int playerIndex;
    public int playerIndexRand;
    public int buttonIndex;
    public int enemyIndex;
	[Header("Money Count:")]
	public int moneyCount;
	[Header("Launches:")]
	public int launches;
	[Space(10)]
	public int moneyWin;

    public int _camera;
    public int evrdReward;
    public int plLg, enLg;
    public int winsNoConcGoals;
	public int award, tGoals;
	public int controls;
	public int bestResult;
	public int isRandGame;
	public int isW;
	public int stadium;
    public int tribunes;
	public int ballN;
    public int winsTotal;

    public float skill_Speed;
    public float skill_Kick;
    public float skill_Jump;

    public int[] openedPlayers;
    public int[] openedPlayers_2;

    public int[] opndLeagues = new int[6];
    public int[,] wonGames = new int[10,6];
    public int lg, game;

    public int upgrSpeed;
    public int upgrKick;
    public int upgrJump;
    public int upgrSlowdown;
    public int upgrShield;
    public int[] upgrBalls = new int[3];
    public int upgrMoney;

	[HideInInspector]
	public bool doCh;
	public bool setMoney;

	
	void Awake()
	{
        moneyCount = PlayerPrefs.GetInt ("MoneyCount");

        System.GC.Collect();

        float ver = 0;
        float.TryParse(PlayerPrefs.GetString("Version"), System.Globalization.NumberStyles.Any,
                        new System.Globalization.CultureInfo("en-US"), out ver);

        if (PlayerPrefs.GetString("Version") != Application.version)
            PlayerPrefs.DeleteAll();

        PlayerPrefs.SetString("Version", Application.version);
		launches = PlayerPrefs.GetInt("Launches");
        pldG = PlayerPrefs.GetInt("PlayedGames");

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (launches == 0)
            {
                //Debug.Log("Launch 0");
                PlayerPrefs.SetInt("UpgradeBall_0", 1);
                //PlayerPrefs.SetFloat("FreezePercent", 1f);
                PlayerPrefs.SetInt("OpenedLeague_0", 1);
                PlayerPrefs.SetInt("Graph", 1);
                PlayerPrefs.SetInt("Tilt", 1);
                PlayerPrefs.SetInt("ButtonsSize", 2);
                PlayerPrefs.SetFloat("ButtonsCapacity", 0.5f);
                PlayerPrefs.SetInt("SoundOn", 0);
                PlayerPrefs.SetInt("PlayerLeague", 1);
                PlayerPrefs.SetInt("EnemyLeague", 1);
                PlayerPrefs.SetInt("EverydayReward", 1000);
                //48 - Piquet
                PlayerPrefs.SetInt("PlayerIndex", 35);
                PlayerPrefs.SetInt("Controls", 1);
                PlayerPrefs.SetInt("Game", 1);
                //PlayerPrefs.SetInt("League", 1);
                PlayerPrefs.SetInt("OpenedPlayers_0", 1);
                PlayerPrefs.SetInt("OpenedPlayers_1", 1);
                PlayerPrefs.SetInt("OpenedGamesL1_0", 1);
                PlayerPrefs.SetInt("OpenedGamesL2_0", 1);
                PlayerPrefs.SetInt("OpenedChallenges_0", 1);
            }

            PlayerPrefs.SetInt("Launches", launches + 1);  
            PlayerPrefs.SetInt("MenuTrigger_1", 1);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (launches == 1)
                PlayerPrefs.SetInt("SoundOn", 1);

            PlayerPrefs.SetInt("PlayedGames", 
                PlayerPrefs.GetInt("PlayedGames") + 1);

            FindObjectOfType<TimeManager>().rigBodies = FindObjectsOfType<Rigidbody2D>();
            FindObjectOfType<TimeManager>().contrRidBodies = 
                new Rigidbody2D[FindObjectOfType<TimeManager>().rigBodies.Length];
        }
            
		doCh = true;
        GetValues();
	}

	void Update()
	{
#if UNITY_EDITOR
        InputFcn();
#endif

        if (doCh)
        {
            SetValues();
            GetValues();
            doCh = false;
        }

        if (setMoney) SetMoneyBank();

        if (moneyCount < 0)
        {
            moneyCount = 0;
            setMoney = true;
        }
	}

    private void MoneyPurchase()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (PlayerPrefs.GetInt("MoneyPurchase") != 0)
            {
                moneyCount += PlayerPrefs.GetInt("MoneyPurchase");
                PlayerPrefs.SetInt("MoneyPurchase", 0);
            }

            PlayerPrefs.SetInt("MoneyCount", moneyCount);
        }
    }

    private void SetMoneyBank()
    {
        PlayerPrefs.SetInt("MoneyCount", moneyCount);
        setMoney = false;
    }


    private void ViewWonGames()
    {
        for (int j = 0; j < 6; j++)
        {
            for (int i = 0; i < 10; i++)
            {
                Debug.Log("Game " + i.ToString() + 
                    ", League " + j.ToString() + 
                    ", Win = " + wonGames[i, j]);
            }
        }
    }

	private void InputFcn()
	{
        if (SceneManager.GetActiveScene ().buildIndex == 1) 
		{
			if (LeanInput.GetDown(KeyCode.P))
                ViewWonGames();

			if (LeanInput.GetDown(KeyCode.D))
			{
				PlayerPrefs.DeleteAll();
				moneyCount = 0;
				setMoney = true;
			}

			if (LeanInput.GetDown(KeyCode.M))
			{
				moneyCount += 50000;
				setMoney = true;
			}

            if (LeanInput.GetDown(KeyCode.Alpha1))
                scr.allAw.CallAwardPanel_1();
            else if (LeanInput.GetDown(KeyCode.Alpha2))
                scr.allAw.CallAwardPanel_3(); 

            if (LeanInput.GetDown(KeyCode.C))
                System.GC.Collect();

            if (LeanInput.GetDown(KeyCode.Alpha0))
            {
                // if (GoogleMobileAd.IsInterstitialReady)
                //     GoogleMobileAd.ShowInterstitialAd ();
            }
		}
        else if (SceneManager.GetActiveScene ().buildIndex == 2) 
        {
            if (LeanInput.GetDown(KeyCode.Alpha1))
                scr.bonObjMan.BallBig(-1);
            else if (LeanInput.GetDown(KeyCode.Alpha2))
                scr.bonObjMan.BallBig(-2);
            else if (LeanInput.GetDown(KeyCode.Alpha3))
                scr.bonObjMan.BallBig(-3);
            else if (LeanInput.GetDown(KeyCode.Alpha4))
                scr.bonObjMan.PlayerSpeedUp(2);
            else if (LeanInput.GetDown(KeyCode.Alpha5))
                scr.bonObjMan.EnemySpeedUp(2);
            else if (LeanInput.GetDown(KeyCode.Alpha6))
                scr.bonObjMan.SetBonusPosition(BonusType.bonusType.WatchVideo);

            if (LeanInput.GetDown(KeyCode.Z))
                scr.timFr.TimeFreeze_StartOrStop();
            else if (LeanInput.GetDown(KeyCode.X))
                scr.molnia.Lightnin_OnOff();
        }
	}
        
	private void SetValues()
	{
		string str;

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                str = "Won_" + i.ToString() + "_" + j.ToString();
                PlayerPrefs.SetInt(str, wonGames[i,j]);
            }
        }

        for (int i = 0; i < opndLeagues.Length; i++)
        {
            str = "OpenedLeague_" + i.ToString();
            PlayerPrefs.SetInt(str, opndLeagues[i]);
        }

        PlayerPrefs.SetInt("Camera", _camera);
        PlayerPrefs.SetInt("UpgradeSpeed", upgrSpeed);
        PlayerPrefs.SetInt("UpgradeKick", upgrKick);
        PlayerPrefs.SetInt("UpgradeJump", upgrJump);
        PlayerPrefs.SetInt("UpgradeSlowdown", upgrSlowdown);
        PlayerPrefs.SetInt("UpgradeShield", upgrShield);
        PlayerPrefs.SetInt("UpgradeMoney", upgrMoney);

        for (int i = 0; i < upgrBalls.Length; i++)
        {
            str = "UpgradeBall_" + i.ToString();
            PlayerPrefs.SetInt(str, upgrBalls[i]);
        }

        PlayerPrefs.SetInt("League", lg);
        PlayerPrefs.SetInt("EverydayReward", evrdReward);
        PlayerPrefs.SetInt("PlayerLeague", plLg);
        PlayerPrefs.SetInt("EnemyLeague", enLg);
        PlayerPrefs.SetFloat("Skill_Speed", skill_Speed);
        PlayerPrefs.SetFloat("Skill_Kick", skill_Kick);
        PlayerPrefs.SetFloat("Skill_Jump", skill_Jump);
        PlayerPrefs.SetInt("WinsNoConcGoals", winsNoConcGoals);
        PlayerPrefs.SetInt("ButtonIndex", buttonIndex);
		PlayerPrefs.SetInt("Controls", controls);
		PlayerPrefs.SetInt("Game", game);
		PlayerPrefs.SetInt("Award", award);
		PlayerPrefs.SetInt("TaskGoals", tGoals);
		PlayerPrefs.SetInt("IsRandGame", isRandGame);
		PlayerPrefs.SetInt("Launches", launches);
		PlayerPrefs.SetInt("BestResult", bestResult);
        PlayerPrefs.SetInt("WinsTotal", winsTotal);


        for (int i = 0; i < openedPlayers.Length; i++)
		{
			str = "OpenedPlayers_" + i.ToString();
			PlayerPrefs.SetInt(str, openedPlayers[i]); 
		}

        for (int i = 0; i < openedPlayers_2.Length; i++)
        {
            str = "OpenedPlayers_2_" + i.ToString();
            PlayerPrefs.SetInt(str, openedPlayers_2[i]);
        }

		PlayerPrefs.SetInt("IsWin", isW);
		PlayerPrefs.SetInt("Stadium", stadium);
        PlayerPrefs.SetInt("Tribunes", tribunes);
		PlayerPrefs.SetInt("BallNumber", ballN);
        PlayerPrefs.SetInt("PlayerIndexRandom", playerIndexRand);
		PlayerPrefs.SetInt("PlayerIndex", playerIndex);
		PlayerPrefs.SetInt("EnemyIndex", enemyIndex);
		PlayerPrefs.SetInt("MoneyWin", moneyWin);
	}

	private void GetValues()
	{
		string str;

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                str = "Won_" + i.ToString() + "_" + j.ToString();
                wonGames[i, j] = PlayerPrefs.GetInt(str);
            }
        }
          
        for (int i = 0; i < opndLeagues.Length; i++)
        {
            str = "OpenedLeague_" + i.ToString();
            opndLeagues[i] = PlayerPrefs.GetInt(str);
        }

        _camera = PlayerPrefs.GetInt("Camera");
        upgrSpeed = PlayerPrefs.GetInt("UpgradeSpeed");
        upgrKick = PlayerPrefs.GetInt("UpgradeKick");
        upgrJump = PlayerPrefs.GetInt("UpgradeJump");
        upgrSlowdown = PlayerPrefs.GetInt("UpgradeSlowdown");
        upgrShield = PlayerPrefs.GetInt("UpgradeShield");
        upgrMoney = PlayerPrefs.GetInt("UpgradeMoney");

        for (int i = 0; i < upgrBalls.Length; i++)
        {
            str = "UpgradeBall_" + i.ToString();
            upgrBalls[i] = PlayerPrefs.GetInt(str);
        }

        lg = PlayerPrefs.GetInt("League");
        evrdReward = PlayerPrefs.GetInt("EverydayReward");
        plLg = PlayerPrefs.GetInt("PlayerLeague");
        enLg = PlayerPrefs.GetInt("EnemyLeague");
        winsNoConcGoals = PlayerPrefs.GetInt("WinsNoConcGoals");
        buttonIndex = PlayerPrefs.GetInt("ButtonIndex");
		controls = PlayerPrefs.GetInt("Controls");
		game = PlayerPrefs.GetInt("Game");
		award = PlayerPrefs.GetInt("Award");
		tGoals = PlayerPrefs.GetInt("TaskGoals");
		launches = PlayerPrefs.GetInt("Launches");
		isRandGame = PlayerPrefs.GetInt("IsRandGame");
		bestResult = PlayerPrefs.GetInt("BestResult");
        winsTotal = PlayerPrefs.GetInt("WinsTotal");

		for (int i = 0; i < openedPlayers.Length; i++)
		{
			str = "OpenedPlayers_" + i.ToString();
			openedPlayers[i] = PlayerPrefs.GetInt(str);
		}

        for (int i = 0; i < openedPlayers_2.Length; i++)
        {
            str = "OpenedPlayers_2_" + i.ToString();
            openedPlayers_2[i] = PlayerPrefs.GetInt(str);
        }

		isW = PlayerPrefs.GetInt("IsWin");
		stadium = PlayerPrefs.GetInt("Stadium");
        tribunes = PlayerPrefs.GetInt("Tribunes");
		ballN = PlayerPrefs.GetInt("BallNumber");
        playerIndexRand = PlayerPrefs.GetInt("PlayerIndexRandom");
		playerIndex = PlayerPrefs.GetInt("PlayerIndex");
		enemyIndex = PlayerPrefs.GetInt("EnemyIndex");
		
		moneyWin = PlayerPrefs.GetInt("MoneyWin");
	}
}
