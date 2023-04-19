using SA.Android.Manifest;

namespace SA.Android.Editor
{
    class AN_GooglePlayResolver : AN_APIResolver
    {
        public override bool IsSettingsEnabled
        {
            get => AN_Settings.Instance.GooglePlay;
            set => AN_Settings.Instance.GooglePlay = value;
        }

        protected override void AppendBuildRequirements(AN_AndroidBuildRequirements buildRequirements)
        {
            buildRequirements.AddBinaryDependency(AN_BinaryDependency.PlayServicesAuth);
            buildRequirements.AddBinaryDependency(AN_BinaryDependency.AndroidX);
            buildRequirements.AddInternalLib("an_gms.aar");

            var gamesAppID = new AMM_PropertyTemplate("meta-data");
            gamesAppID.SetValue("android:name", "com.google.android.gms.games.APP_ID");
            gamesAppID.SetValue("android:value", "@string/app_id");
            buildRequirements.AddApplicationProperty(gamesAppID);

            if (AN_Settings.Instance.GooglePlayGamesAPI)
                buildRequirements.AddBinaryDependency(AN_BinaryDependency.PlayServicesGames);
        }
    }
}
