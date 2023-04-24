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
        public bool      showOnlyRewardedAds;
    }
}