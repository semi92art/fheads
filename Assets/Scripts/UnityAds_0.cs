using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;


public class UnityAds_0 : MonoBehaviour 
{
    private Scripts scr;
    public string androidGameId;
    public string iosGameId;
    public bool testMode;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += SetScr;
    }

    private void SetScr(Scene scene, Scene scene1)
    {
        scr = FindObjectOfType<Scripts>();
    }

    void Start ()
    {
        string gameId = string.Empty;

        #if UNITY_ANDROID
        gameId = androidGameId;
        #elif UNITY_IOS
        gameId = iosGameId;
        #endif

        if (Advertisement.isSupported && !Advertisement.isInitialized)
            Advertisement.Initialize(gameId, testMode);

        SceneManager.LoadScene(1);
    }

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
