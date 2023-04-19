using UnityEngine;
using UnityEditor;
using StansAssets.Plugins.Editor;

namespace SA.Android.Editor
{
    static class AN_Skin
    {
        const string ICONS_PATH = AN_Settings.AndroidNativeFolder + "Editor/Art/Icons/";

        public static Texture2D SettingsWindowIcon
        {
            get
            {
                if (EditorGUIUtility.isProSkin)
                    return EditorAssetDatabase.GetTextureAtPath(ICONS_PATH + "android_pro.png");
                else
                    return EditorAssetDatabase.GetTextureAtPath(ICONS_PATH + "android.png");
            }
        }

        public static Texture2D GetIcon(string iconName)
        {
            return EditorAssetDatabase.GetTextureAtPath(ICONS_PATH + iconName);
            ;
        }
    }
}
