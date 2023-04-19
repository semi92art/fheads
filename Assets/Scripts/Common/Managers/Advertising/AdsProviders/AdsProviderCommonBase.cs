using Common.Managers.Advertising.AdBlocks;
using mazing.common.Runtime.Managers;
using UnityEngine.Events;

namespace Common.Managers.Advertising.AdsProviders
{
    public abstract class AdsProviderCommonBase : AdsProviderBase
    {
        #region inject

        private readonly IAudioManager       m_AudioManager;
        private readonly IInterstitialAdBase m_InterstitialAd;
        private readonly IRewardedAdBase     m_RewardedAd;
        
        protected AdsProviderCommonBase(
            IInterstitialAdBase _InterstitialAd,
            IRewardedAdBase     _RewardedAd)
        {
            m_InterstitialAd         = _InterstitialAd;
            m_RewardedAd             = _RewardedAd;
        }

        #endregion

        #region api
        
        public override bool RewardedAdReady             => 
            m_RewardedAd != null && m_RewardedAd.Ready;
        public override bool InterstitialAdReady         => 
            m_InterstitialAd != null && m_InterstitialAd.Ready;

        public override void LoadRewardedAd()
        {
            m_RewardedAd.LoadAd();
        }
        
        public override void LoadInterstitialAd()
        {
            m_InterstitialAd.LoadAd();
        }

        #endregion

        #region nonpublic methods
        
        protected override void InitRewardedAd()
        {
            m_RewardedAd.Skippable = true;
            m_RewardedAd.Init(AppId, RewardedUnitId);
        }

        protected override void InitInterstitialAd()
        {
            m_InterstitialAd.Skippable = true;
            m_InterstitialAd.Init(AppId, InterstitialUnitId);
        }
        
        protected override void ShowRewardedAdCore(
            UnityAction _OnShown,
            UnityAction _OnClicked,
            UnityAction _OnReward,
            UnityAction _OnClosed,
            UnityAction _OnFailedToShow)
        {
            if (!RewardedAdReady) 
                return;
            MuteAudio(true);
            void OnClosed()
            {
                MuteAudio(false);
                _OnClosed?.Invoke();
            }
            void OnFailedToShow()
            {
                MuteAudio(false);
                _OnFailedToShow?.Invoke();
            }
            m_RewardedAd.ShowAd(_OnShown, _OnClicked, _OnReward, OnClosed, OnFailedToShow);
        }
        
        protected override void ShowInterstitialAdCore(
            UnityAction _OnShown,
            UnityAction _OnClicked, 
            UnityAction _OnClosed,
            UnityAction _OnFailedToShow)
        {
            if (!InterstitialAdReady) 
                return;
            MuteAudio(true);
            void OnClosed()
            {
                MuteAudio(false);
                _OnClosed?.Invoke();
            }
            void OnFailedToShow()
            {
                MuteAudio(false);
                _OnFailedToShow?.Invoke();
            }
            m_InterstitialAd.ShowAd(_OnShown, _OnClicked, OnClosed, OnFailedToShow);
        }

        #endregion
    }
}