using System.Collections.Generic;
using mazing.common.Runtime.Managers;

namespace Common.Managers.Analytics
{
    public interface IAnalyticsProvidersSet
    {
        List<IAnalyticsProvider> GetProviders();
    }
    
    public class AnalyticsProvidersSet : IAnalyticsProvidersSet
    {
        [Zenject.Inject] IUnityAnalyticsProvider UnityAnalyticsProvider { get; }
        [Zenject.Inject] IMyOwnAnalyticsProvider MyOwnAnalyticsProvider { get; }
#if FIREBASE
        [Zenject.Inject] private IFirebaseAnalyticsProvider FirebaseAnalyticsProvider { get; }
#endif
#if APPODEAL_3
        [Zenject.Inject] private IAppodealAnalyticsProvider AppodealAnalyticsProvider { get; }
#endif
        
        public List<IAnalyticsProvider> GetProviders()
        {
            var providers = new List<IAnalyticsProvider> {UnityAnalyticsProvider, MyOwnAnalyticsProvider};
#if FIREBASE
            providers.Add(FirebaseAnalyticsProvider);
#endif
#if APPODEAL_3
            providers.Add(AppodealAnalyticsProvider);
#endif
            return providers;
        }
    }
}