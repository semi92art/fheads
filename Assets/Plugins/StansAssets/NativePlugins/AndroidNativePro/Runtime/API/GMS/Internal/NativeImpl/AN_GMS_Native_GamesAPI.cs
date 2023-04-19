using UnityEngine;
using SA.Android.GMS.Games;
using SA.Android.Utilities;

namespace SA.Android.GMS.Internal
{
    class AN_GMS_Native_GamesAPI : AN_iGMS_GamesAPI
    {
        const string k_JavaPackage = "com.stansassets.gms.games.";

        //--------------------------------------
        // AN_Games
        //--------------------------------------

        const string k_Games = k_JavaPackage + "AN_Games";

        public AN_GamesClient GetGamesClient()
        {
            var json = AN_Java.Bridge.CallStatic<string>(k_Games, "GetGamesClient");
            return JsonUtility.FromJson<AN_GamesClient>(json);
        }

        public AN_PlayersClient GetPlayersClient()
        {
            var json = AN_Java.Bridge.CallStatic<string>(k_Games, "GetPlayersClient");
            return JsonUtility.FromJson<AN_PlayersClient>(json);
        }

        public AN_AchievementsClient GetAchievementsClient()
        {
            var json = AN_Java.Bridge.CallStatic<string>(k_Games, "GetAchievementsClient");
            return JsonUtility.FromJson<AN_AchievementsClient>(json);
        }

        public AN_LeaderboardsClient GetLeaderboardsClient()
        {
            var json = AN_Java.Bridge.CallStatic<string>(k_Games, "GetLeaderboardsClient");
            return JsonUtility.FromJson<AN_LeaderboardsClient>(json);
        }

        public AN_SnapshotsClient GetSnapshotsClient()
        {
            var json = AN_Java.Bridge.CallStatic<string>(k_Games, "GetSnapshotsClient");
            return JsonUtility.FromJson<AN_SnapshotsClient>(json);
        }
    }
}
