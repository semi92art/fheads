using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	
	

	
	#region private methods


	
	#endregion
	
	#region engine methods

	private void Awake()
	{
		if (Instance != null)
		{
			DestroyImmediate(gameObject);
			return;
		}
		Instance = this;

		
		isNoAds = scr.univFunc.Int2Bool(PlayerPrefs.GetInt("NoAds"));

		switch (SceneManager.GetActiveScene().name) 
		{
			case "____Menu":
				scr.alPrScr.game = 0;
				PlayerPrefs.SetInt("CanRestart", 2);

				scr.allAw.allAwPan.SetActive(false);
				PrefsManager.Instance.IsRandomOpponent = false;

				TimeManager.Instance.UnPauseGame();

				_menues = Menues.MainMenu;
				DestroyImmediate (GameObject.Find ("ChampList"));
				DestroyImmediate (GameObject.Find ("ChampListImage"));
				break;
			case "____Level":
				PrefsManager.Instance.PlayedGames++;
				break;
		}
	}

	private void Update()
	{
		if (SceneManager.GetActiveScene().buildIndex == 1)
			Update_Menu();
		else if (SceneManager.GetActiveScene().buildIndex == 2)
			Update_Level();
	}

	#endregion
	
	
	
	
	
    public Scripts scr;

	public bool quitPanel, pauseInLevel, gamePaused;

    private bool firstCallProfPl;
	private int loadPanTimer = -2;
	private int timer;
    [HideInInspector]
    public bool isNoAds;

	[Header("Loaded menues in main scene Menu")]
    public Menues _menues;

    

    private void Update_Menu()
    {
#if UNITY_EDITOR
        if(Input.GetKey(KeyCode.R))
            SceneManager.LoadScene("____Menu");
#endif

        if (Input.GetKeyUp (KeyCode.Escape))
        {
            switch (_menues)
            {
                case Menues.MainMenu:
                    Application.Quit();
                    break;
                case Menues.MenuCareer:
                    DependentMenuBack();
                    break;
                case Menues.MenuPlayers:
                    DependentMenuBack();
                    break;
            }
        }
    }
		
    private void Update_Level()
    {
        if (Input.GetKeyUp (KeyCode.Escape)) 
        {
            if (scr.tM.isBetweenTimes && TimeManager.Instance.GamePaused)
            {
                scr.tM.CallBackBetweenTimesPanel();
                scr.levAudScr.Button_Sound();
            }
            else
            {
                if (!gamePaused && scr.pMov.startGame)
                {
                    scr.levAudScr.Button_Sound();

                    if (!pauseInLevel)
                    {
                        Pause();
                        scr.objLev.mainCanvas.enabled = true;
                    }   
                    else
                    {
	                    if (scr.objLev.quitPanel.activeSelf)
		                    PauseQuitBack();
	                    else
							PauseBack();
                    } 
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (!scr.pMov.startGame || scr.tM.isBetweenTimes)
                StartGame();
        }
    }

	public void StartGame()
	{
		TimeManager.Instance.UnPauseGame();
		scr.pMov.startGame = true;
		scr.objLev.startPanelAnim.gameObject.SetActive(false);
	}

	public void DependentMenuBack() 
	{
        switch (_menues)
        {
	        case Menues.MainMenu:
		        GameSettings();
                break;
            case Menues.MenuCareer:
                scr.objM.Menu_Tournaments(false);
                break;
            case Menues.MenuPlayers:
                scr.objM.Menu_Players(false);
                break;
            case Menues.MenuUpgrades:
                scr.objM.Menu_Upgrades(false);
                break;
        }

        _menues = Menues.MainMenu;
	}

	private void GameSettings()
	{
		//empty function for game settings
	}

    public void MenuProfilePlayers()
    {
        if (!firstCallProfPl)
        {
            scr.objM.cP.anchoredPosition = new Vector2(0.0f, scr.objM.cP.anchoredPosition.y);
            firstCallProfPl = true;
        }
            
        scr.objM.currPrPan.SetActive(false);
        scr.prMng.SetOpenedPlayersCountryText(false);
        _menues = Menues.MenuPlayers;
    }

	public void GoToMenu()
	{
		SceneManager.LoadScene("____Menu");
	}
	
	public void GoToMenuNewGame (RectTransform tr)
	{
		tr.anchoredPosition = new Vector3 (tr.anchoredPosition.x, -300, 0);
	}

	public void IsInPause()
	{
		TimeManager.Instance.UnPauseGame();
	}
	
	public void WinGame1()
	{
		if (SceneManager.GetActiveScene().buildIndex == 2)
		{
			Score.score = 6;
			Score.score1 = 0;
			scr.scoreScr.SetScore();
			scr.tM.time0 = 2;
		}
	}

	public void LooseGame()
	{
		if (SceneManager.GetActiveScene().buildIndex == 2)
		{
			Score.score = 0;
			Score.score1 = 3;
			scr.scoreScr.SetScore();
			scr.tM.time0 = 2;
		}
	}

	public void TieGame()
	{
		if (SceneManager.GetActiveScene().buildIndex == 2)
		{
			Score.score = 1;
			Score.score1 = 1;
			scr.scoreScr.SetScore();
			scr.tM.time0 = 2;
		}
	}

	public void SetStadium()
	{
        PrefsManager.Instance.Stadium = PrefsManager.Instance.IsRandomOpponent ? 
            Mathf.FloorToInt(Random.value * (18 - 0.01f)) : 
            scr.univFunc.Stadium(scr.alPrScr.game);

        PrefsManager.Instance.Tribunes = !PrefsManager.Instance.IsRandomOpponent ? 
            scr.alPrScr.lg : Mathf.FloorToInt(1f + (5 - 0.1f) * Random.value);
	}
        
	public void LoadSimpleLevel()
	{
		SceneManager.LoadScene(2);
	}
        
	public void LoadRandomLevel()
	{
		PrefsManager.Instance.IsRandomOpponent = true;
        SetStadium();
        scr.buf.SetRandomData();
		SceneManager.LoadScene(2);
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void DeleteAllPrefs()
	{
		PlayerPrefs.DeleteAll ();
	}

    [HideInInspector]
    public float currTimeScale;

	public void Pause () 
	{
        System.GC.Collect();
        pauseInLevel = true;
        scr.objLev.pauseMenuAnim.gameObject.SetActive(true);
        scr.objLev.pauseMenuAnim.ResetTrigger(Animator.StringToHash("back"));
        scr.objLev.pauseMenuAnim.SetTrigger (Animator.StringToHash("call"));
        currTimeScale = TimeManager.Instance.TimeScale;
        TimeManager.Instance.PauseGame();
        Rigidbodies_TimeScale(0);
	}
	
	public void PauseBack()
	{
		pauseInLevel = false;
        scr.objLev.pauseMenuAnim.SetTrigger(Animator.StringToHash("back"));
        TimeManager.Instance.TimeScale = currTimeScale;
		scr.objLev.pauseMenuAnim.gameObject.SetActive(false);
        Rigidbodies_TimeScale(1);
	}

	public void PauseQuitBack ()
	{
		scr.objLev.quitPanel.SetActive(false);
		scr.objLev.pauseMenuAnim.gameObject.SetActive(true);
        scr.objLev.pauseMenuAnim.SetTrigger(Animator.StringToHash("call"));
	}

	public void MenuResult()
	{
		gamePaused = true;
        scr.enAlg.enabled = false;
        scr.enAlg_1.enabled = false;
        scr.pMov.enabled = false;
        scr.bonObjMan.enabled = false;
		scr.objLev.resultMenuAnim.gameObject.SetActive(true);
	}

	public void MenuResultBack()
	{
		scr.objLev.resultMenuAnim.SetBool ("call", false);
		TimeManager.Instance.UnPauseGame();
		Time.timeScale = 1f;
		scr.objLev.resultMenuAnim.gameObject.SetActive(false);
	}

    public void Rigidbodies_TimeScale(int tScale)
    {
        for (int i = 0; i < scr.objLev.allRbs.Length; i++)
        {
            if (tScale == 0)
                scr.objLev.allRbs[i].bodyType = RigidbodyType2D.Kinematic;
            else
                scr.objLev.allRbs[i].bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public void Rigidbodies_TimeScale_0()
    {
        for (int i = 0; i < scr.objLev.allRbs.Length; i++)
            scr.objLev.allRbs[i].constraints = RigidbodyConstraints2D.FreezeAll;
    }
    
    
}



