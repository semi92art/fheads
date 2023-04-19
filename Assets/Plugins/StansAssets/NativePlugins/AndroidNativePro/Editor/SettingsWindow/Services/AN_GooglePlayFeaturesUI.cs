using SA.Foundation.Editor;
using StansAssets.Plugins.Editor;
using UnityEditor;
using UnityEngine;

namespace SA.Android.Editor
{
    class AN_GooglePlayFeaturesUI : AN_ServiceSettingsUI
    {
        [SerializeField]
        IMGUIPluginActiveTextLink m_ConfigureYourGameLink;
        [SerializeField]
        IMGUIPluginActiveTextLink m_SetResource;
        
        [SerializeField]
        IMGUIPluginActiveTextLink m_GenerateIdsClass;

        readonly GUIContent m_SingInLabelContent = new GUIContent("Sign-in [?]:", "Before you start using Google Play API with the plugin" +
            "You must first configure your game in the Google Play Developer Console, " +
            "and then define google play resources using the plugin.");

        public static readonly GUIContent GamesLabelContent = new GUIContent("Games API [?]:", "Start integrating popular gaming features " +
            "in your mobile games and web games by using the Google Play games services APIs.");

        public const string GooglePlayIconName = "android_googleplay.png";

        public override void OnAwake()
        {
            base.OnAwake();

            m_ConfigureYourGameLink = new IMGUIPluginActiveTextLink("Configure Your Game");
            m_SetResource = new IMGUIPluginActiveTextLink(string.Empty);
            
            m_GenerateIdsClass = new IMGUIPluginActiveTextLink("Generate AN_GamesIds.cs file");

            AddFeatureUrl("Getting Started", "https://github.com/StansAssets/com.stansassets.android-native/wiki/Getting-Started");
            AddFeatureUrl("Checking Availability", "https://github.com/StansAssets/com.stansassets.android-native/wiki/Android-Games-Sing-in#checking-availability");
            AddFeatureUrl("Player Sing-in", "https://github.com/StansAssets/com.stansassets.android-native/wiki/Android-Games-Sing-in#implementing-player-sign-in");
            AddFeatureUrl("Player Information", "https://github.com/StansAssets/com.stansassets.android-native/wiki/Android-Games-Sing-in#retrieving-player-information");
            AddFeatureUrl("Game Pop-ups", "https://github.com/StansAssets/com.stansassets.android-native/wiki/Android-Games-Sing-in#displaying-game-pop-ups");
            AddFeatureUrl("Player Sing-out", "https://github.com/StansAssets/com.stansassets.android-native/wiki/Android-Games-Sing-in#signing-the-player-out");
            AddFeatureUrl("Server API Access", "https://github.com/StansAssets/com.stansassets.android-native/wiki/Server-side-API-Access");
            AddFeatureUrl("Achievements", "https://github.com/StansAssets/com.stansassets.android-native/wiki/Achievements");
            AddFeatureUrl("Leaderboards", "https://github.com/StansAssets/com.stansassets.android-native/wiki/Leaderboards");
            AddFeatureUrl("Saved Games", "https://github.com/StansAssets/com.stansassets.android-native/wiki/Saved-Games");
            AddFeatureUrl("Image Manager", "https://github.com/StansAssets/com.stansassets.android-native/wiki/Image-Manager");
            AddFeatureUrl("Settings Intent", "https://github.com/StansAssets/com.stansassets.android-native/wiki/Android-Games-Settings-Intent");
        }

        public override string Title => "Google Play";

        protected override string Description =>
            "The Play Games SDK provides Google Play games services " +
            "that lets you easily integrate popular gaming features.";

        protected override Texture2D Icon => AN_Skin.GetIcon(GooglePlayIconName);

        protected override SA_iAPIResolver Resolver => AN_Preprocessor.GetResolver<AN_GooglePlayResolver>();

        bool m_AchievementsShown;
        bool m_LeaderboardsShown;

        protected override void OnServiceUI()
        {
            using (new SA_WindowBlockWithSpace(new GUIContent("Configuration")))
            {
                var setResourceName = "Update Game Resource";
                if (AN_GooglePlayResources.GamesIds == null)
                {
                    EditorGUILayout.HelpBox("Before you start using Google Play API with the plugin" +
                        "You must first configure your game in the Google Play Developer Console, " +
                        "and then define google play resources using the plugin.",
                        MessageType.Warning);
                    using (new IMGUIBeginHorizontal())
                    {
                        GUILayout.FlexibleSpace();
                        var click = m_ConfigureYourGameLink.DrawWithCalcSize();
                        if (click) Application.OpenURL("https://unionassets.com/android-native-pro/getting-started-670");
                    }

                    setResourceName = "Set Game Resource";
                }
                else
                {
                    var applicationIdentifier = PlayerSettings.GetApplicationIdentifier(BuildTargetGroup.Android);
                    if (!string.IsNullOrEmpty(AN_GooglePlayResources.GamesIds.PackageName) && !applicationIdentifier.Equals(AN_GooglePlayResources.GamesIds.PackageName))
                        EditorGUILayout.HelpBox("Player Settings Package Name does not match with " +
                            "Android Games Package Name \n" +
                            "unity: " + applicationIdentifier + "\n" +
                            "games-ids.xml: " + AN_GooglePlayResources.GamesIds.PackageName,
                            MessageType.Warning);

                    using (new IMGUIBeginVertical(EditorStyles.helpBox))
                    {
                        IMGUILayout.SelectableLabel("App Id", AN_GooglePlayResources.GamesIds.AppId);
                        if (!string.IsNullOrEmpty(AN_GooglePlayResources.GamesIds.PackageName))
                        {
                            IMGUILayout.SelectableLabel("Package Name", AN_GooglePlayResources.GamesIds.PackageName);
                        }

                        m_AchievementsShown = EditorGUILayout.Foldout(m_AchievementsShown, "Achievements");
                        if (m_AchievementsShown)
                        {
                            using (new IMGUIIndentLevel(1))
                            {
                                if (AN_GooglePlayResources.GamesIds.Achievements.Count > 0)
                                    AN_GooglePlayResources.GamesIds.Achievements.ForEach(pair =>
                                    {
                                        IMGUILayout.SelectableLabel(pair.Key, pair.Value);
                                    });
                                else
                                    EditorGUILayout.LabelField("There are no achievements in games-ids.xml");
                            }
                        }

                        m_LeaderboardsShown = EditorGUILayout.Foldout(m_LeaderboardsShown, "Leaderboards");
                        if (m_LeaderboardsShown)
                        {
                            using (new IMGUIIndentLevel(1))
                            {
                                if (AN_GooglePlayResources.GamesIds.Leaderboards.Count > 0)
                                    AN_GooglePlayResources.GamesIds.Leaderboards.ForEach(pair =>
                                    {
                                        IMGUILayout.SelectableLabel(pair.Key, pair.Value);
                                    });
                                else
                                    EditorGUILayout.LabelField("There are no leaderboards in games-ids.xml");
                            }
                        }
                    }
                }

                using (new IMGUIBeginHorizontal())
                {
                    GUILayout.FlexibleSpace();
                    m_SetResource.Content.text = setResourceName;
                    var click = m_SetResource.DrawWithCalcSize();
                    if (click) AN_GoolgePlayRersourcesWindow.ShowAsModal();
                }
                
                using (new IMGUIBeginHorizontal())
                {
                    GUILayout.FlexibleSpace();
                    var click = m_GenerateIdsClass.DrawWithCalcSize();
                    if (click)
                    {
                        var generatedCode = new CSharpFileCodeGen(AN_Settings.PlayServiceGamesIdsGeneratedFile);
                        generatedCode.SetNamespace("SA.Android.GMS.CodeGen");
                        
                        var gamesIdsClass = new AN_ClassCodeGen("AN_GamesIds");
                        gamesIdsClass.AddConst("AppId", AN_GooglePlayResources.GamesIds.AppId);
                        
                        var achievementsClass = new AN_ClassCodeGen("Achievements");
                        foreach (var achievement in AN_GooglePlayResources.GamesIds.Achievements)
                        {
                            achievementsClass.AddConst(achievement.Key, achievement.Value);
                        }
                      
                        
                        var leaderboardsClass = new AN_ClassCodeGen("Leaderboards");
                        foreach (var leaderboard in AN_GooglePlayResources.GamesIds.Leaderboards)
                        {
                            leaderboardsClass.AddConst(leaderboard.Key, leaderboard.Value);
                        }
                        
                        gamesIdsClass.AddNestedClass(achievementsClass);
                        gamesIdsClass.AddNestedClass(leaderboardsClass);

                        generatedCode.AddClass(gamesIdsClass);
                        generatedCode.Save();
                    }
                }
            }

            using (new SA_WindowBlockWithSpace(new GUIContent("Google Mobile Services APIs")))
            {
                EditorGUILayout.HelpBox("In order to access Google Play games services functionality, " +
                    "your game needs to provide the signed-in player’s account. If the player is not authenticated, " +
                    "your game may encounter errors when making calls to the Google Play games services APIs.",
                    MessageType.Info);

                using (new IMGUIBeginHorizontal())
                {
                    EditorGUILayout.LabelField(m_SingInLabelContent);
                    using (new IMGUIEnable(false))
                    {
                        IMGUILayout.ToggleFiled(new GUIContent(), true, IMGUIToggleStyle.ToggleType.EnabledDisabled);
                    }
                }

                AN_Settings.Instance.GooglePlayGamesAPI = IMGUILayout.ToggleFiled(GamesLabelContent, AN_Settings.Instance.GooglePlayGamesAPI, IMGUIToggleStyle.ToggleType.EnabledDisabled);
            }
        }
    }
}
