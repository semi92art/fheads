using Common;
using Common.Helpers;
using Common.Managers.Advertising;
using Common.Managers.Advertising.AdBlocks;
using Common.Managers.Advertising.AdsProviders;
using Common.Managers.Analytics;
using mazing.common.Runtime.Helpers;
using mazing.common.Runtime.Managers;
using mazing.common.Runtime.Network;
using mazing.common.Runtime.Ticker;
using RMAZOR.Helpers;
using UnityEngine;
using Zenject;

namespace Mono_Installers
{
    public class GlobalMonoInstaller : MonoInstaller
    {
        public GlobalGameSettings globalGameSettings;
        
        public override void InstallBindings()
        {
            BindSettings();
            BindAnalytics();
            BindPermissionsRequester();
            BindTickers();
            BindAds();
            BindOther();
        }
        
        private void BindSettings()
        {
            Container.Bind<GlobalGameSettings>().FromScriptableObject(globalGameSettings).AsSingle();
        }

        private void BindAnalytics()
        {
            var managerType = Application.isEditor ? typeof(AnalyticsManagerFake) : typeof(AnalyticsManager);
            Container.Bind<IAnalyticsManager>().To(managerType).AsSingle();
            Container.Bind<IAnalyticsProvidersSet>().To<AnalyticsProvidersSet>().AsSingle();
            Container.Bind<IUnityAnalyticsProvider>().To<UnityAnalyticsProvider>().AsSingle();
            Container.Bind<IMyOwnAnalyticsProvider>().To<MyOwnAnalyticsProvider>().AsSingle();
#if FIREBASE && !UNITY_WEBGL
            Container.Bind<IFirebaseAnalyticsProvider>().To<FirebaseAnalyticsProvider>().AsSingle();
#endif
#if APPODEAL_3 && !UNITY_WEBGL
            Container.Bind<IAppodealAnalyticsProvider>().To<AppodealAnalyticsProvider>().AsSingle();
#endif
        }
        
        private void BindPermissionsRequester()
        {
            
#if UNITY_EDITOR || UNITY_WEBGL
            Container.Bind<IPermissionsRequester>() .To<FakePermissionsRequester>().AsSingle();
#elif UNITY_ANDROID
                Container.Bind<IPermissionsRequester>().To<FakePermissionsRequester>() .AsSingle();
#elif UNITY_IOS || UNITY_IPHONE
                Container.Bind<IPermissionsRequester>().To<IosPermissionsRequester>()  .AsSingle();
#endif
        }
        
        private void BindTickers()
        {
            Container.Bind<ICommonTicker>()             .To<CommonTicker>()                 .AsSingle();
            Container.Bind<IViewGameTicker>()           .To<ViewGameTicker>()               .AsSingle();
            Container.Bind<IModelGameTicker>()          .To<ModelGameTicker>()              .AsSingle();
            Container.Bind<IUITicker>()                 .To<UITicker>()                     .AsSingle();
            Container.Bind<ISystemTicker>()             .To<SystemTicker>()                 .AsSingle();
        }
        
        private void BindAds()
        {
            Container.Bind<IAdsManager>()               .To<AdsManager>()                   .AsSingle();
#if ADMOB_API
            Container.Bind<IAdMobAdsProvider>()         .To<AdMobAdsProvider>()             .AsSingle();
            Container.Bind<IAdMobInterstitialAd>()      .To<AdMobInterstitialAd>()          .AsSingle();
            Container.Bind<IAdMobRewardedAd>()          .To<AdMobRewardedAd>()              .AsSingle();
#endif
#if UNITY_ADS_API
            Container.Bind<IUnityAdsProvider>()         .To<UnityAdsProvider>()             .AsSingle();
            Container.Bind<IUnityAdsInterstitialAd>()   .To<UnityAdsInterstitialAd>()       .AsSingle();
            Container.Bind<IUnityAdsRewardedAd>()       .To<UnityAdsRewardedAd>()           .AsSingle();
#endif
#if APPODEAL_3
            Container.Bind<IAppodealAdsProvider>()      .To<AppodealAdsProvider>()          .AsSingle();
            Container.Bind<IAppodealInterstitialAd>()   .To<AppodealInterstitialAd>()       .AsSingle();
            Container.Bind<IAppodealRewardedAd>()       .To<AppodealRewardedAd>()           .AsSingle();
#endif
            Container.Bind<IAdsProvidersSet>()          .To<AdsProvidersSet>()              .AsSingle();
        }
        
        private void BindOther()
        {
            Container.Bind<IRemotePropertiesCommon>().To<RemotePropertiesCommon>().AsSingle();
            Container.Bind<IGameClient>().To<GameClient>().AsSingle();
#if FIREBASE && !UNITY_WEBGL
            Container.Bind<IFirebaseInitializer>().To<FirebaseInitializer>().AsSingle();
#elif FIREBASE && UNITY_WEBGL

#endif
        }
    }
}