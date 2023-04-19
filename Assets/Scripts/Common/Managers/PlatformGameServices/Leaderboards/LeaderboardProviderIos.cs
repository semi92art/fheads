using Common.Constants;
using Common.Entities;
using Common.Helpers;
using Common.Managers.PlatformGameServices.GameServiceAuth;
using mazing.common.Runtime;
using mazing.common.Runtime.Entities;
using mazing.common.Runtime.Helpers;
using mazing.common.Runtime.Managers;
using mazing.common.Runtime.Network;
using SA.iOS.Foundation;
using SA.iOS.GameKit;

namespace Common.Managers.PlatformGameServices.Leaderboards
{
    public class LeaderboardProviderIos : LeaderboardProviderBase
    {
        private IRemotePropertiesCommon RemoteProperties { get; }

        #region nonpublic members

        #endregion

        #region inject
        
        public LeaderboardProviderIos(
            IRemotePropertiesCommon           _RemoteProperties,
            GlobalGameSettings                _GameSettings,
            ILocalizationManager              _LocalizationManager,
            IGameClient                       _GameClient,
            IPlatformGameServiceAuthenticator _Authenticator)
            : base(
                _GameSettings,
                _LocalizationManager, 
                _GameClient,
                _Authenticator)
        {
            RemoteProperties = _RemoteProperties;
        }

        #endregion

        #region api
        
        public override ScoresEntity GetScoreFromLeaderboard(ushort _Key, bool _FromCache)
        {
            var entity = base.GetScoreFromLeaderboard(_Key, _FromCache);
            return entity ?? GetScoreIos(_Key);
        }

        public override bool SetScoreToLeaderboard(ushort _Key, long _Value, bool _OnlyToCache)
        {
            base.SetScoreToLeaderboard(_Key, _Value, _OnlyToCache);
            if (!_OnlyToCache)
                SetScoreIos(_Key, _Value);
            return true;
        }

        public override bool ShowLeaderboard(ushort _Key)
        {
            if (!base.ShowLeaderboard(_Key))
                return false;
            ShowLeaderboardIos(_Key);
            return true;
        }

        #endregion

        #region nonpublic methods

        private void SetScoreIos(ushort _Id, long _Value)
        {
            if (RemoteProperties.DebugEnabled)
                return;
            if (!Authenticator.IsAuthenticated)
            {
                Dbg.LogWarning("User is not authenticated to Game Center");
                return;
            }
            var scoreReporter = new ISN_GKScore(GetScoreId(_Id))
            {
                Value = _Value
            };
            scoreReporter.Report(_Result =>
            {
                if (_Result.IsSucceeded) 
                    Dbg.Log("Score Report Success");
                else
                {
                    Dbg.LogError("Score Report failed! Code: " + 
                                 _Result.Error.Code + " Message: " + _Result.Error.Message);
                }
            });
        }
        
        private ScoresEntity GetScoreIos(ushort _Id)
        {
            var scoreEntity = new ScoresEntity();
            var leaderboardRequest = new ISN_GKLeaderboard
            {
                Identifier = GetScoreId(_Id),
                PlayerScope = ISN_GKLeaderboardPlayerScope.Global,
                TimeScope = ISN_GKLeaderboardTimeScope.AllTime,
                Range = new ISN_NSRange(1, 25)
            };
            leaderboardRequest.LoadScores(_Result => 
            {
                if (_Result.IsSucceeded) 
                {
                    Dbg.Log($"Score Load Succeed: {DataFieldIds.GetDataFieldName(_Id)}: {leaderboardRequest.LocalPlayerScore.Value}");
                    scoreEntity.Value.Add(_Id, leaderboardRequest.LocalPlayerScore.Value);
                    scoreEntity.Result = EEntityResult.Success;
                } 
                else
                {
                    scoreEntity = GetScoreCached(_Id, scoreEntity);
                    Dbg.LogWarning("Score Load failed! Code: " + _Result.Error.Code + " Message: " + _Result.Error.Message);
                }
            });
            return scoreEntity;
        }
        
        private void ShowLeaderboardIos(ushort _Id)
        {
            var viewController = new ISN_GKGameCenterViewController
            {
                ViewState = ISN_GKGameCenterViewControllerState.Leaderboards,
                LeaderboardIdentifier = GetScoreId(_Id)
            };
            viewController.Show();
        }

        #endregion
    }
}