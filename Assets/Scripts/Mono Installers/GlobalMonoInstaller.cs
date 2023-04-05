using Common;
using Common.Managers.Analytics;
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
        public override void InstallBindings()
        {
            BindAnalytics();
            BindPermissionsRequester();
            BindTickers();
            BindOther();
        }

        private void BindAnalytics()
        {
            var managerType = Application.isEditor ? typeof(AnalyticsManagerFake) : typeof(AnalyticsManager);
            Container.Bind<IAnalyticsManager>().To(managerType).AsSingle();
            Container.Bind<IAnalyticsProvidersSet>().To<AnalyticsProvidersSet>().AsSingle();
            Container.Bind<IUnityAnalyticsProvider>().To<UnityAnalyticsProvider>().AsSingle();
            Container.Bind<IMyOwnAnalyticsProvider>().To<MyOwnAnalyticsProvider>().AsSingle();
#if FIREBASE
            Container.Bind<IFirebaseAnalyticsProvider>().To<FirebaseAnalyticsProvider>().AsSingle();
#endif
#if APPODEAL_3
            Container.Bind<IAppodealAnalyticsProvider>().To<AppodealAnalyticsProvider>().AsSingle();
#endif
        }
        
        private void BindPermissionsRequester()
        {
            if (Application.isEditor)
            {
                Container.Bind<IPermissionsRequester>() .To<FakePermissionsRequester>().AsSingle();
            }
            else
            {
#if UNITY_ANDROID
                Container.Bind<IPermissionsRequester>().To<FakePermissionsRequester>() .AsSingle();
#elif UNITY_IOS || UNITY_IPHONE
                Container.Bind<IPermissionsRequester>().To<IosPermissionsRequester>()  .AsSingle();
#endif
            }
        }
        
        private void BindTickers()
        {
            Container.Bind<ICommonTicker>()             .To<CommonTicker>()                 .AsSingle();
            Container.Bind<IViewGameTicker>()           .To<ViewGameTicker>()               .AsSingle();
            Container.Bind<IModelGameTicker>()          .To<ModelGameTicker>()              .AsSingle();
            Container.Bind<IUITicker>()                 .To<UITicker>()                     .AsSingle();
            Container.Bind<ISystemTicker>()             .To<SystemTicker>()                 .AsSingle();
        }
        
        private void BindOther()
        {
            Container.Bind<IGameClient>().To<GameClient>().AsSingle();
#if FIREBASE
            Container.Bind<IFirebaseInitializer>().To<FirebaseInitializer>().AsSingle();
#endif
        }
    }
}