#if APPODEAL_3 && !UNITY_WEBGL
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppodealStack.ConsentManagement.Common;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using Common.Helpers;
using Common.Managers.Advertising.AdBlocks;
using mazing.common.Runtime;
using mazing.common.Runtime.Constants;
using UnityEngine;
using UnityEngine.Events;

namespace Common.Managers.Advertising.AdsProviders
{
    public interface IAppodealAdsProvider : IAdsProvider, IAppodealInitializationListener { }
    
    public class AppodealAdsProvider : AdsProviderCommonBase, IAppodealAdsProvider
    {
        private GlobalGameSettings GlobalGameSettings { get; }

        #region nonpublic members

        protected override string AppId => Application.isEditor ? string.Empty : base.AppId;

        private IConsent    m_Consent;
        private UnityAction m_OnSuccess;

        #endregion
        
        #region inject

        private AppodealAdsProvider(
            GlobalGameSettings      _GlobalGameSettings,
            IAppodealInterstitialAd _InterstitialAd,
            IAppodealRewardedAd     _RewardedAd)
            : base(
                _InterstitialAd,
                _RewardedAd)
        {
            GlobalGameSettings = _GlobalGameSettings;
        }

        #endregion

        #region api

        public override string Source => AdvertisingNetworks.Appodeal;

        public void OnInitializationFinished(List<string> _Errors)
        {
            if (_Errors == null || !_Errors.Any())
            {
                Dbg.Log("Appodeal successfully initialized");
                m_OnSuccess?.Invoke();
                return;
            }
            var sb = new StringBuilder();
            sb.AppendLine("Failed to initialize Appodeal!");
            foreach (string error in _Errors)
                sb.AppendLine(error);
            Dbg.LogError(sb.ToString());
        }

        #endregion

        #region nonpublic methods

        protected override void InitConfigs(UnityAction _OnSuccess)
        {
            m_OnSuccess = _OnSuccess;
            Appodeal.SetLogLevel(AppodealLogLevel.Verbose);
            Appodeal.SetChildDirectedTreatment(true);
            const int adTypes = AppodealAdType.Interstitial | AppodealAdType.RewardedVideo;
            Appodeal.SetAutoCache(adTypes, false);
            Appodeal.Initialize(AppId, adTypes, this);
        }

        #endregion
    }
}
#endif