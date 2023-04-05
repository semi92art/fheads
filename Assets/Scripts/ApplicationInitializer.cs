
using System;
using System.Collections;
using System.Text;
using Common;
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
    private IFirebaseInitializer  FirebaseInitializer  { get; set; }
    private IPermissionsRequester PermissionsRequester { get; set; }

    [Inject]
    private void Inject(
        IAnalyticsManager     _AnalyticsManager,
        IFirebaseInitializer  _FirebaseInitializer,
        IPermissionsRequester _PermissionsRequester)
    {
        AnalyticsManager     = _AnalyticsManager;
        FirebaseInitializer  = _FirebaseInitializer;
        PermissionsRequester = _PermissionsRequester;
    }

    #endregion

    #region engine methods

    private IEnumerator Start()
    {
        FirebaseInitializer.Init();
        // yield return UpdateTodaySessionsCountCoroutine();
        // ApplicationVersionUpdater.UpdateToCurrentVersion();
        // yield return SetGameId();
        yield return LogAppInfoCoroutine();
        yield return PermissionsRequestCoroutine();
        yield return InitStartDataCoroutine();
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
    
    private IEnumerator InitStartDataCoroutine()
    {
        yield return null;
        // ScoreManager.Initialize += OnScoreManagerInitialize;
        // MazorCommonData.Release = true;
        // SaveUtils.PutValue(SaveKeysMazor.AppVersion, Application.version);
        // Application.targetFrameRate = GraphicUtils.GetTargetFps();
        // Dbg.LogLevel = GlobalGameSettings.logLevel;
        // if (SaveUtils.GetValue(SaveKeysMazor.NotFirstLaunch))
        //     yield break;
        // SaveUtils.PutValue(SaveKeysCommon.SettingSoundOn,         true);
        // SaveUtils.PutValue(SaveKeysCommon.SettingMusicOn,         true);
        // SaveUtils.PutValue(SaveKeysCommon.SettingNotificationsOn, true);
        // SaveUtils.PutValue(SaveKeysCommon.SettingHapticsOn,       true);
        // SaveUtils.PutValue(SaveKeysMazor .NotFirstLaunch,         true);
        // CommonUtils.DoOnInitializedEx(LocalizationManager, SetDefaultLanguage);
    }
    
    private IEnumerator InitGameManagersCoroutine()
    {
        yield return null;
        InitAnalyticsManager();
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
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
        yield return WaitWhile(
            () => !FirebaseInitializer.Initialized,
            () =>
            {
                // SceneManager.LoadScene(SceneNames.Level);
                var @params = new LoadSceneParameters(LoadSceneMode.Single);
                SceneManager.LoadSceneAsync(SceneNames.Menu, @params);
            }, 3f);
    }
    
    private void OnSceneLoaded(Scene _Scene, LoadSceneMode _Mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        // GameLogo.Init();
        // Cor.Run(InitGameControllerCoroutine());
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
