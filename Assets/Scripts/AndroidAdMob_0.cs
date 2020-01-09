using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class AndroidAdMob_0 : MonoBehaviour 
{
    public GoogleMobileAdBanner menuBanner;

	void Awake()
	{
        DontDestroyOnLoad(gameObject);
	}

	void Start()
	{
		GoogleMobileAd.OnInterstitialClosed += GoogleMobileAd_OnInterstitialClosed;
        GoogleMobileAd.Init ();
        SceneManager.LoadScene("Menu");
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
        
    void OnDestroy()
    {
        
    }

    /*void GoogleMobileAd_OnRewardedVideoAdClosed ()
    {
        GoogleMobileAd.LoadRewardedVideo();
    }*/

    /*public void CreateMenuBanner()
    {
        if (menuBanner == null)
        {
            menuBanner = GoogleMobileAd.CreateAdBanner(
                TextAnchor.LowerCenter, GADBannerSize.SMART_BANNER);
        }
    }*/

    /*public void HideMenuBanner()
    {
        if (menuBanner != null)
        {
            if (menuBanner.IsLoaded && menuBanner.IsOnScreen)
                menuBanner.Hide();
        }
    }*/

    /*public void ShowMenuBanner()
    {
        if (menuBanner != null)
        {
            if (menuBanner.IsLoaded && !menuBanner.IsOnScreen)
                menuBanner.Show();
        }
    }*/

    /*private void OnLevelWasLoaded_0(Scene scene, Scene scene1)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (PlayerPrefs.GetInt("NoAds") == 0)
            {
                if (FindObjectOfType<AllPrefsScript>().pldG > 2)
                {
                    if (!isMenuLoaded)
                    {
                        CreateMenuBanner();
                        isMenuLoaded = true;   
                    }
                    else
                        ShowMenuBanner();
                }
            }
 
            //Debug.Log("OnLevelWasLoaded_0 Menu");
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            HideMenuBanner();
            //Debug.Log("OnLevelWasLoaded_0 Level");
        }
    }*/
}
