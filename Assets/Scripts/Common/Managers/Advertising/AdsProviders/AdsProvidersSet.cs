using System.Collections.Generic;

namespace Common.Managers.Advertising.AdsProviders
{
    public class AdsProvidersSet : IAdsProvidersSet
    {
#if ADMOB_API && !UNITY_WEBGL
        [Zenject.Inject] private IAdMobAdsProvider AdMobAdsProvider { get; }
#endif
#if UNITY_ADS_API && !UNITY_WEBGL
        [Zenject.Inject] private IUnityAdsProvider UnityAdsProvider { get; }
#endif
#if APPODEAL_3 && !UNITY_WEBGL
        [Zenject.Inject] private IAppodealAdsProvider AppodealAdsProvider { get; }
#endif

        public List<IAdsProvider> GetProviders()
        {
            var result = new List<IAdsProvider>();
#if ADMOB_API && !UNITY_WEBGL
            result.Add(AdMobAdsProvider);
#endif
#if UNITY_ADS_API && !UNITY_WEBGL
            result.Add(UnityAdsProvider);
#endif
#if APPODEAL_3 && !UNITY_WEBGL
            result.Add(AppodealAdsProvider);
#endif
            return result;
        }
    }
}