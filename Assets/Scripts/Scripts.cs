﻿using UnityEngine;

public class Scripts : MonoBehaviour 
{
    [Header("Menu scene:")]
    public Upgrades upgr;
    public CountriesList cntrL;
    public PlayersList plL;
    public AllAwardsScript allAw;
    public CareerManager carMng;
    public CurrentProfilePanel currPrPan;
    public EverydayReward everyDayReward;
    public Objects_Menu objM;

    [Header("Level scene:")]
    public LighteningScript molnia;
    public Practice practScr;
    public RainManager rainMan;
    
    public BonusObjManager bonObjMan;
    public EnableOrDisable enOrDis;
    public Objects_Level objLev;
    public CameraSize camSize;
    public ColorCorrectionControl colCorr;
    public Markers marks;
    public GoalPanelScript goalPanScr;
    public Score scoreScr;
    public LevelTimeManager tM;
    public BallScript ballScr;
    public BallTouchScript ballTScr;
    public GroundTrigger1 grTr;
    public CongradulationsPanel congrPan;
    public LevelSounds levAudScr;
    public StadiumChooseScript stChScr;
    public JumpScript jScr;
    public SkyScript skyScr;
    public FireworkScript fwScr;
    public Anim_Shield plShield;
    public Anim_Shield enShield;

    

    void Awake()
    {
        if (cntrL == null) cntrL = FindObjectOfType<CountriesList>();
        if (plL == null) plL = FindObjectOfType<PlayersList>();

        switch (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex) 
        {
            case 1:
                if (upgr == null) upgr = FindObjectOfType<Upgrades>();
                if (allAw == null) allAw = FindObjectOfType<AllAwardsScript>();
                if (objM == null) objM = FindObjectOfType<Objects_Menu>();
                if (currPrPan == null) currPrPan = FindObjectOfType<CurrentProfilePanel>();
                if (everyDayReward == null) everyDayReward = FindObjectOfType<EverydayReward>();
                if (carMng == null) carMng = FindObjectOfType<CareerManager>();
                break;
            case 2:
                if (fwScr == null) fwScr = FindObjectOfType<FireworkScript>();
                if (molnia == null) molnia = FindObjectOfType<LighteningScript>();
                if (practScr == null) practScr = FindObjectOfType<Practice>();
                if (rainMan == null) rainMan = FindObjectOfType<RainManager>();
                if (bonObjMan == null) bonObjMan = FindObjectOfType<BonusObjManager>();
                if (enOrDis == null) enOrDis = FindObjectOfType<EnableOrDisable>();
                if (camSize == null) camSize = FindObjectOfType<CameraSize>();
                if (colCorr == null) colCorr = FindObjectOfType<ColorCorrectionControl>();
                if (marks == null) marks = FindObjectOfType<Markers>();
                if (goalPanScr == null) goalPanScr = FindObjectOfType<GoalPanelScript>();
                if (objLev == null) objLev = FindObjectOfType<Objects_Level>();
                if (scoreScr == null) scoreScr = FindObjectOfType<Score>();
                if (tM == null) tM = FindObjectOfType<LevelTimeManager>();
                if (ballScr == null) ballScr = FindObjectOfType<BallScript>();
                if (grTr == null) grTr = FindObjectOfType<GroundTrigger1>();
                if (congrPan == null) congrPan = FindObjectOfType<CongradulationsPanel>();
                if (levAudScr == null) levAudScr = FindObjectOfType<LevelSounds>();
                if (stChScr == null) stChScr = FindObjectOfType<StadiumChooseScript>();
                if (ballTScr == null) ballTScr = FindObjectOfType<BallTouchScript>();
                if (jScr == null) jScr = FindObjectOfType<JumpScript>();
                if (skyScr == null) skyScr = FindObjectOfType<SkyScript>();
                break;
        }
    }
}
