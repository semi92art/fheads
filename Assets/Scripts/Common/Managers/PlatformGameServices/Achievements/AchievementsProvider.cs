using System.Collections.Generic;
using System.Linq;
using Common.Managers.PlatformGameServices.GameServiceAuth;
using mazing.common.Runtime;
using mazing.common.Runtime.Entities;
using mazing.common.Runtime.Extensions;
using mazing.common.Runtime.Helpers;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Common.Managers.PlatformGameServices.Achievements
{
    public interface IAchievementsProvider : IInit
    {
        void RegisterAchievementsSet(Dictionary<ushort, string> _Set);

        Entity<IAchievement> UnlockAchievement(ushort _Key);
        Entity<IAchievement> GetAchievement(ushort    _Key);
    }

    public class AchievementsProvider : InitBase, IAchievementsProvider
    {
        #region nonpublic members

        private Dictionary<ushort, string> m_AssumedAchievementsSet;
        private IAchievement[]             m_LoadedAchievements;
        
        #endregion

        #region inject
        
        private IPlatformGameServiceAuthenticator Authenticator { get; }

        public AchievementsProvider(IPlatformGameServiceAuthenticator _Authenticator)
        {
            Authenticator = _Authenticator;
        }

        #endregion

        #region api
        
        public override void Init()
        {
            if (Initialized)
                return;
            if (Authenticator.IsAuthenticated)
                InitAchievements();
            base.Init();
        }

        public void RegisterAchievementsSet(Dictionary<ushort, string> _Set)
        {
            m_AssumedAchievementsSet = _Set;
        }

        public Entity<IAchievement> UnlockAchievement(ushort _Key)
        {
            var entity = new Entity<IAchievement>();
#if UNITY_ANDROID
            Entity<IAchievement> Failed(string _Message)
            {
                Dbg.LogError(_Message);
                entity.Error = _Message;
                entity.Result = EEntityResult.Fail;
                return entity;
            }
            var achievement = GetAchievementCore(_Key, out string error);
            if (!string.IsNullOrEmpty(error))
                return Failed(error);
            achievement.percentCompleted = 100d;
            achievement.ReportProgress(_Success =>
            {
                if (!_Success)
                    Dbg.LogWarning($"Failed to unlock achievement with key {_Key}");
                entity.Result = _Success ? EEntityResult.Success : EEntityResult.Fail;
            });
            entity.Value = achievement;
#elif UNITY_IOS
            string id = m_AssumedAchievementsSet.GetSafe(_Key, out bool containsKey);
            if (!containsKey)
                Dbg.LogError("Achievement with this key was not found in key-id map.");
            SA.iOS.GameKit.ISN_GKAchievement achievement = new SA.iOS.GameKit.ISN_GKAchievement(id);
            achievement.PercentComplete = 100.0f;
            achievement.Report((result) => {
                if(result.IsSucceeded)
                {
                    entity.Result = EEntityResult.Success;
                    Dbg.Log("Achievement reported");
                } else
                {
                    entity.Result = EEntityResult.Fail;
                    Dbg.LogError($"Achievement report failed! Code: {result.Error.Code} Message: {result.Error.Message}");
                }
            });
#endif
            return entity;
        }

        public Entity<IAchievement> GetAchievement(ushort _Key)
        {
            var entity = new Entity<IAchievement>();
            Entity<IAchievement> Failed(string _Message)
            {
                Dbg.LogError(_Message);
                entity.Result = EEntityResult.Fail;
                entity.Error = _Message;
                return entity;
            }
            var achievement = GetAchievementCore(_Key, out string error);
            if (!string.IsNullOrEmpty(error))
                return Failed(error);
            entity.Value = achievement;
            entity.Result = EEntityResult.Success;
            return entity;
        }

        #endregion

        #region nonpublic methods
        
        private void InitAchievements()
        {
            Social.LoadAchievements(_Achievements =>
            {
                foreach (var ach in _Achievements)
                {
                    Dbg.Log("Achievement loaded: " + ach.id);
                }
                m_LoadedAchievements = _Achievements;
            });
        }
        
        private IAchievement GetAchievementCore(ushort _Key, out string _Error)
        {
            _Error = null;
            if (m_AssumedAchievementsSet == null)
                _Error = "Achievements map was not registered.";
            if (m_LoadedAchievements == null)
                _Error = "Achievements were not loaded from server.";
            string id = m_AssumedAchievementsSet.GetSafe(_Key, out bool containsKey);
            if (!containsKey)
                _Error = "Achievement with this key was not found in key-id map.";
            var result = m_LoadedAchievements?.FirstOrDefault(_Ach => _Ach.id == id);
            if (result == null)
                _Error = "Achievement with this key was not found in loaded achievements.";
            return result;
        }

        #endregion
    }
}