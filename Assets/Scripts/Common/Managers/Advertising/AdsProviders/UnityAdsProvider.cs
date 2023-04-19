#if UNITY_ADS_API && !UNITY_WEBGL
using System.Text;
using Common.Managers.Advertising.AdBlocks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Advertisements;

namespace Common.Managers.Advertising.AdsProviders
{
    public interface IUnityAdsProvider : IAdsProvider { }
    
    public class UnityAdsProvider : 
        AdsProviderCommonBase, 
        IUnityAdsProvider, 
        IUnityAdsInitializationListener
    {
        #region nonpublic members

        private UnityAction m_OnSuccess;
        
        #endregion

        #region inject
        
        private UnityAdsProvider(
            IUnityAdsInterstitialAd _InterstitialAd,
            IUnityAdsRewardedAd     _RewardedAd) 
            : base(
                _InterstitialAd,
                _RewardedAd) { }

        #endregion

        #region api
        
        public override string Source              => AdvertisingNetworks.UnityAds;
        public override bool   RewardedAdReady     => Application.isEditor || base.RewardedAdReady;
        public override bool   InterstitialAdReady => Application.isEditor || base.InterstitialAdReady;
        
        public void OnInitializationComplete()
        {
            Dbg.Log("Unity Ads: Initialization completed");
            m_OnSuccess?.Invoke();
        }
    
        public void OnInitializationFailed(UnityAdsInitializationError _Error, string _Message)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Unity Ads: Initialization failed:");
            sb.AppendLine($"Error: {_Error}");
            sb.AppendLine($"Message: {_Message}");
            Dbg.LogWarning(sb.ToString());
        }

        #endregion

        #region nonpublic methods

        protected override void InitConfigs(UnityAction _OnSuccess)
        {
            m_OnSuccess = _OnSuccess;
            SetMetadata();
            Advertisement.Initialize(AppId, TestMode, this);
        }
    
        private static void SetMetadata()
        {
            // Starting April 1, 2022, the Google Families Policy will no longer allow Device IDs
            // to leave users’ devices from apps where one of the target audiences is children
            // https://docs.unity.com/ads/GoogleFamiliesCompliance.html
            MetaData metaData = new MetaData("privacy");
            metaData.Set("mode", "mixed"); // This is a mixed audience game.
            Advertisement.SetMetaData(metaData);
            metaData = new MetaData("user");
            metaData.Set("nonbehavioral", "true"); // This user will NOT receive personalized ads.
            Advertisement.SetMetaData(metaData);
        }
        
        #endregion
    }
}

#endif