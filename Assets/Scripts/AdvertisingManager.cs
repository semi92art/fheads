using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class AdvertisingManager : MonoBehaviour 
{
    private Scripts scr;
    public static AdvertisingManager Instance { get; private set; }
    
    public GoogleMobileAdBanner menuBanner;
    public string androidGameId;
    public string iosGameId;
    public bool testMode;

	void Awake()
	{
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

	void Start()
	{
        GoogleMobileAd.OnInterstitialClosed += GoogleMobileAd_OnInterstitialClosed;
        GoogleMobileAd.Init ();
        SceneManager.LoadScene("____Menu");
        
        string gameId = string.Empty;

#if UNITY_ANDROID
        gameId = androidGameId;
#elif UNITY_IOS
        gameId = iosGameId;
#endif

        if (Advertisement.isSupported && !Advertisement.isInitialized)
            Advertisement.Initialize(gameId, testMode);
	}

    public void ShowInterstitialAd()
    {
        if (GoogleMobileAd.IsInterstitialReady)
            GoogleMobileAd.ShowInterstitialAd ();
    }

    private void GoogleMobileAd_OnInterstitialClosed ()
    {
        GoogleMobileAd.LoadInterstitialAd ();
    }
        


    void GoogleMobileAd_OnRewardedVideoAdClosed ()
    {
        GoogleMobileAd.LoadRewardedVideo();
    }

    public void CreateMenuBanner()
    {
        if (menuBanner == null)
        {
            menuBanner = GoogleMobileAd.CreateAdBanner(
                TextAnchor.LowerCenter, GADBannerSize.SMART_BANNER);
        }
    }

    public void HideMenuBanner()
    {
        if (menuBanner != null)
        {
            if (menuBanner.IsLoaded && menuBanner.IsOnScreen)
                menuBanner.Hide();
        }
    }

    public void ShowMenuBanner()
    {
        if (menuBanner != null)
        {
            if (menuBanner.IsLoaded && !menuBanner.IsOnScreen)
                menuBanner.Show();
        }
    }

    // private void OnLevelWasLoaded_0(Scene scene, Scene scene1)
    // {
    //     if (SceneManager.GetActiveScene().buildIndex == 1)
    //     {
    //         if (PlayerPrefs.GetInt("NoAds") == 0)
    //         {
    //             if (FindObjectOfType<AllPrefsScript>().pldG > 2)
    //             {
    //                 if (!isMenuLoaded)
    //                 {
    //                     CreateMenuBanner();
    //                     isMenuLoaded = true;   
    //                 }
    //                 else
    //                     ShowMenuBanner();
    //             }
    //         }
    //
    //         //Debug.Log("OnLevelWasLoaded_0 Menu");
    //     }
    //     else if (SceneManager.GetActiveScene().buildIndex == 2)
    //     {
    //         HideMenuBanner();
    //         //Debug.Log("OnLevelWasLoaded_0 Level");
    //     }
    // }
    
    public void ShowSimpleAd()
    {
        if (Advertisement.IsReady("video"))
        {
            ShowOptions options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("video", options);
        }
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Debug.Log("Video Is Ready");
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
        else
        {
            Debug.Log("Video Not Ready");
            FindObjectOfType<Anim_RestartButton>().Set_Trigger("1");
        }
    }

    private void HandleShowResult(ShowResult _result)
    {
        switch (_result)
        {
            case ShowResult.Finished:
                if (scr.tM.matchPeriods == 0)
                    GameManager.Instance.LoadSimpleLevel();
                else
                {
                    if (!scr.bonObjMan.isVideoCalled)
                        GameManager.Instance.LoadSimpleLevel();
                    else
                        scr.bonObjMan.isVideoCalled = false;
                }
                break;
            case ShowResult.Skipped:
    
                break;
            case ShowResult.Failed:
                Debug.Log("Video Failed");
                FindObjectOfType<Anim_RestartButton>().Set_Trigger("2");
                break;
        }
    }
}
