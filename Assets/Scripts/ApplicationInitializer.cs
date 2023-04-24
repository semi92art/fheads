
using System;
using System.Collections;
using System.Text;
using Common;
using Common.Managers.Advertising;
using mazing.common.Runtime;
using mazing.common.Runtime.Constants;
using mazing.common.Runtime.Entities;
using mazing.common.Runtime.Managers;
using mazing.common.Runtime.Utils;
using RMAZOR.Helpers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Zenject;

public class ApplicationInitializer : MonoBehaviour
{
    #region inject

    private IAnalyticsManager     AnalyticsManager     { get; set; }
    private IAdsManager           AdsManager           { get; set; }
    private IPermissionsRequester PermissionsRequester { get; set; }
    
#if FIREBASE && !UNITY_WEBGL
    [Inject] private IFirebaseInitializer  FirebaseInitializer  { get; set; }
#endif

    [Inject]
    private void Inject(
        IAnalyticsManager     _AnalyticsManager,
        IAdsManager           _AdsManager,
        IPermissionsRequester _PermissionsRequester)
    {
        AnalyticsManager     = _AnalyticsManager;
        AdsManager           = _AdsManager;
        PermissionsRequester = _PermissionsRequester;
    }

    #endregion

    #region engine methods

    private IEnumerator Start()
    {
#if FIREBASE && !UNITY_WEBGL
        FirebaseInitializer.Init();
#endif
        Application.targetFrameRate = 120;
        // yield return UpdateTodaySessionsCountCoroutine();
        // ApplicationVersionUpdater.UpdateToCurrentVersion();
        // yield return SetGameId();
        yield return LogAppInfoCoroutine();
        yield return PermissionsRequestCoroutine();
        yield return InitGameManagersCoroutine();
        yield return LoadGameSceneCoroutine();
    }

    #endregion

    #region nonpublic methods

    private static IEnumerator LogAppInfoCoroutine()
    {
        var sb = new StringBuilder();
        sb.AppendLine("Application started");
        sb.AppendLine("Platform: "              + Application.platform);
        sb.AppendLine("Installer name: "        + Application.installerName);
        sb.AppendLine("Identifier: "            + Application.identifier);
        sb.AppendLine("Version: "               + Application.version);
        sb.AppendLine("Data path: "             + Application.dataPath);
        sb.AppendLine("Install mode: "          + Application.installMode);
        sb.AppendLine("Sandbox type: "          + Application.sandboxType);
        sb.AppendLine("Unity version: "         + Application.unityVersion);
        sb.AppendLine("Console log path: "      + Application.consoleLogPath);
        sb.AppendLine("Streaming assets path: " + Application.streamingAssetsPath);
        sb.AppendLine("Temporary cache path: "  + Application.temporaryCachePath);
        sb.AppendLine("Absolute url: "          + Application.absoluteURL);
        Dbg.Log(sb.ToString());
        yield return null;
    }
    
    private IEnumerator PermissionsRequestCoroutine()
    {
        var permissionsEntity = PermissionsRequester.RequestPermissions();
        while (permissionsEntity.Result == EEntityResult.Pending)
            yield return new WaitForEndOfFrame();
    }

    private IEnumerator InitGameManagersCoroutine()
    {
        yield return null;
        InitAnalyticsManager();
        InitAdsManager();
        // InitRemoteConfigManager();
        // InitAssetBundleManager();
        // InitShopManager();
        // InitScoreManager();
        // InitGameClient();
        // InitLocalizationManager();
        // InitPushNotificationsProvider();
    }

    private void InitAnalyticsManager()
    {
        AnalyticsManager.Initialize += () => AnalyticsManager.SendAnalytic(AnalyticIds.SessionStart);
        TryExecute(AnalyticsManager.Init);
    }

    private void InitAdsManager()
    {
        TryExecute(AdsManager.Init);
    }
    
    private static void TryExecute(UnityAction _Action)
    {
        try
        {
            _Action?.Invoke();
        }
        catch (Exception ex)
        {
            Dbg.LogError(ex);
        }
    }
    
    private IEnumerator LoadGameSceneCoroutine()
    {
        yield return WaitWhile(
            LoadGameSceneCoroutinePredicate, 
            () =>
            {
                var @params = new LoadSceneParameters(LoadSceneMode.Single);
                SceneManager.LoadSceneAsync(SceneNames.Menu, @params);
            }, 3f);
    }
    
    private bool LoadGameSceneCoroutinePredicate()
    {
#if FIREBASE && !UNITY_WEBGL
        return !FirebaseInitializer.Initialized;
#else
        return false;
#endif
    }

    private static IEnumerator WaitWhile(
        Func<bool>  _Predicate,
        UnityAction _Action,
        float       _Seconds)
    {
        float time = Time.time;
        bool FinalPredicate() => _Predicate() && (time + _Seconds > Time.time);
        while (FinalPredicate())
            yield return null;
        _Action();
    }

    #endregion
}
