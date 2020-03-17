using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Objects_Menu : MonoBehaviour 
{
    public Scripts scr;

    public Sprite 
        spr_SoundOn,
        spr_SoundOff;

    public Color
        col_Gray,
        col_Orange,
        col_Blue;

    [Header("Gameobject:")]
    public GameObject moneyCount;
    public GameObject sampleButton1;
	
	public GameObject cupAw;
	public GameObject currPrPan;
	public GameObject menuProfile;

	[Header("Other:")]
    public Image soundIm;
	public AudioSource mainThemeSource;
	public AudioSource buttonsSource;
    public RectTransform cP;
    public Text[] text_opndPlayers;
    public string[] idForTesting;
    public Text text_BackSettingsButton;
    public GameObject obj_MainMenu;
    public Animator anim_MainMenu;
    public GameObject obj_MenuTournament;
    public Animator anim_MenuTournament;
    public GameObject obj_MenuPlayers;
    public Animator anim_MenuPlayers;
    public GameObject obj_MenuUpgrades;
    public Animator anim_MenuUpgrades;


	void Awake()
	{
        scr.upgr.curr_ind = 0;
        scr.upgr.curr_indBall = 0;

#if UNITY_EDITOR
        if (FindObjectOfType<Reporter>())
            DestroyImmediate(FindObjectOfType<Reporter>().gameObject);
#else
        bool doNotDestroy = false;

        for (int i = 0; i < idForTesting.Length; i++)
        {
            if (Android_Id() == idForTesting[i])
                doNotDestroy = true;
        }

        if (!doNotDestroy)
        {
            if (FindObjectOfType<Reporter>())
                DestroyImmediate(FindObjectOfType<Reporter>().gameObject);
        }
#endif

		currPrPan.SetActive (true);
        EnableSound(true);
	}

    void Start()
    {
        if (scr.alPrScr.pldG > 20 &&
            PlayerPrefs.GetInt("Review_Done") == 0)
        {
            scr.allAw.CallAwardPanel_3();
        }
    }
	
	public void PixelPerfect()
	{
		bool pixelPerfect = gameObject.GetComponent<Canvas>().pixelPerfect;
		gameObject.GetComponent<Canvas>().pixelPerfect = !pixelPerfect;
	}

    public void EnableSound(bool isStart)
    {
        int onInt = PlayerPrefs.GetInt("SoundOn");

        int onInt_1 = onInt == 0 ? 1 : 0;

        if (!isStart)
            PlayerPrefs.SetInt("SoundOn", onInt_1);

        onInt = PlayerPrefs.GetInt("SoundOn");
        mainThemeSource.mute = !scr.univFunc.Int2Bool(onInt);
        buttonsSource.mute = !scr.univFunc.Int2Bool(onInt);
        SoundImage(onInt);
    }

    private void SoundImage(int _on)
    {
        soundIm.sprite = _on == 0 ? spr_SoundOff : spr_SoundOn;
    }
       
    public void OpenGameInMarket()
    {
        Application.OpenURL("market://details?id=com.Artem.FootballHeads");
        PlayerPrefs.SetInt("Review_Done", 1);
    }

    /*public void Purchase_MoneyPack(int case0)
    {
        InAppPurchase_0 inAppPurch_0 = FindObjectOfType<InAppPurchase_0>();

        if (case0 == 1)
            inAppPurch_0.Purchase_MoneyPack_1();
        else if (case0 == 2)
            inAppPurch_0.Purchase_MoneyPack_2();
        else if (case0 == 3)
            inAppPurch_0.Purchase_MoneyPack_3();
    }

    public void Purchase_NoAds()
    {
        FindObjectOfType<InAppPurchase_0>().Purchase_NoAds();
    }

    public void Purchase_UnlimitedFreeze()
    {
        FindObjectOfType<InAppPurchase_0>().Purchase_UnlimFreeze();
    }*/

    public string Android_Id()
    {
        string android_id = "editor";

#if UNITY_EDITOR
#else
        AndroidJavaClass up = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject> ("currentActivity");
        AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject> ("getContentResolver");  
        AndroidJavaClass secure = new AndroidJavaClass ("android.provider.Settings$Secure");
        android_id = secure.CallStatic<string> ("getString", contentResolver, "android_id");
#endif

        return android_id;
    }

    public void Menu_Tournaments(bool _on)
    {
        if (_on)
        {
            text_BackSettingsButton.text = "BACK";
            GameManager.Instance._menues = Menues.MenuCareer;
            anim_MainMenu.SetTrigger(Animator.StringToHash("1"));
            obj_MenuTournament.SetActive(true);
            anim_MenuTournament.SetTrigger(Animator.StringToHash("0"));
        }
        else
        {
            text_BackSettingsButton.text = "SETTINGS";
            anim_MenuTournament.SetTrigger(Animator.StringToHash("1"));
        }
    }

    public void Menu_Players(bool _on)
    {
        if (_on)
        {
            text_BackSettingsButton.text = "BACK";
            GameManager.Instance._menues = Menues.MenuPlayers;
            anim_MainMenu.SetTrigger(Animator.StringToHash("1"));
            obj_MenuPlayers.SetActive(true);
            anim_MenuPlayers.SetTrigger(Animator.StringToHash("0"));
        }
        else
        {
            text_BackSettingsButton.text = "SETTINGS";
            anim_MenuPlayers.SetTrigger(Animator.StringToHash("1"));
        }
    }

    public void Menu_Upgrades(bool _on)
    {
        scr.upgr.Ball_Choose(scr.upgr.curr_indBall);
        scr.upgr.Upgrade_Choose(scr.upgr.curr_ind);

        if (_on)
        {
            text_BackSettingsButton.text = "BACK";
            GameManager.Instance._menues = Menues.MenuUpgrades;
            anim_MainMenu.SetTrigger(Animator.StringToHash("1"));
            obj_MenuUpgrades.SetActive(true);
        }
        else
        {
            text_BackSettingsButton.text = "SETTINGS";
            anim_MenuUpgrades.SetTrigger(Animator.StringToHash("1"));
        }
    }

    public void Menu_Info(bool _on)
    {
        text_BackSettingsButton.text = _on ? "BACK" : "SETTINGS";
    }

    public void Button_Sound()
    {
        if (!buttonsSource.mute && buttonsSource.enabled)
            buttonsSource.Play();
    }
}
