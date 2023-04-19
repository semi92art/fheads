#if APPODEAL_3 && !UNITY_WEBGL
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using Common.Helpers;
using mazing.common.Runtime;
using mazing.common.Runtime.Constants;
using mazing.common.Runtime.Ticker;
using UnityEngine.Events;

namespace Common.Managers.Advertising.AdBlocks
{
    public interface IAppodealInterstitialAd : IInterstitialAdBase, IInterstitialAdListener { }
    
    public class AppodealInterstitialAd : InterstitialAdBase, IAppodealInterstitialAd
    {
        #region nonpublic members
        
        protected override string AdSource   => AdvertisingNetworks.Appodeal;
        protected override string AdType     => AdTypeInterstitial;
        private            int    ShowStyle  => AppodealShowStyle.Interstitial;
        private            int    AppoAdType => AppodealAdType.Interstitial;

        #endregion

        #region inject
        
        private AppodealInterstitialAd(
            GlobalGameSettings _GameSettings,
            ICommonTicker      _CommonTicker)
            : base(_GameSettings, _CommonTicker) { }

        #endregion

        #region api
        
        public override bool Ready => Appodeal.IsLoaded(AppoAdType);
        
        public override void Init(string _AppId, string _UnitId)
        {
            base.Init(_AppId, _UnitId);
            Appodeal.SetInterstitialCallbacks(this);
        }

        public override void LoadAd()
        {
            Appodeal.Cache(AppoAdType);
        }

        public override void ShowAd(
            UnityAction _OnShown,
            UnityAction _OnClicked,
            UnityAction _OnClosed,
            UnityAction _OnFailedToShow)
        {
            if (!Ready) 
                return;
            OnShown        = _OnShown;
            OnClicked      = _OnClicked;
            OnClosed       = _OnClosed;
            OnFailedToShow = _OnFailedToShow;
            const string placement = "default";
            Appodeal.Show(ShowStyle, placement);
        }

        public void OnInterstitialLoaded(bool _IsPrecache) 
        { 
            OnAdLoaded();
        } 

        public void OnInterstitialFailedToLoad() 
        {
            OnAdFailedToLoad();
        } 

        public void OnInterstitialShowFailed() 
        {
            OnAdFailedToShow();
        } 

        public void OnInterstitialShown() 
        {
            OnAdShown();
        } 

        public void OnInterstitialClosed() 
        { 
            OnAdClosed();
        } 

        public void OnInterstitialClicked() 
        {
            OnAdClicked();
        } 

        public void OnInterstitialExpired() 
        {
            Dbg.Log("Appodeal: Interstitial expired"); 
        }

        #endregion
    }
}
#endif