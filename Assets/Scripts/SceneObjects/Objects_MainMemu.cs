using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Objects_MainMemu : MonoBehaviour 
{
	public Scripts scr;
	[Header("Gameobject:")]
	public GameObject topPanelShadow;
	public GameObject fpBuyPlayer, profBuyPlayer, profBuyStadium;
	public GameObject noButtonFP, noButtonProfPl, noButtonProfSt;
	public GameObject sampButExtra;
	public GameObject loadPanelMenu;
	public GameObject sampleButton;
	public GameObject profileStadiumButton;
	public GameObject profilePlayersButton;
	[Header("Animator:")]
	public Animator menuCareerAnim;
	public Animator currProfileAnim;
	public Animator prevPlayerAnim, freePlayPlayerAnim;
	public Animator menuExitAnim;
	public Animator menuProfAnim, topPanelAnim, logoAnim,
	menuOptionsAnim, mainMenuAnim;
	public Animator loadingPanelAnim;
	public Animator freePlayAnim;
	public Animator profilePlayersAnim;
	public Animator profStadiumsAnim;
	[Header("Text:")]
	public Text nowPlayingMainText;
	public Text profileLeagueText;
	public Text previewText, prevTextFP;
	[Header("Sprite:")]
	public Sprite profileMenuButtonSpr;
	public Sprite pushedProfileMenuButtonSpr;
	public Sprite blueBox1, orangeBox1;
	public Sprite blueBox, orangeBox, greenBox, orangeBoxEn;
	[Header("RectTransform:")]
	public RectTransform contentPanel1;
	public RectTransform singlePlayerRect, menuExitRect;
	[Header("Another:")]
	public Slider musicSlider, soundSlider;
	public AudioSource mainThemeSource, buttonsSource;

	private GameObject mainMenu, topPanel;
	private int timerMainMenu, timerTopPanel;
	public bool is1stMenuLaunch;

	void Awake()
	{
		PlayerPrefs.SetInt("MainMenuLaunch", PlayerPrefs.GetInt("MainMenuLaunch") + 1);

		if (PlayerPrefs.GetInt("MainMenuLaunch") == 1)
			is1stMenuLaunch = true;
		else
			is1stMenuLaunch = false;

		mainMenu = mainMenuAnim.gameObject;
		topPanel = topPanelAnim.gameObject;
		currProfileAnim.gameObject.SetActive (true);
		soundSlider.value = scr.alPrScr.soundVolume;
		musicSlider.value = scr.alPrScr.musicVolume;
		scr.changTextScr.text1 = scr.changTextScr.gameObject.GetComponent<Text>();
		scr.changTextScr.text1.text = "";
	}

	void Update()
	{
		if (mainMenu.activeSelf) 
		{
			if (mainMenuAnim.GetBool ("call"))
				timerMainMenu++;
		}

		if (timerMainMenu > 50) 
		{
			mainMenu.SetActive (false);
			timerMainMenu = 0;
		}

		if (topPanel.activeSelf) {
			if (!topPanelAnim.GetBool ("call"))// && !scr.everyDayReward.awardGet && !scr.videoZoneBut.awardGet)
				timerTopPanel++;
		}

		if (timerTopPanel > 50) 
		{
			topPanel.SetActive (false);
			timerTopPanel = 0;
		}
	}
	
	public void PixelPerfect()
	{
		bool pixelPerfect = gameObject.GetComponent<Canvas>().pixelPerfect;
		gameObject.GetComponent<Canvas>().pixelPerfect = !pixelPerfect;
	}

	public void SetMenuVolume()
	{
		scr.alPrScr.musicVolume = musicSlider.value;
		scr.alPrScr.isChange0 = true;
		buttonsSource.volume = scr.alPrScr.musicVolume;
		mainThemeSource.volume = scr.alPrScr.musicVolume;
	}

	public void SetGameVolume()
	{
		scr.alPrScr.soundVolume = soundSlider.value;
		scr.alPrScr.isChange0 = true;
	}
}
