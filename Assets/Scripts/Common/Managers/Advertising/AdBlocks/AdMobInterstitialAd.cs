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
    public interface IAdMobInterstitialAd : IInterstitialAdBase { }
    
    public class AdMobInterstitialAd : InterstitialAdBase, IAdMobInterstitialAd
    {
        #region nonpublic members
        
        protected override string AdSource => AdvertisingNetworks.Admob;
        protected override string AdType   => AdTypeInterstitial;
        
        private InterstitialAd m_InterstitialAd;

        #endregion

        #region inject

        private AdMobInterstitialAd(
            GlobalGameSettings _GameSettings,
            ICommonTicker      _CommonTicker)
            : base(_GameSettings, _CommonTicker) { }

        #endregion

        #region api
        
        public override bool Ready => m_InterstitialAd != null && m_InterstitialAd.CanShowAd();

        public override void ShowAd(
            UnityAction _OnShown, 
            UnityAction _OnClicked,
            UnityAction _OnClosed,
            UnityAction _OnFailedToShow)
        {
            OnShown        = _OnShown;
            OnClicked      = _OnClicked;
            OnClosed       = _OnClosed;
            OnFailedToShow = _OnFailedToShow;
            if (m_InterstitialAd == null)
                LoadAd();
            m_InterstitialAd.Show();
        }

        public override void LoadAd()
        {
            var adRequest = new AdRequest.Builder().Build();
            InterstitialAd.Load(UnitId, adRequest, AdLoadCallback);
        }

        private void AdLoadCallback(InterstitialAd _Ad, LoadAdError _Error)
        {
            if (_Ad != null && _Error == null)
            {
                m_InterstitialAd = _Ad;
                _Ad.OnAdFullScreenContentOpened += OnAdShown;
                _Ad.OnAdFullScreenContentClosed += OnAdClosed;
                _Ad.OnAdFullScreenContentFailed += OnAdFullScreenContentFailed;
                _Ad.OnAdImpressionRecorded      += OnAdImpressionRecorded;
                _Ad.OnAdPaid                    += OnInterstitialAdPaidEvent;
                _Ad.OnAdClicked                 += OnAdClicked;
                OnAdLoaded();
            }
            else
            {
                OnInterstitialAdFailedToLoad(_Error);
            }
        }

        #endregion

        #region nonpublic methods

        private void OnInterstitialAdFailedToLoad(AdError _Error)
        {
            OnAdFailedToLoad();
            // AdMobAdUtils.LogAdError(_Error);
        }
        
        private void OnInterstitialAdPaidEvent(AdValue _AdValue)
        {
            OnAdRewardGot();
            var sb = new StringBuilder();
            AdMobAdUtils.AppendValue(sb, "Precision",    _AdValue.Precision);
            AdMobAdUtils.AppendValue(sb, "Value",        _AdValue.Value);
            AdMobAdUtils.AppendValue(sb, "CurrencyCode", _AdValue.CurrencyCode);
            Dbg.Log(sb);
        }
        
        private void OnAdFullScreenContentFailed(AdError _Error)
        {
            OnAdFailedToShow();
            AdMobAdUtils.LogAdError(_Error);
        }
        
        private static void OnAdImpressionRecorded()
        {
            Dbg.Log("AdMob: Interstitial record impression");
        }

        #endregion
    }
}

#endif