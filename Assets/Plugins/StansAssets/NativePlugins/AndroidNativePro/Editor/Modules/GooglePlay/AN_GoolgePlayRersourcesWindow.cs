using UnityEngine;
using UnityEditor;
using SA.Foundation.Editor;
using StansAssets.Plugins.Editor;

namespace SA.Android.Editor
{
    class AN_GoolgePlayRersourcesWindow : EditorWindow
    {
        [SerializeField]
        string m_rawData;
        [SerializeField]
        bool m_isDirty = false;

        public static void ShowAsModal()
        {
            var window = GetWindow<AN_GoolgePlayRersourcesWindow>(true);
            window.maxSize = new Vector2(700f, 545);
            window.minSize = window.maxSize;
            window.Show();

            window.titleContent = new GUIContent("Google Play Resources");
        }

        void OnEnable()
        {
            if (AN_GooglePlayResources.GamesIds != null) m_rawData = AN_GooglePlayResources.GamesIds.RawData;
        }

        void OnGUI()
        {
            EditorGUILayout.LabelField("Copy the game resources from the console", EditorStyles.boldLabel);

            using (new IMGUIBeginHorizontal())
            {
                GUILayout.Space(10f);
                EditorGUILayout.LabelField("Once you configure at least one resource (event, achievement, or leaderboard), " +
                    "copy the resource configuration from the Google Play Developer Console, " +
                    "and paste inside the Text Area bellow. " +
                    "To get the resources go to the Achievements tab, " +
                    "then click on 'Get resources' on the bottom of the list.",  SettingsWindowStyles.DescriptionLabelStyle);
            }

            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();
            m_rawData = EditorGUILayout.TextArea(m_rawData, SA_PluginSettingsWindowStyles.TextArea, GUILayout.Height(450));
            if (EditorGUI.EndChangeCheck()) m_isDirty = true;

            using (new IMGUIBeginHorizontal())
            {
                EditorGUILayout.Space();
                GUI.enabled = m_isDirty;
                var clicked = GUILayout.Button("Save", GUILayout.Width(300));
                if (clicked)
                {
                    AN_GooglePlayResources.OverrideGamesIds(m_rawData);
                    m_isDirty = false;

                    var image = AN_Skin.GetIcon(AN_GooglePlayFeaturesUI.GooglePlayIconName);
                    var message = new GUIContent("games-ids.xml saved", image);
                    ShowNotification(message);
                }

                GUI.enabled = true;
            }
        }
    }
}
