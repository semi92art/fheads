using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Lean.Common;
using UnityEngine.UI;

public enum Menues
{
    mainMenu,
    menuCareer,
    menuPlayers,
    menuUpgrades
}


public class GameManager : MonoBehaviour 
{
    public Scripts scr;

	public bool quitPanel, pauseInLevel, gamePaused;

    private bool firstCallProfPl;
	private int loadPanTimer = -2;
	private int timer;
    [HideInInspector]
    public bool isNoAds;

	[Header("Loaded menues in main scene Menu")]
    public Menues _menues;

	void Awake()
	{
        isNoAds = scr.univFunc.Int2Bool(PlayerPrefs.GetInt("NoAds"));

		switch (SceneManager.GetActiveScene().buildIndex) 
		{
            case 1:
                scr.alPrScr.game = 0;
                scr.alPrScr.doCh = true;
                PlayerPrefs.SetInt("CanRestart", 2);

                scr.buf.isPractice = false;
                scr.allAw.allAwPan.SetActive(false);
                scr.alPrScr.isRandGame = 0;

                Time.timeScale = 1f;

                _menues = Menues.mainMenu;
    			DestroyImmediate (GameObject.Find ("ChampList"));
    			DestroyImmediate (GameObject.Find ("ChampListImage"));
    			break;
    		case 2:
    			break;
		}

		scr.alPrScr.doCh = true;
	}

	void Update()
	{
        if (SceneManager.GetActiveScene().buildIndex == 1)
            Update_Menu();
        else if (SceneManager.GetActiveScene().buildIndex == 2)
            Update_Level();
	}

    private void Update_Menu()
    {
#if UNITY_EDITOR
        if(LeanInput.GetPressed(KeyCode.R))
            SceneManager.LoadScene("Menu");
#endif

        if (LeanInput.GetUp (KeyCode.Escape))
        {
            switch (_menues)
            {
                case Menues.mainMenu:
                    Application.Quit();
                    break;
                case Menues.menuCareer:
                    DependentMenuBack();
                    break;
                case Menues.menuPlayers:
                    DependentMenuBack();
                    break;
            }
        }
    }
		
    private void Update_Level()
    {
        if (LeanInput.GetUp (KeyCode.Escape)) 
        {
            if (scr.tM.isBetweenTimes && Time.timeScale <= 0.1f)
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
                        switch (scr.objLev.quitPanel.activeSelf)
                        {
                            case true:
                                PauseQuitBack();

                                break;
                            case false:
                                PauseBack();
                                scr.objLev.mainCanvas.enabled = false;
                                break;
                        }
                    } 
                }
            }
        }

        if (LeanInput.GetUp(KeyCode.Space))
        {
            if (!scr.pMov.startGame || scr.tM.isBetweenTimes)
                StartGame();
        }
    }

	public void StartGame()
	{
		Time.timeScale = 1f;
		scr.pMov.startGame = true;
		scr.objLev.startPanelAnim.gameObject.SetActive(false);
	}

	public void DependentMenuBack() 
	{
        switch (_menues)
        {
            case Menues.mainMenu:
                ExitGame();
                break;
            case Menues.menuCareer:
                scr.objM.Menu_Tournaments(false);
                break;
            case Menues.menuPlayers:
                scr.objM.Menu_Players(false);
                break;
            case Menues.menuUpgrades:
                scr.objM.Menu_Upgrades(false);
                break;
        }

        _menues = Menues.mainMenu;
	}

    public void MenuProfilePlayers()
    {
        if (!firstCallProfPl)
        {
            scr.objM.cP.anchoredPosition = new Vector2(
                0.0f, scr.objM.cP.anchoredPosition.y);
            
            firstCallProfPl = true;
        }
            
        scr.objM.currPrPan.SetActive(false);
        scr.prMng.SetOpenedPlayersCountryText(false);
        _menues = Menues.menuPlayers;
    }

	public void GoToMenu()
	{
        scr.buf.is1stPractice = false;
        SceneManager.LoadScene("Menu");
        scr.alPrScr.doCh = true;
	}
	
	public void GoToMenuNewGame (RectTransform tr)
	{
		tr.anchoredPosition = new Vector3 (tr.anchoredPosition.x, -300, 0);
	}

	public void IsInPause()
	{
		Time.timeScale = 1f;
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
        scr.alPrScr.stadium = scr.alPrScr.isRandGame == 1 ? 
            Mathf.FloorToInt(Random.value * (18 - 0.01f)) : 
            scr.univFunc.Stadium(scr.alPrScr.game);

        scr.alPrScr.tribunes = scr.alPrScr.isRandGame == 0 ? 
            scr.alPrScr.lg : Mathf.FloorToInt(1f + (5 - 0.1f) * Random.value);

		scr.alPrScr.doCh = true;
	}
        
	public void LoadSimpleLevel()
	{
		SceneManager.LoadScene(SceneNames.Level);
	}
        
	public void LoadRandomLevel()
	{
        scr.alPrScr.isRandGame = 1;
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
        currTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        Rigidbodies_TimeScale(0);
	}
	
	public void PauseBack()
	{
		pauseInLevel = false;
        scr.objLev.pauseMenuAnim.SetTrigger(Animator.StringToHash("back"));
        Time.timeScale = currTimeScale;
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
		Time.timeScale = 1f;
		scr.objLev.resultMenuAnim.gameObject.SetActive(false);
	}

	/*public void MenuProfilePlayersBack ()
	{
        _menues = Menues.mainMenu;
	}

	public void ChooseLastMenuProfileMaterials()
	{
		GameObject[] menuProfMat = GameObject.FindGameObjectsWithTag ("MenuProfileMaterials");

		if (menuProfMat.Length == 2)
		{
			if (SceneManager.GetActiveScene().buildIndex == 1)
				DestroyImmediate (menuProfMat [0]); 
			 else 
				DestroyImmediate (menuProfMat [1]); 
		}
	}*/

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

    /*public void LoadPractice()
    {
        scr.buf.isPractice = true;
        scr.buf.SetData();
        SceneManager.LoadScene("Level");
        scr.objM.buttonsSource.Play();
    }*/

    private IEnumerator LoadLevelSceneCoroutine()
    {
	    var loadParams = new LoadSceneParameters(LoadSceneMode.Single, LocalPhysicsMode.Physics2D);
	    var op = SceneManager.LoadSceneAsync(SceneNames.Level, loadParams);
	    while (!op.isDone)
		    yield return null;
    }
}



