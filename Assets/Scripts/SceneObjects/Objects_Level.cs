using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Objects_Level : MonoBehaviour
{
	public Scripts scr;

	public GameObject exitButton;
	public GameObject stadiumsObj;
	public GameObject loadingPanel;
	public GameObject loadPanelImage, loadPanelBallIconImage;
	public GameObject optionsButton;
	public GameObject pauseControlsScreen;
	public GameObject quitYesButton0, quitYesButtonFP;
	public GameObject quitPanel;
	public GameObject restartButton;
	public GameObject resumeButton;
	public GameObject tips;

	public Transform ballTr;
	public Transform enemy0Tr, playerTr;

	public Text touchToBeginText, touchToBeginText1;
	public Text quitText;
	public Text loadPanelText;
	public Text secontTimePanelText;

	public Animator secondTimePanelAnim;
	public Animator pauseMenuAnim, pauseOptionsAnim, resultMenuAnim;
	public Animator startPanelAnim;

	public Canvas mainCanvas, controlsCanvas;
	public Sprite goalSpr, goalSprRus;
	public Sprite unknownEnemySpr;
	public Image goalIm, advIm;
	public Slider musicSlider, soundSlider;
	public Rigidbody2D enemy0Rb, playerRb;
	public Rigidbody2D ballRb;
	public BoxCollider2D gates1Coll, gates2Coll;
	public Button playButton;
	public Button startGameButton;
	public Image[] editorColors;
	[HideInInspector]
	public bool isMoneyWinPopulate;
	[HideInInspector]
	public int totalPrice;

	public bool transparate;
	private string onlineStr, disconnectStr;
	private int timer;
	private float currUnscaledTime;
	private float randStartTime;

	void Awake()
	{
		advIm.enabled = false;

		if ((Application.systemLanguage == SystemLanguage.Russian ||
			Application.systemLanguage == SystemLanguage.Ukrainian ||
			Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange) 
		{
			loadPanelText.text = "Выберите стадион";
			goalIm.sprite = goalSprRus;
			onlineStr = "поиск оппонента";
			disconnectStr = "нет соединения с интернетом";
		} 
		else
		{
			loadPanelText.text = "Choose stadium";
			goalIm.sprite = goalSpr;
			onlineStr = "searching for opponent";
			disconnectStr = "no connection to internet";
		}

		tips.SetActive (false);
		quitPanel.SetActive (false);
		soundSlider.value = scr.alPrScr.soundVolume;
		musicSlider.value = scr.alPrScr.musicVolume;
		SetMenuVolume ();
		SetGameVolume ();

		if (transparate)
		{
			foreach (var item in editorColors)
				item.color = Color.clear;
		}
			
		if (scr.alPrScr.freePlay == 0) 
			restartButton.SetActive(false);
		

		if (scr.alPrScr.freePlay == 0)
		{
			if ((Application.systemLanguage == SystemLanguage.Russian ||
				Application.systemLanguage == SystemLanguage.Ukrainian ||
				Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange) 
				quitText.text = "Вам засчтают поражение 3-0.\nПродолжить? ";
			else
				quitText.text = "You will lose 3-0.\nContinue? ";

			quitYesButton0.SetActive(true);
			quitYesButtonFP.SetActive(false);
		}
		else if (scr.alPrScr.freePlay == 1)
		{
			if ((Application.systemLanguage == SystemLanguage.Russian ||
				Application.systemLanguage == SystemLanguage.Ukrainian ||
				Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
				quitText.text = "Выйти в главное меню?";
			else
				quitText.text = "Exit to main menu?";
			
			quitYesButton0.SetActive(false);
			quitYesButtonFP.SetActive(true);
		}

		/*
		if (scr.alPrScr.multiplayer == 1)
		{
			scr.multPlScr.debugText = debugText;
			scr.multPlScr.playerStatus = playerStatus;
			scr.multPlScr.gameStateText = gameStateText;
			scr.multPlScr.statusBarText = statusBarText;
			scr.multPlScr.playButton = playButton;
			scr.multPlScr.patricipants = patricipants;
		}
		*/

		DeactivateMenuesOnStart();

		if (scr.alPrScr.isLeaderboardGame == 1)
		{
			touchToBeginText.text = onlineStr;
			touchToBeginText1.enabled = false;
			startGameButton.interactable = false;
			randStartTime = 3.0f + Random.value * 5.0f;
			scr.enAlg.enemySprite.sprite = unknownEnemySpr;

			string HtmlText = FindObjectOfType<AllLevelsScript>().GetHtmlFromUri("http://google.com");

			if(HtmlText == "")
				isConnectedToInternet = false;
			else if(!HtmlText.Contains("schema.org/WebPage"))
				isConnectedToInternet = false;
			else
				isConnectedToInternet = true;
		}
	}

	public bool isConnectedToInternet;
	private float goBackTimer;

	void Update()
	{
		if(!scr.pMov.startGame)
		{
			if (scr.alPrScr.isLeaderboardGame == 1)
			{
				currUnscaledTime += Time.unscaledDeltaTime;

				if (currUnscaledTime < randStartTime)
					StartTextOnlineUpdate();
				else
				{
					if (!isConnectedToInternet)
					{
						if (goBackTimer < 0.01f)
							touchToBeginText.text = disconnectStr;

						goBackTimer += Time.unscaledDeltaTime;

						if (goBackTimer > 2.0f)
							SceneManager.LoadScene("MainMenu");
					}
					else
					{
						if (!startGameButton.interactable)
						{
							startGameButton.interactable = true;
							startGameButton.onClick.Invoke();
							scr.enAlg.enemySprite.sprite = scr.alScr.enemySprite;

							for (int i = 0; i < scr.plLegCol.FlagColorsList.Count; i++)
							{
								if (scr.alScr.enemyFlag == scr.plLegCol.FlagColorsList[i].flagSpr)
								{
									scr.pMov.enLegSprR.sprite = scr.plLegCol.FlagColorsList [i].legSpr;
									break;
								}
							}
						}
					}
				}
			}
		}
	}
	
	public void SetMenuVolume()
	{
		scr.alPrScr.musicVolume = musicSlider.value;
		scr.alPrScr.isChange0 = true;
		scr.levAudScr.menuButtonsSource.volume = musicSlider.value;
		scr.levAudScr.mainThemeSource.volume = musicSlider.value;
		scr.levAudScr.moneyWinSource.volume = musicSlider.value;
		scr.levAudScr.moneyWinSource1.volume = musicSlider.value;
	}

	public void SetGameVolume()
	{
		scr.alPrScr.soundVolume = soundSlider.value;
		scr.alPrScr.isChange0 = true;
		scr.levAudScr.goalSource.volume = soundSlider.value;
		scr.levAudScr.ballTouchSource.volume = soundSlider.value;
		scr.levAudScr.playerJumpSource.volume = soundSlider.value;
		scr.levAudScr.enemyJumpSource.volume = soundSlider.value;
		scr.levAudScr.ticTocSource.volume = soundSlider.value;
	}

	private void DeactivateMenuesOnStart()
	{
		pauseMenuAnim.gameObject.SetActive(false);
		pauseOptionsAnim.gameObject.SetActive(false);
		resultMenuAnim.gameObject.SetActive(false);
		secondTimePanelAnim.gameObject.SetActive(false);
	}

	public void DestroyGameObject(GameObject obj)
	{
		#if UNITY_EDITOR
		DestroyImmediate(obj);
		#endif
	}

	public void ShowInterstitialAd()
	{
		Debug.Log ("Show Interstitial Ad");
		// TODO
	}

	private void StartTextOnlineUpdate()
	{
		if (Time.frameCount % 50 == 0)
		{
			timer++;

			if (timer == 4)
				timer = 0;

			switch (timer)
			{
			case 0:
				touchToBeginText.text = onlineStr;
				break;
			case 1:
				touchToBeginText.text = onlineStr + ".";
				break;
			case 2:
				touchToBeginText.text = onlineStr + "..";
				break;
			case 3:
				touchToBeginText.text = onlineStr + "...";
				break;
			}
		}

	}
}
