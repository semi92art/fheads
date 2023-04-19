using mazing.common.Runtime;
using UnityEngine;

namespace Common.Helpers
{
    [CreateAssetMenu(fileName = "game_settings", menuName = "Configs and Sets/Game Settings", order = 1)]
    public class GlobalGameSettings : ScriptableObject
    {
        public float     adsLoadDelay;
        public bool      debugAnyway;
        public ELogLevel logLevel;
        public int       firstLevelToShowAds;
        public int       payToContinueMoneyCount;
        public int       moneyItemCoast;
        public float     interstitialAdsRatio;
        public float     moneyItemsRate;
        public float     betweenLevelAdShowIntervalInSeconds;
        public bool      enableExtraLevels;
        public int       extraLevelEveryNStage;
        public bool      showOnlyRewardedAds;
    }
}