using SA.Android.GMS.Games;

namespace SA.Android.GMS.Internal
{
    interface AN_iGMS_GamesAPI
    {
        AN_GamesClient GetGamesClient();
        AN_PlayersClient GetPlayersClient();
        AN_AchievementsClient GetAchievementsClient();
        AN_LeaderboardsClient GetLeaderboardsClient();
        AN_SnapshotsClient GetSnapshotsClient();
    }
}
