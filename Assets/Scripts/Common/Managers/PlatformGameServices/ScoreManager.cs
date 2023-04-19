using System.Collections.Generic;
using Common.Entities;
using Common.Managers.PlatformGameServices.Achievements;
using Common.Managers.PlatformGameServices.GameServiceAuth;
using Common.Managers.PlatformGameServices.Leaderboards;
using Common.Managers.PlatformGameServices.SavedGames;
using mazing.common.Runtime;
using mazing.common.Runtime.Entities;
using mazing.common.Runtime.Helpers;
using mazing.common.Runtime.Ticker;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Common.Managers.PlatformGameServices
{
    public class ScoresEventArgs
    {
        public ScoresEntity ScoresEntity { get; }

        public ScoresEventArgs(ScoresEntity _ScoresEntity)
        {
            ScoresEntity = _ScoresEntity;
        }
    }
    
    public class SavedGameEventArgs
    {
        public object SavedGame { get; }

        public SavedGameEventArgs(object _SavedGame)
        {
            SavedGame = _SavedGame;
        }
    }

    public delegate void ScoresEventHandler(ScoresEventArgs _Args);

    public interface IScoreManager:
        IAchievementsProvider,
        ILeaderboardProvider,
        ISavedGameProvider { }
    
    public class ScoreManager : InitBase, IScoreManager
    {
        #region inject

        private ICommonTicker                     Ticker               { get; }
        private IPlatformGameServiceAuthenticator Authenticator        { get; }
        private IAchievementsProvider             AchievementsProvider { get; }
        private ILeaderboardProvider              LeaderboardProvider  { get; }
        private ISavedGameProvider                SavedGameProvider    { get; }

        protected ScoreManager(
            ICommonTicker                     _Ticker,
            IPlatformGameServiceAuthenticator _Authenticator,
            IAchievementsProvider             _AchievementsProvider,
            ILeaderboardProvider              _LeaderboardProvider,
            ISavedGameProvider                _SavedGameProvider)
        {
            Ticker                  = _Ticker;
            Authenticator           = _Authenticator;
            AchievementsProvider    = _AchievementsProvider;
            LeaderboardProvider     = _LeaderboardProvider;
            SavedGameProvider       = _SavedGameProvider;
        }
        
        #endregion
        
        #region api
        
        public event ScoresEventHandler ScoresChanged;
        public event GameSavedAction GameSaved
        {
            add => SavedGameProvider.GameSaved += value;
            remove => SavedGameProvider.GameSaved -= value;
        }
        
        public override void Init()
        {
            if (Initialized)
                return;
            Ticker.Register(this);
            if (Application.isEditor)
            {
                base.Init();
                return;
            }
            Authenticator.AuthenticatePlatformGameService(_Success =>
            {
                if (!_Success)
                {
                    Dbg.LogError("Failed to authenticate to platform game service");
                    base.Init();
                    return;
                }
                AchievementsProvider.Init();
                LeaderboardProvider .Init();
                SavedGameProvider   .Init();
                base.Init();
            });
        }

        public Entity<object> GetSavedGameProgress(string _FileName)
        {
            return SavedGameProvider.GetSavedGameProgress(_FileName);
        }

        public void SaveGameProgress<T>(T _Data) where T : FileNameArgs
        {
            SavedGameProvider.SaveGameProgress(_Data);
        }

        public SavedGameV2 GetSavedGame(string _FileName)
        {
            return SavedGameProvider.GetSavedGame(_FileName);
        }

        public void SaveGame(SavedGameV2 _SavedGame)
        {
            SavedGameProvider.SaveGame(_SavedGame);
        }

        public void RegisterLeaderboardsSet(Dictionary<ushort, string> _Map)
        {
            LeaderboardProvider.RegisterLeaderboardsSet(_Map);
        }

        public virtual ScoresEntity GetScoreFromLeaderboard(ushort _Key, bool _FromCache)
        {
            return LeaderboardProvider.GetScoreFromLeaderboard(_Key, _FromCache);
        }

        public virtual bool SetScoreToLeaderboard(ushort _Key, long _Value, bool _OnlyToCache)
        {
            return LeaderboardProvider.SetScoreToLeaderboard(_Key, _Value, _OnlyToCache);
        }

        public virtual bool ShowLeaderboard(ushort _Key)
        {
            return LeaderboardProvider.ShowLeaderboard(_Key);
        }
        
        public void RegisterAchievementsSet(Dictionary<ushort, string> _Set)
        {
            AchievementsProvider.RegisterAchievementsSet(_Set);
        }

        public Entity<IAchievement> UnlockAchievement(ushort _Key)
        {
            return AchievementsProvider.UnlockAchievement(_Key);
        }

        public Entity<IAchievement> GetAchievement(ushort _Key)
        {
            return AchievementsProvider.GetAchievement(_Key);
        }

        #endregion
    }
}