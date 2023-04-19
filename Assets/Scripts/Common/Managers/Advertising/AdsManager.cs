using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Helpers;
using Common.Managers.Advertising.AdsProviders;
using mazing.common.Runtime;
using mazing.common.Runtime.Constants;
using mazing.common.Runtime.Enums;
using mazing.common.Runtime.Exceptions;
using mazing.common.Runtime.Helpers;
using mazing.common.Runtime.Managers;
using mazing.common.Runtime.Utils;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Common.Managers.Advertising
{
    public interface IAdsManager : IInit
    {
        bool RewardedAdReady             { get; }
        bool InterstitialAdReady         { get; }
        bool ShowAds                     { get; set; }

        void ShowRewardedAd(
            UnityAction _OnBeforeShown  = null,
            UnityAction _OnShown        = null,
            UnityAction _OnClicked      = null,
            UnityAction _OnReward       = null,
            UnityAction _OnClosed       = null,
            UnityAction _OnFailedToShow = null,
            string      _AdsNetwork     = null,
            bool        _Forced         = false);

        void ShowInterstitialAd(
            UnityAction _OnBeforeShown  = null,
            UnityAction _OnShown        = null,
            UnityAction _OnClicked      = null,
            UnityAction _OnClosed       = null,
            UnityAction _OnFailedToShow = null,
            string      _AdsNetwork     = null,
            bool        _Forced         = false);
    }

    public class AdsManager : InitBase, IAdsManager
    {
        #region nonpublic members

        private readonly Dictionary<string, IAdsProvider> m_Providers = new Dictionary<string, IAdsProvider>();

        #endregion

        #region inject

        private IAdsProvidersSet        AdsProvidersSet  { get; }
        private IAnalyticsManager       AnalyticsManager { get; }

        private AdsManager(
            IAdsProvidersSet        _AdsProvidersSet,
            IAnalyticsManager       _AnalyticsManager)
        {
            AdsProvidersSet  = _AdsProvidersSet;
            AnalyticsManager = _AnalyticsManager;
        }

        #endregion

        #region api

        public bool ShowAds
        {
            get => GetShowAdsCached();
            set => SaveUtils.PutValue(SaveKeysMazor.DisableAds, !value);
        }

        public bool RewardedAdReady     => m_Providers.Values.Any(_P => _P.RewardedAdReady);
        public bool InterstitialAdReady => m_Providers.Values.Any(_P => _P.InterstitialAdReady);

        public override void Init()
        {
            if (Initialized)
                return;
            InitProviders();
            base.Init();
        }

        public void ShowRewardedAd(
            UnityAction _OnBeforeShown,
            UnityAction _OnShown,
            UnityAction _OnClicked,
            UnityAction _OnReward,
            UnityAction _OnClosed,
            UnityAction _OnFailedToShow,
            string      _AdsNetwork = null,
            bool        _Forced     = false)
        {
            var providers = (string.IsNullOrEmpty(_AdsNetwork)
                ? m_Providers.Values
                : m_Providers.Where(_Kvp => _Kvp.Key == _AdsNetwork)
                    .Select(_Kvp => _Kvp.Value)).ToList();
            static bool IsProviderReady(IAdsProvider _Provider)
            {
                return _Provider != null && _Provider.Initialized && _Provider.RewardedAdReady;
            }
            var readyProviders = providers
                .Where(_P => _P.Initialized && _P.RewardedAdReady)
                .ToList();
            if (!readyProviders.Any())
            {
                Dbg.LogWarning("Rewarded ad was not ready to be shown.");
                foreach (var provider in providers
                    .Where(_P => _P != null && _P.Initialized)
                    .Where(_P => !IsProviderReady(_P)))
                {
                    provider.LoadRewardedAd();
                }
                return;
            }
            Cor.Run(ShowAd(
                readyProviders,
                _OnBeforeShown, 
                _OnShown,
                _OnClicked, 
                _OnReward,
                _OnClosed,
                _OnFailedToShow,
                EAdvertisingType.Rewarded,
                _Forced));

        }

        public void ShowInterstitialAd(
            UnityAction _OnBeforeShown,
            UnityAction _OnShown,
            UnityAction _OnClicked,
            UnityAction _OnClosed,
            UnityAction _OnFailedToShow,
            string      _AdsNetwork = null,
            bool        _Forced     = false)
        {
            var providers = (string.IsNullOrEmpty(_AdsNetwork)
                ? m_Providers.Values
                : m_Providers.Where(_Kvp => _Kvp.Key == _AdsNetwork)
                    .Select(_Kvp => _Kvp.Value)).ToList();
            static bool IsProviderReady(IAdsProvider _Provider)
            {
                return _Provider != null && _Provider.Initialized && _Provider.InterstitialAdReady;
            }
            var readyProviders = providers
                .Where(_P => _P.Initialized && _P.InterstitialAdReady)
                .ToList();
            if (!readyProviders.Any())
            {
                Dbg.LogWarning("Interstitial ad was not ready to be shown.");
                foreach (var provider in providers.Where(_P => _P != null && _P.Initialized)
                    .Where(_P => !IsProviderReady(_P)))
                {
                    provider.LoadInterstitialAd();
                }
                return;
            }
            Cor.Run(ShowAd(
                readyProviders,
                _OnBeforeShown,
                _OnShown,
                _OnClicked, 
                null,
                _OnClosed,
                _OnFailedToShow,
                EAdvertisingType.Interstitial,
                _Forced));
        }

        #endregion

        #region nonpublic methods

        private void InitProviders()
        {
            var adsConfig = ResLoader.FromResources(@"configs\ads");
            foreach (var adsProvider in AdsProvidersSet.GetProviders())
            {
                adsProvider.Init(false, 100f, adsConfig);
                m_Providers.Add(adsProvider.Source, adsProvider);
            }
        }

        private IEnumerator ShowAd(
            IReadOnlyCollection<IAdsProvider> _Providers,
            UnityAction                       _OnBeforeShown,
            UnityAction                       _OnShown,
            UnityAction                       _OnClicked,
            UnityAction                       _OnReward,
            UnityAction                       _OnClosed,
            UnityAction                       _OnFailedToShow,
            EAdvertisingType                   _Type,
            bool                              _Forced)
        {
            _OnBeforeShown?.Invoke();
            IAdsProvider selectedProvider = null;
            if (_Providers.Count == 1)
                selectedProvider = _Providers.First();
            else
            {
                float showRateSum = _Providers.Sum(_P => _P.ShowRate);
                float randValue = Random.value;
                float showRateSumIteration = 0f;
                foreach (var provider in _Providers)
                {
                    if (MathUtils.IsInRange(
                        randValue,
                        showRateSumIteration / showRateSum,
                        (showRateSumIteration + provider.ShowRate) / showRateSum))
                    {
                        selectedProvider = provider;
                        break;
                    }
                    showRateSumIteration += provider.ShowRate;
                }
            }
            if (Application.isEditor)
            {
                selectedProvider = _Providers.First(
                    _P => _P.Source == AdvertisingNetworks.Admob);
            }
            if (selectedProvider == null)
                yield break;
            var eventData = new Dictionary<string, object>
            {
                {AnalyticIds.ParameterAdSource, selectedProvider.Source},
                {AnalyticIds.ParameterAdType, Enum.GetName(typeof(EAdvertisingType), _Type)}
            };
            void OnShownExtended()
            {
                _OnShown?.Invoke();
                AnalyticsManager.SendAnalytic(AnalyticIds.AdShown, eventData);
            }
            void OnClicked()
            {
                _OnClicked?.Invoke();
                AnalyticsManager.SendAnalytic(AnalyticIds.AdClicked, eventData);
            }
            void OnReward()
            {
                _OnReward?.Invoke();
                AnalyticsManager.SendAnalytic(AnalyticIds.AdReward, eventData);
            }
            void OnClosed()
            {
                _OnClosed?.Invoke();
                AnalyticsManager.SendAnalytic(AnalyticIds.AdClosed, eventData);
            }
            void OnFailedToShow()
            {
                _OnFailedToShow?.Invoke();
                AnalyticsManager.SendAnalytic(AnalyticIds.AdFailedToShow, eventData);
            }
            switch (_Type)
            {
                case EAdvertisingType.Interstitial:
                    selectedProvider.ShowInterstitialAd(
                        OnShownExtended, 
                        OnClicked, 
                        OnClosed, 
                        OnFailedToShow,
                        ShowAds, 
                        _Forced);
                    break;
                case EAdvertisingType.Rewarded:
                    selectedProvider.ShowRewardedAd(
                        OnShownExtended, 
                        OnClicked, 
                        OnReward, 
                        OnClosed, 
                        OnFailedToShow,
                        ShowAds, 
                        _Forced);
                    break;
                default:
                    throw new SwitchCaseNotImplementedException(_Type);
            }
        }

        private static bool GetShowAdsCached()
        {
            var val = SaveUtils.GetValue(SaveKeysMazor.DisableAds);
            return !val.HasValue || !val.Value;
        }

        #endregion
    }
}