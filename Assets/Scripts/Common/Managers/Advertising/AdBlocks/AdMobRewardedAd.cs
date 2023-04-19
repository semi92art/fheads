#if ADMOB_API && !UNITY_WEBGL
using System.Text;
using Common.Helpers;
using UnityEngine.Events;
using GoogleMobileAds.Api;
using mazing.common.Runtime;
using mazing.common.Runtime.Constants;
using mazing.common.Runtime.Ticker;

namespace Common.Managers.Advertising.AdBlocks
{
    public interface IAdMobRewardedAd : IRewardedAdBase { }
    
    public class AdMobRewardedAd : RewardedAdBase, IAdMobRewardedAd
    {
        #region nonpublic members

        protected override string AdSource => AdvertisingNetworks.Admob;
        protected override string AdType   => AdTypeRewarded;
        
        private RewardedAd m_RewardedAd;

        #endregion

        #region inject
        
        public AdMobRewardedAd(
            GlobalGameSettings _GlobalGameSettings,
            ICommonTicker      _CommonTicker) 
            : base(_GlobalGameSettings, _CommonTicker) { }
        
        #endregion

        #region api
        
        public override bool Ready => m_RewardedAd != null && m_RewardedAd.CanShowAd();
        
        public override void ShowAd(
            UnityAction _OnShown,
            UnityAction _OnClicked,
            UnityAction _OnReward,
            UnityAction _OnClosed,
            UnityAction _OnFailedToShow)
        {
            OnShown        = _OnShown;
            OnClicked      = _OnClicked;
            OnReward       = _OnReward;
            OnClosed       = _OnClosed;
            OnFailedToShow = _OnFailedToShow;
            if (m_RewardedAd == null)
                LoadAd();
            m_RewardedAd.Show(_R => { });
        }

        public override void LoadAd()
        {
            var adRequest = new AdRequest.Builder()
                .Build();
            RewardedAd.Load(UnitId, adRequest, AdLoadCallback);
        }

        #endregion

        #region nonpublic methods
        
        private void AdLoadCallback(RewardedAd _Ad, LoadAdError _Error)
        {
            if (_Ad != null && _Error == null)
            {
                m_RewardedAd = _Ad;
                _Ad.OnAdFullScreenContentOpened += OnAdShown;
                _Ad.OnAdFullScreenContentClosed += OnAdClosed;
                _Ad.OnAdFullScreenContentFailed += OnAdFullScreenContentFailed;
                _Ad.OnAdImpressionRecorded      += OnAdImpressionRecorded;
                _Ad.OnAdPaid                    += OnRewardedAdPaidEvent;
                _Ad.OnAdClicked                 += OnAdClicked;
                OnAdLoaded();
            }
            else
            {
                OnRewardedAdFailedToLoad(_Error);
            }
        }

        private void OnRewardedAdFailedToLoad(AdError _Error)
        {
            OnAdFailedToLoad();
            // AdMobAdUtils.LogAdError(_Error);
        }

        private void OnRewardedAdPaidEvent(AdValue _AdValue)
        {
            OnAdRewardGot();
            var sb = new StringBuilder();
            AdMobAdUtils.AppendValue(sb, "Precision:",    _AdValue.Precision);
            AdMobAdUtils.AppendValue(sb, "Value:",        _AdValue.Value);
            AdMobAdUtils.AppendValue(sb, "CurrencyCode:", _AdValue.CurrencyCode);
            Dbg.Log(sb);
        }
        
        private void OnAdFullScreenContentFailed(AdError _Error)
        {
            OnAdFailedToShow();
            AdMobAdUtils.LogAdError(_Error);
        }

        private static void OnAdImpressionRecorded()
        {
            Dbg.Log("AdMob: Rewarded record impression");
        }

        #endregion
    }
}

#endif