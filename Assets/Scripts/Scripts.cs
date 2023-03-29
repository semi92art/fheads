using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Scripts : MonoBehaviour 
{
	// Scripts in all scenes.
	[HideInInspector]
	public AllPrefsScript alPrScr;
	[HideInInspector]
	public AllLevelsScript alScr;
	[HideInInspector]
	public GameManager gM;

	[HideInInspector]
	public DoubleScroll dblScr;
	[HideInInspector]
	public CareerScrollList carScrL;
	[HideInInspector]
	public ProfileScrollList prScrL;
	[HideInInspector]
	public ShopScrollList shScrL;
	[HideInInspector]
	public CupPlayerList cupL;
	[HideInInspector]
	public CupListImage cupLIm;
	[HideInInspector]
	public PlayoffPlayerList plfL;
	[HideInInspector]
	public PlayoffPlayerListSave plfLS;
	[HideInInspector]
	public ChampList chL;
	[HideInInspector]
	public ChampListImage chLIm;
	[HideInInspector]
	public ChampInfoPanelManager infM;
	[HideInInspector]
	public ChampManager chMan;
	[HideInInspector]
	public LostCup lostC;
	[HideInInspector]
	public CurrentProfilePanel currPrPan;
	[HideInInspector]
	public ChallengesScrollList chScrL;
	[HideInInspector]
	public ChallengesManager chalMan;
	[HideInInspector]
	public ContinueTournScript contTrnScr;
	[HideInInspector]
	public StadiumsScrollList stScrL;
	[HideInInspector]
	public PlayoffManager plfMan;
	[HideInInspector]
	public ChangableTextScript changTextScr;
	[HideInInspector]
	public LanguageScript langScr;
	[HideInInspector]
	public EverydayReward everyDayReward;
	[HideInInspector]
	public PlayVideoZoneButton videoZoneBut;
	[HideInInspector]
	public LotteryScript lotteryScr;

	// Scene "Level".
	[Header("Scene 'Level':")]
	[HideInInspector]
	public Score scoreScr;
	[HideInInspector]
	public PlayerMovement pMov;
	[HideInInspector]
	public TimeManager tM;
	[HideInInspector]
	public BallScript ballScr;
	[HideInInspector]
	public BallTouchScript ballTScr;
	[HideInInspector]
	public EnemyCollisionScript enCollScr;
	[HideInInspector]
	public GroundTrigger1 grTr;
	[HideInInspector]
	public CongradulationsPanel congrPan;
	[HideInInspector]
	public LevelAudioScript levAudScr;
	[HideInInspector]
	public StadiumChooseScript stChScr;
	[HideInInspector]
	public JumpScript jScr;
	[HideInInspector]
	public Hints hints;
	[HideInInspector]
	public FlagPanel flagPan;
	[HideInInspector]
	public PlayersLegColor plLegCol;
	[HideInInspector]
	public SunEmitationInGame sunEmit;
	[HideInInspector]
	public SkyScript skyScr;
	[HideInInspector]
	public EnemyAlgoritm enAlg;
	[HideInInspector]
	public ControlOptionsScript controlOptScr;

	// Objects scripts.
	[Header("Objects GameObject")]
	public GameObject obj;

	[HideInInspector]
	public Objects_MainMemu objM;
	[HideInInspector]
	public Objects_Level objLev;
	[HideInInspector]
	public Objects_Cup objCup;
	[HideInInspector]
	public Objects_Playoff objPlf;
	[HideInInspector]
	public Objects_Championship objCh;
	[HideInInspector]
	public Objects_Start objSt;



	void Awake()
	{
		alPrScr = FindObjectOfType<AllPrefsScript> ();
		alScr = FindObjectOfType<AllLevelsScript> ();
		prScrL = FindObjectOfType<ProfileScrollList> ();
		gM = FindObjectOfType<GameManager> ();
		langScr = FindObjectOfType<LanguageScript> ();
		switch (SceneManager.GetActiveScene().name) 
		{
		case "MainMenu":
			dblScr = FindObjectOfType<DoubleScroll> ();
			objM = obj.GetComponent<Objects_MainMemu> ();
			carScrL = FindObjectOfType<CareerScrollList> ();
			shScrL = FindObjectOfType<ShopScrollList> ();
			currPrPan = FindObjectOfType<CurrentProfilePanel> ();
			chScrL = FindObjectOfType<ChallengesScrollList> ();
			chalMan = FindObjectOfType<ChallengesManager> ();
			stScrL = FindObjectOfType<StadiumsScrollList> ();
			contTrnScr = FindObjectOfType<ContinueTournScript> ();
			changTextScr = FindObjectOfType<ChangableTextScript> ();
			everyDayReward = FindObjectOfType<EverydayReward> ();
			plLegCol = FindObjectOfType<PlayersLegColor> ();
			videoZoneBut = FindObjectOfType<PlayVideoZoneButton> ();
			lotteryScr = FindObjectOfType<LotteryScript> ();
			break;
		case "Cup":
			objCup = obj.GetComponent<Objects_Cup> ();
			lostC = FindObjectOfType<LostCup> ();
			cupL = FindObjectOfType<CupPlayerList> ();
			cupLIm = FindObjectOfType<CupListImage> ();
			gM.ChooseLastMenuProfileMaterials ();
			prScrL = FindObjectOfType<ProfileScrollList> ();
			break;
		case "Playoff":
			objPlf = obj.GetComponent<Objects_Playoff>();
			gM.ChooseLastPlayoffListSave ();
			alPrScr = FindObjectOfType<AllPrefsScript> ();
			alScr = FindObjectOfType<AllLevelsScript> ();
			plfL = FindObjectOfType<PlayoffPlayerList> ();
			plfLS = FindObjectOfType<PlayoffPlayerListSave> ();
			lostC = FindObjectOfType<LostCup> ();
			gM.ChooseLastMenuProfileMaterials ();
			prScrL = FindObjectOfType<ProfileScrollList> ();
			plfMan = FindObjectOfType<PlayoffManager> ();
			break;
		case "Championship":
			objCh = obj.GetComponent<Objects_Championship>();
			chMan = FindObjectOfType<ChampManager>();
			chMan.var1 = alPrScr.trn - 6;


			gM.ChooseLastMenuProfileMaterials ();
			chMan.ChooseLastChampList();
			chMan.ChooseLastChampListImage();
			chL = FindObjectOfType<ChampList> ();
			chLIm = FindObjectOfType<ChampListImage> ();
			infM = FindObjectOfType<ChampInfoPanelManager>();
			lostC = FindObjectOfType<LostCup>();
			congrPan = FindObjectOfType<CongradulationsPanel>();
			break;
		case "Level":
			objLev = obj.GetComponent<Objects_Level> ();
			scoreScr = FindObjectOfType<Score> ();
			pMov = FindObjectOfType<PlayerMovement> ();
			tM = FindObjectOfType<TimeManager> ();
			ballScr = FindObjectOfType<BallScript> ();
			enCollScr = FindObjectOfType<EnemyCollisionScript> ();
			grTr = FindObjectOfType<GroundTrigger1> ();
			congrPan = FindObjectOfType<CongradulationsPanel> ();
			levAudScr = FindObjectOfType<LevelAudioScript> ();
			stChScr = FindObjectOfType<StadiumChooseScript> ();
			ballTScr = FindObjectOfType<BallTouchScript> ();
			jScr = FindObjectOfType<JumpScript> ();
			hints = FindObjectOfType<Hints> ();
			flagPan = FindObjectOfType<FlagPanel> ();
			plLegCol = FindObjectOfType<PlayersLegColor> ();
			sunEmit = FindObjectOfType<SunEmitationInGame> ();
			skyScr = FindObjectOfType<SkyScript> ();
			enAlg = FindObjectOfType<EnemyAlgoritm> ();
			controlOptScr = FindObjectOfType<ControlOptionsScript> ();
			
			switch (alPrScr.finishTourn)
			{
			case "":

				break;
			case "Finished":

				break;
			case "FinishedCup":

				break;
			case "CupGoes":

				break;
			case "PlayoffGoes":
				if (alPrScr.freePlay == 0)
					plfLS = FindObjectOfType<PlayoffPlayerListSave> ();
				
				break;
			case "ChampGoes":
				if (alPrScr.freePlay == 0)
				{
					chL = FindObjectOfType<ChampList> ();
					chLIm = FindObjectOfType<ChampListImage> ();
				}
				break;
			}
				
			break;
		case "Start":
			objSt = obj.GetComponent<Objects_Start>();
			break;
		}
	}
}
