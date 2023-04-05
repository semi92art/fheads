using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Practice : MonoBehaviour 
{
    public Scripts scr;

    public bool isHandPractice;
    public Text
    text_Practice_0,
    text_Practice_1,
    text_Practice_2;

    public GameObject obj_PracticePlayerText;
    public GameObject obj_PracticeEnemyText;
    
    public Transform plGatesTr, enGatesTr;
    public GameObject[] objs_ToDisable;
    public GameObject[] objs_ToEnable;

    public GameObject obj_MoveLeftButton, obj_MoveRightButton;
    public GameObject obj_JumpButton;
    public GameObject obj_KickButton;

    [HideInInspector]
    public bool isPractice;


    void Awake()
    {
        /*
        //SetLanguageTexts();

        //obj_PracticePlayerText.GetComponent<TextMesh>().text = str_PlayerText;
        //obj_PracticeEnemyText.GetComponent<TextMesh>().text = str_EnemyText;

        #if UNITY_EDITOR
        if (isHandPractice)
            isPractice = true;
        else
            isPractice = scr.buf != null ? scr.buf.isPractice : false;
        #else
        isPractice = scr.buf.isPractice;

        #endif

        if (!isPractice)
        {
            text_Practice_0.enabled = false;
            text_Practice_1.text = scr.alPrScr.pldG < 5 ? str_TouchTopHalf : "";
        }
        else
        {
            scr.levAudScr.tribunes.mute = true;
            text_Practice_1.text = "";
        }
            

        if (scr.univFunc.sysLang() == 1 || scr.univFunc.sysLang() == 2 || scr.univFunc.sysLang() == 3)
        {
            text_Practice_0.font = scr.langScr.font_Second;
            text_Practice_1.font = scr.langScr.font_Second;
            text_Practice_2.font = scr.langScr.font_Second;
            //text_Practice_3.font = scr.langScr.font_Second;
            obj_PracticeEnemyText.GetComponent<TextMesh>().font = scr.langScr.font_Second;
            obj_PracticePlayerText.GetComponent<TextMesh>().font = scr.langScr.font_Second;
        }

        text_Practice_0.gameObject.SetActive(true);*/
    }

    [HideInInspector]
    public string str_TouchTopHalf;
    [HideInInspector]
    public string str_StartPractice;
    [HideInInspector]
    public string str_FinishPractice;
    [HideInInspector]
    public string str_MoveHimUsing;
    [HideInInspector]
    public string str_KickJump;
    [HideInInspector]
    public string str_BycicleKick;
    [HideInInspector]
    public string str_Slowdown;
    [HideInInspector]
    public string str_MeetYourOpp;
    [HideInInspector]
    public string str_Score3Goals;
    [HideInInspector]
    public string str_PlayerText;
    [HideInInspector]
    public string str_EnemyText;

	void Start () 
    {
        /*if (isPractice)
        {
            obj_MoveLeftButton.SetActive(false);
            obj_MoveRightButton.SetActive(false);
            obj_JumpButton.SetActive(false);
            obj_KickButton.SetActive(false);
            scr.objLev.obj_BK_But1.SetActive(false);
            text_Practice_2.gameObject.SetActive(true);
            //text_Practice_3.gameObject.SetActive(true);
            obj_PracticePlayerText.SetActive(true);
            obj_PracticeEnemyText.SetActive(true);
            enGatesTr.GetComponent<Animator>().enabled = true;
            plGatesTr.GetComponent<Animator>().enabled = true;


            //scr.timFr.EnableUnlimitedFreeze();
            scr.objLev.touchToBeginText.text = str_StartPractice;
            scr.tM.timeFreeze = true;
            scr.tM.timeText.gameObject.SetActive(false);
            scr.enAlg._tr.gameObject.SetActive(false);
            scr.enAlg._tr.position = scr.marks.enGatesPracticeTr.position;
            scr.scoreScr.playerScore.enabled = false;
            scr.scoreScr.enemyScore.enabled = false;
            scr.rainMan.SetRain_Off();
            scr.bonObjMan.gameObject.SetActive(false);
            scr.stChScr.SetStadiumHandleByNumber(0);
            scr.skyScr.SetTrainingTribunes();
            plGatesTr.position = scr.marks.plGatesPracticeTr.position;
            enGatesTr.position = scr.marks.enGatesPracticeTr.position;
            text_Practice_0.gameObject.SetActive(true);
            scr.ballScr.enArrBoxTr.gameObject.SetActive(false);

            for (int i = 0; i < objs_ToDisable.Length; i++)
                objs_ToDisable[i].SetActive(false);

            for (int i = 0; i < objs_ToEnable.Length; i++)
                objs_ToEnable[i].SetActive(true);
        }
        else
        {
            //scr.timFr.EnableUnlimitedFreeze();
            enGatesTr.GetComponent<Animator>().enabled = false;
            plGatesTr.GetComponent<Animator>().enabled = false;
            text_Practice_1.GetComponent<Animator>().SetTrigger(
                Animator.StringToHash("0"));

            for (int i = 0; i < objs_ToEnable.Length; i++)
                objs_ToEnable[i].SetActive(false);

            text_Practice_2.gameObject.SetActive(false);
            //text_Practice_3.gameObject.SetActive(false);
            obj_PracticePlayerText.SetActive(false);
            obj_PracticeEnemyText.SetActive(false);
        }*/
	}

    private bool isMoved, isMovedPrev;
    private bool isKicked, isKickedPrev;
    private bool isJumped, isJumpedPrev;
    private bool isBKicked, isBKickedPrev;
    private bool isTimeFr, isTimeFrPrev;
    private bool isScored_3_Goals, isScored_3_GoalsPrev;

    void Update()
    {
        /*if (isPractice)
        {
            isMovedPrev = isMoved;
            isKickedPrev = isKicked;
            isJumpedPrev = isJumped;
            isBKickedPrev = isBKicked;
            isTimeFrPrev = isTimeFr;
            isScored_3_GoalsPrev = isScored_3_Goals;

            if (scr.pMov.isMoveLeft || scr.pMov.isMoveRight)
                isMoved = true;

            if (scr.pMov.kick1)
                isKicked = true;

            if (scr.pMov.jump)
                isJumped = true;

            if (scr.pMov.kOvH)
                isBKicked = true;

            if (scr.timFr.isFreeze)
                isTimeFr = true;

            switch (Score.score)
            {
                case 3:
                    isScored_3_Goals = true;
                    break;
            }

            if (isMoved && !isMovedPrev)
            {
                CallPracticeText2(1);
                SetClearPlayerText();
            }

            if (isJumpedPrev)
            {
                if (isKicked && !isKickedPrev)
                    CallPracticeText2(3);
            }
            else
            {
                if (isJumped && isKickedPrev)
                    CallPracticeText2(3);
            }

            if (isBKicked && !isBKickedPrev)
                CallPracticeText2(5);

            if (isTimeFr && !isTimeFrPrev)
                CallPracticeText2(7);

            if (isScored_3_Goals &&
                !isScored_3_GoalsPrev &&
                isFinish && 
                !isCalledFinish)
            {
                FinishPractice();
                isCalledFinish = true;
            }
        }*/
    }

    [HideInInspector]
    public bool isFinish;
    private bool isCalledFinish;

    public void CallPracticeText2(int state)
    {
        //int state_hash = Animator.StringToHash(state.ToString());
        //text_Practice_2.GetComponent<Animator>().SetTrigger(state_hash);
    }
        
    private void SetClearPlayerText()
    {
        //obj_PracticePlayerText.GetComponent<Animator>().SetTrigger(Animator.StringToHash("0"));
    }

    private void SetClearEnemyText()
    {
        //obj_PracticeEnemyText.GetComponent<Animator>().SetTrigger(Animator.StringToHash("0"));
    }

    private void FinishPractice()
    {
        //CallPracticeText2(11);
        //text_Practice_2.alignment = TextAnchor.MiddleCenter;
        //.text = str_FinishPractice;
    }

    /*private void SetLanguageTexts()
    {
        switch (scr.univFunc.sysLang())
        {
            case 0: //English
                str_TouchTopHalf = "Touch top half of screen to pause";
                str_StartPractice = "Start practice!";
                str_FinishPractice = "Nice! You have been trained and\n" +
            "now you are ready to play!\n" +
            "Good luck!";
                str_MoveHimUsing = "move him using these buttons";
                str_KickJump = "use these buttons\nto kick ball and jump";
                str_BycicleKick = "this is bycicle kick\nbutton. try it!";
                str_Slowdown = "you can slow down time\nby this button. try it!";
                str_MeetYourOpp = "meet your opponent!";
                str_Score3Goals = "score 3 goals!";
                str_PlayerText = "this is your\nplayer";
                str_EnemyText = "this is your\nopponent";
                break;
        }
    }*/
}
