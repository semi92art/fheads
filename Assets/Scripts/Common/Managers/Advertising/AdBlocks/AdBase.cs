using Common.Helpers;
using mazing.common.Runtime;
using mazing.common.Runtime.Ticker;
using UnityEngine.Events;

namespace Common.Managers.Advertising.AdBlocks
{
    public interface IAdBase
    {
        bool Skippable { get; set; }
        bool Ready     { get; }
        void Init(string _AppId, string _UnitId);
        void LoadAd();
    }
    
    public abstract class AdBase : IAdBase, IUpdateTick
    {
        #region constants

        protected const string AdTypeRewarded     = "Rewarded";
        protected const string AdTypeInterstitial = "Interstitial";

        #endregion
        
        #region nonpublic members

        protected abstract string AdSource { get; }
        protected abstract string AdType   { get; }

        protected        UnityAction OnShown;
        protected        UnityAction OnClosed;
        protected        UnityAction OnClicked;
        protected        UnityAction OnFailedToShow;
        protected        UnityAction OnReward;
        protected        string      UnitId;
        private          float       m_LoadAdDelayTimer;

        private volatile bool m_DoLoadAdWithDelay;
        private volatile bool m_DoInvokeOnShown;
        private volatile bool m_DoInvokeOnClosed;
        private volatile bool m_DoInvokeOnClicked;
        private volatile bool m_DoInvokeOnFailedToShow;
        private volatile bool m_DoInvokeOnReward;
        
        #endregion

        #region inject

        private GlobalGameSettings GlobalGameSettings { get; }
        private ICommonTicker      CommonTicker       { get; }

        protected AdBase(
            GlobalGameSettings _GlobalGameSettings, 
            ICommonTicker      _CommonTicker)
        {
            GlobalGameSettings = _GlobalGameSettings;
            CommonTicker       = _CommonTicker;
        }

        #endregion

        #region api
        
        public virtual void Init(string _AppId, string _UnitId)
        {
            UnitId = _UnitId;
            CommonTicker.Register(this);
            LoadAd();
        }

        public          bool Skippable { get; set; }
        public abstract bool Ready     { get; }
        
        public abstract void LoadAd();
        
        public virtual void UpdateTick()
        {
            ProceedShownAction();
            ProceedClickedAction();
            ProceedRewardAction();
            ProceedClosedAction();
            ProceedFailedToShownAction();
            ProceedLoadAdOnDelay();
        }

        #endregion

        #region nonpublic methods

        private void ProceedShownAction()
        {
            if (!m_DoInvokeOnShown) 
                return;
            Dbg.Log("Ad shown action, time:" + CommonTicker.Time);
            if (OnShown == null)
                Dbg.LogWarning("OnShown action is null");
            OnShown?.Invoke();
            OnShown = null;
            m_DoInvokeOnShown = false;
        }
        
        private void ProceedClickedAction()
        {
            if (!m_DoInvokeOnClicked)
                return;
            Dbg.Log("Ad click action, time:" + CommonTicker.Time);
            OnClicked?.Invoke();
            OnClicked = null;
            m_DoInvokeOnClicked = false;
        }

        private void ProceedClosedAction()
        {
            if (!m_DoInvokeOnClosed) 
                return;
            Dbg.Log("Ad close action, time:" + CommonTicker.Time);
            OnClosed?.Invoke();
            OnClosed = null;
            m_DoInvokeOnClosed = false;
            LoadAd();
        }
        
        private void ProceedFailedToShownAction()
        {
            if (!m_DoInvokeOnFailedToShow) 
                return;
            Dbg.Log("Ad failed to show action, time:" + CommonTicker.Time);
            if (OnFailedToShow == null)
                Dbg.LogWarning("OnFailedToShowShown action is null");
            OnFailedToShow?.Invoke();
            OnFailedToShow = null;
            LoadAd();
            m_DoInvokeOnFailedToShow = false;
        }

        private void ProceedLoadAdOnDelay()
        {
            m_LoadAdDelayTimer += CommonTicker.DeltaTime;
            if (!m_DoLoadAdWithDelay)
                return;
            if (m_LoadAdDelayTimer < GlobalGameSettings.adsLoadDelay) 
                return;
            LoadAd();
            m_LoadAdDelayTimer = 0f;
            m_DoLoadAdWithDelay = false;
        }
        
        private void ProceedRewardAction()
        {
            if (!m_DoInvokeOnReward) 
                return;
            Dbg.Log("Ad reward action");
            OnReward?.Invoke();
            OnReward = null;
            m_DoInvokeOnReward = false;
        }
        
        protected void OnAdLoaded()
        {
            Dbg.Log($"{AdSource}: {AdType} loaded");
        }
        
        protected void OnAdFailedToLoad()
        {
            // Dbg.LogWarning($"{AdSource}: {AdType} load failed");
            m_DoLoadAdWithDelay = true;
        }
        
        protected void OnAdShown()
        {
            Dbg.Log($"{AdSource}: {AdType} shown");
            m_DoInvokeOnShown = true;
        }
        
        protected void OnAdClosed()
        {
            Dbg.Log($"{AdSource}: {AdType} closed");
            m_DoInvokeOnClosed = true;
        }
        
        protected void OnAdFailedToShow()
        {
            Dbg.LogWarning($"{AdSource}: {AdType} show failed");
            m_DoLoadAdWithDelay = true;
            m_DoInvokeOnFailedToShow = true;
        }
        
        protected void OnAdClicked()
        {
            Dbg.Log($"{AdSource}: {AdType} clicked");
            m_DoInvokeOnClicked = true;
        }
        
        protected void OnAdRewardGot()
        {
            Dbg.Log($"{AdSource}: {AdType} reward got");
            m_DoInvokeOnReward = true;
        }

        #endregion
    }
}