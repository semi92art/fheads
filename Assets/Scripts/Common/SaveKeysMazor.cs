using System.Collections.Generic;
using mazing.common.Runtime.Entities;
using mazing.common.Runtime.Utils;
using UnityEngine;

namespace Common
{
    public static class SaveKeysMazor
    {
        private static SaveKey<bool?>        _disableAds;
        private static SaveKey<bool>         _notFirstLaunch;
        private static SaveKey<string>       _login;
        private static SaveKey<string>       _passwordHash;
        private static SaveKey<List<int>>    _boughtPurchaseIds;
        private static SaveKey<List<string>> _debugConsoleCommandsHistory;
        private static SaveKey<string>       _appVersion;
        
        [RuntimeInitializeOnLoadMethod]
        public static void ResetState()
        {
            SetAllToNull();
            CacheFromDisc();
        }
        
        public static SaveKey<bool?>  DisableAds               =>
            _disableAds ??= new SaveKey<bool?>(nameof(DisableAds));
        public static SaveKey<bool>   NotFirstLaunch           =>
            _notFirstLaunch ??= new SaveKey<bool>(nameof(NotFirstLaunch));
        public static SaveKey<string> Login                    => 
            _login ??= new SaveKey<string>(nameof(Login));
        public static SaveKey<string> PasswordHash             => 
            _passwordHash ??= new SaveKey<string>(nameof(PasswordHash));
        public static SaveKey<string> ServerUrl                =>
            new SaveKey<string>(nameof(ServerUrl));
        public static SaveKey<string> AppVersion               =>
            _appVersion ??= new SaveKey<string>(nameof(AppVersion));
        public static SaveKey<List<int>> BoughtPurchaseIds     => 
            _boughtPurchaseIds ??= new SaveKey<List<int>>(nameof(BoughtPurchaseIds));
        public static SaveKey<List<string>> DebugConsoleCommandsHistory =>
            _debugConsoleCommandsHistory ??= new SaveKey<List<string>>(nameof(DebugConsoleCommandsHistory));

        private static void SetAllToNull()
        {
            _disableAds                  = null;
            _notFirstLaunch              = null;
            _login                       = null;
            _passwordHash                = null;
            _boughtPurchaseIds           = null;
            _debugConsoleCommandsHistory = null;
            _appVersion                  = null;
        }

        private static void CacheFromDisc()
        {
            const bool onlyCache = true;
            SaveUtils.PutValue(DisableAds,                  SaveUtils.GetValue(DisableAds),                 onlyCache);
            SaveUtils.PutValue(NotFirstLaunch,              SaveUtils.GetValue(NotFirstLaunch),             onlyCache);
            SaveUtils.PutValue(BoughtPurchaseIds,           SaveUtils.GetValue(BoughtPurchaseIds),          onlyCache);
            SaveUtils.PutValue(DebugConsoleCommandsHistory, SaveUtils.GetValue(DebugConsoleCommandsHistory),onlyCache);
            SaveUtils.PutValue(AppVersion,                  SaveUtils.GetValue(AppVersion),                 onlyCache);
        }
    }
}