using SA.Foundation.Utility;
using SA.Foundation.UtilitiesEditor;
using UnityEditor;

namespace SA.Android.Editor
{
    class AN_GooglePlayResources : AssetPostprocessor
    {
        static AN_GamesIds s_GamesIds;

        public static AN_GamesIds GamesIds
        {
            get
            {
                if (s_GamesIds == null)
                    if (SA_AssetDatabase.IsFileExists(AN_Settings.AndroidGamesIdsFilePath))
                        LoadLocalGamesIds();
                return s_GamesIds;
            }
        }

        public static void OverrideGamesIds(string data)
        {
            SA_FilesUtil.Write(AN_Settings.AndroidGamesIdsFilePath, data);
            SA_AssetDatabase.ImportAsset(AN_Settings.AndroidGamesIdsFilePath);
        }

        public static void LoadLocalGamesIds()
        {
            var rawData = SA_FilesUtil.Read(AN_Settings.AndroidGamesIdsFilePath);
            s_GamesIds = new AN_GamesIds(rawData);
        }

        public static void DropGamesIds()
        {
            s_GamesIds = null;
        }

        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (var assetPath in importedAssets) //games-ids.xml was created or modified;
                if (assetPath.Equals(AN_Settings.AndroidGamesIdsFilePath))
                    LoadLocalGamesIds();

            foreach (var assetPath in deletedAssets) //games-ids.xml was deleted;
                if (assetPath.Equals(AN_Settings.AndroidGamesIdsFilePath)) { }
        }
    }
}
