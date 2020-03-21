using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AllPrefsScript : MonoBehaviour 
{
	public Scripts scr;
    
    public int[,] wonGames = new int[10,6];
    public int lg, game;
    public int upgrSpeed;
    public int upgrKick;
    public int upgrJump;
    public int upgrSlowdown;
    public int upgrShield;
    public int[] upgrBalls = new int[3];
    public int upgrMoney;

    
	#region engine methods
	
	private void Awake()
	{
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (PrefsManager.Instance.LaunchesCount == 0)
            {
                PlayerPrefs.SetInt("UpgradeBall_0", 1);
                PlayerPrefs.SetInt("OpenedLeague_0", 1);
                PlayerPrefs.SetInt("Graph", 1);
                PlayerPrefs.SetInt("Tilt", 1);
                PlayerPrefs.SetInt("ButtonsSize", 2);
                PlayerPrefs.SetFloat("ButtonsCapacity", 0.5f);
                PlayerPrefs.SetInt("SoundOn", 0);
                PlayerPrefs.SetInt("PlayerLeague", 1);
                PlayerPrefs.SetInt("EnemyLeague", 1);
                PlayerPrefs.SetInt("EverydayReward", 1000);
                PlayerPrefs.SetInt("PlayerIndex", 35);
                PlayerPrefs.SetInt("Controls", 1);
                PlayerPrefs.SetInt("Game", 1);
                PlayerPrefs.SetInt("OpenedPlayers_0", 1);
                PlayerPrefs.SetInt("OpenedPlayers_1", 1);
                PlayerPrefs.SetInt("OpenedGamesL1_0", 1);
                PlayerPrefs.SetInt("OpenedGamesL2_0", 1);
                PlayerPrefs.SetInt("OpenedChallenges_0", 1);
            }
            
            PlayerPrefs.SetInt("MenuTrigger_1", 1);
            PrefsManager.Instance.LaunchesCount++;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (PrefsManager.Instance.LaunchesCount == 1)
                PlayerPrefs.SetInt("SoundOn", 1);

            FindObjectOfType<LevelTimeManager>().rigBodies = FindObjectsOfType<Rigidbody2D>();
            FindObjectOfType<LevelTimeManager>().contrRidBodies = 
                new Rigidbody2D[FindObjectOfType<LevelTimeManager>().rigBodies.Length];
        }

        GetValues();
	}

	void Update()
	{
#if UNITY_EDITOR
        InputFcn();
#endif
    }
	
	#endregion

    private void ViewWonGames()
    {
        for (int j = 0; j < 6; j++)
        for (int i = 0; i < 10; i++)
            Debug.Log($"Game {i}, League {j}, Win = {wonGames[i, j].ToString()}");
        
    }

	private void InputFcn()
	{
        if (SceneManager.GetActiveScene ().buildIndex == 1) 
		{
            if (Input.GetKeyDown(KeyCode.P))
                ViewWonGames();

			if (Input.GetKeyDown(KeyCode.D))
			{
				PlayerPrefs.DeleteAll();
				PrefsManager.Instance.MoneyCount = 0;
			}

			if (Input.GetKeyDown(KeyCode.M))
				PrefsManager.Instance.MoneyCount += 50000;
		

            if (Input.GetKeyDown(KeyCode.Alpha1))
                scr.allAw.CallAwardPanel_1();
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                scr.allAw.CallAwardPanel_3(); 

            if (Input.GetKeyDown(KeyCode.C))
                System.GC.Collect();

            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                if (GoogleMobileAd.IsInterstitialReady)
                    GoogleMobileAd.ShowInterstitialAd ();
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                FindObjectOfType<UnityAds_0>().ShowSimpleAd();       
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                FindObjectOfType<UnityAds_0>().ShowRewardedAd();        
            }
		}
        else if (SceneManager.GetActiveScene ().buildIndex == 2) 
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                scr.bonObjMan.BallBig(-1);
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                scr.bonObjMan.BallBig(-2);
            else if (Input.GetKeyDown(KeyCode.Alpha3))
                scr.bonObjMan.BallBig(-3);
            else if (Input.GetKeyDown(KeyCode.Alpha4))
                scr.bonObjMan.PlayerSpeedUp(2);
            else if (Input.GetKeyDown(KeyCode.Alpha5))
                scr.bonObjMan.EnemySpeedUp(2);
            else if (Input.GetKeyDown(KeyCode.Alpha6))
                scr.bonObjMan.SetBonusPosition(BonusType.bonusType.WatchVideo);

            if (Input.GetKeyDown(KeyCode.Z))
                scr.timFr.TimeFreeze_StartOrStop();
            else if (Input.GetKeyDown(KeyCode.X))
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
                str = $"Won_{i}_{j}";
                PlayerPrefs.SetInt(str, wonGames[i,j]);
            }
        }

        PlayerPrefs.SetInt("UpgradeSpeed", upgrSpeed);
        PlayerPrefs.SetInt("UpgradeKick", upgrKick);
        PlayerPrefs.SetInt("UpgradeJump", upgrJump);
        PlayerPrefs.SetInt("UpgradeSlowdown", upgrSlowdown);
        PlayerPrefs.SetInt("UpgradeShield", upgrShield);
        PlayerPrefs.SetInt("UpgradeMoney", upgrMoney);

        for (int i = 0; i < upgrBalls.Length; i++)
        {
            str = $"UpgradeBall_{i}";
            PlayerPrefs.SetInt(str, upgrBalls[i]);
        }

        PlayerPrefs.SetInt("League", lg);
        PlayerPrefs.SetInt("Game", game);
    }

	private void GetValues()
	{
		string str;

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                str = $"Won_{i}_{j}";
                wonGames[i, j] = PlayerPrefs.GetInt(str);
            }
        }

        upgrSpeed = PlayerPrefs.GetInt("UpgradeSpeed");
        upgrKick = PlayerPrefs.GetInt("UpgradeKick");
        upgrJump = PlayerPrefs.GetInt("UpgradeJump");
        upgrSlowdown = PlayerPrefs.GetInt("UpgradeSlowdown");
        upgrShield = PlayerPrefs.GetInt("UpgradeShield");
        upgrMoney = PlayerPrefs.GetInt("UpgradeMoney");

        for (int i = 0; i < upgrBalls.Length; i++)
        {
            str = $"UpgradeBall_{i}";
            upgrBalls[i] = PlayerPrefs.GetInt(str);
        }

        lg = PlayerPrefs.GetInt("League");
        game = PlayerPrefs.GetInt("Game");
    }
}
