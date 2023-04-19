#if ADMOB_API && !UNITY_WEBGL
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Events;
using Common.Managers.Advertising.AdBlocks;
using mazing.common.Runtime;
using mazing.common.Runtime.Constants;

namespace Common.Managers.Advertising.AdsProviders
{
    public interface IAdMobAdsProvider : IAdsProvider { }
    
    public class AdMobAdsProvider : AdsProviderCommonBase, IAdMobAdsProvider
    {
        #region inject
        
        private AdMobAdsProvider(
            IAdMobInterstitialAd    _InterstitialAd,
            IAdMobRewardedAd        _RewardedAd) 
            : base(
                _InterstitialAd,
                _RewardedAd) { }

        #endregion

        #region api

        public override bool RewardedAdReady => Application.isEditor || base.RewardedAdReady;

        public override bool InterstitialAdReady => Application.isEditor || base.InterstitialAdReady;

        public override string Source => AdvertisingNetworks.Admob;

        #endregion

        #region nonpublic methods

        protected override void InitConfigs(UnityAction _OnSuccess)
        {
            // TODO хз нужно это или нет
            // AppLovin.SetHasUserConsent(true);
            // AppLovin.SetDoNotSell(true);
            // MyTarget.SetUserConsent(true);
            // MyTarget.SetCCPAUserConsent(true);
            // IronSource.SetConsent(true);
            // IronSource.SetMetaData("do_not_sell", "true");
            // UnityAds.SetConsentMetaData("gdpr.consent", true);
            // UnityAds.SetConsentMetaData("privacy.consent", true);
            // Vungle.UpdateConsentStatus(VungleConsent.ACCEPTED);
            // AdColonyAppOptions.SetTestMode(false);
            
            var reqConfig = new RequestConfiguration.Builder().build();
            MobileAds.SetiOSAppPauseOnBackground(true);
            MobileAds.SetRequestConfiguration(reqConfig);
            MobileAds.Initialize(_InitStatus =>
            {
                var map = _InitStatus.getAdapterStatusMap();
                foreach ((string key, var value) in map)
                {
                    Dbg.Log($"Google AdMob initialization status: {key}:" +
                            $"{value.Description}:{value.InitializationState}");
                }
                _OnSuccess?.Invoke();
            });
        }

        #endregion
    }
}

#endif