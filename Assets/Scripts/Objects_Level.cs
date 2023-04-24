using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[System.Serializable]
public class StartPanelObjects
{
    public Image im_PlayerHead;
    public Image im_PlayerLeg;
    public Image im_EnemyHead_1;
    public Image im_EnemyLeg_1;
    public Image im_EnemyHead_2;
    public Image im_EnemyLeg_2;
}

public class Objects_Level : MonoBehaviour
{
    public Scripts scr;
    [Space(5)]
    public Text text_WatchVideo_0;
    public Text text_WatchVideo_1;
    [Space(5)]
    public Anim_VictoryText _anim_VictText;
    public Image[] im_ButSize;

    public RectTransform tr_Controls_1;
    public RectTransform tr_Controls_2;
    public Animator secondTimePanelAnim, quitPanAmim;
    public Animator pauseMenuAnim, resultMenuAnim;
    public Animator startPanelAnim;

    public GameObject obj_QuitPan;
    public GameObject obj_BK_But1;

    public Animator anim_VictoryText;
    public Animator anim_TiltOn;

    public bool isTiltOn;
	public GameObject stadiumsObj;
	public GameObject quitPanel;
    public GameObject obj_RestartButon, obj_RestartButon1;

    public Text text_Victory;
    public Text text_GameNum;
    public Text text_Result;
	public Text touchToBeginText;
	public Text quitText;
	public Text secontTimePanelText;
	public Canvas mainCanvas, controlsCanvas;
	public Button startGameButton;

	public Image c1RamkaIm, c2RamkaIm;
	public Image leftButSprR, rightButSprR;
	public Sprite jump1Spr, jump2Spr, kick1Spr, kick2Spr;

    public string[] idForTesting;

	[HideInInspector]
	public bool isMoneyWinPopulate;
	[HideInInspector]
	public int totalPrice;

    [Header("From Player Movement:")]
    public SpriteRenderer plLegSprR;
    public SpriteRenderer enLegSprR;
    public SpriteRenderer enLegSprR_1;

    [Header("Rigidbodies to control in timeScale = 0")]
    public Rigidbody2D[] allRbs;
    [Header("Buttons to control their capacity")]
    public Image[] im_ContrButtons;
    [Space(5)]
    public Scrollbar scrBar_ButtCap;

    [Header("Start Panel Objects:")]
    public StartPanelObjects startPanObjs;
    private float cap_val;


	void Awake()
	{
        if (scr.alPrScr.isRandGame == 1)
        {
            text_WatchVideo_0.gameObject.SetActive(false);
            text_WatchVideo_1.gameObject.SetActive(false);
            text_GameNum.enabled = false;
        }
        else
        {
            int gameNum = scr.alPrScr.game + 1;
            text_GameNum.text = "GAME " + gameNum.ToString();
            obj_RestartButon.SetActive(false);
            int _canRestart = PlayerPrefs.GetInt("CanRestart");
            scr.objLev.obj_RestartButon.SetActive(!CommonUtilsFheads.Int2Bool(_canRestart));
        }

        startPanObjs.im_PlayerHead.sprite = scr.buf.plSpr;
        startPanObjs.im_PlayerLeg.sprite = scr.buf.plBoot;
        startPanObjs.im_EnemyHead_1.sprite = scr.buf.enSpr;
        startPanObjs.im_EnemyLeg_1.sprite = scr.buf.enBoot;

        if (scr.buf.is2Enemies)
        {
            scr.enAlg_1.gameObject.SetActive(true);
            startPanObjs.im_EnemyHead_2.sprite = scr.buf.enSpr_1;
            startPanObjs.im_EnemyLeg_2.sprite = scr.buf.enBoot_1;
        }
        else
            scr.enAlg_1.gameObject.SetActive(false);
        
        isTiltOn = CommonUtilsFheads.Int2Bool(PlayerPrefs.GetInt("Tilt"));
        EnableTilt(1);
        scr.levAudScr.isSoundOn = 
            CommonUtilsFheads.Int2Bool(PlayerPrefs.GetInt("SoundOn"));
        //scr.levAudScr.EnableSound(1);

        ButtonsSize(-1);

		if (scr.alPrScr.controls == 1)
			SetControls_1();
		else if (scr.alPrScr.controls == 2)
			SetControls_2();

		scr.pMov.Left_JK_EndButton();
		scr.pMov.Right_JK_EndButton();
		mainCanvas.enabled = true;
		controlsCanvas.enabled = true;
		quitPanel.SetActive (false);
        quitText.text = "You will lose this game.\nContinue?";
                
        obj_BK_But1.SetActive(
            CommonUtilsFheads.Int2Bool(
                PlayerPrefs.GetInt("BycicleKick")));

        scrBar_ButtCap.value = PlayerPrefs.GetFloat("ButtonsCapacity");
        Buttons_Capacity();
	}

    void Start()
    {
        DeactivateMenuesOnStart();
        //scr.levAudScr.EnableSound(1);
        startPanObjs.im_PlayerHead.sprite = scr.buf.plSpr;
        startPanObjs.im_PlayerLeg.sprite = scr.buf.plBoot;
        startPanObjs.im_EnemyHead_1.sprite = scr.buf.enSpr;
        startPanObjs.im_EnemyLeg_1.sprite = scr.buf.enBoot;

        int _trigger = scr.buf.is2Enemies ? 
            Animator.StringToHash("1") : Animator.StringToHash("0");
        startPanelAnim.SetTrigger(_trigger);
        Destroy(scr.prMng.gameObject, 0.5f);
    }

	private void DeactivateMenuesOnStart()
	{
		resultMenuAnim.gameObject.SetActive(false);
		secondTimePanelAnim.gameObject.SetActive(false);
	}

	public void DestroyGameObject(GameObject obj)
	{
		#if UNITY_EDITOR
		DestroyImmediate(obj);
		#endif
	}

	public void SetControls_1()
	{
		scr.alPrScr.controls = 1;
		scr.alPrScr.doCh = true;

		c1RamkaIm.enabled = true;
		c2RamkaIm.enabled = false;

		leftButSprR.sprite = jump1Spr;
		rightButSprR.sprite = kick1Spr;
	}

	public void SetControls_2()
	{
		scr.alPrScr.controls = 2;
		scr.alPrScr.doCh = true;

		c1RamkaIm.enabled = false;
		c2RamkaIm.enabled = true;

		leftButSprR.sprite = kick1Spr;
		rightButSprR.sprite = jump1Spr;
	}

    public void Exit_Button()
    {
        obj_QuitPan.SetActive(true);
        quitPanAmim.SetTrigger(Animator.StringToHash("call"));
        pauseMenuAnim.SetTrigger(Animator.StringToHash("back"));
    }

    public void EnableTilt(int isAwake)
    {
        isTiltOn = isAwake == 1 ? isTiltOn : !isTiltOn;
        int isTiltOn_int = isTiltOn ? 1 : 0;

        anim_TiltOn.SetTrigger(
            Animator.StringToHash(isAwake.ToString() + isTiltOn_int.ToString()));

        if (isAwake == 0)
            PlayerPrefs.SetInt("Tilt", isTiltOn_int);
    }

    public void ButtonsSize(int _size)
    {
        if (_size == -1)
            _size = PlayerPrefs.GetInt("ButtonsSize");
        else
            PlayerPrefs.SetInt("ButtonsSize", _size);

        for (int i = 0; i < im_ButSize.Length; i++)
            im_ButSize[i].enabled = false;
        
        im_ButSize[_size].enabled = true;
        SetButtonSize(_size);
    }

    private float RealCapacityValue(float cap_val)
    {
        //float new_cap_val = cap_val * 0.9f + 0.1f;
        float new_cap_val = cap_val;
        return new_cap_val;
    }

    public void Buttons_Capacity()
    {
        cap_val = scrBar_ButtCap.value;
        PlayerPrefs.SetFloat("ButtonsCapacity", cap_val);

        for (int i = 0; i < im_ContrButtons.Length; i++)
        {
            im_ContrButtons[i].color = new Color(
                im_ContrButtons[i].color.r,
                im_ContrButtons[i].color.g,
                im_ContrButtons[i].color.b,
                RealCapacityValue(cap_val));
        }
    }

    private void SetButtonSize(int _size)
    {
        switch (_size)
        {
            case 0:
                tr_Controls_1.anchoredPosition = new Vector2(170f, 82f);
                tr_Controls_1.localScale = new Vector3(0.7f, 0.7f, 1f);
                tr_Controls_2.anchoredPosition = new Vector2(-183f, 82f);
                tr_Controls_2.localScale = new Vector3(0.7f, 0.7f, 1f);
                break;
            case 1:
                tr_Controls_1.anchoredPosition = new Vector2(187f, 92f);
                tr_Controls_1.localScale = new Vector3(0.85f, 0.85f, 1f);
                tr_Controls_2.anchoredPosition = new Vector2(-229f, 92f);
                tr_Controls_2.localScale = new Vector3(0.85f, 0.85f, 85f);
                break;
            case 2:
                tr_Controls_1.anchoredPosition = new Vector2(229f, 100f);
                tr_Controls_1.localScale = new Vector3(1f, 1f, 1f);
                tr_Controls_2.anchoredPosition = new Vector2(-269f, 100f);
                tr_Controls_2.localScale = new Vector3(1f, 1f, 1f);
                break;
        }
    }

    public void ContinueTournament()
    {
        scr.buf.Set_Tournament_Data_0(scr.alPrScr.game, scr.alPrScr.lg);
        PlayerPrefs.SetInt("MenuTrigger_1", 1);
        SceneManager.LoadScene(2);
    }

    private void FinishTournament()
    {
        scr.congrPan.anim_CongrPan.SetTrigger(Animator.StringToHash("call"));
        mainCanvas.enabled = true;
        controlsCanvas.enabled = false;
        scr.congrPan.CongradulationsPanelCall();
        scr.congrPan.DisableSomeObjects();
        scr.monWin.SetMoneyWin();
    }

    public void FinishOrContinue()
    {
        if (TimeManager.resOfGame == 1)
            ContinueTournament();
        else
            FinishTournament();
    }

    public void LevelRestartInLevel()
    {
        PlayerPrefs.SetInt("MenuTrigger_1", 1);
        SceneManager.LoadScene("Level");
    }

}
