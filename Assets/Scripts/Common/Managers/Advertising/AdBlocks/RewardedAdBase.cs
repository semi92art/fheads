using Common.Helpers;
using mazing.common.Runtime.Ticker;
using UnityEngine.Events;

namespace Common.Managers.Advertising.AdBlocks
{
    public interface IRewardedAdBase : IAdBase
    {
        void ShowAd(
            UnityAction _OnShown,
            UnityAction _OnClicked,
            UnityAction _OnReward,
            UnityAction _OnClosed,
            UnityAction _OnFailedToShow);
    }
    
    public abstract class RewardedAdBase : AdBase, IRewardedAdBase
    {
        #region nonpublic members

        #endregion

        #region inject
        
        protected RewardedAdBase(
            GlobalGameSettings _GlobalGameSettings,
            ICommonTicker      _CommonTicker) 
            : base(_GlobalGameSettings, _CommonTicker) { }

        #endregion

        #region api
        
        public abstract void ShowAd(
            UnityAction _OnShown, 
            UnityAction _OnClicked, 
            UnityAction _OnReward,
            UnityAction _OnClosed,
            UnityAction _OnFailedToShow);

        #endregion
    }
}