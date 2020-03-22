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
        if (PrefsManager.Instance.IsRandomOpponent)
        {
            text_WatchVideo_0.gameObject.SetActive(false);
            text_WatchVideo_1.gameObject.SetActive(false);
            text_GameNum.enabled = false;
        }
        else
        {
            text_GameNum.text = $"GAME {(PrefsManager.Instance.Game + 1).ToString()}";
            obj_RestartButon.SetActive(false);
            scr.objLev.obj_RestartButon.SetActive(
                !Customs.Int2Bool(PlayerPrefs.GetInt("CanRestart")));
        }

        startPanObjs.im_PlayerHead.sprite = ProfileManager.Instance.itemList[PrefsManager.Instance.PlayerIndex].icon;
        startPanObjs.im_PlayerLeg.sprite = scr.cntrL.Countries[ProfileManager.Instance.itemList[PrefsManager.Instance.PlayerIndex].cntrInd].boot;
        startPanObjs.im_EnemyHead_1.sprite = ProfileManager.Instance.itemList[ProfileManager.Instance.EnemyIndex].icon;
        startPanObjs.im_EnemyLeg_1.sprite = scr.cntrL.Countries[ProfileManager.Instance.itemList[ProfileManager.Instance.EnemyIndex].cntrInd].boot;
        
        scr.enAlg_1.gameObject.SetActive(false);
        
        isTiltOn = PrefsManager.Instance.Tilt;
        EnableTilt(1);
        scr.levAudScr.isSoundOn = PrefsManager.Instance.SoundOn;

        ButtonsSize(-1);

        switch (PrefsManager.Instance.ControlsType)
        {
            case 1:
                SetControls_1();
                break;
            case 2:
                SetControls_2();
                break;
        }

		scr.pMov.Left_JK_EndButton();
		scr.pMov.Right_JK_EndButton();
		mainCanvas.enabled = true;
		controlsCanvas.enabled = true;
		quitPanel.SetActive (false);
        quitText.text = "You will lose this game.\nContinue?";
                
        obj_BK_But1.SetActive(PrefsManager.Instance.BycicleKickEnabled);

        scrBar_ButtCap.value = 1;
        Buttons_Capacity();
	}

    void Start()
    {
        DeactivateMenuesOnStart();
        startPanObjs.im_PlayerHead.sprite = ProfileManager.Instance.itemList[PrefsManager.Instance.PlayerIndex].icon;
        startPanObjs.im_PlayerLeg.sprite = scr.cntrL.Countries[ProfileManager.Instance.itemList[PrefsManager.Instance.PlayerIndex].cntrInd].boot;
        startPanObjs.im_EnemyHead_1.sprite = ProfileManager.Instance.itemList[ProfileManager.Instance.EnemyIndex].icon;
        startPanObjs.im_EnemyLeg_1.sprite = scr.cntrL.Countries[ProfileManager.Instance.itemList[ProfileManager.Instance.EnemyIndex].cntrInd].boot;
        
        startPanelAnim.SetTrigger("0");
        Destroy(ProfileManager.Instance.gameObject, 0.5f);
    }

	private void DeactivateMenuesOnStart()
	{
		resultMenuAnim.gameObject.SetActive(false);
		secondTimePanelAnim.gameObject.SetActive(false);
	}

	public void SetControls_1()
	{
        PrefsManager.Instance.ControlsType = 1;

        c1RamkaIm.enabled = true;
		c2RamkaIm.enabled = false;

		leftButSprR.sprite = jump1Spr;
		rightButSprR.sprite = kick1Spr;
	}

	public void SetControls_2()
	{
        PrefsManager.Instance.ControlsType = 2;

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

        anim_TiltOn.SetTrigger(
            Animator.StringToHash(string.Format("{0}{1}", isAwake, (isTiltOn ? "1" : "0"))));

        if (isAwake == 0)
            PrefsManager.Instance.Tilt = isTiltOn;
    }

    public void ButtonsSize(int _Size)
    {
        if (_Size == -1)
            _Size = PrefsManager.Instance.ButtonsSize;
        else
            PrefsManager.Instance.ButtonsSize = _Size;

        for (int i = 0; i < im_ButSize.Length; i++)
            im_ButSize[i].enabled = i == _Size;
        
        SetButtonSize(_Size);
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
        CareerManager.Instance.Set_Tournament_Data_0(PrefsManager.Instance.Game, PrefsManager.Instance.League);
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
        if (LevelTimeManager.resOfGame == 1)
            ContinueTournament();
        else
            FinishTournament();
    }

    public void LevelRestartInLevel()
    {
        SceneManager.LoadScene("____Level");
    }

}
