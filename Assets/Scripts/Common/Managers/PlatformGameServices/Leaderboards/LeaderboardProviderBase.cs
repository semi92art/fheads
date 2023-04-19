using System.Collections.Generic;
using System.Linq;
using Common.Entities;
using Common.Helpers;
using Common.Managers.PlatformGameServices.GameServiceAuth;
using Common.Utils;
using mazing.common.Runtime;
using mazing.common.Runtime.Entities;
using mazing.common.Runtime.Extensions;
using mazing.common.Runtime.Helpers;
using mazing.common.Runtime.Managers;
using mazing.common.Runtime.Network;
using mazing.common.Runtime.Network.DataFieldFilters;
using mazing.common.Runtime.Utils;

namespace Common.Managers.PlatformGameServices.Leaderboards
{
    public interface ILeaderboardProvider : IInit
    {
        event ScoresEventHandler ScoresChanged;
        void                     RegisterLeaderboardsSet(Dictionary<ushort, string> _Map);

        ScoresEntity GetScoreFromLeaderboard(ushort _Key, bool _FromCache);
        bool         SetScoreToLeaderboard(ushort   _Key, long _Value, bool _OnlyToCache);
        bool         ShowLeaderboard(ushort         _Key);
    }


    public abstract class LeaderboardProviderBase : InitBase, ILeaderboardProvider
    {
        private Dictionary<ushort, string> m_LeaderboardsMap;

        private   GlobalGameSettings                GameSettings        { get; }
        private   ILocalizationManager              LocalizationManager { get; }
        private   IGameClient                       GameClient          { get; }
        protected IPlatformGameServiceAuthenticator Authenticator       { get; }

        protected LeaderboardProviderBase(
            GlobalGameSettings                _GameSettings,
            ILocalizationManager              _LocalizationManager,
            IGameClient                       _GameClient,
            IPlatformGameServiceAuthenticator _Authenticator)
        {
            GameSettings            = _GameSettings;
            LocalizationManager = _LocalizationManager;
            GameClient          = _GameClient;
            Authenticator       = _Authenticator;
        }

        public event ScoresEventHandler ScoresChanged;

        public void RegisterLeaderboardsSet(Dictionary<ushort, string> _Map)
        {
            m_LeaderboardsMap = _Map;
        }

        public virtual ScoresEntity GetScoreFromLeaderboard(ushort _Key, bool _FromCache)
        {
            if (_FromCache)
                return GetScoreCached(_Key);
            var scoreEntity = new ScoresEntity();
            if (Authenticator.IsAuthenticated) 
                return null;
            Dbg.LogWarning($"{nameof(GetScoreFromLeaderboard)}: User is not authenticated");
            scoreEntity = GetScoreCached(_Key, scoreEntity);
            return scoreEntity;
        }

        public virtual bool SetScoreToLeaderboard(ushort _Key, long _Value, bool _OnlyToCache)
        {
            SetScoreCached(_Key, _Value);
            return true;
        }

        public virtual bool ShowLeaderboard(ushort _Key)
        {
            string oopsText = LocalizationManager.GetTranslation("oops");
            if (!NetworkUtils.IsInternetConnectionAvailable())
            {
                string noIntConnText = LocalizationManager.GetTranslation("no_internet_connection");
                Dbg.LogWarning($"{nameof(ShowLeaderboard)}: {noIntConnText}");
                MazorCommonUtils.ShowAlertDialog(oopsText, noIntConnText);
                return false;
            }
            if (Authenticator.IsAuthenticated) 
                return true;
            string failedToLoadLeadText = LocalizationManager.GetTranslation("failed_to_load_lead");
            Dbg.LogWarning($"{nameof(ShowLeaderboard)}: {failedToLoadLeadText}");
            MazorCommonUtils.ShowAlertDialog(oopsText, failedToLoadLeadText);
            return false;
        }
        
        protected string GetScoreId(ushort _Key)
        {
            string id = m_LeaderboardsMap.GetSafe(_Key, out bool containsKey);
            if (containsKey) 
                return id;
            Dbg.LogError($"Score with id {_Key} does not exist.");
            return null;
        }
        
        
        protected ScoresEntity GetScoreCached(ushort _Id, ScoresEntity _Entity = null)
        {
            var entity = _Entity ?? new ScoresEntity();
            var gdff = new GameDataFieldFilter(
                    GameClient,
                    GameClientUtils.AccountId, 
                    CommonData.GameId,
                    _Id)
                {OnlyLocal = true};
            gdff.Filter(_Fields =>
            {
                var scoreField = _Fields.FirstOrDefault();
                if (scoreField == null)
                {
                    entity.Result = EEntityResult.Fail;
                }
                else
                {
                    entity.Result = EEntityResult.Success;
                    entity.Value.Add(_Id, scoreField.ToInt());
                }
            });
            return entity;
        }

        private void SetScoreCached(ushort _Id, long _Value)
        {
            var gdff = new GameDataFieldFilter(
                GameClient, GameClientUtils.AccountId, 
                CommonData.GameId,
                _Id) {OnlyLocal = true};
            gdff.Filter(_Fields =>
            {
                var scoreField = _Fields.First();
                scoreField.SetValue(_Value).Save(true);
                Cor.Run(Cor.Action(() =>
                {
                    var entity = new ScoresEntity
                    {
                        Result = EEntityResult.Success,
                        Value = new Dictionary<ushort, long> {{_Id, _Value}}
                    };
                    var args = new ScoresEventArgs(entity);
                    ScoresChanged?.Invoke(args);
                }));
            });
        }
    }
}