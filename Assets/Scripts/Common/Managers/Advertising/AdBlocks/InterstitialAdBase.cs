using Common.Helpers;
using mazing.common.Runtime.Ticker;
using UnityEngine.Events;

namespace Common.Managers.Advertising.AdBlocks
{
    public interface IInterstitialAdBase : IAdBase
    {
        void ShowAd(
            UnityAction _OnShown,
            UnityAction _OnClicked,
            UnityAction _OnClosed, 
            UnityAction _OnFailedToShow);
    }
    
    public abstract class InterstitialAdBase : AdBase, IInterstitialAdBase
    {
        protected InterstitialAdBase(
            GlobalGameSettings _GlobalGameSettings,
            ICommonTicker      _CommonTicker) 
            : base(_GlobalGameSettings, _CommonTicker) { }

        public abstract void ShowAd(
            UnityAction _OnShown, 
            UnityAction _OnClicked, 
            UnityAction _OnClosed,
            UnityAction _OnFailedToShow);
    }
}